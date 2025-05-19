using System.Collections;
using TMPro;
using UnityEngine;
using EZCameraShake;

public class PlayerShootingScript : MonoBehaviour
{
    [Header("gameObjects")]
    public GameObject firePoint;

    [Header("stats")]
    public int currentAmmo;
    public int maxAmmo;
    public int damage = 5;
    public float range = 15f;

    private float nextTimeToFire = 0f;
    public float fireRate = 1f;

    public bool Reloadable;

    [Header("UI")]
    public TextMeshProUGUI ammoCountText1;
    public TextMeshProUGUI ammoCountText2;

    [Header("effects")]
    public LineRenderer lineRenderer;
    public GameObject greyImpactEffect;
    public GameObject redImpactEffect;
    public ParticleSystem muzzleflash;

    [Header("animation/s")]
    public Animator anim;

    private bool hasLineOfSight = false;
    private bool isShooting = false;


    private void Awake()
    {
        anim = gameObject.GetComponentInChildren<Animator>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ammoCountText1.text = currentAmmo.ToString();
        ammoCountText2.text = maxAmmo.ToString();
        currentAmmo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
        Reload();

        if (currentAmmo > 0)
        {
            lineRenderer.gameObject.SetActive(true);
        }
    }

    public void Shoot()
    {
        isShooting = false;

        if (Input.GetKey(KeyCode.Mouse0))
        {
            Time.timeScale = .5f;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;

            if (Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;

                StartCoroutine(targetRays());

                isShooting = true;
                CameraShaker.Instance.ShakeOnce(3f, 1f, .1f, .4f);
                MuzzleFlash();

                currentAmmo -= 1;

                FindAnyObjectByType<AudioManager>().Play("shoot");

                //print("Ammo count " + currentAmmo);
                ammoCountText1.text = currentAmmo.ToString();
                ammoCountText2.text = maxAmmo.ToString();

                if (currentAmmo <= 0)
                {
                    isShooting = false;

                    currentAmmo = 0;

                    if (Reloadable)
                    {
                        ammoCountText1.text = "--".ToString();
                    }
                    else
                    {
                        ammoCountText1.text = "--".ToString();
                    }

                    FindAnyObjectByType<AudioManager>().Stop("shoot");
                    FindAnyObjectByType<AudioManager>().Play("empty");

                    //print("out of ammo");

                    lineRenderer.gameObject.SetActive(false);

                    Time.timeScale = 1f;
                }

            }

        }
        else
        {
            Time.timeScale = 1f;
        }

        anim.SetBool("isShooting", isShooting);
    }

    public IEnumerator targetRays()//as i understand it, IEnumerators allow for timers, or other such time specific functions, and is convenient for that
    {
        RaycastHit2D ray = Physics2D.Raycast(firePoint.transform.position, transform.up, range);
        if (ray.collider != null)
        {
            hasLineOfSight = ray.collider.CompareTag("Enemy");
            if (hasLineOfSight)
            {
                Debug.DrawRay(firePoint.transform.position, transform.up * range, Color.green);

                TargetHealthScript target = ray.transform.GetComponent<TargetHealthScript>();

                if (target != null && currentAmmo != 0)//meaning that this will only happen when the object being fired at has a target script
                {
                    target.takeDamage(damage);
                }

                lineRenderer.SetPosition(0, firePoint.transform.position);
                lineRenderer.SetPosition(1, ray.point);
                //lineRenderer.material.SetColor("_Color", Color.red);

                if (currentAmmo != 0)
                {
                    GameObject redImpactGameobject = Instantiate(redImpactEffect, ray.point, Quaternion.LookRotation(ray.normal));
                    Destroy(redImpactGameobject, 2f);
                }
            }
            else
            {
                Debug.DrawRay(firePoint.transform.position, transform.up * range, Color.red);

                lineRenderer.SetPosition(0, firePoint.transform.position);
                lineRenderer.SetPosition(1, ray.point);
                //lineRenderer.material.SetColor("_Color", Color.white);

                if (currentAmmo != 0)
                {
                    GameObject greyImpactGameobject = Instantiate(greyImpactEffect, ray.point, Quaternion.LookRotation(ray.normal));
                    Destroy(greyImpactGameobject, 2f);
                }
            }

            lineRenderer.enabled = true;

            yield return new WaitForSeconds(0.02f);

            lineRenderer.enabled = false;

            /*
            if (ray.collider.CompareTag("Non player character"))
            {
                print("I will not do that.");
            }
            */
        }
    }

    public void MuzzleFlash()
    {
        muzzleflash.Emit(30);
    }

    public void Reload()
    {
        if (Reloadable == true && Input.GetKeyDown(KeyCode.R))
        {
            isShooting = false;

            if (currentAmmo < maxAmmo)
            {
                FindAnyObjectByType<AudioManager>().Play("reload");
            }

            currentAmmo = maxAmmo;
            ammoCountText1.text = currentAmmo.ToString();
            ammoCountText2.text = maxAmmo.ToString();

            lineRenderer.gameObject.SetActive(true);
        }
    }

    public void AddAmmo(int amount)
    {
        currentAmmo += amount;
        ammoCountText1.text = currentAmmo.ToString();
        ammoCountText2.text = maxAmmo.ToString();

        if (currentAmmo >= maxAmmo)
        {
            currentAmmo = maxAmmo;
        }

        lineRenderer.gameObject.SetActive(true);
    }
}
