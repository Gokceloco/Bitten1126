using UnityEngine;
using UnityEngine.InputSystem;

public class GameDirector : MonoBehaviour
{
    public LevelManager levelManager;
    public Player player;

    public static GameDirector instance { get; private set; }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void LoadPersistanceData()
    {
        
    }

    private void Start()
    {
        RestartLevel();
    }

    private void Update()
    {
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            RestartLevel();
        }
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            LoadNextLevel();
        }
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            LoadPreviousLevel();
        }
    }
    public void RestartLevel()
    {
        levelManager.RestartLevelManager();
        player.RestartPlayer();
    }

    public void LoadNextLevel()
    {

    }
    public void LoadPreviousLevel()
    {
        
    }

    public void LevelCompleted()
    {
        
    }

    public void LevelFailed()
    {
        
    }
}
