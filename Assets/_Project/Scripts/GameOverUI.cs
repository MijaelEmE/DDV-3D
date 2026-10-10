using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private GameObject defeatPanel;
    [SerializeField] private Button restartButton;
    [SerializeField] private Text victoryText;
    [SerializeField] private Text defeatText;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        if (gameManager != null)
            gameManager.onGameStateChanged += OnGameStateChanged;

        if (restartButton != null)
            restartButton.onClick.AddListener(RestartGame);

        HideAllPanels();
    }

    private void OnGameStateChanged(GameManager.GameState state)
    {
        switch (state)
        {
            case GameManager.GameState.Victory:
                ShowVictoryPanel();
                break;
            case GameManager.GameState.Defeat:
                ShowDefeatPanel();
                break;
            default:
                HideAllPanels();
                break;
        }
    }

    private void ShowVictoryPanel()
    {
        HideAllPanels();
        if (victoryPanel != null)
            victoryPanel.SetActive(true);
    }

    private void ShowDefeatPanel()
    {
        HideAllPanels();
        if (defeatPanel != null)
            defeatPanel.SetActive(true);
    }

    private void HideAllPanels()
    {
        if (victoryPanel != null)
            victoryPanel.SetActive(false);
        if (defeatPanel != null)
            defeatPanel.SetActive(false);
    }

    private void RestartGame()
    {
        if (gameManager != null)
            gameManager.RestartGame();
    }
}
