using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour,IDamageable
{
    protected float health;
    [SerializeField] float maxHealth;
    public bool alive = true;
    protected Collider2D enemyCollider;
    [SerializeField] GameUI gameUI;
    [SerializeField] DeathCounter deathCounter;
    [SerializeField,RequiredMember] Image healthBar;

    
    private void Awake()
    {
        if (healthBar!=null)
        {
            healthBar.enabled = false; 
        }
        health = maxHealth;
        enemyCollider = GetComponent<Collider2D>();

    }

    void Update()
    {
        if (health <= 0 & alive)
        {
            Death();
        }
    }

    public void TakeDamage (int damageAmount, bool damageFromLeft = true)
    {
        if (alive)
        {
            health -= (float)damageAmount;
            
            if (healthBar!=null)
            {
                StartCoroutine(nameof(ShowHealthBar));
                healthBar.fillAmount = health / maxHealth; 
            }
        }
        AfterDamage(damageFromLeft);
    }
    public void  Death()
    {
        WhenDead();        
        health = 0;
        deathCounter.AddDeath();
        alive = false;
        var soundOfDeath = gameObject.GetComponent<AudioSource>();
        soundOfDeath?.Play();



        enemyCollider.attachedRigidbody.bodyType = RigidbodyType2D.Static;
        enemyCollider.enabled = false;
    }
    virtual public void WhenDead()
    {
       
    }

    virtual public void AfterDamage(bool damageFromLeft)
    {
       
    }

    IEnumerator ShowHealthBar()
    {
        healthBar.enabled = true;
        yield return new WaitForSeconds(0.5f);
        healthBar.enabled = false;
    }
}
