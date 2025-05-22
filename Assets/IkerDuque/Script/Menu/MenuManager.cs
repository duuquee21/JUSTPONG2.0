using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Pause Menu UI")]
    public GameObject pauseMenuUI; // Asigna el panel del menú de pausa en el Inspector

    private bool isPaused = false;

    private void Start()
    {
        if (pauseMenuUI  != null) // canvas desactivado de inicio
        {
            pauseMenuUI.SetActive(false);
        }
    }

    void Update()
    {
        // Detectar la tecla ESC para pausar o reanudar el juego
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
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
