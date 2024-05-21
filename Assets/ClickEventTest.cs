using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickEventTest : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Клик Мышкой по " + name);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Мышка над " + name);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Отпустил Мышку с " + name);
    }
}
