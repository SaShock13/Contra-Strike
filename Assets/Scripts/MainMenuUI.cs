using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] GameObject aboutPanel;


   public void ExitBtnClick()
    {
        Application.Quit();
    }
    public void PlayBtnClick()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
    public void AboutBtnClick()
    {
        aboutPanel.SetActive(true);
    }

    public void BackToMenuBtnClick()
    {
        aboutPanel.SetActive(false);
    }
}
