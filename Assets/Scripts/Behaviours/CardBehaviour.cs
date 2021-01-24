using UnityEngine;

public class CardBehaviour : MonoBehaviour, ISelectible
{
    [Header("Bindings")]
    [SerializeField]
    private TMPro.TMP_Text powerText;
    [SerializeField]
    private TMPro.TMP_Text nameText;
    [SerializeField]
    private Renderer faceRenderer;
    [SerializeField]
    private GameObject selection;


    [Header("Configuration")]
    public bool isSelectible;
    public bool IsSelectible => isSelectible;

    private Card _card;
    public Card Card {
        get => _card;
        set {
            _card = value;
            if (_card != null)
            {
                if (powerText != null)
                    powerText.text = _card.power.ToString();
                if (nameText != null)
                    nameText.text = _card.name;
                if (faceRenderer != null)
                    faceRenderer.material.mainTexture = _card.face;
            }
        }
    }

    private void Start()
    {
        if (selection != null)
            selection.SetActive(false);
        if (powerText != null)
            powerText.enableCulling = true;
        if (nameText != null)
            nameText.enableCulling = true;
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
