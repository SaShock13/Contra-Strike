using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] int bulletDamage=1;
    bool damageFromLeft;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        Destroy(gameObject,1);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IDamageable>(out IDamageable objToDamage))
        {
            if (transform.position.x < collision.transform.position.x)
            {
                damageFromLeft = true;
            }
            else damageFromLeft = false;
            objToDamage.TakeDamage(bulletDamage,damageFromLeft);
        }

        //if (collision.CompareTag("Enemy"))
        //{
        //    Debug.Log("Сработал триггер на врага");
        //    if (transform.position.x < collision.transform.position.x)
        //    { 
        //        damageFromLeft = true; 
        //    }
        //    else damageFromLeft = false;
        //    (collision.gameObject).GetComponent<EnemyHealth>().TakeDamage(bulletDamage, damageFromLeft);
        //}
        spriteRenderer.enabled = false;
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log("Коллизион ");
    //}
}
