using UnityEngine;

public class MovimientoPlataforma : MonoBehaviour
{


    private Transform selectedPlatform; // Plataforma actualmente seleccionada
    private Plane dragPlane; // Plano sobre el que se arrastra la plataforma
    private Vector3 offset; // Desplazamiento entre el punto de clic y el centro de la plataforma
    public float rotationSpeed = 50f; // Velocidad de rotación de la plataforma
    public float tiltSpeed = 50f; // Velocidad de inclinación de la plataforma

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

                    // Crear un plano en la posición de la plataforma y obtener el desplazamiento
                    dragPlane = new Plane(Vector3.up, selectedPlatform.position);
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
                selectedPlatform.position = hitPoint + offset; // Ajustar posición de la plataforma
            }
        }

        // Rotar o inclinar la plataforma seleccionada con teclas
        if (selectedPlatform != null)
        {
            // Rotación alrededor del eje Y con Q y E
            if (Input.GetKey(KeyCode.Q)) // Rotar en sentido horario
            {
                selectedPlatform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.E)) // Rotar en sentido antihorario
            {
                selectedPlatform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
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
}


