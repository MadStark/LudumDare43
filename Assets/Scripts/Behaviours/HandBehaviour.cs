using DG.Tweening;
using UnityEngine;

[ExecuteInEditMode]
public class HandBehaviour : MonoBehaviour
{
    public GameObject cardPrefab;
    public float spacing = 0.145f;
    public bool userHand;
    public Hand hand;

    private void Start()
    {
        if (Application.isPlaying)
            foreach (Transform child in transform)
                Destroy(child.gameObject);
    }

    private Vector3 TargetChildPosition(int index, int childCount)
    {
        return new Vector3((index - (childCount * 0.5f)) * spacing + (0.5f * spacing), 0, 0);
    }

    public void AddBehaviourForCard(Card card)
    {
        //Move children aside
        foreach (Transform child in transform)
        {
            var targetPosition = TargetChildPosition(child.GetSiblingIndex() + 1, transform.childCount + 1);
            var tweens = DOTween.TweensByTarget(child);
            if (tweens != null)
            {
                foreach (var tween in tweens)
                {
                    if (tween is Tweener tweener && tweener.intId == 325)
                        tweener.ChangeEndValue(targetPosition, true);
                }
            }
            else
                child.DOLocalMove(targetPosition, 0.1f);
        }

        var instance = Instantiate(cardPrefab, transform).GetComponent<CardBehaviour>();
        instance.isSelectible = userHand;
        instance.Card = card;
        instance.transform.SetSiblingIndex(0);
        instance.transform.position = Board.Instance.deck.transform.position;
        instance.transform.localRotation = Quaternion.Euler(0, 180, 0);

        //Animate card coming from deck
        var localMoveTweener = instance.transform.DOLocalMove(TargetChildPosition(0, transform.childCount), 0.5f);
        localMoveTweener.intId = 325;
        if (userHand || Application.isEditor)
        {
            instance.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.5f);
        }
    }

    public void Layout()
    {
        foreach (Transform child in transform)
            child.DOLocalMove(TargetChildPosition(child.GetSiblingIndex(), transform.childCount), 0.1f);
    }

    public CardBehaviour BehaviourForCard(Card card)
    {
        var allBehaviours = GetComponentsInChildren<CardBehaviour>();
        foreach (var behaviour in allBehaviours)
            if (behaviour.Card == card)
                return behaviour;
        return null;
    }
}
