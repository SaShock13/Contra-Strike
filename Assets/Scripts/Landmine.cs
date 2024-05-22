using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.VFX;

public class Landmine : MonoBehaviour
{
    [SerializeField, Range(0.2f,2)] float detonateTime;
    [SerializeField] int landmineDamage;
    [SerializeField] float damageRange;

    float flashTime = 0.2f;

    VisualEffect explosionEffect;
    AudioSource explosionSound;
    PointEffector2D explodePhysics;
    SpriteRenderer sprite;
    Light2D flash;
    Collider2D[] collidersInRange;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(nameof(Detonate)); 
        }

    }

    private void Awake()
    {

        explosionSound = GetComponent<AudioSource>();
        explosionEffect = GetComponentInChildren<VisualEffect>();
        explosionEffect.Stop();
        explodePhysics = GetComponentInChildren<PointEffector2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        flash = GetComponentInChildren<Light2D>();
    }



    IEnumerator Detonate()
    {
        yield return new WaitForSeconds(detonateTime-1);
        explosionEffect.Play();
        yield return new WaitForSeconds(1);
        StartCoroutine(nameof(FlashCoroutine));
        MakeDamageInRange();
        explodePhysics.enabled = true;
        explosionSound?.Play();
        sprite.enabled = false;
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
    void MakeDamageInRange()
    {
        collidersInRange = Physics2D.OverlapCircleAll(transform.position, damageRange);
        foreach (Collider2D collider in collidersInRange)
        {
            if (collider.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                if (damageable != this)
                {
                    damageable.TakeDamage(landmineDamage);
                }
            }
        }
    }
    IEnumerator FlashCoroutine()
    {
        flash.enabled = true;
        yield return new WaitForSeconds(flashTime);
        flash.enabled = false;
    }
}
