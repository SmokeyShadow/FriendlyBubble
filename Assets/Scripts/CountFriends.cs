using Supercyan.AnimalPeopleSample;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CountFriends : MonoBehaviour
{
    [SerializeField]
    private SimpleSampleCharacterControl player;
    [SerializeField]
    private TextMeshProUGUI numberCounter;
    Coroutine countNumbersRoutine = null;
    private List<string> numbersStrList = new List<string> { "q", "w", "e", "r", "t" };
    private float initialCameraSize = 11.38807f;
    private float cameraElevatorSize = 25f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            countNumbersRoutine = StartCoroutine(CountNumbersRoutine(player.FriendsCount));
            Camera.main.orthographicSize = cameraElevatorSize;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            Camera.main.orthographicSize = initialCameraSize;
            UIController.Instance.ClearDialogue();
            numberCounter.text = "";
            StopCoroutine(countNumbersRoutine);
            
        }
    }

    IEnumerator CountNumbersRoutine(int count)
    {
        for (int i = 0; i < count; i++)
        {
            yield return new WaitForSeconds(1f);
            numberCounter.text = numbersStrList[i];
        }
        if(count != 5)
        {
            UIController.Instance.ShowDialougeWithOutFreeze(UIController.DialogueType.Room4, SoundPlayer.SoundClip.Nothing, 0);
        }
        else
        {
            SceneManager.LoadScene("GlassLine");
        }
    }
}
