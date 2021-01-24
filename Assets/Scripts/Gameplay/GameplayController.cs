using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GameplayController : SingletonBehaviour<GameplayController>
{
    public uint initialHandSize = 4;
    public uint cardsPerSacrifice = 1;
    public bool winnerStarts;
    public List<Card> cards = new List<Card>();

    [HideInInspector]
    public readonly Deck deck = new Deck();
    [HideInInspector]
    public readonly Player user = new Player();
    [HideInInspector]
    public readonly Player opponent = new Player();
    [HideInInspector]
    public uint sacrifice = 0;

    [HideInInspector]
    public Card userCard;
    [HideInInspector]
    public Card opponentCard;

    private void Start()
    {
        NewGame();
    }

    public void NewGame()
    {
        StartCoroutine(GameLoop());
    }

    private IEnumerator GameLoop()
    {
        userCard = null;
        opponentCard = null;
        user.points = 0;
        opponent.points = 0;
        sacrifice = 0;
        BoardInteractionSystem.Instance.enabled = false;

        yield return new DeckConstruction(new List<Card>(cards));
        for (int i = 0; i < initialHandSize; i++)
        {
            yield return new CardDistribution(user.hand, Board.Instance.userHand);
            yield return new CardDistribution(opponent.hand, Board.Instance.opponentHand);
        }

        bool userTurn = true;

        while (true)
        {
            if (userTurn)
            {
                BoardInteractionSystem.Instance.enabled = true;
                var turn = new UserTurn(user.hand, Board.Instance.userHand, true);
                yield return turn;
                if (turn.sacrificed)
                {
                    for (int i = 0; i < cardsPerSacrifice; i++)
                        yield return new CardDistribution(user.hand, Board.Instance.userHand);
                    Debug.Log($"User sacrificed {turn.card?.power}");
                    turn = new UserTurn(user.hand, Board.Instance.userHand, false);
                    yield return turn;
                    userCard = turn.card;
                }
                else
                    userCard = turn.card;
                Debug.Log($"User plays {userCard?.power}");
                userTurn = false;
                BoardInteractionSystem.Instance.enabled = false;
            }
            else
            {
                var turn = new AITurn(opponent.hand, Board.Instance.opponentHand, true);
                yield return turn;
                if (turn.sacrificed)
                {
                    for (int i = 0; i < cardsPerSacrifice; i++)
                        yield return new CardDistribution(opponent.hand, Board.Instance.opponentHand);
                    Debug.Log($"Computer sacrificed {turn.card?.power}");
                    turn = new AITurn(opponent.hand, Board.Instance.opponentHand, false);
                    yield return turn;
                    opponentCard = turn.card;
                }
                else
                    opponentCard = turn.card;
                Debug.Log($"Computer plays {opponentCard?.power}");
                userTurn = true;
            }

            if (ShouldEndRound())
            {
                Player winner = null;
                CardDestinationBehaviour cardDestination = null;

                bool userWins = (opponentCard == null && userCard != null) || (userCard != null && opponentCard != null && userCard.power > opponentCard.power);
                if (userWins)
                {
                    Debug.Log("User wins! " + userCard?.power + " > " + opponentCard?.power);
                    winner = user;
                    cardDestination = Board.Instance.userWins;
                    userTurn = winnerStarts;
                }
                else
                {
                    Debug.Log("Computer wins!" + opponentCard?.power + " > " + userCard?.power);
                    winner = opponent;
                    cardDestination = Board.Instance.opponentWins;
                    userTurn = !winnerStarts;
                }

                yield return new RoundEnd(winner, cardDestination);

                if (userCard != null)
                    winner.points += userCard.power;
                if (opponentCard != null)
                    winner.points += opponentCard.power;
                winner.points += sacrifice;

                opponentCard = null;
                userCard = null;
                sacrifice = 0;
            }

            if (IsGameOver())
            {
                Debug.LogWarning("Game Over!");
                UIManager.Instance.GameOver(user.points > opponent.points);
                break;
            }
        }
    }

    private bool ShouldEndRound()
    {
        if (opponentCard != null && userCard != null)
        {
            Debug.Log("Reason 1");
            return true;
        }
        if (opponentCard != null && user.hand.Count == 0 && deck.Count == 0)
        {
            Debug.Log("Reason 2");
            return true;
        }
        if (userCard != null && opponent.hand.Count == 0 && deck.Count == 0)
        {
            Debug.Log("Reason 3");
            return true;
        }
        return false;
    }

    private bool IsGameOver()
    {
        return user.hand.Count == 0 && opponent.hand.Count == 0 && deck.Count == 0;
    }
}
