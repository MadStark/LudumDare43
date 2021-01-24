using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DeckBehaviour : MonoBehaviour
{
    [SerializeField]
    private TMPro.TMP_Text countText;

    [HideInInspector]
    public Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        countText.text = GameplayController.Instance.deck.Count.ToString();
    }
}
