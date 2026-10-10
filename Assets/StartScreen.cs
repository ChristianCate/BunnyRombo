using UnityEngine;

public class StartScreen : MonoBehaviour
{
    public GameObject startScreenPanel;
    public GameObject gameplayUI;
    public GridLines gridLines;

    private void Start()
    {
        Time.timeScale = 0f;

        startScreenPanel.SetActive(true);

        if (gameplayUI != null)
        {
            gameplayUI.SetActive(false);
        }

        if (gridLines != null)
        {
            gridLines.SetGridVisible(false);
        }
    }

    public void StartGame()
    {
        Time.timeScale = 1f;

        startScreenPanel.SetActive(false);

        if (gameplayUI != null)
        {
            gameplayUI.SetActive(true);
        }

        if (GameManager.Instance != null)
        {
            GameManager.Instance.BeginGame();
        }

        if (gridLines != null)
        {
            gridLines.SetGridVisible(true);
        }
    }
}