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
    public TextMeshProUGUI timerText;
    
    public bool isLevel = true;
    private float _roundTimer;
    private bool _timerPaused = false;
    
    // References
    private LevelManager _levelManager;

    private void Start()
    {
        _levelManager = LevelManager.Instance;
        gameObject.SetActive(isLevel);
    }


    // Update is called once per frame
    void Update()
    {
        if (!isLevel) return;
        if (_timerPaused) return;
        
        UpdateTimer();
    }

    public void RestartLevel()
    {
        if (_levelManager)
        {
            pauseMenu.SetActive(false);
            _levelManager.RestartLevel();
        } else {
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
    
    public void Pause()
    {
        pauseMenu.SetActive(true);
        _levelManager.SetPaused(true);
        SetTimerPause(true);
    }
    
    public void Unpause()
    {
        pauseMenu.SetActive(false);
        _levelManager.SetPaused(false);
        SetTimerPause(false);
    }

    private void UpdateTimer()
    {
        _roundTimer += Time.deltaTime;
        SetTimerText();
    }

    private void SetTimerText()
    {
        timerText.text = _roundTimer.ToString("00.00");
    }

    public void ResetTimer()
    {
        _roundTimer = 0;
        SetTimerText();
    }
    
    public float GetRoundTime()
    {
        return _roundTimer;
    }

    public void SetTimerPause(bool isPaused)
    {
        _timerPaused = isPaused;
    }

    public void ActivateLevel()
    {
        isLevel = true;
        gameObject.SetActive(true);
    }
    
    public void DeactivateLevel()
    {
        isLevel = false;
        gameObject.SetActive(false);
    }
}
