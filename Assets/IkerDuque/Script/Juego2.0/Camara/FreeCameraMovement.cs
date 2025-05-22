using UnityEngine;

public class MouseControlledCamera : MonoBehaviour
{
    public float moveSpeed = 10f; // Velocidad de movimiento
    public float rotationSpeed = 5f; // Velocidad de rotación
    public float zoomSpeed = 50f; // Velocidad de zoom
    public float minHeight = 2f; // Altura mínima de la cámara
    public float maxHeight = 100f; // Altura máxima de la cámara

    public Vector3 minBounds = new Vector3(-50f, 2f, -50f); // Límites mínimos (X, Y, Z)
    public Vector3 maxBounds = new Vector3(50f, 100f, 50f); // Límites máximos (X, Y, Z)

    void Update()
    {
        HandleRotation();
        HandleMovement();
        HandleZoom();
        ClampPosition(); // Restringir la posición dentro de los límites
    }

    private void HandleRotation()
    {
        // Rotación al mantener pulsado el botón derecho del ratón
        if (Input.GetMouseButton(1))
        {
            float rotationX = Input.GetAxis("Mouse X") * rotationSpeed;
            float rotationY = Input.GetAxis("Mouse Y") * rotationSpeed;

            transform.Rotate(Vector3.up, rotationX, Space.World); // Rotar horizontalmente (alrededor del eje Y)
            transform.Rotate(Vector3.left, rotationY, Space.Self); // Rotar verticalmente (alrededor del eje X)
        }
    }

    private void HandleMovement()
    {
        // Movimiento al mantener pulsado el botón central del ratón (rueda)
        if (Input.GetMouseButton(2))
        {
            float moveX = -Input.GetAxis("Mouse X") * moveSpeed * Time.deltaTime; // Movimiento horizontal
            float moveY = -Input.GetAxis("Mouse Y") * moveSpeed * Time.deltaTime; // Movimiento vertical

            transform.Translate(new Vector3(moveX, moveY, 0), Space.Self);
        }
    }

    private void HandleZoom()
    {
        // Zoom con la rueda del ratón
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 position = transform.position;

        position += transform.forward * scroll * zoomSpeed * Time.deltaTime; // Zoom en dirección de la cámara
        position.y = Mathf.Clamp(position.y, minHeight, maxHeight); // Limitar altura

        transform.position = position;
    }

    private void ClampPosition()
    {
        // Restringir la posición de la cámara dentro de los límites
        Vector3 clampedPosition = transform.position;

        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minBounds.x, maxBounds.x);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, minBounds.y, maxBounds.y);
        clampedPosition.z = Mathf.Clamp(clampedPosition.z, minBounds.z, maxBounds.z);

        transform.position = clampedPosition;
    }
}
