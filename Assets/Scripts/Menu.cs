using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void GoToPuzzle1()
    {
        SceneManager.LoadScene("Puzzle1");
    }
    
    public void GoToMiniGame()
    {
        SceneManager.LoadScene("MiniGame");
    }
}
