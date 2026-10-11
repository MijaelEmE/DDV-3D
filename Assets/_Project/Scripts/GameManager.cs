using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState { Menu, Playing, Paused, Victory, Defeat }

    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private GameObject defeatPanel;
    [SerializeField] private AudioClip victorySound;
    [SerializeField] private AudioClip defeatSound;

    private GameState currentState = GameState.Menu;
    private PlayerInventory inventory;
    private ObjectiveManager objectiveManager;

    public System.Action<GameState> onGameStateChanged;

    private static GameManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        inventory = FindFirstObjectByType<PlayerInventory>();
        objectiveManager = FindFirstObjectByType<ObjectiveManager>();

        StartGame();
    }

    public void StartGame()
    {
        SetGameState(GameState.Playing);
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        if (currentState == GameState.Playing)
            SetGameState(GameState.Paused);
    }

    public void ResumeGame()
    {
        if (currentState == GameState.Paused)
            SetGameState(GameState.Playing);
    }

    public void Victory()
    {
        if (!IsGameActive()) return;
        if (inventory == null || !inventory.HasAllItems()) return;
        if (objectiveManager == null || !objectiveManager.IsPortalActive()) return;
        SetGameState(GameState.Victory);

        if (victoryPanel != null)
            victoryPanel.SetActive(true);

        if (victorySound != null)
            AudioSource.PlayClipAtPoint(victorySound, Vector3.zero);

        Debug.Log("VICTORIA! Escapaste del templo!");

        Invoke(nameof(RestartGame), 5f);
    }

    public void Defeat()
    {
        SetGameState(GameState.Defeat);

        if (defeatPanel != null)
            defeatPanel.SetActive(true);

        if (defeatSound != null)
            AudioSource.PlayClipAtPoint(defeatSound, Vector3.zero);

        Debug.Log("DERROTA! El guardian te capturo.");

        // Cambia 5f por el tiempo deseado en segundos (ej. 1.5f)
        Invoke(nameof(RestartGame), 1.5f); 
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }

    private void SetGameState(GameState newState)
    {
        if (currentState == newState) return;

        currentState = newState;
        onGameStateChanged?.Invoke(currentState);
    }

    public GameState GetGameState() => currentState;
    public bool IsGameActive() => currentState == GameState.Playing;
}
