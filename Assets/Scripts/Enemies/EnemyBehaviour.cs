using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;




public abstract class EnemyBehaviour : MonoBehaviour
{
    protected EnemysChasing chasing;
    [SerializeField] protected WeapoRotateToPlayer rotator;
    [SerializeField] protected float pauseBetweenAttacks;
    [SerializeField] protected float attackRangeRadius;
    [SerializeField] protected float chasingRangeRadius;
    [SerializeField] protected LayerMask layerMask;
    protected EnemyHealth health;
    

    protected bool alreadyShoot = false;


    void Update()
    {
        if (health.alive)
        {
            CheckChasingRange();
            CheckTheAttackRange();
        }
        else
        {
            chasing.StopChasing();
            if (rotator!=null)
            {
                rotator.enabled = false; 
            }
        }
    }

    void CheckChasingRange()
    {
        var colliderToChaise = Physics2D.OverlapCircle(transform.position, chasingRangeRadius, layerMask);  
        if (colliderToChaise != null)
        {
           
            
            if (colliderToChaise.CompareTag("Player"))


            {
                bool b = chasing.chaseMode != true;


                if (chasing.chaseMode != true)
                {
                    chasing.StartChasing(colliderToChaise.gameObject);
                }
            }
            else
            {
                if (chasing.chaseMode == true)
                {
                    chasing.StopChasing();
                }
            }
        }
        else
        {
            if (chasing.chaseMode == true)
            {
                chasing.StopChasing();
            }
        }
    }

    void CheckTheAttackRange()
    {
        var collider = Physics2D.OverlapCircle(transform.position, attackRangeRadius,layerMask);

        if (collider != null )
        {
            if (collider.CompareTag("Player"))
            {
                //Debug.Log("Игрок в зоне Атаки");
                chasing.StopChasing();
                if (!alreadyShoot)
                {
                    if (rotator!=null) rotator.StartAiming();
                    //if (collider.transform.position.x < transform.position.x)
                    //{
                    //    transform.right = Vector3.right;
                    //}
                    //else transform.right = Vector3.left;

                    Attack(collider.gameObject);
                } 
            }
            else if (rotator != null) rotator?.StopAiming();
        }
        else
        {
            if (rotator != null) rotator?.StopAiming();
        }
    }

    /// <summary>
    /// Метод должен реализовывать атаку конкретного врага. Принимает коллайдер игрока для атаки
    /// </summary>
    /// <param name="collider"></param>
    protected abstract void Attack(GameObject collider);


    
}
