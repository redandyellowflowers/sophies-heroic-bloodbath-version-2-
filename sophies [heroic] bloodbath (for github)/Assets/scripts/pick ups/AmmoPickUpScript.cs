using TMPro.Examples;
using TMPro;
using UnityEngine;

public class AmmoPickUpScript : MonoBehaviour
{
    [Header("stats")]
    public int ammo = 5;

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
            PlayerShootingScript playerShooting = collision.GetComponent<PlayerShootingScript>();

            if (playerShooting.currentAmmo < playerShooting.maxAmmo)
            {
                FindAnyObjectByType<AudioManager>().Play("health pick");//REFERENCING AUDIO MANAGER

                playerShooting.AddAmmo(ammo);
                Destroy(gameObject);
            }
            else
                playerShooting.currentAmmo = playerShooting.maxAmmo;//meaning nothing happens

            playerShooting.ammoCountText1.text = playerShooting.currentAmmo.ToString();
            playerShooting.ammoCountText2.text = playerShooting.maxAmmo.ToString();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        interactText.GetComponent<TextMeshPro>().enabled = false;
        interactText.GetComponent<TextMeshPro>().GetComponent<VertexJitter>().enabled = false;
    }
}
