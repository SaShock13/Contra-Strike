using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Vector3 offset;
    [SerializeField] float lerpSpeed;

    void Update()
    {
        transform.position = Vector3.Lerp( transform.position, player.transform.position + offset, Time.deltaTime * lerpSpeed);
    }
}
