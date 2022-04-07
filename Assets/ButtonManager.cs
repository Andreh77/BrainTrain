using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log(gameObject.name);
        gameObject.GetComponent<Image>().color 
            = transform.root.GetComponent<MainMenuManager>().background;
    }

    public void OnPointerExit(PointerEventData eventData)
    {

        gameObject.GetComponent<Image>().color
            = transform.root.GetComponent<MainMenuManager>().header;
    }
}
