using UnityEngine;

public class BoardInteractionSystem : SingletonBehaviour<BoardInteractionSystem>
{
    public new Camera camera;
    public LayerMask layerMask = 0;

    [HideInInspector]
    public GameObject selection;
    [HideInInspector]
    public bool clicked;

    private GameObject pointerDown;

    private void OnDisable()
    {
        CallPointerExitEvent(selection);
        selection = null;
        clicked = false;
        pointerDown = null;
    }

    private void Update()
    {
        GameObject currentGo = null;

        if (camera != null)
        {
            var hit = Physics2D.GetRayIntersection(camera.ScreenPointToRay(Input.mousePosition), 50f, layerMask);
            if (hit)
            {
                if (hit.rigidbody != null)
                    currentGo = hit.rigidbody.gameObject;
                else
                    currentGo = hit.collider.gameObject;
            }
        }

        if (currentGo != null)
        {
            var selectible = currentGo.GetComponent<ISelectible>();
            if (selectible == null || !selectible.IsSelectible)
                currentGo = null;
        }

        if (currentGo != selection)
        {
            CallPointerExitEvent(selection);
            selection = currentGo;
            CallPointerEnterEvent(selection);
        }

        if (Input.GetMouseButtonDown(0))
            pointerDown = selection;
        if (Input.GetMouseButtonUp(0))
        {
            if (pointerDown == selection)
                clicked = true;
            pointerDown = null;
        }
        else
            clicked = false;
    }

    public void CallPointerEnterEvent(GameObject go)
    {
        if (go == null)
            return;
        var selectible = go.GetComponent<ISelectible>();
        if (selectible == null)
            return;
        selectible.OnPointerEnter();
    }

    public void CallPointerExitEvent(GameObject go)
    {
        if (go == null)
            return;
        var selectible = go.GetComponent<ISelectible>();
        if (selectible == null)
            return;
        selectible.OnPointerExit();
    }

    private void Reset()
    {
        camera = GetComponent<Camera>();
        if (camera == null)
            camera = Camera.main;
    }
}
