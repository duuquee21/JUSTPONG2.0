using UnityEngine;

public class PersistentMusic : MonoBehaviour
{
    private static PersistentMusic instance;

    void Awake()
    {
        // Si ya existe una instancia, destruye el duplicado
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Configurar como instancia única
        instance = this;
        DontDestroyOnLoad(gameObject); // Evita que se destruya al cambiar de escena
    }
}
