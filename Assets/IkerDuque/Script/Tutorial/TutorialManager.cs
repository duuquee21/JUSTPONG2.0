using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public TMP_Text tutorialText; // Texto del tutorial en pantalla
    public Transform cameraTransform; // Transform de la cámara
    public Transform platformTransform; // Transform de una plataforma
    private int tutorialStep = 0; // Paso actual del tutorial

    private Vector3 initialCameraPosition; // Posición inicial de la cámara
    private float initialCameraZoom; // Distancia inicial de la cámara al punto de referencia
    private float cameraMoveThreshold = 5f; // Distancia mínima para completar el movimiento de cámara
    private float zoomThreshold = 2f; // Distancia mínima para completar el zoom
    private float platformMoveThreshold = 1f; // Distancia mínima para mover la plataforma
    private Vector3 initialPlatformPosition; // Posición inicial de la plataforma

    private bool cameraMoved = false;
    private bool zoomChanged = false;
    private bool platformMoved = false;

    void Start()
    {
        tutorialText.text = "Usa el botón derecho del ratón para mover la cámara estaticamente y  pulsa la rueda del raton para moverte por el escenario.";
        initialCameraPosition = cameraTransform.position;
        initialPlatformPosition = platformTransform.position;
        initialCameraZoom = Vector3.Distance(cameraTransform.position, Vector3.zero);
    }

    void Update()
    {
        switch (tutorialStep)
        {
            case 0:
                CheckCameraMovement();
                break;
            case 1:
                CheckZoom();
                break;
            case 2:
                CheckPlatformMovement();
                break;
        }
    }

    private void CheckCameraMovement()
    {
        float distanceMoved = Vector3.Distance(cameraTransform.position, initialCameraPosition);
        if (distanceMoved >= cameraMoveThreshold)
        {
            cameraMoved = true;
        }

        if (cameraMoved)
        {
            tutorialStep++;
            tutorialText.text = "Usa la rueda del ratón para hacer zoom.";
            initialCameraZoom = Vector3.Distance(cameraTransform.position, Vector3.zero); // Recalcular el zoom inicial
        }
    }

    private void CheckZoom()
    {
        float currentZoom = Vector3.Distance(cameraTransform.position, Vector3.zero);
        if (Mathf.Abs(currentZoom - initialCameraZoom) >= zoomThreshold)
        {
            zoomChanged = true;
        }

        if (zoomChanged)
        {
            tutorialStep++;
            tutorialText.text = "Mientras mantienes clickada una plataforma:   Usa las teclas W y S para mover una plataforma. Usa Q,E,A,D para rotarlas";
        }
    }

    private void CheckPlatformMovement()
    {
        // Movimiento de la plataforma con W y S
        if (Input.GetKey(KeyCode.W))
        {
            platformTransform.Translate(Vector3.up * Time.deltaTime, Space.World); // Mover hacia arriba
        }
        if (Input.GetKey(KeyCode.S))
        {
            platformTransform.Translate(Vector3.down * Time.deltaTime, Space.World); // Mover hacia abajo
        }

        // Comprobar si la plataforma se ha movido una distancia mínima
        float platformYMoved = Mathf.Abs(platformTransform.position.y - initialPlatformPosition.y);
        if (platformYMoved >= platformMoveThreshold)
        {
            platformMoved = true;
        }

        // Si se ha movido suficiente, avanzar al siguiente paso
        if (platformMoved)
        {
            tutorialStep++;
            tutorialText.text = "¡Bien hecho! Ahora guía la pelota al hoyo.";
        }
    }

}
