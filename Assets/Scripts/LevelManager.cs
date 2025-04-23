using System.Text.RegularExpressions;
using Audio;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public bool debugMode;
    private bool _paused = false;
    
    public delegate void OnSceneLoaded();
    public static event OnSceneLoaded SceneLoadedEvent;
    
    [System.Serializable]
    public class LevelData
    {
        public string sceneName;
        public int requiredKeys;
        public float[] starThresholds = new float[3];
        public AudioClip levelAudio;
    }
    
    public LevelData[] levels; // Array of levels (set in Inspector)
    public string activeSceneName = "Menu";
    private int _currentLevelIndex = 0;
    private int _keysCollected = 0;
    private int _maxLevelUnlocked;
    
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
        // Pull Highest Unlocked Level
        _maxLevelUnlocked = PlayerPrefs.GetInt("MaxLevelUnlocked", 0);
        
        if (debugMode)
        {
            // Set Active Scene Number
            activeSceneName = SceneManager.GetActiveScene().name;
            var match = Regex.Match(activeSceneName, @"\d+");
            if (match.Success)
            {
                int levelNumber = int.Parse(match.Value);
                Debug.Log($"Parsed level number: {levelNumber}");
                // Optionally use levelNumber for anything you want here
                _currentLevelIndex = levelNumber - 1;
            }
            else
            {
                Debug.LogWarning("No number found in scene name.");
            }
        }
    }

    private bool CheckIfActiveSceneIsLevel()
    {
        var match = Regex.Match(activeSceneName, @"\d+");
        return match.Success;
    }
    
    public void LoadLevel(int index)
    {
        if (index < levels.Length && index <= _maxLevelUnlocked)
        {
            // Get Next Scene Ready
            _currentLevelIndex = index;
            _keysCollected = 0; // Reset collected keys
            activeSceneName = levels[index].sceneName;
            SetPaused(false);
            
            // Audio Handler
            if (PlayerPrefs.GetInt("ProgressiveSoundtrack", 0) == 1)
            {
                SoundManager.Instance.HandleEnterLevel(index);
            }
            
            // Load Scene
            SceneManager.LoadScene(activeSceneName);
            
            // Create an intial loading screen
            Debug.Log($"Loading {activeSceneName}...");
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

    private void UpdateCanvasController()
    {
        var instanceRef = CoreCanvasController.Instance;
        if (instanceRef)
        {
            var isLevel = CheckIfActiveSceneIsLevel();
            if (isLevel)
            {
                instanceRef.UpdateKeyScoreText(_keysCollected, levels[_currentLevelIndex].requiredKeys);
                instanceRef.ResetTimer();
                instanceRef.ActivateLevel();
                instanceRef.SetTimerPause(false);
            } else {
                instanceRef.DeactivateLevel();
            }
        }
    }

    public void CheckSaveRoundTime()
    {
        if (CoreCanvasController.Instance.isLevel)
        {
            var currenRoundTime = CoreCanvasController.Instance.GetRoundTime();
            var currentSetFloat = PlayerPrefs.GetFloat($"BestTime_{_currentLevelIndex}", float.MaxValue);
            print($"Current Set Best Time: {currentSetFloat}");
            if (currenRoundTime < currentSetFloat)
            {
                PlayerPrefs.SetFloat($"BestTime_{_currentLevelIndex}", currenRoundTime);
                print($"Set Best Time for level {_currentLevelIndex}");
                print($"Set New RECORD: {currenRoundTime}");
                PlayerPrefs.Save();
            }

            CoreCanvasController.Instance.SetTimerPause(true);
        }
    }
    
    public void UpdateRoundTime()
    {

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

    public void CheckSaveNextLevel()
    {
        // Increment Highest Unlock Level
        if (_currentLevelIndex + 1 > _maxLevelUnlocked)
        {
            _maxLevelUnlocked++;
            PlayerPrefs.SetInt("MaxLevelUnlocked", _maxLevelUnlocked);
        }
    }

    public void LoadHighestUnlockedLevel()
    {
        LoadLevel(_maxLevelUnlocked);
    }

    public void RestartLevel()
    {
        LoadLevel(_currentLevelIndex);
    }

    public void GoToMiniGame()
    {
        Debug.Log("Going to MiniGame...");
        activeSceneName = "MiniGame";
        SceneManager.LoadScene(activeSceneName);
    }
    
    public void GoToMenu()
    {
        Debug.Log("Going to menu...");
        activeSceneName = "Menu";
        
        // Handle Audio
        if (PlayerPrefs.GetInt("ProgressiveSoundtrack", 0) == 1)
        {
            SoundManager.Instance.ReturnToMain();
        }
        
        // Load Scene
        SceneManager.LoadScene(activeSceneName);
    }
    
    public void GoToLevelSelect()
    {
        Debug.Log("Going to level select...");
        activeSceneName = "Levels";
        SceneManager.LoadScene(activeSceneName);
    }
    
    public void QuitGame()
    {
        // Do Later
    }

    public int GetHighestUnlockedLevel()
    {
        return _maxLevelUnlocked;
    }
    
    public float[] GetThresholdsForLevel(int levelIndex)
    {
        return levels[levelIndex].starThresholds;
    }

    public bool IsPaused()
    {
        return _paused;
    }

    public void SetPaused(bool paused)
    {
        _paused = paused;
    }
    
    // Handle On Scene Load
    private void OnSceneLoadedCallback(Scene scene, LoadSceneMode mode)
    {
        SceneLoadedEvent?.Invoke();
        UpdateCanvasController();
    }
    
    private void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoadedCallback;
        }
    }
}
