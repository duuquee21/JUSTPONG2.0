using UnityEngine;

public class ButtonColorChanger : MonoBehaviour
{
    private Renderer buttonRenderer;

    public Color defaultColor = Color.green;
    public Color collisionColor = Color.red;

    private void Start()
    {
        buttonRenderer = GetComponent<Renderer>();

        if (buttonRenderer != null )
        {
            buttonRenderer.material.color = defaultColor;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("FallingObject"))
        {
            if (buttonRenderer != null)
            {
                buttonRenderer.material.color = collisionColor;
            }
        }
    }
}
