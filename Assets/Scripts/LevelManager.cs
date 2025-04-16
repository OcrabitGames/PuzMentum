using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public bool debugMode;
    
    public delegate void OnSceneLoaded();
    public static event OnSceneLoaded SceneLoadedEvent;
    
    [System.Serializable]
    public class LevelData
    {
        public string sceneName;
        public int requiredKeys;
    }
    
    public LevelData[] levels; // Array of levels (set in Inspector)
    private int _currentLevelIndex = 0;
    private int _keysCollected = 0;
    private int _maxLevelUnlocked = 0;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoadedCallback;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        if (debugMode) return;
        
        // if (SceneManager.GetActiveScene().name != levels[currentLevelIndex].sceneName)
        // {
        //     SceneManager.LoadScene(levels[currentLevelIndex].sceneName);
        // }
    }
    
    public void LoadLevel(int index)
    {
        if (index < levels.Length && index <= _maxLevelUnlocked)
        {
            _currentLevelIndex = index;
            _keysCollected = 0; // Reset collected keys
            SceneManager.LoadScene(levels[index].sceneName);
            
            // Create an intial loading screen
            Debug.Log($"Loading {levels[index].sceneName}...");
        } else {
            print("Level not unlocked!");
        }
    }
    
    public void CollectKey()
    {
        _keysCollected++;
        CoreCanvasController.Instance.UpdateKeyScoreText(_keysCollected, levels[_currentLevelIndex].requiredKeys);
        Debug.Log($"Keys Collected: {_keysCollected}/{levels[_currentLevelIndex].requiredKeys}");
    }

    private void UpdateKeyText()
    {
        var instanceRef = CoreCanvasController.Instance;
        if (instanceRef)
        {
            instanceRef.UpdateKeyScoreText(_keysCollected, levels[_currentLevelIndex].requiredKeys);
        }
    }

    public bool CheckConditions()
    {
        return _keysCollected >= levels[_currentLevelIndex].requiredKeys;
    }

    public void NextLevel()
    {
        if (CheckConditions()) {
            int nextLevelIndex = _currentLevelIndex + 1;
            if (nextLevelIndex < levels.Length)
            {
                // Increment Highest Unlock Level
                if (nextLevelIndex > _maxLevelUnlocked)  { _maxLevelUnlocked++; }
                LoadLevel(nextLevelIndex);
            }
            else
            {
                // Do some action on completion
                
                Debug.Log("Game Completed!");
                // Optionally load a game over or main menu scene
                //SceneManager.LoadScene("MainMenu");
                // Roll Credits
            }
        }
    }

    public void LoadHighestUnlockedLevel()
    {
        LoadLevel(_maxLevelUnlocked);
    }

    public void RestartLevel()
    {
        LoadLevel(_currentLevelIndex);
        _keysCollected = 0;
    }

    public void GoToMiniGame()
    {
        SceneManager.LoadScene("MiniGame");
    }
    
    public void GoToMenu()
    {
        Debug.Log("Going to menu");
        SceneManager.LoadScene("Menu");
    }
    
    public void GoToLevelSelect()
    {
        Debug.Log("Going to level select");
        SceneManager.LoadScene("Levels");
    }
    
    public void QuitGame()
    {
        // Do Later
    }

    public int GetHighestUnlockedLevel()
    {
        return _maxLevelUnlocked;
    }
    
    
    // Handle On Scene Load
    private void OnSceneLoadedCallback(Scene scene, LoadSceneMode mode)
    {
        SceneLoadedEvent?.Invoke();
        UpdateKeyText();
    }
    
    private void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoadedCallback;
        }
    }
}
