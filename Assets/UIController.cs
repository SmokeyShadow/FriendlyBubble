using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    private static UIController _instance;
    public static UIController Instance
    {
        get
        {
            if (!_instance)
                _instance = GameObject.FindObjectOfType<UIController>();
            return _instance;
        }
    }
    public enum DialogueType { Room1, Room2, Room3, Room4, Room5, Greeting, GlassLine, Heaven}

    [SerializeField]
    private List<string> Dialouges;
    [SerializeField]
    private TextMeshProUGUI dialougeText;

    public void ShowDialouge(DialogueType type, SoundPlayer.SoundClip dialogueAudio, float afterTime)
    {
        StartCoroutine(ShowDialougeRoutine(type, dialogueAudio, afterTime));
    }

    public void ShowDialougeWithOutFreeze(DialogueType type, SoundPlayer.SoundClip dialogueAudio, float afterTime)
    {
        StartCoroutine(ShowDialougeWithOutFreezeRoutine(type, dialogueAudio, afterTime));
    }

    IEnumerator ShowDialougeRoutine(DialogueType type, SoundPlayer.SoundClip dialogueAudio, float afterTime)
    {
        yield return new WaitForSecondsRealtime(afterTime);
        SoundPlayer.Instance.VolumeDown();
        SoundPlayer.Instance.PlaySound(dialogueAudio);
        Time.timeScale = 0;
        dialougeText.text = Dialouges[(int)type];
        dialougeText.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(6);
        SoundPlayer.Instance.VolumeUp();
        dialougeText.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    IEnumerator ShowDialougeWithOutFreezeRoutine(DialogueType type, SoundPlayer.SoundClip dialogueAudio, float afterTime)
    {
        yield return new WaitForSecondsRealtime(afterTime);
        SoundPlayer.Instance.VolumeDown();
        SoundPlayer.Instance.PlaySound(dialogueAudio);
        dialougeText.text = Dialouges[(int)type];
        dialougeText.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(6);
        SoundPlayer.Instance.VolumeUp();
        dialougeText.gameObject.SetActive(false);
    }

}
