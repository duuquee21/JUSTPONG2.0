using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Método que carga la escena del juego
    public void CargarEscenaJuego()
    {
        // Asegúrate de que la escena que deseas cargar esté añadida en las configuraciones de Build
        SceneManager.LoadScene("CampoPruebasDef");
    }
}
