using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string soundName;
    public AudioClip clip;
    
    [Range(0f, 1f)] public float volume; 
    [Range(0.3f, 2f)]public float pitch;
    [HideInInspector] public AudioSource source;
    public bool loop, flagged;
}
