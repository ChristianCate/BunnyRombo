using UnityEngine;

public class SnakeHealth : MonoBehaviour
{

    public float maxHealth = 100f;
    private float currentHealth;
    public int reward = 10;

    [SerializeField] private AudioClip soundEffect;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
     
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        

        Debug.Log("Enemy health: " + currentHealth);

        if (currentHealth <= 0 )
        {

            
            Die();
            
        }
    }

    private void Die()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.EnemyDefeated(reward);
        }

        if (WaveManager.Instance != null)
        {
            WaveManager.Instance.EnemyRemoved();
        }

        PlaySFX();
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySFX()
    {
        if (soundEffect != null)
        {
            AudioSource.PlayClipAtPoint(soundEffect, Camera.main.transform.position, 1f);
        }
    }
}
