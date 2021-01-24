using System.Collections;
using DG.Tweening;
using UnityEngine;

public class CardDraw : GameplaySequence
{
    readonly CardBehaviour card;
    readonly CardDestinationBehaviour destination;

    public CardDraw(CardBehaviour card, CardDestinationBehaviour destination)
    {
        this.card = card;
        this.destination = destination;
    }

    protected override IEnumerator Run()
    {
        card.isSelectible = false;
        var collider = card.GetComponentInChildren<Collider2D>();
        if (collider != null)
            collider.enabled = false;
        HandBehaviour hand = card.transform.parent?.GetComponent<HandBehaviour>();
        card.transform.SetParent(null);
        if (hand)
            hand.Layout();
        destination.linkedCards.Add(card);
        card.transform.DOMove(destination.transform.position, 0.6f);
        if (hand == Board.Instance.opponentHand && !destination.cardsFlipped)
            card.transform.DORotate(new Vector3(0, 0, 0), 0.5f);
        else if (destination.cardsFlipped)
            card.transform.DORotate(new Vector3(0, 180, 0), 0.5f);
        yield return new WaitForSeconds(0.6f);
    }
}
