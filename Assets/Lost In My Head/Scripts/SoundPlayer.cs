using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Written by : Smoky Shadow
 * This script is a Manager for sounds
*/
public class SoundPlayer : MonoBehaviour
{
    #region STATIC FIELDS
    private static SoundPlayer instance;
    #endregion

    #region FIELDS
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioSource loopaudioSource;
    [SerializeField]
    private AudioClip[] audioClips;
    Dictionary<SoundClip, AudioClip> clips = new Dictionary<SoundClip, AudioClip>();

    [SerializeField]
    private AudioClip[] backAudioClips;
    int audioIndex = 0;
    #endregion

    #region PROPERTIES
    public static SoundPlayer Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<SoundPlayer>();
            return instance;
        }
    }
    #endregion

    #region ENUMS
    public enum SoundClip { Room1 = 0, Room2, Room3, Room5, Greeting, GlassLine, Heaven, Nothing };
    #endregion

    #region MONO BEHAVIOURS
    private void Start()
    {
        instance = this;
        SetAudioDictionary();
    }

    private void Update()
    {
        
        if (!loopaudioSource.isPlaying)
        {
            if (audioIndex == backAudioClips.Length)
                audioIndex = 0;
            if (loopaudioSource.clip)
            {
                loopaudioSource.clip = backAudioClips[audioIndex++];
                loopaudioSource.Play();
            }
        }

    }
    #endregion

    #region PUBLIC METHODS
    public void PlaySound(SoundClip audio)
    {
        if (audio == SoundClip.Nothing)
            return;
        audioSource.PlayOneShot(clips[audio]);
    }

    public void MuteSound(bool enable)
    {
        audioSource.enabled = !enable;
    }
    #endregion

    #region PRIVATE METHODS
    void SetAudioDictionary()
    {
        for (int i = 0; i < audioClips.Length; i++)
        {
            clips.Add((SoundClip)i, audioClips[i]);
        }
    }
    #endregion
}
