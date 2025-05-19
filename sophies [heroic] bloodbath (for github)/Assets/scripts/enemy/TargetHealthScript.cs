using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TargetHealthScript : MonoBehaviour
{
    [Header("gameObjects")]
    public GameObject bloodSplatter;
    public GameObject bloodExplosionEffect;

    [Header("stats")]
    public int health = 10;

    [Header("visual feedback")]
    public Light2D lightOBJ;

    public TextMeshPro text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (text != null)
        {
            text.text = health.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        text.transform.rotation = Quaternion.Euler(0f, 0f, gameObject.transform.rotation.z * -1f);
    }

    public void takeDamage(int amount)
    {
        health -= amount;

        StartCoroutine(LightFX());

        if (text != null)
        {
            text.text = health.ToString();
        }

        //print("Enemy Damage " + health);
        if (health <= 0)
        {
            GameObject groundBloodSplatter = Instantiate(bloodSplatter, gameObject.transform.position, gameObject.transform.rotation);

            GameObject BloodExplosion = Instantiate(bloodExplosionEffect, gameObject.transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
            Destroy(BloodExplosion, 1f);

            FindAnyObjectByType<AudioManager>().Play("enemy death");

            //Destroy(impactGameobject, .5f);
            Destroy(gameObject);
        }
        else
        {
            //GameObject groundBloodSplatter = Instantiate(bloodSplatter, gameObject.transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
            //Destroy(groundBloodSplatter, 1.5f);
        }
    }

    public IEnumerator LightFX()//when the enemy is hit, a red light is emmitted to indictate it
    {
        lightOBJ.color = Color.red;

        yield return new WaitForSeconds(.1f);

        lightOBJ.color = Color.white;
    }
}
