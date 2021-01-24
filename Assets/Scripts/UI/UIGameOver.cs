using UnityEngine;

public class UIGameOver : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem particles;
    [SerializeField]
    private TMPro.TMP_Text text;

    public Color victoryColor;
    public Color defeatColor;

    public void Configure(bool victory)
    {
        var main = particles.main;
        main.startColor = victory ? victoryColor : defeatColor;
        text.text = victory ? "VICTORY" : "DEFEAT";
    }

    public void NewGame()
    {
        var scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene.name);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
