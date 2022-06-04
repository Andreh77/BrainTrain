using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainGameMusic : MonoBehaviour
{
    private static BrainGameMusic brainGameMusic;

    private void Awake() 
    {
        DontDestroyOnLoad(this);

        if (brainGameMusic == null)
        {
            brainGameMusic = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
    }
}
