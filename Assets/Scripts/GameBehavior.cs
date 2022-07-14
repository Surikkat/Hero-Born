using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

//Мэнеджер игры
public class GameBehavior : MonoBehaviour
{
    public Transform spawnPoints;
    public List<Transform> itemSpawn;
    public GameObject pickupItem;

    //Флаги для проверки победы и порожения игрока
    public bool showWinScreen = false;
    public bool showLossScreen = false;

    public string labelText = "Collect all 4 items and win your freedom!";
    public int maxItems = 1;
    private int _itemCollected = 0;
    public int Items
    {
        get { return _itemCollected; }
        set
        {
            _itemCollected = value;

            if (_itemCollected >= maxItems)
            {
                labelText = "You have found all the items!";
                showWinScreen = true;
                Time.timeScale = 0f;
            }
            else
            {
                labelText = "Item found, only " + (maxItems - _itemCollected) + " more to go!";
            }
        }
    }

    private int _playerHP = 30;
    public int HP
    {
        get { return _playerHP; }
        set
        {
            _playerHP = value;

            if (_playerHP <= 0)
            {
                labelText = "You want another life with that?";
                showLossScreen = true;
                Time.timeScale = 0;
            }
            else
            {
                labelText = "Ouch... that's got hurt.";
            }

        }
    }

    
    public void Initialize()
    {
        foreach(Transform spawn in spawnPoints)
        {
            itemSpawn.Add(spawn);
        }

        List<int> randomNumbers = new List<int>();

        for(int i =0;i<3;++i)
        {
            System.Random rnd = new System.Random();
            int newNumber;
            bool flag = false;
            do
            {
                newNumber = rnd.Next() % 6;
                if(randomNumbers.Count==0)
                {

                }
                else
                {
                    foreach (int x in randomNumbers)
                    {
                        if (newNumber == x)
                        {
                            flag = true;
                        }
                    }
                }
            } while (flag);
            Debug.Log(newNumber);
            randomNumbers.Add(newNumber);
        }

        for(int i = 0;i < 3; ++i)
        {
            GameObject newPickUpItem = Instantiate(pickupItem, itemSpawn[randomNumbers[i]]) as GameObject;
        }
    }

    public void Start()
    {
        Initialize();
    }

    //Перезапуск уровня
    void RestartLevel()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1.0f;
    }

    //Вывод UI
    void OnGUI()
    {
        GUI.Box(new Rect(20, 20, 150, 25), "Player Health: " + _playerHP);
        GUI.Box(new Rect(20, 50, 150, 25), "Items collected: " + _itemCollected);
        GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height - 50, 300, 50), labelText);

        if (showWinScreen)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "YOU WON!"))
            {
                RestartLevel();
            }
        }

        if (showLossScreen)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "You lose..."))
            {
                RestartLevel();
            }
        }
    }
}