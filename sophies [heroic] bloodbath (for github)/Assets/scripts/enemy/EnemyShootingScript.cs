using Unity.VisualScripting;
using UnityEngine;

public class EnemyShootingScript : MonoBehaviour
{
    [Header("player")] 
    public GameObject player;

    [HideInInspector]
    public bool hasLineOfSight = false;

    [Header("weapon gameObjects")]
    public GameObject firePoint;
    //public LineRenderer lineRenderer;
    public GameObject bulletPrefab;
    public ParticleSystem muzzleflash;
    public Animator anim;

    [Header("stats")]
    public float bulletForce = 20f;

    private float nextTimeToFire = 0f;
    public float fireRate = 1f;

    private bool isShooting = false;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {
        anim = gameObject.GetComponentInChildren<Animator>();

        if (player != null)
        {
            Shooting();
        }
        else
            gameObject.GetComponent<EnemyShootingScript>().enabled = false;
    }

    public void Shooting()
    {
        isShooting = false;

        RaycastHit2D ray = Physics2D.Raycast(firePoint.transform.position, player.transform.position - firePoint.transform.position);

        if (ray.collider != null)
        {
            EnemyControllerScript enemyMove = gameObject.GetComponent<EnemyControllerScript>();

            hasLineOfSight = ray.collider.CompareTag(player.tag);

            if (hasLineOfSight && enemyMove.distance < enemyMove.detectionRadius)//if player is in line of sight, and within the detecttion radius
            {
                //gameObject.GetComponent<BoxCollider2D>().enabled = true;

                if (Time.time >= nextTimeToFire)
                {
                    nextTimeToFire = Time.time + 1f / fireRate;//adds a bit of delay before the enemy fires (by dividing 1 by the fire rate and adding that to the time.time, which is the current "game" time)

                    MuzzleFlash();
                    isShooting = true;

                    FindAnyObjectByType<AudioManager>().Play("enemy shoot");

                    GameObject bullet = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    rb.AddForce(firePoint.transform.up * bulletForce, ForceMode2D.Impulse);
                }

                /*
                lineRenderer.SetPosition(0, firePoint.transform.position);
                lineRenderer.SetPosition(1, ray.point);
                */

                Debug.DrawRay(firePoint.transform.position, player.transform.position - firePoint.transform.position, Color.green);
            }
            else
            {
                isShooting = false;

                //gameObject.GetComponent<BoxCollider2D>().enabled = false;
                Debug.DrawRay(firePoint.transform.position, player.transform.position - firePoint.transform.position, Color.red);
            }
        }
        anim.SetBool("isShooting", isShooting);
    }

    public void MuzzleFlash()
    {
        muzzleflash.Emit(30);
    }
}
