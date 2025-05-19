using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfantryHealth : EnemyHealth
{
    Animator animator;
    
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        
    }

    public override void AfterDamage(bool damageFromLeft)
    {
        //Debug.Log("Жизни пехотинца :" + health);
    }
    public override void WhenDead()
    {
        animator.SetTrigger("Death");
    }

}
