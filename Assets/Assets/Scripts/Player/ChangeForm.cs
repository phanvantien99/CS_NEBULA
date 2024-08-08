using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeForm : MonoBehaviour
{

    [SerializeField] private GameObject[] players;
    [SerializeField] private ParticleSystem transformEffect;
    [SerializeField] private int _currentPlaneIndex = 0;
    private void Start()
    {

        GameObject player = InitPlayer();
        if (player)
        {
            player.SetActive(true);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            _currentPlaneIndex++;
            if (_currentPlaneIndex > players.Length - 1)
            {
                _currentPlaneIndex = 0;
            }
            // destroy current player
            GameObject player = FindGameObjectInChildWithTag("Player");
            ParticleSystem ps = Instantiate(transformEffect, player.transform.position, Quaternion.identity, transform);
            Destroy(player);
            Destroy(ps.gameObject, 1f);

            // setup new player
            GameObject newPlayer = InitPlayer();
            PlayerHealth playerHealth = newPlayer.GetComponent<PlayerHealth>();
            playerHealth.setMaxHealthBar();
            playerHealth.setHealthBar(player.GetComponent<PlayerHealth>().Health);
            newPlayer.SetActive(true);
        }
    }

    private GameObject InitPlayer()
    {
        GameObject player = Instantiate(players[_currentPlaneIndex], transform);
        PlayerShooting muzzleSetup = player.GetComponent<PlayerShooting>();
        muzzleSetup.initializeMuzzle(shootBullet);
        return player;
    }

    // return children of this game obejct
    private GameObject FindGameObjectInChildWithTag(string tag)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.tag == tag)
            {
                return transform.GetChild(i).gameObject;
            }

        }

        return null;
    }

    private void shootBullet(PlayerShooting muzzle)
    {
        GameObject bullet = Instantiate(muzzle.Bullet, muzzle.transform.position, Quaternion.identity);
        muzzle.ShootSound.Play();
        bullet.SetActive(true);

    }
}
