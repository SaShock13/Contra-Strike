using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunnerHealth : EnemyHealth
{

    [SerializeField] float deathJumpForce;
    [SerializeField] float deathTorqueForce;
    Animator animator;

    

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        
    }

    public override void AfterDamage(bool damageFromLeft)
    {
        //Debug.Log("Жизни пулеметчика :"+ health );
        //if( damageFromLeft ) rb.AddForce(Vector2.right * 15);
        //else rb.AddForce(Vector2.left * 15);
    }

    public override void WhenDead()
    {
        animator.SetBool("Dead",true);
        Debug.Log("MG Death");
        
        //rb.AddForce(Vector2.up * deathJumpForce,ForceMode2D.Impulse);
        //rb.freezeRotation = false;
        //rb.AddTorque(-deathTorqueForce);

        //Debug.Log("Пулеметчик Убит");    
    }
}
