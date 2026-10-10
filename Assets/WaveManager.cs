using UnityEngine;
using UnityEngine.InputSystem;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance { get; private set; }

    [Header("References")]
    public EnemySpawner enemySpawner;

    [Header("Wave Information")]
    public int currentWave = 1;
    public int enemiesAlive;

    [Header("Enemy Scaling")]
    public int baseEnemies = 5;
    public int extraEnemiesPerWave = 3;

    public bool waitingForNextWave { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        // Begin in the build phase instead of starting immediately.
        waitingForNextWave = true;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.EndWave();
        }

        Debug.Log("Ready to start Wave 1.");
    }

    private void Update()
    {
        if (waitingForNextWave && Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            StartNextWave();
        }
    }

    public void StartWave()
    {
        waitingForNextWave = false;

        enemiesAlive = baseEnemies + (currentWave - 1) * extraEnemiesPerWave;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.StartWave();
        }

        enemySpawner.SpawnWave(enemiesAlive);
    }

    public void EnemyRemoved()
    {
        enemiesAlive--;

        if (enemiesAlive <= 0)
        {
            WaveComplete();
        }
    }

    private void WaveComplete()
    {

        int completedWave = currentWave;
        currentWave++;

        enemiesAlive = 0;
        waitingForNextWave = true;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.EndWave();
        }
    }

    public void StartNextWave()
    {
        if (GameManager.Instance == null || !GameManager.Instance.GameStarted)
        {
            return;
        }

        if (!waitingForNextWave)
        {
            return;
        }

        StartWave();
    }
}