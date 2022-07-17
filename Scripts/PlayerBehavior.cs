using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Поведение игрока
public class PlayerBehavior : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float rotateSpeed = 75f;
    public LayerMask groundLayer;

    public GameObject bullet;
    public float bulletSpeed = 100f;

    private float vInput;
    private float hInput;
    private float mouseX;
    private float mouseY;

    private Rigidbody _rb;
    private CapsuleCollider _col;

    private GameBehavior _gameManager;
    public GameObject camera;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<CapsuleCollider>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameBehavior>();
    }

    void Update()
    {
        vInput = Input.GetAxis("Vertical") * moveSpeed;
        hInput = Input.GetAxis("Horizontal") * rotateSpeed;

        mouseX += (Input.GetAxis("Mouse X") * 10f);
        mouseY += (Input.GetAxis("Mouse Y") * 10f);

        var rotationY = Mathf.Clamp(mouseY, 0f, 0f);
        var rotationX = mouseX;

        var newRotation = Quaternion.Euler(rotationY, rotationX, 0f);
        //var newPosition = newRotation * new Vector3(0f, 0f, 2.6f) + this.transform.position;

        this.transform.rotation = newRotation;
        //camera.transform.position = newPosition;
    }

    void FixedUpdate()
    {
        Vector3 rotation = Vector3.up * hInput;
        Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime);
        _rb.MovePosition(this.transform.position + this.transform.forward * vInput * Time.fixedDeltaTime);
        _rb.MoveRotation(_rb.rotation * angleRot);

        if (Input.GetMouseButtonDown(0))
        {
            GameObject newBullet = Instantiate(bullet, this.transform.position + new Vector3(1, 0, 0), this.transform.rotation) as GameObject;
            Rigidbody bulletRB = newBullet.GetComponent<Rigidbody>();
            bulletRB.velocity = this.transform.forward * bulletSpeed;
        }
    }

    //Если сталкиваемся с врагом то отнимается одна жизнь
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Enemy" || collision.gameObject.name == "Enemy 2" || collision.gameObject.name == "Enemy 3")
        {
            _gameManager.HP -= 1;
        }
    }
}