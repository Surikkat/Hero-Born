using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Класс описывающий поведение снарядов
public class BulletBehavior : MonoBehaviour
{
    public float onscreenDelay = 3f;

    void Start()
    {
        //После появления снаряд исчезает чтобы не загромождать сцену объектами
        Destroy(this.gameObject, onscreenDelay);
    }
}
