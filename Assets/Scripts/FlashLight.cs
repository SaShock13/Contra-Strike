using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    private void Awake()
    {
        Destroy(gameObject,0.2f);
    }
}
