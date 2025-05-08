using UnityEngine;

public class DisablePhysicsTrigger : MonoBehaviour
{
    public string ballTag = "Ball";

    public PhysicsMaterial ballDefaultMaterial;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ballTag))
        {
            Collider ballCollider = other.GetComponent<Collider>();

            if (ballCollider != null)
            {

                ballDefaultMaterial = ballCollider.material;
                ballCollider.material = null; // eliminar el physics material
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(ballTag))
        {
            Collider ballCollider = other.GetComponent<Collider>();

            if (ballCollider != null && ballDefaultMaterial != null)
            {
                ballCollider.material = ballDefaultMaterial;
            }
        }

    }
}
