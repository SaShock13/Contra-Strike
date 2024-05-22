using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
    
{
    [SerializeField] PlayerInput playerInput;
    public void PauseGame()
    {
        Time.timeScale = 0;
        playerInput.enabled = false;
    }

    public void ReplayBtnClick()
    {
        Statistic.killsCount = 0;
        SceneManager.LoadScene(1);
    }

    public void ResumeGame()
    {
        playerInput.enabled = true;
        Time.timeScale = 1;
    }

    public void WinGame()
    {
        SceneManager.LoadScene(2);
    }
    public void GameOver()
    {
        SceneManager.LoadScene(3);
    }
}
