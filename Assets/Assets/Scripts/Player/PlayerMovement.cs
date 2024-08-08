using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float _xMovement;
    [SerializeField] private float _yMovement;
    [SerializeField] private float _speed = 10;


    private Rigidbody2D rb;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // priv         
    // move with keyboard
    // _xMovement = Input.GetAxis("Horizontal");
    // _yMovement = Input.GetAxis("Vertical");
    // }

    // Update is called once per frame
    void FixedUpdate()
    {
        move();
        // transform.position = new Vector3(transform.position.x + _xMovement * _speed * Time.deltaTime, transform.position.y + _yMovement * _speed * Time.deltaTime, transform.position.z);

    }

    void move()
    {
        if (mainCamera)
        {
            Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            rb.MovePosition(Vector2.MoveTowards(rb.position, mousePosition, _speed * Time.deltaTime));
        }
    }
}
