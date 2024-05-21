using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class TruckDamage : MonoBehaviour, IDamageable
{
    [SerializeField] Sprite wholeTruckSprite;
    [SerializeField] Sprite brokenTruckSprite;
    Collider2D[] colliders;
    VisualEffect explodeVFX;
    VisualEffect fireFX;
    VisualEffect[] visualEffects;
    SpriteRenderer sr;
    int health = 20;
    bool isBroken = false;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>(); 
        colliders = GetComponents<Collider2D>();
        visualEffects = GetComponentsInChildren<VisualEffect>();
        explodeVFX = visualEffects[0];
        //explodeVFX.Stop();
        fireFX = visualEffects[1];
        //fireFX.Stop();

    }


    public void TakeDamage(int damage, bool onLeft)
    {
        health -= damage;
        Debug.Log("Жизнь грузовика "+ health);
    }

    private void Update()
    {
        if(health <= 0 & !isBroken) 
        {
            isBroken = true;
            StartCoroutine(nameof(PlayFX));
        }
    }

    void Broke()
    {
        sr.sprite = brokenTruckSprite;
        foreach (var coll in colliders)
        {
            coll.enabled = false;
        }
    }

    IEnumerator PlayFX()
    {
        fireFX.enabled = true;
        explodeVFX.enabled = true;
        //fireFX.Play();
        //explodeVFX.Play();

        yield return new WaitForSeconds(1);
        Broke();
        fireFX.Stop();
    }
}
