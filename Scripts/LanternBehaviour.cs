using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternBehaviour : MonoBehaviour
{
    public Vector3 LanternOffset = new Vector3(0f, 0f, 0f);

    private Transform target;

    void Start()
    {
        target = GameObject.Find("Player").transform;
    }

    void Update()
    {
        this.transform.position = target.TransformPoint(LanternOffset);

        this.transform.LookAt(target);

        this.transform.rotation = target.transform.rotation;
    }
}
