using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TextOpacityFader : MonoBehaviour
{
    public float min = 0.5f;
    public float speed = 5;
    TMP_Text text;
    public bool up;

    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        if (text.alpha < min)
        {
            up = true;
        }
        else if(text.alpha > 0.99f)
        {
            up = false;
        }

        if(up)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, Mathf.Lerp(text.color.a, 1, speed * Time.deltaTime));
        }
        else
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, Mathf.Lerp(text.color.a, 0, speed * Time.deltaTime));
        }
    }
}