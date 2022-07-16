using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//¬с€ логика и поведение врагов
public class EnemyBehavior : MonoBehaviour
{
    public Transform player;
    public Transform patrolRoute;
    public List<Transform> locations;

    private int locationIndex = 0;
    private NavMeshAgent agent;

    private bool hunting = false;

    private int _lives = 1;
    public int EnemyLives
    {
        get { return _lives; }

        private set
        {
            _lives = value;

            if (_lives <= 0)
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
        agent.speed = 5f;

        InitializePatrolRoute();
        MoveToNextPatrolLocation();
    }

    void Update()
    {
        if (agent.remainingDistance < 0.5f && !agent.pathPending)
        {
            if (hunting)
            {
                agent.destination = player.position;
                agent.speed = 10f;
            }
            else
            {
                MoveToNextPatrolLocation();
            }
        }

    }

    //ѕри запуске уровн€ в список занос€тс€ все точки дл€ патрул€ территории на уровне
    void InitializePatrolRoute()
    {
        foreach (Transform child in patrolRoute)
        {
            locations.Add(child);
        }
    }

    //ѕереход к следующей точке патрулироани€
    void MoveToNextPatrolLocation()
    {
        if (locations.Count == 0)
        {
            return;
        }
        agent.destination = locations[locationIndex].position;

        locationIndex = (locationIndex + 1) % locations.Count;
    }

    //≈сли игрок приблизилс€ то следующа€ точка назначени€ - игрок
    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            agent.destination = player.position;
            Debug.Log("Player detected - attack!");

            hunting = true;
        }

    }

    //≈сли игрок вышел из зоны видимости - возврат к патрулированию
    void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            Debug.Log("Player out of range, resume patrol!");
            MoveToNextPatrolLocation();

            hunting = false;
            agent.speed = 5f;
        }
    }

    //ѕри столкновении с пулей игрока отнимаетс€ 1 жизнь
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Bullet(Clone)")
        {
            EnemyLives -= 1;
            Debug.Log("Critical Hit!");
        }

        if (collision.gameObject.name == "Player")
        {
            hunting = false;
        }
    }

    void OnCollisionExit(Collision collision)
    {

    }
}