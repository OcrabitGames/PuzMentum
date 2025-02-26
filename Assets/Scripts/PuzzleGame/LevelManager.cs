using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    
    [System.Serializable]
    public class LevelData
    {
        public string sceneName;
        public int requiredKeys;
    }
    
    public LevelData[] levels; // Array of levels (set in Inspector)
    private int currentLevelIndex = 0;
    private int keysCollected = 0;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        if (SceneManager.GetActiveScene().name != levels[currentLevelIndex].sceneName)
        {
            SceneManager.LoadScene(levels[currentLevelIndex].sceneName);
        }
    }
    
    public void LoadLevel(int index)
    {
        if (index < levels.Length)
        {
            currentLevelIndex = index;
            keysCollected = 0; // Reset collected keys
            SceneManager.LoadScene(levels[index].sceneName);
            
            // Create an intial loading screen
            Debug.Log($"Loading {levels[index].sceneName}...");
        }
        else
        {
            // Later do some win effect
            Debug.Log("All levels completed!");
        }
    }
    
    public void CollectKey()
    {
        keysCollected++;
        Debug.Log($"Keys Collected: {keysCollected}/{levels[currentLevelIndex].requiredKeys}");
    }

    public bool CheckConditions()
    {
        return keysCollected == levels[currentLevelIndex].requiredKeys;
    }

    public void NextLevel()
    {
        if (keysCollected >= levels[currentLevelIndex].requiredKeys) {
            int nextLevelIndex = currentLevelIndex + 1;
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
            }
        }
    }
}
