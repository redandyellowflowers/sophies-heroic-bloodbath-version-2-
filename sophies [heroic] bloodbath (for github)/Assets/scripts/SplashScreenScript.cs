using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SplashScreenScript : MonoBehaviour
{
    [Header("player")]
    public GameObject player;

    [Header("gameObjects")]
    public IntroScript intro;
    public GameObject beforeIntroUi;
    public GameObject pauseMenu_gameManager;

    /*
    [Header("values")]
    public float timeBeforeSplashAnimEnds = 1.75f;
    public float timeBeforeDialogueIntroIsEnabled = 1f;
    */

    [SerializeField] private Volume volume;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");

        intro = gameObject.GetComponent<IntroScript>();
        pauseMenu_gameManager = GameObject.Find("game manager");

        intro.introUi.SetActive(false);
        intro.enabled = false;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Intro());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator Intro()
    {
        //there is the intro dialogue that plays at the start of a level, and before that theres a title card for the level
        //this script ensures that the dialogue ui is enabled halfway through the animation for the title card
        //this makes the overall transition look less janky, but it took an unreasonable amount of time to implement :(

        if (volume.profile.TryGet(out LensDistortion distortion))
        {
            distortion.active = true;
        }

        pauseMenu_gameManager.GetComponent<PauseMenuScript>().enabled = false;

        player.GetComponent<PlayerControllerScript>().enabled = false;
        player.GetComponent<PlayerShootingScript>().enabled = false;

        beforeIntroUi.SetActive(true);
        intro.introUi.SetActive(false);
        intro.enabled = false;
        yield return new WaitForSeconds(1.75f);
        StartCoroutine(IntroIntersect());
    }

    IEnumerator IntroIntersect()
    {
        if (volume.profile.TryGet(out LensDistortion distortion))
        {
            distortion.active = false;
        }

        intro.enabled = true;
        intro.introUi.SetActive(true);
        yield return new WaitForSeconds(1f);

        beforeIntroUi.SetActive(false);
        pauseMenu_gameManager.GetComponent<PauseMenuScript>().enabled = true;
    }
}
