using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Experimental.GlobalIllumination;
public class ToggleNightMode : MonoBehaviour
{
    public GameObject nightModeVolume;
    public TMP_Text buttonText; // Cambia Text por TMP_Text
    public  GameObject DirectionallIght;

    private bool isNightMode = false;
    private bool lightOn = true;

    public void ToggleMode()
    {
        lightOn = !lightOn;
        isNightMode = !isNightMode;
        nightModeVolume.SetActive(isNightMode);
        DirectionallIght.SetActive(lightOn);

        if (buttonText != null)
        {
            buttonText.text = isNightMode ? "Modo Día" : "Modo Noche";
        }
    }
}