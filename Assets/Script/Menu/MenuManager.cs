using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Nivel_1");
    }
    public void OptionsMenu()
    { 
        SceneManager.LoadScene("OptionsMenu");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
