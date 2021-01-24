using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckConstruction : GameplaySequence
{
    readonly List<Card> cards;

    public DeckConstruction(List<Card> cards)
    {
        this.cards = cards;
    }

    protected override IEnumerator Run()
    {
        Shuffle(cards);

        GameplayController.Instance.deck.Clear();
        foreach (var card in cards)
            GameplayController.Instance.deck.Enqueue(card);

        Board.Instance.deck.animator.Play("Shuffle");
        yield return new WaitForSeconds(1.4f);
    }

    private static void Shuffle<T>(IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
