using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    public static ButtonSound Instance; // Singleton para persistencia

    public AudioClip buttonClickSound; // Sonido de clic
    private AudioSource audioSource;  // Fuente de audio global

    void Awake()
    {
        // Implementación del patrón Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Evita que este objeto se destruya al cambiar de escena
        }
        else
        {
            Destroy(gameObject); // Destruye duplicados
            return;
        }
    }

    void Start()
    {
        // Configurar el AudioSource si no existe
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        AssignSoundToAllButtons(); // Asignar sonidos a los botones actuales
    }

    void OnEnable()
    {
        // Registrar para asignar sonidos al cargar nuevas escenas
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // Eliminar registro al deshabilitar
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        AssignSoundToAllButtons(); // Asignar sonidos a los botones en la nueva escena
    }

    private void AssignSoundToAllButtons()
    {
        Button[] buttons = Object.FindObjectsByType<Button>(FindObjectsSortMode.None);
        foreach (Button button in buttons)
        {
            button.onClick.RemoveListener(() => PlayClickSound()); // Evitar asignaciones duplicadas
            button.onClick.AddListener(() => PlayClickSound());
        }
    }

    public void PlayClickSound()
    {
        if (audioSource != null && buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound);
        }
    }
}
