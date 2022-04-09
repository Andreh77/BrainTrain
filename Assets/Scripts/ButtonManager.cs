using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public bool select;
    public List<Image> buttonImages = new List<Image>();
    public MainMenuManager mainMenuManager;
    public GameObject panel;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        mainMenuManager.clickButtonSound.Play();
        if (mainMenuManager.selectedButton != null) { mainMenuManager.selectedButton.ChangeImageColour(mainMenuManager.normal); mainMenuManager.selectedButton.panel.SetActive(false); }
        mainMenuManager.selectedButton = this;

        ChangeImageColour(mainMenuManager.background);
        panel.SetActive(true);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mainMenuManager.overButtonSound.Play();
        ChangeImageColour(mainMenuManager.background);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(mainMenuManager.selectedButton == this) { return; }
        ChangeImageColour(mainMenuManager.normal);
    }

    public void ChangeImageColour(Color color)
    {
        foreach (Image i in buttonImages)
        {
            i.color = color;
        }
    }
}
