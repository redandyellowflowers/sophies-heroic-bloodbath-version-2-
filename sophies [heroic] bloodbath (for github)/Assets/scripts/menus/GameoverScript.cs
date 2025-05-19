using System.Collections;
using TMPro;
using UnityEngine;

public class GameoverScript : MonoBehaviour
{
    [Header("player")]
    public GameObject player;

    [Header("gameObjects")]
    public GameObject gameOverUI;
    public GameObject hud;

    [Header("text")]
    public TextMeshProUGUI gameoverText;

    [TextArea(2, 4)]
    public string text;

    public GameObject borderImage;
    private bool gameIsPaused = false;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        borderImage = GameObject.Find("borderImage");
        gameoverText = gameOverUI.GetComponentInChildren<TextMeshProUGUI>();

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
            StartCoroutine(DeathThenUi());
        }

        borderImage.GetComponent<Animator>().SetBool("isInGameOverState", gameIsPaused);
    }


    IEnumerator DeathThenUi()
    {
        FindAnyObjectByType<AudioManager>().Stop("background");
        //FindAnyObjectByType<AudioManager>().Play("enemies are dead");
        Time.timeScale = .2f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        yield return new WaitForSeconds(.3f);

        hud.SetActive(false);

        gameIsPaused = true;
        gameOverUI.SetActive(true);
        gameoverText.text = text.ToString();
        Time.timeScale = 1;
    }
}
