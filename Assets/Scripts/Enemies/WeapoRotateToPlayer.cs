using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WeapoRotateToPlayer : MonoBehaviour
{
    public float offsetFromPlayerAngle = -90;
    public float lessAngle1 = -270;
    public float moreAngle2 = 0;
    [SerializeField] GameObject player;
    [SerializeField] GameObject enemySprites;
    [SerializeField] GameObject gunObj;
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
        //Debug.Log($"rotating ");
        Vector3 directionToPlayer = player.transform.position - transform.position;
        float rotateAngleZ = Mathf.Atan2(directionToPlayer.x, directionToPlayer.y) * Mathf.Rad2Deg;
        //gunObj.transform.LookAt(player.transform.position);
        Vector3 gunScale = Vector3.one;
        if (rotateAngleZ < lessAngle1 | rotateAngleZ > moreAngle2)
        {
            gunScale.y = 1;
            gunObj.transform.rotation = Quaternion.Euler(0, 0, -1 * rotateAngleZ + offsetFromPlayerAngle);
        }
        else

        {
            gunScale.y = -1;
            gunObj.transform.rotation = Quaternion.Euler(0, 0, -1 * rotateAngleZ -offsetFromPlayerAngle);
        }

        //gunObj.transform.rotation = Quaternion.Euler(0, 0, -1 *rotateAngleZ + offsetFromPlayerAngle);

        enemySprites.transform.localScale = new Vector3(gunScale.y, 1, 1);
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
