using UnityEngine;

public class CableController : MonoBehaviour
{
    public Transform startPoint;  // Punto de inicio del cable
    public Transform middlePoint; // Punto intermedio del cable
    public Transform endPoint;    // Punto final del cable

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 3; // El cable tiene tres puntos
    }

    void Update()
    {
        // Actualizar las posiciones de los puntos del cable
        lineRenderer.SetPosition(0, startPoint.position);  // Punto inicial
        lineRenderer.SetPosition(1, middlePoint.position); // Punto intermedio
        lineRenderer.SetPosition(2, endPoint.position);    // Punto final
    }
}
