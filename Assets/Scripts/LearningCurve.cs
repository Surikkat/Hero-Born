using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearningCurve : MonoBehaviour
{
    public Transform camTransform;
    public GameObject directionLight;
    private Transform lightTransorm;

    void Start()
    {
        //directionLight = GameObject.Find("Directional Light");
        lightTransorm = directionLight.GetComponent<Transform>();
        Debug.Log(lightTransorm.localPosition);
    }

    void Update()
    {
        
    }
}
