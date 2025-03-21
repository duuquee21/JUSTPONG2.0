using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    private Transform selectedPlatform; // Plataforma actualmente seleccionada
    private Plane dragPlane; // Plano sobre el que se arrastra la plataforma
    private Vector3 offset; // Desplazamiento entre el punto de clic y el centro de la plataforma
    public float rotationSpeed = 50f; // Velocidad de rotación de la plataforma
    public float tiltSpeed = 50f; // Velocidad de inclinación de la plataforma

    public SmoothCameraSwitcher cameraSwitcher; // Referencia al script de cambio de cámara

    void Update()
    {
        // Detectar selección de plataforma con clic izquierdo
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Platform")) // Asegúrate de etiquetar las plataformas como "Platform"
                {
                    selectedPlatform = hit.collider.transform; // Guarda la plataforma seleccionada

                    // Crear un plano en la posición de la plataforma y ajustar según la cámara activa
                    Vector3 planeNormal = GetPlaneNormal(); // Determina el plano según el eje activo
                    dragPlane = new Plane(planeNormal, selectedPlatform.position);

                    if (dragPlane.Raycast(ray, out float enter))
                    {
                        Vector3 hitPoint = ray.GetPoint(enter);
                        offset = selectedPlatform.position - hitPoint;
                    }
                }
            }
        }

        // Arrastrar la plataforma con el ratón mientras se mantiene el clic izquierdo
        if (selectedPlatform != null && Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (dragPlane.Raycast(ray, out float enter))
            {
                Vector3 hitPoint = ray.GetPoint(enter);
                Vector3 newPosition = hitPoint + offset;

                // Restringir el movimiento según la cámara activa
                switch (cameraSwitcher.activeAxis)
                {
                    case "Y":
                        // Permitir movimiento en X y Z, pero bloquear Y
                        newPosition.x = selectedPlatform.position.x;
                        newPosition.z = selectedPlatform.position.z;
                        break;

                    case "XZ":
                        // Permitir movimiento en X y Z, bloquear Y
                        newPosition.y = selectedPlatform.position.y;
                        break;

                    case "X":
                        // Permitir movimiento solo en Y (bloquear X y Z)
                        newPosition.x = selectedPlatform.position.x;
                        newPosition.z = selectedPlatform.position.z;
                        break;
                }

                selectedPlatform.position = newPosition; // Ajustar posición de la plataforma
            }
        }

        // Rotar o inclinar la plataforma seleccionada con teclas
        if (selectedPlatform != null)
        {
            // Rotación alrededor del eje Y con Q y E
            if (Input.GetKey(KeyCode.Q)) // Rotar en sentido horario
            {
                selectedPlatform.Rotate(Vector3.forward, -rotationSpeed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.E)) // Rotar en sentido antihorario
            {
                selectedPlatform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
            }

            // Inclinación (tumbar o levantar) con R y F
            if (Input.GetKey(KeyCode.R)) // Inclinar hacia adelante
            {
                selectedPlatform.Rotate(Vector3.right, -tiltSpeed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.F)) // Inclinar hacia atrás
            {
                selectedPlatform.Rotate(Vector3.right, tiltSpeed * Time.deltaTime);
            }
        }

        // Deseleccionar plataforma al soltar el clic izquierdo
        if (Input.GetMouseButtonUp(0))
        {
            selectedPlatform = null;
        }
    }

    // Método para determinar el plano según el eje activo
    private Vector3 GetPlaneNormal()
    {
        if (cameraSwitcher == null) return Vector3.up;

        // Define el plano según el eje activo
        switch (cameraSwitcher.activeAxis)
        {
            case "Y": return Vector3.forward; // Movimiento en el plano XZ
            case "XZ": return Vector3.up;    // Movimiento en el plano horizontal
            case "X": return Vector3.up;    // Movimiento restringido al eje Y
            default: return Vector3.up;
        }
    }
}

