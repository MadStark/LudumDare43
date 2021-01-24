using System.Collections;
using UnityEngine;

public class CardDistribution : GameplaySequence
{
    readonly Hand hand;
    readonly HandBehaviour handBehaviour;

    public Card card;

    public CardDistribution(Hand hand, HandBehaviour handBehaviour)
    {
        this.hand = hand;
        this.handBehaviour = handBehaviour;
    }

    protected override IEnumerator Run()
    {
        if (GameplayController.Instance.deck.Count > 0)
        {
            card = GameplayController.Instance.deck.Dequeue();
            hand.Add(card);
            handBehaviour.AddBehaviourForCard(card);
            yield return new WaitForSeconds(0.3f);
        }
    }
}
