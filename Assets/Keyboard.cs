using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections.Generic;


public class Keyboard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject childText ;

    private void Awake()
    {
        childText.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        childText.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        childText.SetActive(false);
    }

}
