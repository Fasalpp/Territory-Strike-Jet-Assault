using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public void Resume()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene(2);
        Time.timeScale = 1;
    }
   
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
