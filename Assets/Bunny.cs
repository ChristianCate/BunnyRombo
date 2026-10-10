using UnityEngine;

public class Bunny : MonoBehaviour
{

    public float range = 3f;
    public float damage = 50f;
    public float fireRate = 1f;

    public GameObject projectilePrefab;
    public Transform firePoint;

    [SerializeField] private AudioClip soundEffect;
    
    

    private float fireTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fireTimer -= Time.deltaTime;
        float currentDamage = damage;
        float currentFireRate = fireRate;

        if (GameManager.Instance != null)
        {
            currentDamage = GameManager.Instance.globalBunnyDamage;
            currentFireRate = GameManager.Instance.globalBunnyFireRate;
        }

        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, range);

        foreach (Collider2D enemy in enemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                if (fireTimer <= 0)
                {
                    Shoot(enemy, currentDamage);
                    PlaySFX();
                    fireTimer = currentFireRate;
                }

                break;
            }
        }

    }

    private void Shoot(Collider2D enemy, float damageAmount)
    {
        Transform launchPoint = firePoint != null
            ? firePoint
            : transform;

        
        GameObject projectile = Instantiate(projectilePrefab, launchPoint.position, projectilePrefab.transform.rotation);

        Carrot carrotScript = projectile.GetComponent<Carrot>();

        if (carrotScript != null)
        {
            carrotScript.SetTarget(enemy.transform, damageAmount);
        }
    }

    public void PlaySFX()
    {
        if (soundEffect != null)
        {
            AudioSource.PlayClipAtPoint(soundEffect, Camera.main.transform.position, 1f);
        }
    }
}