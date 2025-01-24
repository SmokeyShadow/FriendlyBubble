using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DayNightChange : MonoBehaviour
{
    Scene scene;
    string sceneName;
    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();
        sceneName = scene.name;
        if(sceneName == "DemoDay")
        {
            UIController.Instance.ShowDialougeWithOutFreeze(UIController.DialogueType.Heaven, SoundPlayer.SoundClip.Heaven, 3.5f);
        }
        StartCoroutine(ChangeDayRoutine());
    }

   IEnumerator ChangeDayRoutine()
    {
        yield return new WaitForSeconds(10);
        if (sceneName == "DemoDay")
            SceneManager.LoadScene("DemoNight");
        else if (sceneName == "DemoNight")
            SceneManager.LoadScene("DemoDay");
    }
}
