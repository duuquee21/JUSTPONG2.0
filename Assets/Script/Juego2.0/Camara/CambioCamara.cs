using UnityEngine;

public class SmoothCameraSwitcher : MonoBehaviour
{
    public Transform[] defaultCameraOffsets; // Offsets por defecto para cada eje (X, Y, Z)
    public float transitionSpeed = 2f; // Velocidad de transici�n
    private Camera mainCamera; // C�mara principal
    private int currentCameraIndex = 0; // �ndice de la c�mara actual
    private Vector3 targetPosition; // Posici�n objetivo
    private Quaternion targetRotation; // Rotaci�n objetivo

    public string activeAxis { get; private set; } = "XZ"; // Eje activo, por defecto XZ

    void Start()
    {
        mainCamera = Camera.main;

        // Establecemos la posici�n inicial
        if (defaultCameraOffsets.Length > 0)
        {
            UpdateTarget(defaultCameraOffsets[currentCameraIndex].position, defaultCameraOffsets[currentCameraIndex].rotation);
        }
    }

    void Update()
    {
        // Detectar la rueda del rat�n para cambiar de c�mara
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f)
        {
            SwitchCamera((currentCameraIndex + 1) % defaultCameraOffsets.Length);
        }
        else if (scroll < 0f)
        {
            SwitchCamera((currentCameraIndex - 1 + defaultCameraOffsets.Length) % defaultCameraOffsets.Length);
        }

        // Interpolar hacia la posici�n objetivo
        SmoothTransition();
    }

    private void SwitchCamera(int newCameraIndex)
    {
        currentCameraIndex = newCameraIndex;
        UpdateTarget(defaultCameraOffsets[currentCameraIndex].position, defaultCameraOffsets[currentCameraIndex].rotation);

        // Cambiar el eje activo seg�n la c�mara seleccionada
        switch (currentCameraIndex)
        {
            case 0: activeAxis = "Y"; break;  // C�mara X: mover en Y
            case 1: activeAxis = "XZ"; break; // C�mara Y: mover en XZ
            case 2: activeAxis = "X"; break;  // C�mara Z: mover en X
        }
    }

    private void UpdateTarget(Vector3 offsetPosition, Quaternion offsetRotation)
    {
        targetPosition = offsetPosition;
        targetRotation = offsetRotation;
    }

    private void SmoothTransition()
    {
        // Interpolar posici�n y rotaci�n
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, Time.deltaTime * transitionSpeed);
        mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, targetRotation, Time.deltaTime * transitionSpeed);
    }
}
