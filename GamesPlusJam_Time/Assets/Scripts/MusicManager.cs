using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    public static MusicManager instance;

    public AudioClip jumpClip;
    public AudioClip pwrUpClip;
    public AudioClip hurtClip;

    public AudioSource backSnd;
    public AudioSource effects;

    private void Awake()
    {
        if (MusicManager.instance == null)
        {
            MusicManager.instance = this;
        }
        else if (MusicManager.instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void PlayHurt()
    {
        PlayAudio(hurtClip);
    }

    public void PlayJump()
    {
        PlayAudio(jumpClip);
    }

    public void PlayPwrUp()
    {
        PlayAudio(pwrUpClip);
    }

    void PlayAudio(AudioClip cs)
    {
        effects.clip = cs;
        effects.Play();
    }

    private void OnDestroy()
    {
        if (MusicManager.instance == this)
        {
            MusicManager.instance = null;
        }
    }
}
