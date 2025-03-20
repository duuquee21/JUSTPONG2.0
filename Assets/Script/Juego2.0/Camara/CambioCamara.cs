using UnityEngine;

public class SmoothCameraSwitcher : MonoBehaviour
{
    public Transform[] defaultCameraOffsets; // Offsets por defecto para cada eje (X, Y, Z)
    public float transitionSpeed = 2f; // Velocidad de transición
    private Transform selectedPlatform; // Plataforma actualmente seleccionada
    private Camera mainCamera; // Cámara principal
    private int currentCameraIndex = 0; // Índice de la cámara actual
    private Vector3 targetPosition; // Posición objetivo
    private Quaternion targetRotation; // Rotación objetivo

    public string activeAxis { get; private set; } = "XZ"; // Eje activo, por defecto XZ

    void Start()
    {
        mainCamera = Camera.main;

        // Establecemos la posición inicial
        if (defaultCameraOffsets.Length > 0)
        {
            UpdateTarget(defaultCameraOffsets[currentCameraIndex].position, defaultCameraOffsets[currentCameraIndex].rotation);
        }
    }

    void Update()
    {
        // Detectar la rueda del ratón para cambiar de cámara
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f)
        {
            SwitchCamera((currentCameraIndex + 1) % defaultCameraOffsets.Length);
        }
        else if (scroll < 0f)
        {
            SwitchCamera((currentCameraIndex - 1 + defaultCameraOffsets.Length) % defaultCameraOffsets.Length);
        }

        // Interpolar hacia la posición objetivo
        SmoothTransition();

        // Detectar selección de plataforma con clic izquierdo
        if (Input.GetMouseButtonDown(0))
        {
            DetectPlatform();
        }
    }

    private void SwitchCamera(int newCameraIndex)
    {
        currentCameraIndex = newCameraIndex;
        UpdateTarget(defaultCameraOffsets[currentCameraIndex].position, defaultCameraOffsets[currentCameraIndex].rotation);

        // Cambiar el eje activo según la cámara seleccionada
        switch (currentCameraIndex)
        {
            case 0: activeAxis = "Y"; break;  // Cámara X: mover en Y
            case 1: activeAxis = "X"; break;  // Cámara Y: mover en X
            case 2: activeAxis = "Z"; break;  // Cámara Z: mover en Z
        }
    }

    private void UpdateTarget(Vector3 offsetPosition, Quaternion offsetRotation)
    {
        if (selectedPlatform != null)
        {
            // Ajustar el objetivo a la plataforma seleccionada
            targetPosition = selectedPlatform.position + offsetPosition;
        }
        else
        {
            // Usar las posiciones por defecto
            targetPosition = offsetPosition;
        }
        targetRotation = offsetRotation;
    }

    private void SmoothTransition()
    {
        // Interpolar posición y rotación
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, Time.deltaTime * transitionSpeed);
        mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, targetRotation, Time.deltaTime * transitionSpeed);
    }

    private void DetectPlatform()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("Platform")) // Asegúrate de que las plataformas tengan la etiqueta "Platform"
            {
                selectedPlatform = hit.collider.transform;

                // Actualizar la posición objetivo en función de la plataforma seleccionada
                UpdateTarget(defaultCameraOffsets[currentCameraIndex].position, defaultCameraOffsets[currentCameraIndex].rotation);
            }
        }
    }

    // Devuelve el índice de la cámara actual
    public int GetCurrentCameraIndex()
    {
        return currentCameraIndex;
    }
}


