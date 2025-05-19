using EZCameraShake;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealthScript : MonoBehaviour
{
    [Header("stats")]
    public int currentHealth;
    public int maxHealth;
    public int lowHealthAlert = 10;

    [Header("UI")]
    public Slider healthBar;
    public TextMeshProUGUI healthText;

    [Header("gameObjects")]
    public GameObject bloodSplatter;
    public GameObject bloodExplosionEffect;
    public GameObject damageScreenUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthText.text = currentHealth + "%".ToString();

        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth < lowHealthAlert)
        {
            damageScreenUI.SetActive(true);
        }
        else
            damageScreenUI.SetActive(false);
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        healthBar.value = currentHealth;
        healthText.text = currentHealth + "%".ToString();
        //print("Enemy Damage " + currentHealth);

        GameObject groundBloodSplatter = Instantiate(bloodSplatter, gameObject.transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        Destroy(groundBloodSplatter, 3f);

        if (currentHealth <= 0)
        {
            FindAnyObjectByType<AudioManager>().Play("enemy death");

            CameraShaker.Instance.ShakeOnce(6f, 1f, .1f, .4f);

            GameObject BloodExplosion = Instantiate(bloodExplosionEffect, gameObject.transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
            Destroy(BloodExplosion, 1f);
            damageScreenUI.SetActive(false);

            Destroy(gameObject);
        }
    }

    public void AddHealth(int amount)
    {
        currentHealth += amount;
        healthBar.value = currentHealth;
        healthText.text = currentHealth + "%".ToString();

        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
