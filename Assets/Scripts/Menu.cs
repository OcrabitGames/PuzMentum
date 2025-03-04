using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void GoToMenu()
    {
        Debug.Log("Going to menu");
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
