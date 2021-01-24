using System.Collections;
using DG.Tweening;
using UnityEngine;

public class RoundEnd : GameplaySequence
{
    readonly Player winner;
    readonly CardDestinationBehaviour destination;

    public RoundEnd(Player winner, CardDestinationBehaviour destination)
    {
        this.winner = winner;
        this.destination = destination;
    }

    protected override IEnumerator Run()
    {
        yield return new WaitForSeconds(2f);

        foreach (var card in Board.Instance.sacrifice.linkedCards)
        {
            card.transform.DOMove(destination.transform.position, 0.6f);
            card.transform.DORotate(new Vector3(0, 180, 0), 0.4f);
        }

        if (Board.Instance.userDraw.LinkedCard != null)
        {
            Board.Instance.userDraw.LinkedCard.transform.DOMove(destination.transform.position, 0.6f);
            Board.Instance.userDraw.LinkedCard.transform.DORotate(new Vector3(0, 180, 0), 0.4f);
        }
        if (Board.Instance.opponentDraw.LinkedCard)
        {
            Board.Instance.opponentDraw.LinkedCard.transform.DOMove(destination.transform.position, 0.6f);
            Board.Instance.opponentDraw.LinkedCard.transform.DORotate(new Vector3(0, 180, 0), 0.4f);
        }

        Board.Instance.opponentDraw.linkedCards.Clear();
        Board.Instance.sacrifice.linkedCards.Clear();
        Board.Instance.userDraw.linkedCards.Clear();

        yield return new WaitForSeconds(1f);
    }
}
