using UnityEngine;

public class FallingObject : MonoBehaviour
{
    public GameObject windZone;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            // Añadir un Rigidbody si no existe en el objeto
            rb = gameObject.AddComponent<Rigidbody>();
            rb.isKinematic = true; // Para que no caiga inicialmente
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            rb.isKinematic = false;
        }

        if (collision.gameObject.CompareTag("button"))
        {
            if (windZone != null)
            {
                windZone.SetActive(false);
            }
        }
    }

}




