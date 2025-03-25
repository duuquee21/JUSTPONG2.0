using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    private Transform selectedPlatform; // Plataforma seleccionada
    private Plane dragPlane; // Plano de arrastre
    private Vector3 offset; // Desplazamiento entre el clic y la posición de la plataforma
    public float rotationSpeed = 50f; // Velocidad de rotación
    public float tiltSpeed = 50f; // Velocidad de inclinación
    public float heightAdjustmentSpeed = 5f; // Velocidad para ajustar la posición en Y
    private float currentHeightOffset = 0f; // Desplazamiento acumulado en el eje Y

    void Update()
    {
        HandlePlatformSelection();
        HandlePlatformMovement();
        HandlePlatformRotationAndTilt();
        HandlePlatformHeightAdjustment();
    }

    private void HandlePlatformSelection()
    {
        // Detectar selección de plataforma con el botón izquierdo del ratón
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Platform")) // Asegúrate de etiquetar las plataformas como "Platform"
                {
                    selectedPlatform = hit.collider.transform; // Guardar la plataforma seleccionada

                    // Crear un plano de arrastre basado en la posición de la plataforma
                    dragPlane = new Plane(Vector3.up, selectedPlatform.position);

                    if (dragPlane.Raycast(ray, out float enter))
                    {
                        Vector3 hitPoint = ray.GetPoint(enter);
                        offset = selectedPlatform.position - hitPoint;
                    }

                    // Inicializar desplazamiento acumulado de altura
                    currentHeightOffset = 0f;
                }
            }
        }

        // Deseleccionar la plataforma al soltar el botón izquierdo
        if (Input.GetMouseButtonUp(0))
        {
            selectedPlatform = null;
        }
    }

    private void HandlePlatformMovement()
    {
        // Mover la plataforma seleccionada mientras se mantiene el botón izquierdo del ratón
        if (selectedPlatform != null && Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (dragPlane.Raycast(ray, out float enter))
            {
                Vector3 hitPoint = ray.GetPoint(enter);
                Vector3 newPosition = hitPoint + offset;

                // Ajustar la posición Y acumulada
                newPosition.y += currentHeightOffset;

                // Actualizar la posición de la plataforma
                selectedPlatform.position = newPosition;
            }
        }
    }

    private void HandlePlatformRotationAndTilt()
    {
        // Rotar e inclinar la plataforma seleccionada con las teclas
        if (selectedPlatform != null)
        {
            // Rotación alrededor del eje Y con Q y E
            if (Input.GetKey(KeyCode.A)) // Rotar en sentido horario
            {
                selectedPlatform.Rotate(Vector3.forward, -rotationSpeed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.D)) // Rotar en sentido antihorario
            {
                selectedPlatform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
            }

            // Inclinación (tumbar o levantar) con R y F
            if (Input.GetKey(KeyCode.Q)) // Inclinar hacia adelante
            {
                selectedPlatform.Rotate(Vector3.right, -tiltSpeed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.E)) // Inclinar hacia atrás
            {
                selectedPlatform.Rotate(Vector3.right, tiltSpeed * Time.deltaTime);
            }
        }
    }

    private void HandlePlatformHeightAdjustment()
    {
        // Cambiar la posición en Y de la plataforma seleccionada con W y S
        if (selectedPlatform != null)
        {
            if (Input.GetKey(KeyCode.W)) // Subir plataforma
            {
                currentHeightOffset += heightAdjustmentSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.S)) // Bajar plataforma
            {
                currentHeightOffset -= heightAdjustmentSpeed * Time.deltaTime;
            }

            // Aplicar el desplazamiento acumulado en Y
            Vector3 newPosition = selectedPlatform.position;
            newPosition.y = selectedPlatform.position.y + currentHeightOffset;
            selectedPlatform.position = newPosition;
        }
    }
}
