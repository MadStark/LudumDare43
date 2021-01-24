using System.Collections.Generic;
using UnityEngine;

public class CardDestinationBehaviour : MonoBehaviour, ISelectible
{
    [SerializeField]
    private GameObject selection;

    public bool isSelectible;
    public bool IsSelectible => isSelectible;
    public bool cardsFlipped;

    public CardBehaviour LinkedCard {
        get {
            if (linkedCards.Count > 0)
                return linkedCards[0];
            return null;
        }
    }
    public readonly List<CardBehaviour> linkedCards = new List<CardBehaviour>();

    private void Start()
    {
        if (selection != null)
            selection.SetActive(false);
    }

    public void OnPointerEnter()
    {
        if (selection != null)
            selection.SetActive(true);
    }

    public void OnPointerExit()
    {
        if (selection != null)
            selection.SetActive(false);
    }
}
