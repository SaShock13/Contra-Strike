using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerHealth : MonoBehaviour,IDamageable,IHealable
{
    int health;
    [SerializeField] int maxHealth;
    [SerializeField] GameUI gameUI;
    [SerializeField] GameManager gameManager;
    PlayerInput playerInput;
    WeapoRotate weapoRotate;
    Animator animator;

    [SerializeField] AudioSource soundOfDeath;
    bool alive = true;
    Collider2D collider;
    Rigidbody2D rb;
    int ddd;

    public delegate void OnHealthChanged(int maxHealth,int healthNow);
    public event OnHealthChanged onHealthChangedEvent;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        weapoRotate = GetComponentInChildren<WeapoRotate>();
        animator = GetComponentInChildren<Animator>();
        //gameUI.maxHealthCount = maxHealth;
        health = maxHealth;
        onHealthChangedEvent?.Invoke(maxHealth,health);        
        //gameUI.healthCount = health;
        collider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();

    }

    private void Start()
    {
    }

    private void Update()
    {
        if ((health <= 0 & alive)||Input.GetKeyDown(KeyCode.Q))
        {
            
            Death();
        }
    }
    public void TakeDamage(int damage, bool onLeft = true)
    {
        if (alive)
        {
            health -= damage;
            RefreshUI();
        }
    }

    void Death()
    {       
            Debug.Log("Игрок погиб");
        animator.SetTrigger("Death");
        alive = false;
        health = 0;
        RefreshUI();
        playerInput.enabled = false;
        weapoRotate.ResetRotate();
        weapoRotate.enabled  = false;
        soundOfDeath?.Play();
        //rb.isKinematic = true;
        //enemyCollider.attachedRigidbody.velocity = Vector2.zero;
        //collider.enabled = false;
        //rb.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        //rb.freezeRotation = false;
        //rb.AddTorque(-3);
        Debug.Log("Игрок Убит");
        StartCoroutine(nameof(GameOver));
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(3);
        gameManager.GameOver();
    }

    void RefreshUI()
    {
        onHealthChangedEvent?.Invoke(maxHealth, health);
    }

    public void Heal(int healAmount)
    {
        health += healAmount;
        RefreshUI();
    }
}
