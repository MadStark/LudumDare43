using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Renderer))]
public class ExposeSortingLayer : MonoBehaviour
{
    public string sortingLayer;
    public int orderInLayer;

    private new Renderer renderer;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
#if !UNITY_EDITOR
        renderer.sortingLayerName = sortingLayer;
        renderer.sortingOrder = orderInLayer;
#endif
    }

#if UNITY_EDITOR
    private void Update()
    {
        renderer.sortingLayerName = sortingLayer;
        renderer.sortingOrder = orderInLayer;
    }
#endif
}
