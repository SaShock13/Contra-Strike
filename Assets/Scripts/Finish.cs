using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null& collision.CompareTag("Player"))
        {
            gameManager.WinGame();
        }
    }

    
}
