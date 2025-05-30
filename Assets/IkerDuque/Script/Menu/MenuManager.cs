using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Pause Menu UI")]
    public GameObject pauseMenuUI; // Asigna el panel del menú de pausa en el Inspector

    [Header("Controls UI")]
    public GameObject controlsCanvas; // Asigna el Canvas de controles en el Inspector

    private bool isPaused = false;

    private void Start()
    {
        if (pauseMenuUI != null) // Canvas desactivado de inicio
        {
            pauseMenuUI.SetActive(false);
        }

        if (controlsCanvas != null) // Canvas de controles desactivado de inicio
        {
            controlsCanvas.SetActive(false);
        }
    }

    void Update()
    {
        // Detectar la tecla ESC para gestionar el estado del menú
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (controlsCanvas.activeSelf)
            {
                HideControls(); // Cierra el menú de controles si está activo
            }
            else if (isPaused)
            {
                ResumeGame(); // Reanuda el juego si el menú de pausa está activo
            }
            else
            {
                PauseGame(); // Pausa el juego y muestra el menú de pausa
            }
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0; // Detiene el tiempo en el juego
        pauseMenuUI.SetActive(true); // Muestra el menú de pausa
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1; // Restaura el tiempo del juego
        pauseMenuUI.SetActive(false); // Oculta el menú de pausa
    }

    public void ShowControls()
    {
        if (controlsCanvas != null && pauseMenuUI != null)
        {
            controlsCanvas.SetActive(true); // Muestra el Canvas de controles
            pauseMenuUI.SetActive(false); // Oculta el menú de pausa
        }
    }

    public void HideControls()
    {
        if (controlsCanvas != null && pauseMenuUI != null)
        {
            controlsCanvas.SetActive(false); // Oculta el Canvas de controles
            pauseMenuUI.SetActive(true); // Vuelve a mostrar el menú de pausa
        }
    }

    public void StartGame()
    {
        Time.timeScale = 1; // Asegúrate de que el tiempo esté activo al cambiar de escena
        SceneManager.LoadScene("Sel_Level");
    }

    public void MenuPrincipal()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("menu");
    }

    public void Tutorial_Level()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Tutorial");
    }

    public void EasyLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Nivel_1");
    }

    public void MediumLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Nivel_3");
    }

    public void HardLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Nivel_2");
    }

    public void OptionsMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("OptionsMenu");
    }

    public void ExitGame()
    {
        Time.timeScale = 1;
        Application.Quit();
    }
}
