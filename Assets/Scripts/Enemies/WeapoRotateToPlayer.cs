using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeapoRotateToPlayer : MonoBehaviour
{



    public float offsetFromPlayerAngle = -90;
    public float lessAngle1 = -270;
    public float moreAngle2 = 0;
    [SerializeField] GameObject player;
    [SerializeField] GameObject enemySprites;
    Vector3 directionToPlayer;
    int CheckPerSecond = 10;
    int timerMax;
    int timerCurrent;
    bool isAiming =false;

    private void Awake()
    {
        timerMax = 60 / CheckPerSecond;
        timerCurrent = timerMax;
    }
    private void FixedUpdate()
    {
        if (isAiming)
        {
            timerCurrent--;
            if (timerCurrent <= 0)
            {
                RotateToPlayer();
                timerCurrent = timerMax;
            } 
        }
    }

   
    void RotateToPlayer()
    {
        Vector3 directionToPlayer = player.transform.position - transform.position;
        float rotateAngleZ = Mathf.Atan2(directionToPlayer.x, directionToPlayer.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, -1 *rotateAngleZ + offsetFromPlayerAngle);
        Vector3 gunScale = Vector3.one;

        if (rotateAngleZ < lessAngle1 | rotateAngleZ > moreAngle2)
        {
            gunScale.y = 1;
        }
        else gunScale.y = -1;

        //transform.localScale = gunScale;
        enemySprites.transform.localScale = new Vector3(gunScale.y,1,1);

    }

    public void StartAiming()
    {
        isAiming = true;
    }

    public void StopAiming()
    {
        isAiming = false;
    }


}
