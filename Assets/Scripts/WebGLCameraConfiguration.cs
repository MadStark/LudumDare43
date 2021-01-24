using UnityEngine;

public class WebGLCameraConfiguration : MonoBehaviour
{
    public GameObject webGlCamera;
    public GameObject mainCamera;

    private void Awake()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        webGlCamera.SetActive(true);
        mainCamera.SetActive(false);
        FindObjectOfType<UIManager>().GetComponent<Canvas>().worldCamera = webGlCamera.GetComponent<Camera>();
#else
        webGlCamera.SetActive(false);
        mainCamera.SetActive(true);
#endif
    }
}
