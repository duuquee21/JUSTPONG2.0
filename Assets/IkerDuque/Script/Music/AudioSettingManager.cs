using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioSettings : MonoBehaviour
{
    public AudioMixer audioMixer; // El Audio Mixer
    public Slider volumeSlider;  // El Slider de volumen

    private const string VOLUME_PARAM = "MasterMixer"; // Nombre del parámetro del Mixer

    void Start()
    {
        // Inicializa el Slider con el valor actual del volumen
        if (audioMixer.GetFloat(VOLUME_PARAM, out float currentVolume))
        {
            // Convertir de decibeles a rango [0, 1] para el Slider
            volumeSlider.value = Mathf.InverseLerp(-80f, 0f, currentVolume);
        }

        // Asignar el evento del Slider
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float sliderValue)
    {
        // Convertir de rango [0, 1] a decibeles
        float volumeInDb = Mathf.Lerp(-80f, 0f, sliderValue);
        audioMixer.SetFloat(VOLUME_PARAM, volumeInDb);
    }
}
