using UnityEngine;

public class LowBouncePlatform : MonoBehaviour
{
    public float bounceReductionFactor = 0.5f; 

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Ball")) 
        {
            Rigidbody ballRigidbody = collision.gameObject.GetComponent<Rigidbody>();

            if (ballRigidbody != null)
            {
                
                Vector3 currentVelocity = ballRigidbody.linearVelocity;// velocidad actual de la pelota

                
                Vector3 reducedVelocity = currentVelocity * bounceReductionFactor; // reducir la velocidad con el factor de reduccion de rebote

               
                ballRigidbody.linearVelocity = reducedVelocity; // aplicar la velocidad reducida
            }
        }
    }
}
