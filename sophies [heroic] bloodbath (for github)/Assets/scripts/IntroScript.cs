using EZCameraShake;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class IntroScript : MonoBehaviour
{
    [Header("player")]
    public GameObject player;

    [Header("animation/s")]
    public Animator anim;
    bool intro;

    [Header("values")]
    public float exitAnimTime;

    [Header("gameObjects")]
    //public GameObject pauseMenu_gameManager;

    [Header("text")]
    public TextMeshProUGUI dialogueText;
    public GameObject introUi;
    public TextMeshProUGUI pressToContinue;

    [Header("dialogue")]
    public float dialogueSpeed;
    public string npcName;
    [TextArea(2, 4)] public string[] sentences;
    private int index = 0;//tracking the sentences

    private bool isDoneTalking = true;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        FindAnyObjectByType<AudioManager>().Play("level complete");

        //pauseMenu_gameManager = GameObject.Find("game manager");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        NextSentence();

        introUi.SetActive(true);

        intro = true;
    }

    // Update is called once per frame
    void Update()
    {
        //pauseMenu_gameManager.GetComponent<PauseMenuScript>().enabled = false;
        player.GetComponent<PlayerControllerScript>().enabled = false;
        player.GetComponent<PlayerShootingScript>().enabled = false;

        if (Input.GetKeyDown(KeyCode.E) && isDoneTalking)
        {
            dialogueText.text = "<u><#FFFFFF><size=150%>" + npcName + "</size></u></color><size=100%>" + "<br>" + "<br>" + "";
            NextSentence();
        }

        anim.SetBool("intro", intro);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(DisableUi());
        }
    }

    void NextSentence()
    {
        if (index <= sentences.Length - 1)
        {
            dialogueText.text = "<u><#FFFFFF><size=150%>" + npcName + "</size></u></color><size=100%>" + "<br>" + "<br>" + "";
            StartCoroutine(WriteSentence());
        }
        else
        {
            StartCoroutine(DisableUi());
            //Destroy(gameObject, .5f);
        }
    }

    IEnumerator WriteSentence()
    {
        foreach (char character in sentences[index].ToCharArray())
        {
            dialogueText.text += character;
            FindAnyObjectByType<AudioManager>().PlayForButton("typing");
            yield return new WaitForSeconds(dialogueSpeed);

            isDoneTalking = false;
            pressToContinue.enabled = false;
        }
        isDoneTalking = true;
        pressToContinue.enabled = true;
        index++;
    }

    /*
    public void SkipButton()
    {
        StartCoroutine(DisableUi());
    }
    */

    IEnumerator DisableUi()
    {
        FindAnyObjectByType<AudioManager>().PlayForButton("click_backward");

        dialogueText.text = "<u><#FFFFFF><size=150%>" + npcName + "</size></u></color><size=100%>" + "<br>" + "<br>" + "";
        index = 0;

        intro = false;

        FindAnyObjectByType<AudioManager>().Play("background");
        FindAnyObjectByType<AudioManager>().Stop("level complete");

        yield return new WaitForSeconds(exitAnimTime);

        introUi.SetActive(false);
        gameObject.GetComponent<IntroScript>().enabled = false;
        FindAnyObjectByType<AudioManager>().Stop("typing");
        player.GetComponent<PlayerControllerScript>().enabled = true;
        player.GetComponent<PlayerShootingScript>().enabled = true;

        //pauseMenu_gameManager.GetComponent<PauseMenuScript>().enabled = true;
    }
}
