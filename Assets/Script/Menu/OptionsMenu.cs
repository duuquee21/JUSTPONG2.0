using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Scrollbar volumeScrollbar;
    public AudioSource audioSource;

    public Scrollbar BrightnessScrollbar;
    public Light mainLight;

    private void Start()
    {
        //cargar el volumen guardado 
        volumeScrollbar.value = PlayerPrefs.GetFloat("GameVolume", 1f);
        audioSource.volume = volumeScrollbar.value;

        BrightnessScrollbar.value = PlayerPrefs.GetFloat("GameBrightness", 1f);
        mainLight.intensity = BrightnessScrollbar.value;
    }

    public void ChangeVolume()
    {
        audioSource.volume = volumeScrollbar.value;
        PlayerPrefs.SetFloat("GameVolume", volumeScrollbar.value);
        PlayerPrefs.Save();// guarda el valor guardado por el jugador
    }
    
    public void ChangeBrightness()
    {
        mainLight.intensity = BrightnessScrollbar.value * 2;
        PlayerPrefs.SetFloat ("GameBrightness",  BrightnessScrollbar.value);
        PlayerPrefs.Save();
    }

    public void Menu()
    {
        SceneManager.LoadScene("menu");
    }
}
