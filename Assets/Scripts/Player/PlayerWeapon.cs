using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class PlayerWeapon : MonoBehaviour
{
    SoundManager soundManager;

    [SerializeField] Texture2D aimCursor;
    [SerializeField] GameObject inLiveTargetFX;
    [SerializeField] GameObject inNotLiveTargetFX;
    [SerializeField] GameUI gameUI;
    [SerializeField] Image singleModeImage;
    [SerializeField] Image shortModeImage;
    [SerializeField] Image longModeImage;

    [Header("Grenade Preferences")]
    [SerializeField] Transform grenadeEmiterTransform;
    [SerializeField] GameObject grenade;
    [SerializeField] float grenadeThrowForce = 5;
    [SerializeField] float grenadeToDetonationPause = 3;
    [SerializeField] float grenadeThrowHeight=2;
    [SerializeField] float grenadeThrowPause = 1.5f;
    bool canThrowGrenade = true;
    [SerializeField] Vector3 grenadeThrowVectorOffset;


    [Header("AK47 Preferences")]
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletEmiterTransform;
    [SerializeField] Transform emmiterAimerTransform;
    Vector3 shotVector;
    [SerializeField] float bulletToDestroyPause;
    [SerializeField] float bulletForce;
    [SerializeField] float shootPause;
    [SerializeField] int shortBurstCount;
    [SerializeField] int gunClipMax = 100;
    [SerializeField] int bulletCounter = 1000;
    [SerializeField] float weaponReloadTime = 3;
    [SerializeField] float weaponCoolingTime = 5;
    [SerializeField] float gunHeatIncreaseCoef;
    [SerializeField] float gunCoolingCoef;
    [SerializeField] AudioSource reloadSound;
    [SerializeField] AudioSource falseShotSound;
    [SerializeField] AudioSource shotSound;
    [SerializeField] ShootMode shortMode;
    [SerializeField] float maxGunTemperature;
    [SerializeField] VisualEffect muzzleFlashEffect;
    [SerializeField] GameObject flashlight;
    GameObject hitEffect;
    [SerializeField] GameObject shotSoundSource;

    public delegate void OnAmmoCountChanged(int ammoCountNow);
    public event OnAmmoCountChanged onAmmoCountChangedEvent;

    public delegate void OnTemperatureChanged(float tempNow);
    public event OnTemperatureChanged onTemperatureChangedEvent;
    public delegate void OnOverHeat();
    public event OnOverHeat onOverHeatEvent;
    public delegate void OnCooled();
    public event OnCooled onCooledEvent;



    [Header("Other")]
    [SerializeField] GameObject persSprite;
    bool canFire = true;
    bool needToReload = false;
    bool isGunOverHeated = false;
    float gunTemperature;
    [NonSerialized] public int burstCounter = 0;

    private void Start()
    {
        Cursor.SetCursor(aimCursor,Vector2.zero,CursorMode.Auto);
        onAmmoCountChangedEvent?.Invoke(bulletCounter);
        onTemperatureChangedEvent?.Invoke(gunTemperature);
        soundManager = FindObjectOfType<SoundManager>();
    }

    private void Update()
    {
        ChangeShootModeHandler();
    }

    private void ChangeShootModeHandler()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            shortMode = ShootMode.Single;
            singleModeImage.enabled = true;
            shortModeImage.enabled = false;
            longModeImage.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            shortMode = ShootMode.Short;
            singleModeImage.enabled = false;
            shortModeImage.enabled = true;
            longModeImage.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            shortMode = ShootMode.Long;
            singleModeImage.enabled = false;
            shortModeImage.enabled = false;
            longModeImage.enabled = true;
        }
    }

    private void FixedUpdate()
    {
        GunTemperatureTracker();
    }

    void GunTemperatureTracker()
    {     
        if ((gunTemperature - (Time.fixedDeltaTime * gunCoolingCoef)) < 40 )
        {
            gunTemperature = 40;
        }
        else
        {
            gunTemperature -= Time.fixedDeltaTime * gunCoolingCoef;
            onTemperatureChangedEvent?.Invoke(gunTemperature);
        }
    }

    void BulletCounter()
    {
        if (bulletCounter <= 0)
        {

        }
    }

    enum ShootMode
    {
        Single,
        Short,
        Long
    }

    public void GunAttack()
    {
        switch (shortMode)
        {
            case ShootMode.Single:
                shortBurstCount = 1;
                break;
            case ShootMode.Short:
                shortBurstCount = 5;
                break;
            case ShootMode.Long:
                shortBurstCount = 300;
                break;
            default:
                break;
        }
        StartCoroutine(nameof(ShootTheBullet));
    }

    IEnumerator ShootTheBullet()
    {
        if (canFire & burstCounter < shortBurstCount & !isGunOverHeated)
        {
            if (!needToReload)
            {
                if (bulletCounter > 0)
                {
                    canFire = false;
                    var shotSound = Instantiate(shotSoundSource,transform.position,Quaternion.identity);
                    Destroy(shotSound,0.1f);
                    muzzleFlashEffect.Play();
                    Instantiate(flashlight, bulletEmiterTransform.position, Quaternion.identity);
                    ShootWithRay();
                    burstCounter++;
                    DecreaseBulletCount();
                    gunTemperature += Time.fixedDeltaTime * gunHeatIncreaseCoef;
                    onTemperatureChangedEvent?.Invoke(gunTemperature);
                    if (gunTemperature >= maxGunTemperature)
                    {
                        isGunOverHeated = true;
                        onOverHeatEvent?.Invoke();
                        StartCoroutine(nameof(CooolingGun));
                    }
                    if (bulletCounter % gunClipMax == 0 & bulletCounter > 0)
                    {
                        gameUI.StartShowInfo("Для перезарядки нажмите R ...");
                        needToReload = true;
                    }
                    yield return new WaitForSeconds(shootPause);
                    canFire = true;
                }
                else
                {
                    soundManager.falseShot.Play();
                }
            }
            else soundManager.falseShot.Play();
        }
    }

    void DecreaseBulletCount()
    {
        bulletCounter--;
        onAmmoCountChangedEvent?.Invoke(bulletCounter);
    }

   

    void ShootWithRay()
    {
        shotVector = bulletEmiterTransform.position - emmiterAimerTransform.position;
        RaycastHit2D hit = Physics2D.Raycast(bulletEmiterTransform.position, shotVector, 20f);
        Debug.DrawRay(bulletEmiterTransform.position, shotVector, Color.blue);
        if (hit!= null& hit.collider!=null)
        {
            if(hit.collider.CompareTag("Head"))
            {
                var damageabe = hit.collider.GetComponentInParent<IDamageable>();
                if(damageabe!=null)
                {
                    damageabe.TakeDamage(10);
                    //Debug.Log("HeadShot");
                }
            }
            if (hit.collider.TryGetComponent<IDamageable>(out IDamageable objToDamage))
            {
                objToDamage.TakeDamage(1);
                if (hit.collider.CompareTag("Enemy"))
                {
                    hitEffect = Instantiate(inLiveTargetFX, hit.point, Quaternion.identity);
                }
                else
                {
                    hitEffect =Instantiate(inNotLiveTargetFX, hit.point, Quaternion.identity);
                }
                Destroy(hitEffect,0.3f);
            }
            else 
            {
                //эффект попадания в платформу
                hitEffect = Instantiate(inNotLiveTargetFX, hit.point, Quaternion.identity);
                Destroy(hitEffect, 0.3f);
            }
        }
    }
    IEnumerator CooolingGun()
    {
        yield return new WaitForSeconds(weaponCoolingTime);
        isGunOverHeated = false;
        onCooledEvent?.Invoke();
    }

    public void Reload()
    {
        if (bulletCounter > 0)
        {
            StartCoroutine(nameof(ReloadGun));
        }
        else soundManager.falseShot.Play();
    }

    IEnumerator ReloadGun()
    {
        soundManager.reload.Play();
        yield return new WaitForSeconds(weaponReloadTime);
        gameUI.StopShowInfo();
        needToReload = false;
    }

    public void Grenade()
    {
        if (canThrowGrenade)
        {
            StartCoroutine(nameof(ThrowGrenade)); 
        }
    }

    IEnumerator ThrowGrenade()
    {        
        canThrowGrenade = false;
        var currentGrenade = Instantiate(grenade, grenadeEmiterTransform.position, Quaternion.identity);
        currentGrenade.GetComponentInChildren<VisualEffect>().Stop();
        Rigidbody2D grenadeRB = currentGrenade.GetComponent<Rigidbody2D>();
        shotVector = bulletEmiterTransform.position - emmiterAimerTransform.position;
        shotVector += grenadeThrowVectorOffset;
        grenadeRB.AddForce(shotVector * grenadeThrowForce);
        grenadeRB.AddTorque(-3);
        yield return new WaitForSeconds(grenadeThrowPause);
        canThrowGrenade = true;
    }
}
