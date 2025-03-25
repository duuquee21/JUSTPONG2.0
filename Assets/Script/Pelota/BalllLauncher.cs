using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    public GameObject ballPrefab; // Prefab de la bola que será disparada
    public Transform launchPoint; // Punto desde el cual se disparan las bolas
    public float launchForce = 10f; // Fuerza con la que se dispara la bola
    public float fireRate = 3f; // Intervalo entre disparos en segundos
    public float ballLifetime = 5f; // Tiempo de vida de cada bola en segundos

    private float timer = 0f; // Temporizador para controlar los disparos

    void Update()
    {
        // Incrementar el temporizador basado en el tiempo transcurrido
        timer += Time.deltaTime;

        // Disparar una bola si el temporizador alcanza el tiempo de disparo
        if (timer >= fireRate)
        {
            FireBall();
            timer = 0f; // Reiniciar el temporizador
        }
    }

    private void FireBall()
    {
        // Instanciar una nueva bola en el punto de lanzamiento
        GameObject newBall = Instantiate(ballPrefab, launchPoint.position, launchPoint.rotation);

        // Agregar fuerza a la bola
        Rigidbody rb = newBall.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(launchPoint.forward * launchForce, ForceMode.Impulse);
        }

        // Destruir la bola después de un tiempo
        Destroy(newBall, ballLifetime);
    }
}
