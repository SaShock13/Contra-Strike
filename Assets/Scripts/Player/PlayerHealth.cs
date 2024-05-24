using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerHealth : MonoBehaviour,IDamageable,IHealable
{
    [SerializeField] int maxHealth;
    [SerializeField] GameUI gameUI;
    [SerializeField] GameManager gameManager;
    [SerializeField] AudioSource soundOfDeath;

    int health;
    PlayerInput playerInput;
    WeapoRotate weapoRotate;
    Animator animator;
    bool alive = true;

    public delegate void OnHealthChanged(int maxHealth,int healthNow);
    public event OnHealthChanged onHealthChangedEvent;

    private void Awake()
    {
        health = maxHealth;
        playerInput = GetComponent<PlayerInput>();
        weapoRotate = GetComponentInChildren<WeapoRotate>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        RefreshUI();        
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
            Debug.Log("Player damaged with ... ");
            health -= damage;
            RefreshUI();
        }
    }

    void Death()
    {       
        Debug.Log("����� �����");
        animator.SetTrigger("Death");
        alive = false;
        health = 0;
        RefreshUI();
        playerInput.enabled = false;
        weapoRotate.ResetRotate();
        weapoRotate.enabled  = false;
        soundOfDeath?.Play();
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
        health = health < maxHealth ? health : maxHealth;
        RefreshUI();
    }
}
