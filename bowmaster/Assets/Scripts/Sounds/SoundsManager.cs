using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// gameobject is not destroy between scenes
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class SoundsManager : MonoBehaviour
{
    
    private static SoundsManager instance = null;
    /// <summary>
    /// singleton
    /// </summary>
    public static SoundsManager Instance
    {
        get { return instance; }
    }

    private AudioSource audio;
    private float maxVolume = 1;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        audio = GetComponent<AudioSource>();
        maxVolume = AudioListener.volume;
        DontDestroyOnLoad(this.gameObject);
    }

    public void setVolume(int turnOn)
    {
        if(turnOn == 1)
            AudioListener.volume = maxVolume;
        else
            AudioListener.volume = 0;
    }

    /// <summary>
    /// play sound
    /// </summary>
    /// <param name="sound">clip which will be played</param>
    public void Sound(AudioClip sound)
    {
        audio.PlayOneShot(sound, 0.7F);
    }

    /// <summary>
    /// method to play sound from any object
    /// </summary>
    /// <param name="sound">clip which will be played</param>
    static public void PlaySound(AudioClip sound)
    {
        if (SoundsManager.Instance)
            SoundsManager.Instance.Sound(sound);
        else
            Debug.LogWarning("SoundsManager:PlaySound() Couldn't get SoundsManager Instance.");
    }
}