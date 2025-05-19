using EZCameraShake;
using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DialogueScript : MonoBehaviour
{
    /*
    Title: Animated Dialogue System - Unity Tutorial
    Author: Zyger
    Date: 27 April 2025
    Code version: 1
    Availability: https://www.youtube.com/watch?v=hvgfFNorZH8
    */

    public bool isBrokenSophie;

    [Header("player")]
    public GameObject player;
    public CameraFollowScript cam;
    PlayerControllerScript MovementScript;
    PlayerShootingScript playerShoot;

    [Header("values")]
    public float new_y_Offset = -5f;
    public float new_x_Offset = 0f;

    [Header("UI")]
    public GameObject textBox;
    public GameObject dialogueUI;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI pressToContinue;
    public GameObject interactText;

    [Header("dialogue")]
    public float dialogueSpeed;
    public string npcName;
    [TextArea(2, 4)]public string[] sentences;
    private int index = 0;//tracking the sentences

    [Header("effects")]
    public GameObject bloodSplatter;
    public GameObject bloodExplosionEffect;

    public Image flashScreen;

    [Header("animation/s")]
    public Animator anim;
    bool dialogueHasEnded;

    private bool isDoneTalking = true;

    [Header("event after dialogue")]
    public GameObject nextNPC;
    public GameObject blockade;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollowScript>();
        MovementScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControllerScript>();

        playerShoot = player.gameObject.GetComponent<PlayerShootingScript>();
        interactText = gameObject.GetComponentInChildren<TextMeshPro>().gameObject;

        anim = dialogueUI.GetComponent<Animator>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        interactText.GetComponent<TextMeshPro>().enabled = false;

        if (blockade != null)
        {
            blockade.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        interactText.transform.rotation = Quaternion.Euler(0f, 0f, gameObject.transform.rotation.z * -1f);

        if (player == null)
        {
            gameObject.GetComponent<DialogueScript>().enabled = false;
        }

        if (gameObject.GetComponent<NpcScript>().distance < gameObject.GetComponent<NpcScript>().detectionRadius / 2 && gameObject.GetComponent<NpcScript>().hasLineOfSight)
        {
            interactText.GetComponent<TextMeshPro>().enabled = true;

            if (Input.GetKeyDown(KeyCode.E) && isDoneTalking)
            {
                //FindAnyObjectByType<AudioManager>().PlayForButton("click_forward");

                dialogueHasEnded = false;

                playerShoot.enabled = false;

                MovementScript.moveSpeed = 0f;
                cam.yOffset = new_y_Offset;
                cam.xOffset = new_x_Offset;

                textBox.SetActive(true);
                dialogueUI.SetActive(true);
                dialogueText.text = "<u><#FFFFFF><size=150%>" + npcName + "</size></u></color><size=100%>" + "<br>" + "<br>" + "";

                dialogueText.enabled = true;
                NextSentence();

                anim.SetBool("dialogue_ended", dialogueHasEnded);
            }
        }
        else if (gameObject.GetComponent<NpcScript>().distance > gameObject.GetComponent<NpcScript>().detectionRadius / 2)
        {
            interactText.GetComponent<TextMeshPro>().enabled = false;
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
            dialogueHasEnded = true;

            playerShoot.enabled = true;

            dialogueText.text = "<u><#FFFFFF><size=150%>" + npcName + "</size></u></color><size=100%>" + "<br>" + "<br>" + "";
            index = 0;

            MovementScript.moveSpeed = 16f;
            cam.yOffset = 0f;
            cam.xOffset = 0f;

            StartCoroutine(Flash());

            dialogueUI.SetActive(false);
            textBox.SetActive(false);

            FindAnyObjectByType<AudioManager>().Play("click_backward");

            if (isBrokenSophie)
            {
                StartCoroutine(Flash());

                GameObject impactGameobject = Instantiate(bloodSplatter, gameObject.transform.position, gameObject.transform.rotation);

                GameObject BloodExplosion = Instantiate(bloodExplosionEffect, gameObject.transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
                Destroy(BloodExplosion, 1f);

                dialogueUI.SetActive(false);
                textBox.SetActive(false);

                FindAnyObjectByType<AudioManager>().Play("enemy death");//REFERENCING AUDIO MANAGER
            }

            if (nextNPC != null)
            {
                nextNPC.SetActive(true);
            }
            else
            {
                print("nothing");
            }

            if (blockade != null)
            {
                Destroy(blockade);
            }
            else
            {
                print("nothing");
            }
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

    IEnumerator Flash()
    {
        if (flashScreen != null)
        {
            flashScreen.enabled = true;

            if (isBrokenSophie)
            {
                CameraShaker.Instance.ShakeOnce(5f, 1f, .1f, .4f);
            }

            yield return new WaitForSeconds(0.02f);

            flashScreen.enabled = false;

            if (isBrokenSophie)
            {
                Destroy(gameObject);
            }

        }
    }


    IEnumerator Done()
    {
        yield return new WaitForSeconds(.6f);
        dialogueUI.SetActive(false);
        textBox.SetActive(false);
    }
}
