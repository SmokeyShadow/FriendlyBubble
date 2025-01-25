using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuControl : MonoBehaviour
{
    #region SERIALIZED FIELDS
    [SerializeField]
    private GameObject howtoPlay;

    [SerializeField]
    private GameObject startPanel;

    [SerializeField]
    private Button startBtn;

    [SerializeField]
    private Button howtoBtn;

    [SerializeField]
    private Button quitBtn;

    [SerializeField]
    private Button backBtn;
    #endregion

    #region MONO BEHAVIOURS
    void Start()
    {
        startBtn.onClick.AddListener(StartGame);
        quitBtn.onClick.AddListener(QuitGame);
        howtoBtn.onClick.AddListener(HowToPlay);
        backBtn.onClick.AddListener(BackToStart);
    }
    #endregion

    #region PRIVATE METHODS
    void BackToStart()
    {
        howtoPlay.SetActive(false);
        startPanel.SetActive(true);
    }

    void HowToPlay()
    {
        howtoPlay.SetActive(true);
        startPanel.SetActive(false);
    }

    void StartGame()
    {
        SceneManager.LoadScene("main");
    }

    void QuitGame()
    {
        Application.Quit();
    }
    #endregion
}
