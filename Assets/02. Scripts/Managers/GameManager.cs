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
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if( _instance == null)
            {
                _instance = new GameObject().AddComponent<GameManager>();
            }

            return _instance;
        }
    }



    [Header("Managers")]
    public AudioManager AudioManager;
    public UIManager UIManager;
    public SceneLoader SceneLoader;



    public GameState CurrentState { get; private set; }



    private void Awake()
    {
        if (Instance == this)
            DontDestroyOnLoad(gameObject);
        else
            Destroy(gameObject);
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