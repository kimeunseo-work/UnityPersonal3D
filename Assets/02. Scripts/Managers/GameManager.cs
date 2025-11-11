using UnityEngine;

public enum GameState
{
    Title,
    Loading,
    Playing,
    Paused,
    GameOver
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Managers")]
    public AudioManager AudioManager;
    public UIManager UIManager;
    public SceneLoader SceneLoader;



    public GameState CurrentState { get; private set; }



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Reset()
    {
        AudioManager = GetComponent<AudioManager>();
        UIManager = GetComponent<UIManager>();
        SceneLoader = GetComponent<SceneLoader>();
    }



    public void ChangeState(GameState newState)
    {
        if (CurrentState != newState)
            CurrentState = newState;
    }
}