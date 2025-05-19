using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float walkSpeed = 3;
    [SerializeField] float runSpeed =6;
    [SerializeField] float jumpForce;
    [SerializeField] GameObject persSprite;
    [SerializeField] GameObject playerSprites;
    public bool isOnTheFloor;
    private float playerMoveSpeed;
    private float rotationY;
    private Rigidbody2D rb;
    private Vector3 playerSCale;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        playerSCale = Vector3.one;
        playerMoveSpeed = walkSpeed;
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnTheFloor = true;
        }
    }

    public void Move(float horizontal)
    {
        Vector2 resultVector = new Vector2(horizontal * playerMoveSpeed, rb.linearVelocity.y) ;
        rb.linearVelocity = resultVector;
        float animatorX = persSprite.transform.localScale.x == -1 ? resultVector.x * -1 : resultVector.x;        
        if (isOnTheFloor)
        {
            animator.SetFloat("X", animatorX);
        }
        else animator.SetFloat("X", 0);
    } 

    public void Jump()
    {
        if (isOnTheFloor)
        {
            isOnTheFloor = false;
            animator.SetTrigger("Jump"); 
        }
    }

    public void AddJumpForce()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
    public void RunMode()
    {
        playerMoveSpeed = runSpeed;
    }

    public void WalkMode()
    {
        playerMoveSpeed = walkSpeed;
    }
}
