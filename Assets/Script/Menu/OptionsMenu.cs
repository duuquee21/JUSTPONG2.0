using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Scrollbar volumeScrollbar;
    public AudioSource audioSource;


    private void Start()
    {
        //cargar el volumen guardado 
        volumeScrollbar.value = PlayerPrefs.GetFloat("GameVolume", 1f);
        audioSource.volume = volumeScrollbar.value;
    }

    public void ChangeVolume()
    {
        audioSource.volume = volumeScrollbar.value;
        PlayerPrefs.SetFloat("GameVolume", volumeScrollbar.value);
        PlayerPrefs.Save();// guarda el valor guardado por el jugador
    }
    
}
