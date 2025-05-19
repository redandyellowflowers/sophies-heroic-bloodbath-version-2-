using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    [Header("player")]
    public GameObject player;

    [Header("gameObjects")]
    public GameObject pauseUI;
    public GameObject hud;

    [Header("animations")]
    public GameObject borderImage;

    private bool gameIsPaused = false;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        borderImage = GameObject.Find("borderImage");
        borderImage.SetActive(true);

        hud = GameObject.Find("_hud");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            gameObject.GetComponent<PauseMenuScript>().enabled = false;
            FindAnyObjectByType<AudioManager>().Play("enemies are dead");//this is meant to go in the gameover script, if you can find out why the audio bugs out when placed there
        }

        if (Input.GetKeyDown(KeyCode.Escape) && gameIsPaused == false)
        {
            FindAnyObjectByType<AudioManager>().PlayForButton("click_forward");

            pauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && gameIsPaused == true)
        {
            FindAnyObjectByType<AudioManager>().PlayForButton("click_backward");

            resumeGame();
        }

        borderImage.GetComponent<Animator>().SetBool("isPaused", gameIsPaused);
    }

    public void pauseGame()
    {
        Time.timeScale = 0;
        pauseUI.SetActive(true);

        hud.SetActive(false);

        gameIsPaused = true;


        player.GetComponent<PlayerControllerScript>().enabled = false;
        player.GetComponent<PlayerShootingScript>().enabled = false;
    }

    public void resumeGame()
    {
        Time.timeScale = 1;
        pauseUI.SetActive(false);

        hud.SetActive(true);

        gameIsPaused = false;

        player.GetComponent<PlayerControllerScript>().enabled = true;
        player.GetComponent<PlayerShootingScript>().enabled = true;
    }
}
