using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(AudioSource))]
public class CharacterSoundManager : MonoBehaviour {

    public AudioClip jumpSound;

    private AudioSource source;
    public List<Sound> sounds;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start() {
        source = GetComponent<AudioSource>();
    }

    public void PlayJumpSound(float volume = 1f) {
        source.PlayOneShot(jumpSound);
    }

    public void PlaySound(string soundname)
    {
        AudioClip clip = FindSound(soundname);
        if (clip == null) 
        {
            Debug.LogWarning("The requested sound " + soundname + " is not found.");
            return; 
        }
        source.PlayOneShot(clip);
    }

    public void PlaySound(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }

    AudioClip FindSound(string soundname)
    {
        for (int i = 0; i < sounds.Count; i++)
        {
            if (sounds[i].soundName == soundname)
            {
                return sounds[i].audioClip;
            }
        }
        return null;
    }

    [Serializable]
    public class Sound
    {
        public string soundName = "Empty Audio Clip";
        public AudioClip audioClip = null;
    }
}