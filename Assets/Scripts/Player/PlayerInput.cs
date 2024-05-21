using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerWeapon))]
public class PlayerInput : MonoBehaviour
{
    PlayerMovement playerMovement;
    PlayerWeapon weapon;
    float horizontal;
    float gunTimer;
    bool canShoot = false;
    bool move = false;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        weapon = GetComponent<PlayerWeapon>();
    }

    private void Update()
    {
        KeyListener();
    }

    private void FixedUpdate()
    {
        if(move) playerMovement.Move(horizontal);

    }
    void KeyListener()
    {
        horizontal = Input.GetAxis("Horizontal");

        if (horizontal!=0)
        {
            move= true;
        }
        else move = false;

        if (Input.GetButtonDown("Jump"))
        {
            playerMovement.Jump();
        }

        if (Input.GetButton("Fire1"))
        {
            weapon.GunAttack(); 
        }
        if (Input.GetButtonUp("Fire1"))
        {
            weapon.burstCounter = 0;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            weapon.Reload();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            weapon.Grenade();
        }

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            playerMovement.RunMode();
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            playerMovement.WalkMode();
        }


        //if (Input.GetButtonDown("Fire1"))
        //{
        //    playerMovement.SwordAttack();
        //}

        //if (Input.GetButtonDown("Fire2"))
        //{
        //    playerMovement.GunAttack();
        //}

        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    playerMovement.Death();
        //}

        //if (Input.GetKeyDown(KeyCode.LeftShift) && playerMovement.isOnTheFloor)
        //{
        //    playerMovement.ChangeRunSpeed(2f);
        //}
        //if (Input.GetKeyUp(KeyCode.LeftShift) && playerMovement.playerRunSpeed > 3)
        //{
        //    playerMovement.ChangeRunSpeed(0.5f);
        //}


    }
}
