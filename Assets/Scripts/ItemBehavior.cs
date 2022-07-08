using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Поведение предметов
public class ItemBehavior : MonoBehaviour
{
    public GameBehavior gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameBehavior>();
    }
    void OnCollisionEnter(Collision collision)
    {
        //Если игрок сталкивается с предметом то он подбирается
        if (collision.gameObject.name == "Player")
        {
            Destroy(this.transform.parent.gameObject);
            Debug.Log("Item collected!");

            gameManager.Items += 1;
        }
    }
}
