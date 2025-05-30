using UnityEngine;

public class BallSound : MonoBehaviour
{
    public AudioClip bounceSound; // Clip de sonido del rebote
    private AudioSource audioSource;

    void Start()
    {
        // Obtener el componente AudioSource
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        // Reproducir el sonido al colisionar
        if (bounceSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(bounceSound);
        }
    }
}
