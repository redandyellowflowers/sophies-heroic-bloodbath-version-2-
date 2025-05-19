using EZCameraShake;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextLevelScript : MonoBehaviour
{
    [Header("gameObjects")]
    public GameObject interactText;
    public GameObject pauseMenu_gameManager;
    public GameObject levelCompleteUi;

    public GameObject hud;

    public Image flashScreen;

    public GameObject borderImage;
    private bool gameIsPaused;

    //'serializedfield' means that variable is still private, but is now accessable in the editor >> removed the serialized variable, but still good to know

    public void Awake()
    {
        pauseMenu_gameManager = GameObject.Find("game manager");
        borderImage = GameObject.Find("borderImage");

        //pressInteractText = GameObject.Find("finish level button");
        interactText.GetComponent<TextMeshPro>().enabled = false;
        interactText.GetComponent<TextMeshPro>().GetComponent<VertexJitter>().enabled = false;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.SetActive(false);
        levelCompleteUi.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        borderImage.GetComponent<Animator>().SetBool("isInWinState", gameIsPaused);
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        interactText.GetComponent<TextMeshPro>().enabled = true;
        interactText.GetComponent<TextMeshPro>().GetComponent<VertexJitter>().enabled = true;

        if (collision.gameObject.tag == "Player" && Input.GetKey(KeyCode.E))
        {
            gameIsPaused = true;
            {
                StartCoroutine(Flash());

                //FindAnyObjectByType<AudioManager>().Stop("enemies are dead");

                collision.gameObject.GetComponent<PlayerControllerScript>().enabled = false;
                collision.gameObject.GetComponent<PlayerShootingScript>().enabled = false;
                collision.gameObject.SetActive(false);

                pauseMenu_gameManager.GetComponent<PauseMenuScript>().enabled = false;
            }
        }
    }

    IEnumerator Flash()
    {
        if (flashScreen != null)
        {
            FindAnyObjectByType<AudioManager>().Play("enemy death");
            flashScreen.enabled = true;
            CameraShaker.Instance.ShakeOnce(6f, 1f, .1f, .4f);

            yield return new WaitForSeconds(0.02f);

            //FindAnyObjectByType<AudioManager>().Play("level complete");
            flashScreen.enabled = false;

            hud.SetActive(false);
            levelCompleteUi.SetActive(true);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (interactText != null)
        {
            interactText.GetComponent<TextMeshPro>().enabled = false;
            interactText.GetComponent<TextMeshPro>().GetComponent<VertexJitter>().enabled = false;
        }
    }
}
