using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeapoRotate : MonoBehaviour
{
    //??????????????������� ����� � ������

    [SerializeField] float offsetFromMouseAngle;
    [SerializeField] GameObject pers;
    [SerializeField] Transform pivot;
    [SerializeField] GameObject playerSprites;
    Vector3 directionToMouse;
    float toMouseAngle;
    Vector3 playerScale;
    float angleToRotateZ;

    void Update()
    {
        RotateToMouse();
    }

    void RotateToMouse()
    {
        directionToMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition) - pivot.position;
        toMouseAngle = Mathf.Atan2(directionToMouse.x, directionToMouse.y) * Mathf.Rad2Deg;
        playerScale = Vector3.one;
        SetAngleConstrains();        
        angleToRotateZ = (toMouseAngle <0) ? 180 + (toMouseAngle * -1 + offsetFromMouseAngle) + 5 : (toMouseAngle * -1 + offsetFromMouseAngle)-5;            
        transform.rotation = Quaternion.Euler(0, 0, angleToRotateZ  );
        if (toMouseAngle>0)
        {
            playerScale.y = 1;            
        }
        else playerScale.y = -1;        
        playerSprites.transform.localScale = new Vector3(playerScale.y, 1, 1);
    }

    void SetAngleConstrains()
    {
        if (toMouseAngle < 30 & toMouseAngle >= 0)
        {
            //Debug.Log("������ 0 ");
            toMouseAngle = 30;
        }
        else if (toMouseAngle > 150 & toMouseAngle <= 180)
        {
            //Debug.Log("������ 180 ");
            toMouseAngle = 150;
        }
        else if (toMouseAngle > -30 & toMouseAngle < 0)
        {
            //Debug.Log("������ 180 ");
            toMouseAngle = -30;
        }
        else if (toMouseAngle < -150 & toMouseAngle > -180)
        {
            //Debug.Log("������ 180 ");
            toMouseAngle = -150;
        }
    }

    public void ResetRotate()
    {
        transform.rotation = Quaternion.identity;
    }
}
