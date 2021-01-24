using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AITurn : Turn
{
    private const float fakeDuration = 1.5f;

    public AITurn(Hand hand, HandBehaviour handBehaviour, bool canSacrifice) : base(hand, handBehaviour, canSacrifice) { }

    protected override IEnumerator Run()
    {
        yield return base.Run();
        yield return new WaitForSeconds(fakeDuration);

        if (hand.Count > 0)
        {
            Card cardToPlay;
            Card userCard = GameplayController.Instance.userCard;
            bool sacrifice;
            List<Card> cards = new List<Card>(hand);
            cards.Sort((x, y) => x.power > y.power ? 1 : -1);

            //User has a card placed
            if (userCard != null)
            {
                var cardsAboveUser = cards.FindAll(x => x.power > userCard.power);
                var cardsBellowUser = cards.FindAll(x => x.power <= userCard.power);

                if (cardsAboveUser.Count > 0 && !canSacrifice)
                {
                    cardToPlay = cardsAboveUser[0];
                    sacrifice = false;
                }
                //Intentional sacrifice
                else if (cardsAboveUser.Count > 1 && canSacrifice)
                {
                    cardToPlay = cards.Last();
                    sacrifice = true;
                }
                else if (cardsAboveUser.Count > 0 && cards.Count > 1 && canSacrifice)
                {
                    cardToPlay = cards[0];
                    sacrifice = true;
                }
                else if ((userCard.power < 12 || cards[0].power < 6) && canSacrifice && GameplayController.Instance.deck.Count > 0)
                {
                    cardToPlay = cards[0];
                    sacrifice = true;
                }
                else
                {
                    cardToPlay = cards[0];
                    sacrifice = false;
                }
            }
            //User doesn't have a card placed
            else
            {
                if (canSacrifice && cards.Count > 1)
                {
                    cardToPlay = cards[0];
                    sacrifice = true;
                }
                else if (!canSacrifice && cards.Count > 1)
                {
                    sacrifice = false;
                    if (cards.Last().power > 11)
                        cardToPlay = cards.Last();
                    else
                        cardToPlay = cards[0];
                }
                else if (cards.Count == 1 && canSacrifice)
                {
                    cardToPlay = cards[0];
                    sacrifice = cards[0].power < 11;
                }
                else
                {
                    cardToPlay = cards[0];
                    sacrifice = false;
                    //cardToPlay = cards[Random.Range(0, cards.Count)];
                    //sacrifice = canSacrifice && Random.value < 0.5f;
                }
            }

            card = cardToPlay;
            var cardBehaviour = handBehaviour.BehaviourForCard(card);
            sacrificed = sacrifice && canSacrifice;
            if (sacrificed)
            {
                yield return new CardDraw(cardBehaviour, Board.Instance.sacrifice);
                GameplayController.Instance.sacrifice += card.power;
            }
            else
            {
                yield return new CardDraw(cardBehaviour, Board.Instance.opponentDraw);
            }
            hand.Remove(card);
        }
    }
}
