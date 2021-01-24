using System.Collections;
using UnityEngine;

public class Turn : GameplaySequence
{
    protected readonly bool canSacrifice;
    protected readonly Hand hand;
    protected readonly HandBehaviour handBehaviour;

    public Turn(Hand hand, HandBehaviour handBehaviour, bool canSacrifice)
    {
        this.hand = hand;
        this.handBehaviour = handBehaviour;
        this.canSacrifice = canSacrifice;
    }

    public Card card;
    public bool sacrificed;

    protected override IEnumerator Run()
    {
        if (hand.Count == 0 && GameplayController.Instance.deck.Count > 0)
        {
            yield return new CardDistribution(hand, handBehaviour);
        }
        yield return null;
    }
}
