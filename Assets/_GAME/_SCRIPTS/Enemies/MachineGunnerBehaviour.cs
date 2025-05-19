using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.VFX;

public class MachineGunnerBehaviour : EnemyBehaviour
{
    [SerializeField] GameObject inLiveTargetFX;
    [SerializeField] GameObject inNotLiveTargetFX;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject player;
    [SerializeField] float bulletForce;
    [SerializeField] LayerMask playerLayerMask;
    
    Animator animator;
    SoundManager soundManager;
    
    [SerializeField] Transform bulletEmiterTransform;
    [SerializeField] Transform gunAimTransform;

    Collider2D playerCollider;

    [SerializeField] GameObject flashlight;

    Quaternion bulletRotation;
    Vector3 shotVector;
    [SerializeField] VisualEffect muzzleEffect;
    GameObject hitFX;


    private void Awake()
    {
        health = GetComponent<MachineGunnerHealth>();
        chasing = GetComponent<EnemysChasing>();
        soundManager = FindObjectOfType<SoundManager>();
    }

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    protected override void Attack(GameObject collider)
    {
        
        StartCoroutine(AttackCoroutine(collider.gameObject));
    }

    IEnumerator AttackCoroutine(GameObject objToAttack)
    {
        
        alreadyShoot = true;
        animator.SetTrigger("Attack");
        muzzleEffect.Play();
        soundManager?.shot.Play();
        shotVector = bulletEmiterTransform.position - gunAimTransform.position;

        bulletRotation = Quaternion.Euler(new Vector3(0, 0, Vector3.Angle(Vector3.right, shotVector)));
        Instantiate(flashlight, bulletEmiterTransform.position, Quaternion.identity);
        ShotWithRay();
        yield return new WaitForSeconds(pauseBetweenAttacks);
        alreadyShoot = false;
    }

    void ShotWithBullet()
    {
        var currentBullet = Instantiate(bullet, bulletEmiterTransform.position, bulletRotation);
        currentBullet.GetComponent<Rigidbody2D>().AddForce(shotVector * bulletForce);
    }

    void ShotWithRay()
    {
        RaycastHit2D hit = Physics2D.Raycast(bulletEmiterTransform.position, shotVector, 20f, playerLayerMask);
        if (hit != null & hit.collider != null)
        {
            if (hit.collider.TryGetComponent<IDamageable>(out IDamageable objToDamage))
            {
                if (!hit.collider.CompareTag("Enemy"))
                {                    
                    if (hit.collider.CompareTag("Player"))
                    {
                        hitFX = Instantiate(inLiveTargetFX, hit.point, Quaternion.identity);
                        objToDamage.TakeDamage(1);
                    }
                    else hitFX = Instantiate(inNotLiveTargetFX, hit.point, Quaternion.identity); 
                }
                Destroy(hitFX, 0.2f);
            }
        }
    }
}
