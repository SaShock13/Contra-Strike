using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickEventTest : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("���� ������ �� " + name);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("����� ��� " + name);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("�������� ����� � " + name);
    }
}
