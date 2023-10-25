using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour, IPointerDownHandler
{

    public Material Material1;
    public Material Material2;
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Button Clicked");
        gameObject.GetComponent<Renderer>().material = gameObject.GetComponent<Renderer>().material.name == Material1.name ? Material2 : Material1;
    }
}