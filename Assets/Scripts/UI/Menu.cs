using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private LevelManager _levelManager;

    private void Start()
    {
        _levelManager = LevelManager.Instance;
    }

    public void GoToHighestLevel()
    {
        _levelManager.LoadHighestUnlockedLevel();
    }
    public void GoToLevelSelect()
    {
        _levelManager.GoToLevelSelect();
    }
    
    public void GoToMiniGame()
    {
        _levelManager.GoToMiniGame();
    }
}
