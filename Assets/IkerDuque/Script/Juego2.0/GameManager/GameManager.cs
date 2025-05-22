using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cambiar escenas

public class GameManager : MonoBehaviour
{
    public GameObject winCanvas;

    public string ballTag = "Ball";

    private void Start()
    {
        if (winCanvas != null) // canvas desactivado de inicio
        {
            winCanvas.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ballTag))
        {

            ShowWinCanvas();
        }
    }

    public void ShowWinCanvas()
    {
        if ( winCanvas != null )
        {
            winCanvas.SetActive(true); // mostrar el mensaje de victori
            
        }
    }


}