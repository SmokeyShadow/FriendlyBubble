using Supercyan.AnimalPeopleSample;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friend : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    private Animator animator;
    bool prisoned;

    void Start()
    {
        animator = GetComponent<Animator>();
        SimpleSampleCharacterControl.OnWavePressed += Greet;
    }

    public void Greet(Transform parent)
    {
        if (prisoned)
            return;
        if (Vector3.Distance(player.position, transform.position) <= 20)
        {
            StartCoroutine(GreetRoutine(parent));
        }
    }

    IEnumerator GreetRoutine(Transform parent)
    {
        prisoned = true;
        yield return new WaitForSeconds(1.5f);
        animator.SetBool("Wave", true);
        yield return new WaitForSeconds(5f);
        parent.parent.gameObject.SetActive(true);
        transform.SetParent(parent);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = new Vector3(1f, 1f, 1f);
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(2f);
        parent.parent.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
