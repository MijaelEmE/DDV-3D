using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private GameObject defeatPanel;
    [SerializeField] private Button startButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button defeatRestartButton;
    [SerializeField] private Canvas[] gameplayCanvases;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        if (gameManager == null) return;
        gameManager.onGameStateChanged += OnGameStateChanged;
        if (startButton != null) startButton.onClick.AddListener(StartGame);
        if (restartButton != null) restartButton.onClick.AddListener(RestartGame);
        if (defeatRestartButton != null) defeatRestartButton.onClick.AddListener(RestartGame);
        OnGameStateChanged(gameManager.GetGameState());
    }

    private void OnGameStateChanged(GameManager.GameState state)
    {
        if (startPanel != null) startPanel.SetActive(state == GameManager.GameState.Menu);
        if (victoryPanel != null) victoryPanel.SetActive(state == GameManager.GameState.Victory);
        if (defeatPanel != null) defeatPanel.SetActive(state == GameManager.GameState.Defeat);
        if (gameplayCanvases != null)
            foreach (Canvas canvas in gameplayCanvases)
                if (canvas != null) canvas.enabled = state == GameManager.GameState.Playing;
    }

    private void StartGame()
    {
        if (gameManager != null) gameManager.StartGame();
    }

    private void RestartGame()
    {
        if (gameManager != null) gameManager.RestartGame();
    }

    private void OnDestroy()
    {
        if (gameManager != null) gameManager.onGameStateChanged -= OnGameStateChanged;
        if (startButton != null) startButton.onClick.RemoveListener(StartGame);
        if (restartButton != null) restartButton.onClick.RemoveListener(RestartGame);
        if (defeatRestartButton != null) defeatRestartButton.onClick.RemoveListener(RestartGame);
    }
}
