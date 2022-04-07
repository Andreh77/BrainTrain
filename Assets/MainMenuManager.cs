using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public Color header, background;

    public void Hover(BaseEventData data)
    {
        if(data.selectedObject != null)
        {
            GameObject canvas = data.selectedObject;
            canvas.GetComponent<Image>().color = background;
            Debug.Log("OVER BUTTON");
        }
    }
}
