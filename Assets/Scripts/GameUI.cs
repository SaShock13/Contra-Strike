using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    [SerializeField] TMP_Text ammoCounter;
    [SerializeField] TMP_Text killsCounter;
    [SerializeField] TMP_Text healthCounter;
    [SerializeField] TMP_Text temperatureCounter;
    [SerializeField] Image HPImage;
    [SerializeField] TMP_Text infoText;
    [SerializeField] GameObject pausePanel;
    [SerializeField] PlayerWeapon playerWeapon;
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] DeathCounter deathCounter;

    //[NonSerialized] public int ammoCount;
    [NonSerialized] public int killsCount = 0;
    [NonSerialized] public int maxHealthCount ;
    [NonSerialized] public int healthCount;
    [NonSerialized] public float gunTemperature ;
    bool IsShowInfo = false;

    

    private void Update()
    {
    }

    private void Start()
    {
        playerWeapon.onAmmoCountChangedEvent += UpdateAmmoCount;
        playerWeapon.onTemperatureChangedEvent += UpdTemperature;
        playerHealth.onHealthChangedEvent += UpdHealth;
        deathCounter.onDeathCountChangedEvent += UpdDeaths;
        //healthCount = maxHealthCount;
        UpdateCounters();
    }

    public void UpdateCounters()
    {
        //ammoCounter.text = ammoCount.ToString();
        //killsCounter.text = killsCount.ToString();
        //healthCounter.text = healthCount.ToString();
        //temperatureCounter.text = gunTemperature.ToString("#");
        
    }

    public void PauseBtnClick()
    {
        gameManager.PauseGame();
        pausePanel.SetActive(true);
    }

    public void ResumeBtnClick()
    {
        gameManager.ResumeGame();
        pausePanel.SetActive(false);
    }

    public void StartShowInfo(string info)
    {
        IsShowInfo = true;
        infoText.text = info;
        StartCoroutine(BlinkText(infoText));
    }

    public void StopShowInfo()
    {
        IsShowInfo = false;
    }

    IEnumerator BlinkText(TMP_Text text)
    {
        while (IsShowInfo)
        {
            text.enabled = true;
            yield return new WaitForSeconds(0.5f);
            text.enabled = false;
            yield return new WaitForSeconds(0.5f); 
        }
    }

    public void BackToMenuBtnClick()
    {
        SceneManager.LoadScene(0);
    }


    void UpdateAmmoCount(int ammo)
    {
        ammoCounter.text = ammo.ToString();
    }

    void UpdTemperature(float temperature)
    {
        temperatureCounter.text = temperature.ToString("#");
    }
    void UpdHealth(int maxHealth,int health)
    {
        healthCounter.text = health.ToString();
        HPImage.fillAmount = (float)health / (float)maxHealth;
    }
    void UpdDeaths(int deaths)
    {
        killsCounter.text = deaths.ToString();
    }
}