using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WalkOnGlass : MonoBehaviour
{
    private static WalkOnGlass _instance;

    [SerializeField]
    private HeavenPlayer player;
    [SerializeField]
    private List<GameObject> friends;
    [SerializeField]
    private Transform friendTransform;
    [SerializeField]
    public List<GameObject> rows;
    public List<int> availGlasses = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7 };

    public List<int> breakableGlasses = new List<int>();
    public List<int> allRows = new List<int>();

    float jumpTimer;
    int currentRow;
    int friendIndex;
    Vector3 firstPosition;
    public static WalkOnGlass Instance
    {
        get
        {
            if (!_instance)
                _instance = GameObject.FindObjectOfType<WalkOnGlass>();
            return _instance;
        }
    }

    void Start()
    {
        firstPosition = player.gameObject.transform.position;
        player.gameObject.GetComponent<Animator>().SetBool("Grounded", true);
        player.enabled = false;

        //assign breakble glasses (count = 5) between 8 rows
        GenerateGlassWay();
        UIController.Instance.ShowDialouge(UIController.DialogueType.GlassLine, SoundPlayer.SoundClip.GlassLine, 8);
        for (int i = 0; i < rows.Count; i++)
        {
            int random = Random.Range(0, 2);

            //it's not breakable, both will set breakables to false
            if (!breakableGlasses.Contains(i))
            {
                rows[i].transform.GetChild(0).GetComponent<BreakableWindow>().Unbreakable();
                rows[i].transform.GetChild(1).GetComponent<BreakableWindow>().Unbreakable();
            }
            else
            {
                //left is 0 and right is 1
                if (random == 0)
                {
                    rows[i].transform.GetChild(0).GetComponent<BreakableWindow>().enabled = true;
                    rows[i].transform.GetChild(1).GetComponent<BreakableWindow>().Unbreakable();
                }
                else
                {
                    rows[i].transform.GetChild(0).GetComponent<BreakableWindow>().Unbreakable();
                    rows[i].transform.GetChild(1).GetComponent<BreakableWindow>().enabled = true;
                }
            }
            allRows.Add(random);
        }
        StartCoroutine(GenerateFriend(8));
    }

    private void Update()
    {
        if (player.gameObject.transform.position.y <= -3.5f)
            player.gameObject.transform.position = firstPosition;
    }

    void GenerateGlassWay()
    {
        List<int> tempAvailGlass = new List<int>();


        for (int i = 0; i < availGlasses.Count; i++)
        {
            tempAvailGlass.Add(availGlasses[i]);
        }

        for (int i = 0; i < 5; i++)
        {
            int rand = Random.Range(0, tempAvailGlass.Count - 1);
            breakableGlasses.Add(tempAvailGlass[rand]);
            tempAvailGlass.RemoveAt(rand);
        }
    }

    public void ChangeRows(int row)
    {
        if (allRows[row] == 0)
            allRows[row] = 1;
        else if (allRows[row] == 1)
            allRows[row] = 0;
        breakableGlasses.Remove(row);
    }

    IEnumerator GenerateFriend(float after)
    {
        yield return new WaitForSeconds(after);
        friends[friendIndex].transform.position = friendTransform.position;
        friends[friendIndex].SetActive(true);
        StartCoroutine(JumpRoutine(friends[friendIndex]));
    }

    IEnumerator JumpRoutine(GameObject friend)
    {
        if (allRows[currentRow] == 0)
            friend.transform.LookAt(rows[currentRow].transform.GetChild(0).transform.GetChild(0).position);
        else
            friend.transform.LookAt(rows[currentRow].transform.GetChild(1).transform.GetChild(0).position);
        yield return new WaitForSeconds(2);
        friend.GetComponent<Rigidbody>().AddForce(friend.transform.forward * 3.15f + Vector3.up * 10f, ForceMode.Impulse);
        yield return new WaitForSeconds(2);
        if (breakableGlasses.Contains(currentRow))
        {
            currentRow = 0;
            friendIndex++;
            if (friendIndex == friends.Count)
            {
                player.enabled = true;
                yield break;
            }
            StartCoroutine(GenerateFriend(2));
        }
        else
        {
            currentRow++;
            StartCoroutine(JumpRoutine(friend));
        }
    }
}
