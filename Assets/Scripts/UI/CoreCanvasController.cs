using TMPro;
using UnityEngine;

public class CoreCanvasController : MonoBehaviour
{
    public static CoreCanvasController Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    public GameObject pauseMenu;
    public GameObject optionsMenu;
    public TextMeshProUGUI keyScoreText;
    
    // References
    private LevelManager _levelManager;

    private void Start()
    {
        _levelManager = LevelManager.Instance;
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartLevel()
    {
        if (_levelManager)
        {
            _levelManager.RestartLevel();
            pauseMenu.SetActive(false);
        }
        else
        {
            print("Error Restarting Level, No Level Manager Found");
        }
    }
    
    public void GoToMenu()
    {
        _levelManager.GoToMenu();
        pauseMenu.SetActive(false);
    }

    public void UpdateKeyScoreText(int collectedKeys, int maxKeys)
    {
        keyScoreText.text = $"{collectedKeys}/{maxKeys}";
    }
}
