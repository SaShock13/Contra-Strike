using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{

    [SerializeField] TMP_Text statisticText;

    private void Start()
    {
        statisticText.text = "Совершено " + Statistic.killsCount + " убийств!!!" ;
    }

    public void ExitBtnClick()
    {
        Application.Quit();
    }
    public void ReplayBtnClick()
    {
        Statistic.killsCount = 0;
        SceneManager.LoadScene(1);
    }
    
    public void BackToMenuBtnClick()
    {
        SceneManager.LoadScene(0);
    }
}
