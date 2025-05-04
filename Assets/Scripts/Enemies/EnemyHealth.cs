using System.Collections;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour,IDamageable
{
    protected float health;
    [SerializeField] float maxHealth;
    public bool alive = true;
    protected Collider2D [] bodyColliders;
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
        bodyColliders = GetComponentsInChildren<Collider2D>();
    }

    void Update()
    {
        
    }

    public void TakeDamage (int damageAmount, bool damageFromLeft = true)
    {
        if (alive)
        {
            health -= (float)damageAmount;
            if (health <= 0 )
            {
                Death();
            }
            else if (healthBar!=null)
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

        foreach (var bodyCollider in bodyColliders)
        {
            bodyCollider.attachedRigidbody.bodyType = RigidbodyType2D.Static;
            //Debug.Log($" body {bodyCollider.name}");
            bodyCollider.enabled = false;
        }
        
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
