using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    bool isDamaged = false;
    [SerializeField] int damage = 5;
    void OnTriggerStay2D(Collider2D collider)
    {   
        StartCoroutine(MakeDamage(collider));
    }

    void OnTriggerEnter2D(Collider2D collider)
    {   
        StartCoroutine(MakeDamage(collider));
    }

    IEnumerator MakeDamage(Collider2D collider)
    {
         if(collider.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            Debug.Log("collided player");
            if(!isDamaged)
            {
                Debug.Log("damage player");
                isDamaged =true;       
                yield return new WaitForSeconds(0.5f);
                damageable.TakeDamage(damage);
                isDamaged = false;
            }
        }
        

    }

}
