using UnityEngine;
using UnityEngine.Rendering;

public class PlataformaRebotePlus : MonoBehaviour
{
  public float bounceImpulse = 1.0f; //cantidad de impulso adicional

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Rigidbody ballRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if (ballRigidbody != null)
            {
                Vector3 reboundDirection = collision.contacts[0].normal;

                ballRigidbody.AddForce(-reboundDirection * bounceImpulse, ForceMode.Impulse); //el addImpulse se aplica en el forcemode
            }
        }
    }
}
