using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public void ExitBtnClick()
    {
        Application.Quit();
    }
    public void ReplayBtnClick()
    {
        SceneManager.LoadScene(1);
    }
    
    public void BackToMenuBtnClick()
    {
        SceneManager.LoadScene(0);
    }
}
