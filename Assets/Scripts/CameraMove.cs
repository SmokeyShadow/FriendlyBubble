using Supercyan.AnimalPeopleSample;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    private float xOffset;
    private float yOffset;

    private void Start()
    {
        xOffset = target.position.x - transform.position.x;
        yOffset = target.position.y - transform.position.y;
    }
    void Update()
    {
        transform.position = new Vector3(target.transform.position.x + xOffset, target.transform.position.y + yOffset, -23.1f);
    }
}
