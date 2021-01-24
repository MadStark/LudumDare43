using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private GameObject selection;

    public UnityEvent onClick = new UnityEvent();

    private void Start()
    {
        if (selection != null)
            selection.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        onClick.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (selection != null)
            selection.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (selection != null)
            selection.SetActive(false);
    }
}
