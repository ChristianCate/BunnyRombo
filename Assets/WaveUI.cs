using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveUI : MonoBehaviour
{
    public WaveManager waveManager;
    public GameManager gameManager;

    public TMP_Text waveText;
    public TMP_Text currencyText;
    public TMP_Text healthText;
    public TMP_Text damageText;
    public TMP_Text rateText;
    public Button startNextWaveButton;

    private void Start()
    {
        startNextWaveButton.onClick.AddListener(waveManager.StartNextWave);
    }

    private void Update()
    {
        if (waveManager == null || gameManager == null || startNextWaveButton == null)
        {
            return;
        }

        waveText.text = "Wave: " + waveManager.currentWave;
        currencyText.text = "$" + gameManager.money;
        healthText.text = "Health: " + gameManager.baseLives;
        damageText.text = "Damage: " + gameManager.globalBunnyDamage;
        rateText.text = "Fire Rate: " + gameManager.globalBunnyFireRate;

        startNextWaveButton.gameObject.SetActive(gameManager.GameStarted && waveManager.waitingForNextWave);
    }
}