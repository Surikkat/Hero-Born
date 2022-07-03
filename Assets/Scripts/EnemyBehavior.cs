using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Вся логика и поведение врагов
public class EnemyBehavior : MonoBehaviour
{
    public Transform player;
    public Transform patrolRoute;
    public List<Transform> locations;

    private int locationIndex = 0;
    private NavMeshAgent agent;

    private int _lives = 3;
    public int EnemyLives
    {
        get { return _lives; }

        private set
        {
            _lives = value;

            if(_lives <= 0)
            {
                Destroy(this.gameObject);
                Debug.Log("Enemy down.");
            }
        }
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;

        InitializePatrolRoute();
        MoveToNextPatrolLocation();
    }

    void Update()
    {
        if(agent.remainingDistance<0.2f && !agent.pathPending)
        {
            MoveToNextPatrolLocation();
        }
    }

    //При запуске уровня в список заносятся все точки для патруля территории на уровне
    void InitializePatrolRoute()
    {
        foreach(Transform child in patrolRoute)
        {
            locations.Add(child);
        }
    }
    
    //Переход к следующей точке патрулироания
    void MoveToNextPatrolLocation()
    {
        if(locations.Count==0)
        {
            return;
        }

        agent.destination = locations[locationIndex].position;
        locationIndex = (locationIndex+1)%locations.Count;
    }

    //Если игрок приблизился то следующая точка назначения - игрок
    void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            agent.destination = player.position;
            Debug.Log("Player detected - attack!");
        }

    }

    //Если игрок вышел из зоны видимости - возврат к патрулированию
    void OnTriggerExit(Collider other)
    {
        if(other.name == "Player")
        {
            Debug.Log("Player out of range, resume patrol!");
        }
    }

    //При столкновении с пулей игрока отнимается 1 жизнь
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Bullet(Clone)")
        {
            EnemyLives -= 1;
            Debug.Log("Critical Hit!");
        }
    }
}
