using UnityEngine;

public class ButtonColorChanger : MonoBehaviour
{
    private Renderer buttonRenderer;

    public Color defaultColor = Color.green;
    public Color collisionColor = Color.red;
    public GameObject viento;

    private void Start()
    {
        buttonRenderer = GetComponent<Renderer>();

        if (buttonRenderer != null)
        {
            buttonRenderer.material.color = defaultColor;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            if (buttonRenderer != null)
            {
                buttonRenderer.material.color = collisionColor;
            }
            if (viento != null)
            {
                viento.SetActive(false);
            }

        }
    }
}
