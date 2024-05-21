using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    int damage=1;
    SpriteRenderer spriteRenderer;


    private void Awake()
    {
        Destroy(gameObject,1);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Пуля попала в игрока");
            (collision.gameObject).GetComponent<PlayerHealth>().TakeDamage(damage);
            
        }
        spriteRenderer.enabled = false;
    }

   
}
