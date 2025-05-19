using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.UI;

public class HealthPickUpScript : MonoBehaviour
{
    [Header("stats")]
    public int health = 5;

    [Header("gameObjects")]
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
            PlayerHealthScript playerHealth = collision.GetComponent<PlayerHealthScript>();

            if (playerHealth.currentHealth < playerHealth.maxHealth)
            {
                FindAnyObjectByType<AudioManager>().Play("health pick");

                playerHealth.AddHealth(health);
                Destroy(gameObject);
            }
            else
                playerHealth.currentHealth = playerHealth.maxHealth;//meaning nothing happens
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        interactText.GetComponent<TextMeshPro>().enabled = false;
        interactText.GetComponent<TextMeshPro>().GetComponent<VertexJitter>().enabled = false;
    }
}
