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
        inventory = FindObjectOfType<PlayerInventory>();
        objectiveManager = FindObjectOfType<ObjectiveManager>();

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
        SetGameState(GameState.Victory);
        Time.timeScale = 0f;

        if (victoryPanel != null)
            victoryPanel.SetActive(true);

        if (victorySound != null)
            AudioSource.PlayClipAtPoint(victorySound, Vector3.zero);

        Debug.Log("¡VICTORIA! ¡Escapaste del templo!");
    }

    public void Defeat()
    {
        SetGameState(GameState.Defeat);
        Time.timeScale = 0f;

        if (defeatPanel != null)
            defeatPanel.SetActive(true);

        if (defeatSound != null)
            AudioSource.PlayClipAtPoint(defeatSound, Vector3.zero);

        Debug.Log("¡DERROTA! El guardián te capturó.");
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
