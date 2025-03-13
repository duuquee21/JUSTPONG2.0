using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    public GameObject ballPrefab; // Prefab de la pelota
    public Transform launchPoint; // Punto desde el cual se lanza la pelota
    public Vector3 launchForce = new Vector3(5f, 10f, 0f); // Fuerza inicial del lanzamiento
    private GameObject currentBall;

    void Update()
    {
        // Lanza la pelota con la tecla Espacio
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LaunchBall();
        }

        // Reinicia la pelota con la tecla R
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            ResetBall();
        }
    }

    void LaunchBall()
    {
        // si no hay otr
        if (currentBall == null)
        {
            currentBall = Instantiate(ballPrefab, launchPoint.position, Quaternion.identity);
            Rigidbody rb = currentBall.GetComponent<Rigidbody>();
            rb.AddForce(launchForce, ForceMode.Impulse);
        }
    }

    void ResetBall()
    {
        // Destruye la pelota actual si existe
        if (currentBall != null)
        {
            Destroy(currentBall);
        }
    }
}
