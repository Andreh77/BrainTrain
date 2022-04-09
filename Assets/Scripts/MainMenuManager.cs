using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public Color normal, header, background;
    public ButtonManager selectedButton;
    public AudioSource overButtonSound;
    public AudioSource clickButtonSound;

    public void Quit()
    {
        Application.Quit();
    }
}
