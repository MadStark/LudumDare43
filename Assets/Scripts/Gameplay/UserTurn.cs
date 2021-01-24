using System.Collections;
using UnityEngine;
using DG.Tweening;

public class UserTurn : Turn
{
    private const float selectionOffset = 0.06f;

    public UserTurn(Hand hand, HandBehaviour handBehaviour, bool canSacrifice) : base(hand, handBehaviour, canSacrifice) { }

    private CardBehaviour selectedCard;

    protected override IEnumerator Run()
    {
        yield return base.Run();

        if (canSacrifice)
            UIManager.Instance.UserTurn();

        if (hand.Count > 0)
        {
            while (true)
            {
                if (BoardInteractionSystem.Instance.clicked)
                {
                    //Select card
                    if (selectedCard == null)
                    {
                        selectedCard = BoardInteractionSystem.Instance.selection?.GetComponent<CardBehaviour>();
                        if (selectedCard != null)
                        {
                            selectedCard.transform.DOLocalMoveY(selectionOffset, 0.1f);
                            Board.Instance.sacrifice.isSelectible = canSacrifice;
                            Board.Instance.userDraw.isSelectible = true;
                        }
                    }
                    else
                    {
                        var otherCard = BoardInteractionSystem.Instance.selection?.GetComponent<CardBehaviour>();
                        //Change card
                        if (otherCard != null)
                        {
                            selectedCard.transform.DOLocalMoveY(0, 0.1f);
                            //Select no card
                            if (otherCard == selectedCard)
                            {
                                selectedCard = null;
                                Board.Instance.sacrifice.isSelectible = false;
                                Board.Instance.userDraw.isSelectible = false;
                            }
                            //Swap card
                            else
                            {
                                selectedCard = otherCard;
                                selectedCard.transform.DOLocalMoveY(selectionOffset, 0.1f);
                                Board.Instance.sacrifice.isSelectible = canSacrifice;
                                Board.Instance.userDraw.isSelectible = true;
                            }
                        }
                        //Action
                        else
                        {
                            var destination = BoardInteractionSystem.Instance.selection;
                            //Draw
                            if (destination == Board.Instance.userDraw.gameObject)
                            {
                                sacrificed = false;
                                card = selectedCard.Card;
                                yield return new CardDraw(selectedCard, Board.Instance.userDraw);
                                GameplayController.Instance.user.hand.Remove(card);
                                break;
                            }
                            //Sacrifice
                            else if (destination == Board.Instance.sacrifice.gameObject && canSacrifice)
                            {
                                sacrificed = true;
                                card = selectedCard.Card;
                                yield return new CardDraw(selectedCard, Board.Instance.sacrifice);
                                GameplayController.Instance.user.hand.Remove(card);
                                GameplayController.Instance.sacrifice += card.power;
                                break;
                            }
                        }
                    }
                }
                yield return null;
            }
        }
        Board.Instance.sacrifice.isSelectible = false;
        Board.Instance.userDraw.isSelectible = false;
    }
}
