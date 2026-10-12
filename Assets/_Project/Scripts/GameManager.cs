using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState { Menu, Playing, Paused, Victory, Defeat }

    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private GameObject defeatPanel;
    [SerializeField] private AudioClip victorySound;
    [SerializeField] private AudioClip defeatSound;
    [SerializeField] private MonoBehaviour[] gameplayBehaviours;

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
        {
            Destroy(gameObject);
            return;
        }
        ApplyPlaybackState();
    }

    private void Start()
    {
        inventory = FindFirstObjectByType<PlayerInventory>();
        objectiveManager = FindFirstObjectByType<ObjectiveManager>();

        ApplyPlaybackState();
    }

    public void StartGame()
    {
        if (currentState != GameState.Menu) return;
        SetGameState(GameState.Playing);
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

    }

    public void Defeat()
    {
        if (!IsGameActive()) return;
        SetGameState(GameState.Defeat);

        if (defeatPanel != null)
            defeatPanel.SetActive(true);

        if (defeatSound != null)
            AudioSource.PlayClipAtPoint(defeatSound, Vector3.zero);

        Debug.Log("DERROTA! El guardian te capturo.");

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
        ApplyPlaybackState();
        onGameStateChanged?.Invoke(currentState);
    }

    private void ApplyPlaybackState()
    {
        bool playing = IsGameActive();
        Time.timeScale = playing ? 1f : 0f;
        if (gameplayBehaviours != null)
            foreach (MonoBehaviour behaviour in gameplayBehaviours)
                if (behaviour != null) behaviour.enabled = playing;
        Cursor.lockState = playing ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !playing;
    }

    private void OnDestroy()
    {
        if (instance != this) return;
        instance = null;
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public GameState GetGameState() => currentState;
    public bool IsGameActive() => currentState == GameState.Playing;
}
