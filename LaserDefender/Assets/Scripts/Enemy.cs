using UnityEngine;

public class Enemy : MonoBehaviour
{

    [Header("Enemy Stats")]
    [SerializeField] int scoreValue = 150;
    [SerializeField] float health = 100;
    [Header("Enemy Shooting")]
    float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject LaserPrefab;
    [SerializeField] GameObject ExplosionPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [Header("Enemy SFX")]
    [SerializeField] AudioClip LaserSound;
    [SerializeField] AudioClip DeathSound;
    [SerializeField] [Range (0,1)] float lasersoundvolume= 0.2f;
    [SerializeField] [Range(0, 1)] float deathsoundvolume = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
     }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }

    }

    private void Fire()
    {
        GameObject laser = Instantiate(LaserPrefab, transform.position, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        AudioSource.PlayClipAtPoint(LaserSound, Camera.main.transform.position, lasersoundvolume);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);

    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<GameSession>().AddToScore(scoreValue);
        CreateParticle();
        AudioSource.PlayClipAtPoint(DeathSound, Camera.main.transform.position, deathsoundvolume);
        Destroy(gameObject);
    }

    private void CreateParticle()
    {
        GameObject explosion = Instantiate(ExplosionPrefab, transform.position, Quaternion.identity) as GameObject;
              
    }



}
