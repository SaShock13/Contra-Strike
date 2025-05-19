using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animatorScript : MonoBehaviour
{
     [SerializeField] PlayerMovement playerMovement;
    
    public void JumpForce()
    {
        playerMovement.AddJumpForce();
    }
}
