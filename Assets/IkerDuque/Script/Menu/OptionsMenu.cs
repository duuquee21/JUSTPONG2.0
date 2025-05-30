using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Scrollbar volumeScrollbar;
    public AudioSource audioSource;

    public Scrollbar BrightnessScrollbar;
    public Light mainLight;

  

   
    
 

    public void Menu()
    {
        SceneManager.LoadScene("menu");
    }
}
