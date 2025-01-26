using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    private List<AudioClip> backAudioClips;
    int audioIndex = 0;
    static bool onDontDestroyed;
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
        SetAudioDictionary();

        if (SceneManager.GetActiveScene().name == "DemoDay")
        {
            if (!onDontDestroyed)
            {
                DontDestroyOnLoad(this.gameObject);
                onDontDestroyed = true;
            }
        }

    }
    private void Update()
    {
        if (!loopaudioSource.isPlaying)
        {
            if (audioIndex == backAudioClips.Count)
                audioIndex = 0;

            if (backAudioClips.Count != 0)
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
        if (!audioSource)
            audioSource = GameObject.FindGameObjectWithTag("soundEffect").GetComponent<AudioSource>();
        if (audio == SoundClip.Nothing)
            return;
        audioSource.PlayOneShot(clips[audio]);
    }

    public void MuteSound(bool enable)
    {
        audioSource.enabled = !enable;
    }

    public bool IsPlaying()
    {
        return audioSource.isPlaying;
    }

    public void VolumeDown()
    {
        loopaudioSource.volume = 0.2f;
    }

    public void VolumeUp()
    {
        loopaudioSource.volume = 1f;
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
