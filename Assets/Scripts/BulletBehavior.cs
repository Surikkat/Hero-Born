using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Поведение пули
public class BulletBehavior : MonoBehaviour
{
    public float onscreenDelay = 3f;

    void Start()
    {
        //Пуля будет исчезать спустя какое то время после появления чтобы на сцене не было много объектов
        Destroy(this.gameObject, onscreenDelay);
    }
}
