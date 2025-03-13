using UnityEngine;

public class Viento : MonoBehaviour
{
    public Vector3 windForce = new Vector3(5f, 0f, 0f); // Dirección y magnitud del viento

    private void OnTriggerStay(Collider other)
    {
        // Verifica si el objeto que entra es la pelota
        if (other.CompareTag("Ball")) // Asegúrate de etiquetar la pelota como "Ball"
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Aplica la fuerza del viento a la pelota
                rb.AddForce(windForce, ForceMode.Force);
            }
        }
    }
}
