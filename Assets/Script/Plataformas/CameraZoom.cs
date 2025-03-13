using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public Camera mainCamera; // Asigna tu cámara principal aquí
    public float zoomFOV = 30f; // Campo de visión para el zoom
    public float zoomSpeed = 2f; // Velocidad de transición
    public Vector3 offset = new Vector3(0, 1, -2); // Ajuste de posición respecto a la plataforma
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
                if (hit.collider.CompareTag("Platform")) // Asegúrate de etiquetar las plataformas como "Platform"
                {
                    currentPlatform = hit.collider.transform; // Guarda la plataforma seleccionada
                    targetPosition = currentPlatform.position + offset; // Calcula la posición de zoom
                }
            }
        }

        // Activar/desactivar el zoom con clic derecho
        if (Input.GetMouseButtonDown(1)) // Detecta clic derecho
        {
            isZoomed = !isZoomed; // Alterna el estado de zoom
        }

        // Realiza el zoom o des-zoom según el estado
        if (isZoomed && currentPlatform != null)
        {
            // Interpola la posición de la cámara hacia la plataforma
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, Time.deltaTime * zoomSpeed);
            // Interpola el FOV de la cámara para hacer zoom
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, zoomFOV, Time.deltaTime * zoomSpeed);
        }
        else
        {
            // Interpola la posición de la cámara hacia su posición original
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, originalPosition, Time.deltaTime * zoomSpeed);
            // Interpola el FOV de la cámara para des-zoom
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, originalFOV, Time.deltaTime * zoomSpeed);
        }
    }
}
