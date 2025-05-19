using TMPro.Examples;
using TMPro;
using UnityEngine;

public class PickUpToCompleteLevelScript : MonoBehaviour
{
    //public static int numberOfKeys;

    public GameObject interactText;

    private void Awake()
    {
        interactText.GetComponent<TextMeshPro>().enabled = false;
        interactText.GetComponent<TextMeshPro>().GetComponent<VertexJitter>().enabled = false;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        interactText.GetComponent<TextMeshPro>().enabled = true;
        interactText.GetComponent<TextMeshPro>().GetComponent<VertexJitter>().enabled = true;

        if (collision.gameObject.tag == "Player" && Input.GetKey(KeyCode.E))
        {
            FindAnyObjectByType<AudioManager>().Play("health pick");
            //numberOfKeys++;
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        interactText.GetComponent<TextMeshPro>().enabled = false;
        interactText.GetComponent<TextMeshPro>().GetComponent<VertexJitter>().enabled = false;
    }
}
