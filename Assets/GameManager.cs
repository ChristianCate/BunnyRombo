using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game Settings")]
    public int baseLives = 20;
    public int money = 100;

    [Header("Build Phase")]
    public GridLines gridLines;

    [Header("Global Bunny Upgrades")]
    public float globalBunnyDamage = 50f;
    public float globalBunnyFireRate = 1f;

    [SerializeField] private AudioClip soundEffect;

    public int damageUpgradeCost = 100;
    public int fireRateUpgradeCost = 100;

    public float damageIncrease = 25f;
    public float fireRateDecrease = 0.1f;

    public bool IsBuildPhase { get; private set; } = true;
    public bool GameStarted { get; private set; } = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void BeginGame()
    {
        GameStarted = true;
        Time.timeScale = 1f;

    }

    public void StartWave()
    {
        IsBuildPhase = false;

        if (gridLines != null)
        {
            gridLines.SetGridVisible(false);
        }

    }

    public void EndWave()
    {
        IsBuildPhase = true;

        if (GameStarted && gridLines != null)
        {
            gridLines.SetGridVisible(true);
        }
    }

    public void EnemyReachedBase()
    {
        baseLives--;

        if (baseLives <= 0)
        {
            Time.timeScale = 0f;
            StartCoroutine(PlaySoundAndGameOver());
        }
    }

    private IEnumerator PlaySoundAndGameOver()
    {
        PlaySFX();

        float waitTime = (soundEffect != null) ? soundEffect.length : 1f;
        yield return new WaitForSecondsRealtime(waitTime);

        GameOver();
    }

    public void EnemyDefeated(int reward)
    {
        money += reward;

    }

    public bool SpendMoney(int amount)
    {
        if (money < amount)
        {
            return false;
        }

        money -= amount;
        return true;
    }

    public bool UpgradeBunnyDamage()
    {
        if (!GameStarted || !IsBuildPhase)
        {
            return false;
        }

        if (!SpendMoney(damageUpgradeCost))
        {
            return false;
        }

        globalBunnyDamage += damageIncrease;

        return true;
    }

    public bool UpgradeBunnyFireRate()
    {
        if (!GameStarted || !IsBuildPhase)
        {
            return false;
        }

        if (!SpendMoney(fireRateUpgradeCost))
        {
            return false;
        }

        globalBunnyFireRate = Mathf.Max(0.1f, globalBunnyFireRate - fireRateDecrease);

        return true;
    }

    public void GameOver()
    {

        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PlaySFX()
    {
        if (soundEffect != null)
        {
            AudioSource source = GetComponent<AudioSource>();
            if (source == null)
            {
                source = gameObject.AddComponent<AudioSource>();
            }
            source.ignoreListenerPause = true;

            source.PlayOneShot(soundEffect);
        }
    }
}