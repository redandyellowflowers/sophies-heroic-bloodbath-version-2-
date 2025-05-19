using EZCameraShake;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class WinConditionScript : MonoBehaviour
{
    [Header("text")]
    [TextArea(2, 3)]public string completionPrompt = "'leave!'";//to be displayed once all enemies have been killed

    public TextMeshProUGUI enemyCount;
    //public TextMeshProUGUI keyCount;

    [Header("gameObjects")]
    public Image flashScreen;

    public GameObject endTrigger;

    GameObject obstruction;

    [Header("lists")]
    public GameObject[] enemies;
    //public GameObject[] keys;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyCount.text = enemies.Length.ToString();
        //endTrigger.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        obstruction = GameObject.FindGameObjectWithTag("Obstruction");

        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        int numberOfEnemies = enemies.Length;
        enemyCount.text = "<size=100%>enemies: " + enemies.Length.ToString();

        /*
        keys = GameObject.FindGameObjectsWithTag("Respawn");
        int maxNumberOfKeys = keys.Length;
        keyCount.text = "<size=45%><#ff0000>digital data: <size=75%>" + keys.Length.ToString();

        if (maxNumberOfKeys == 0)
        {
            keyCount.text = "<size=40%>all the <#ff0000>digital data</color> has been collected";
        }

        if (numberOfEnemies == 0)
        {
            enemyCount.text = "<size=100%>everyone's <#ff0000>dead</color>";
        }
        */

        if (numberOfEnemies <= 0)// && maxNumberOfKeys <= 0)
        {
            CameraShaker.Instance.ShakeOnce(6f, 1f, .1f, .4f);

            StartCoroutine(Flash());
            enemyCount.text = completionPrompt.ToString();

            FindAnyObjectByType<AudioManager>().Stop("background");
            FindAnyObjectByType<AudioManager>().Play("enemies are dead");

            if (endTrigger != null)
            {
                endTrigger.SetActive(true);
            }

            if (obstruction != null)
            {
                Destroy(obstruction);
            }
        }
    }

    IEnumerator Flash()
    {
        if (flashScreen != null)
        {
            FindAnyObjectByType<AudioManager>().Play("enemy death");
            flashScreen.enabled = true;

            yield return new WaitForSeconds(0.02f);

            flashScreen.enabled = false;
            gameObject.GetComponent<WinConditionScript>().enabled = false;
        }
    }
}
