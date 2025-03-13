using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public Camera mainCamera; // Asigna tu c�mara principal aqu�
    public float zoomFOV = 30f; // Campo de visi�n para el zoom
    public float zoomSpeed = 2f; // Velocidad de transici�n
    public Vector3 offset = new Vector3(0, 1, -2); // Ajuste de posici�n respecto a la plataforma
    private bool isZoomed = false;
    private Vector3 targetPosition;
    private float originalFOV;
    private Vector3 originalPosition;

    private Transform currentPlatform; // Plataforma actualmente seleccionada

    void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        originalFOV = mainCamera.fieldOfView;
        originalPosition = mainCamera.transform.position;
    }

    void Update()
    {
        // Detectar clic izquierdo para seleccionar la plataforma
        if (Input.GetMouseButtonDown(0)) // Detecta clic izquierdo
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Platform")) // Aseg�rate de etiquetar las plataformas como "Platform"
                {
                    currentPlatform = hit.collider.transform; // Guarda la plataforma seleccionada
                    targetPosition = currentPlatform.position + offset; // Calcula la posici�n de zoom
                }
            }
        }

        // Activar/desactivar el zoom con clic derecho
        if (Input.GetMouseButtonDown(1)) // Detecta clic derecho
        {
            isZoomed = !isZoomed; // Alterna el estado de zoom
        }

        // Realiza el zoom o des-zoom seg�n el estado
        if (isZoomed && currentPlatform != null)
        {
            // Interpola la posici�n de la c�mara hacia la plataforma
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, Time.deltaTime * zoomSpeed);
            // Interpola el FOV de la c�mara para hacer zoom
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, zoomFOV, Time.deltaTime * zoomSpeed);
        }
        else
        {
            // Interpola la posici�n de la c�mara hacia su posici�n original
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, originalPosition, Time.deltaTime * zoomSpeed);
            // Interpola el FOV de la c�mara para des-zoom
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, originalFOV, Time.deltaTime * zoomSpeed);
        }
    }
}
