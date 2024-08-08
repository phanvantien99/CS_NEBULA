using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;

    private AudioSource shootSound;
    private Action<PlayerShooting> shoot;

    public GameObject Bullet { get => _bullet; set => _bullet = value; }
    public AudioSource ShootSound { get => shootSound; set => shootSound = value; }

    // Start is called before the first frame update
    void Start()
    {
        ShootSound = GetComponent<AudioSource>();
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            shoot.Invoke(this);
        }
        // else if (Input.GetMouseButtonDown(1))
        // {
        //     shootFollowBullet();
        // }
    }

    public void initializeMuzzle(Action<PlayerShooting> shoot)
    {
        this.shoot = shoot;
    }

}
