using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.VFX;

public class Grenade : MonoBehaviour,IDamageable
{
    VisualEffect explosionEffect;
    AudioSource explosionSound;
    PointEffector2D explodePhysics;
    SpriteRenderer sprite;
    Rigidbody2D rb;
    Light2D flash;
    Collider2D[] collidersInRange;
    EnemyHealth enemyHealth;
    PlayerHealth playerHealth;

    float lifetime;
    [SerializeField] float detonateTime;
    [SerializeField] float flashTime;
    [SerializeField] float damageRange;
    [SerializeField] int grenadeDamage;

    private void Awake()
    {
        collidersInRange = new Collider2D[10];
        rb = GetComponent<Rigidbody2D>();
        flash  =  GetComponentInChildren<Light2D>();
        explodePhysics = GetComponentInChildren<PointEffector2D>();
        explosionSound = GetComponent<AudioSource>();
        explosionEffect = GetComponentInChildren<VisualEffect>();
        explosionEffect.Stop();
        sprite = GetComponentInChildren<SpriteRenderer>();
        StartCoroutine(nameof(DetonatePause));
        lifetime = detonateTime + 1;
    }

    public IEnumerator DetonatePause()
    {
        yield return new WaitForSeconds(detonateTime);
        PlayVisualEffect();
        Detonate();
        yield return new WaitForSeconds(lifetime - detonateTime);
        DestroyGrenade();
    }

    public IEnumerator DetonateNow()
    {
        rb.isKinematic = true;
        PlayVisualEffect();
        Detonate();
        yield return new WaitForSeconds(1.2f);
        DestroyGrenade();
    }

    void Detonate()
    {        
        rb.isKinematic = true;
        explodePhysics.enabled = true;
        explosionSound.Play();
        StartCoroutine(nameof(FlashCoroutine));
        MakeDamageInRange();
        sprite.enabled = false;        
    }

    void DestroyGrenade()
    {
        Destroy(gameObject);
    }

    void PlayVisualEffect()
    {
        explosionEffect.Play();
    }

    IEnumerator FlashCoroutine()
    {
        flash.enabled = true;
        yield return new WaitForSeconds(flashTime);
        flash.enabled = false;
    }

    void MakeDamageInRange()
    {
        collidersInRange =  Physics2D.OverlapCircleAll(transform.position,damageRange);
        foreach (Collider2D collider in collidersInRange) 
        {
            if (collider.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                if (damageable != this)
                {
                    damageable.TakeDamage(grenadeDamage); 
                }
            }
        }
    }

    public void TakeDamage(int damage, bool onLeft = true)
    {
        StartCoroutine(nameof(DetonateNow));
    }
}
