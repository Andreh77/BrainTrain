using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashCards : MonoBehaviour
{
    [SerializeField] private GameObject Hidden;
    [SerializeField] private GameController gameController;

    public void OnMouseDown()
    {
        if (Hidden.activeSelf && gameController.canOpen)
        {
            Hidden.SetActive(false);
            gameController.imageOpened(this);
        }
    }

    private int _spriteId;
    public int spriteId
    {
        get { return _spriteId; }
    }

    public void ChangeSprite(int id, Sprite image)
    {
        _spriteId = id;
        GetComponent<SpriteRenderer>().sprite = image;
    }

    public void Close()
    {
        Hidden.SetActive(true);
    }
}
