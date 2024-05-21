using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class InfantryBeh : EnemyBehaviour
{    
    Animator animator;
    
    private void Awake()
    {
        health = GetComponent<InfantryHealth>();
        animator = GetComponentInChildren<Animator>();
        chasing = GetComponent<EnemysChasing>();
        rotator = GetComponent<WeapoRotateToPlayer>();
    }

    protected override void Attack(GameObject collider)
    {
        StartCoroutine(AttackCoroutine(collider.gameObject));
    }

    IEnumerator AttackCoroutine(GameObject objToAttack)
    {
        alreadyShoot = true;
        animator.SetTrigger("Attack");
        objToAttack.GetComponent<PlayerHealth>().TakeDamage(3);
        yield return new WaitForSeconds(pauseBetweenAttacks);
        alreadyShoot = false;
    }
}
