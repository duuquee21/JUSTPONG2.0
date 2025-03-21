using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject winMessageUI;
    public string ballTag = "Ball";
    public string winTag = "Win";
    void Start()
    {
        if (winMessageUI != null)
        {
            winMessageUI.SetActive(false);
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ballTag))
        {
            ShowWinMessage();
        }
    }

    public void ShowWinMessage()
    {
        if (winMessageUI != null)
        {
            winMessageUI.SetActive(true);
        }
    }
}
