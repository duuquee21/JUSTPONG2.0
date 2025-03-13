using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    private Transform selectedPlatform; // Plataforma actualmente seleccionada
    public float moveSpeed = 5f; // Velocidad de movimiento de la plataforma
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
                }
            }
        }

        // Mover la plataforma seleccionada con las flechas
        if (selectedPlatform != null)
        {
            // Movimiento en el plano XZ
            float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime; // Flechas izquierda/derecha
            float moveZ = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;   // Flechas arriba/abajo
            selectedPlatform.position += new Vector3(moveX, 0, moveZ);

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

            // Deseleccionar plataforma al presionar la tecla Escape
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                selectedPlatform = null;
            }
        }
    }
}
