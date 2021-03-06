using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    private static AudioManager instance;
    private bool isSFXPlaying;

    public bool IsSFXPlaying
    {
        get { return isSFXPlaying; }
        set { isSFXPlaying = value; }
    }

    // Start is called before the first frame update
    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start() 
    {
        // Play("Background"); 
        // Play("Theme");   
    }

    public void Play(string name)
    {
       Sound s = Array.Find(sounds, sound => sound.soundName == name);
       if (s == null)
       {
           Debug.LogWarning("Sound label " + name + " not found. Check spelling or capitalization.");
           return;
       }
       
       if (!s.flagged || s.flagged && !isSFXPlaying) s.source.Play();
       if (s.flagged && !isSFXPlaying) StartCoroutine(SFXCheck());
    }

    public void PlaySpecial(string name, float pitch)
    {
       Sound s = Array.Find(sounds, sound => sound.soundName == name);
       if (s == null)
       {
           Debug.LogWarning("Sound label " + name + " not found. Check spelling or capitalization.");
           return;
       }
       
       s.source.pitch = pitch;
       if (!s.flagged || s.flagged && !isSFXPlaying) s.source.Play();
       if (s.flagged && !isSFXPlaying) StartCoroutine(SFXCheck());
    }

    public void Pause(string name)
    {
       Sound s = Array.Find(sounds, sound => sound.soundName == name);
       if (s == null)
       {
           Debug.LogWarning("Sound label " + name + " could not be paused. File not found. Check spelling or capitalization.");
           return;
       }
       s.source.Pause();
    }

    IEnumerator SFXCheck()
    {
        isSFXPlaying = true;
        yield return new WaitForSeconds(5f);
        isSFXPlaying = false;
    }
}
