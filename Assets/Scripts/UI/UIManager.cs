using UnityEngine;

public class UIManager : SingletonBehaviour<UIManager>
{
    public UIGameOver gameOverUI;
    public Animator yourTurnText;

    private void Start()
    {
        gameOverUI.gameObject.SetActive(false);
    }

    public void GameOver(bool victory)
    {
        gameOverUI.Configure(victory);
        gameOverUI.gameObject.SetActive(true);
    }

    public void UserTurn()
    {
        yourTurnText.Play("YourTurn");
    }
}
