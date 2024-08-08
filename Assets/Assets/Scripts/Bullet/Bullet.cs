using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Vector3 _direction;
    [SerializeField] private float _speed;
    [SerializeField] private bool fromEnemy;
    [SerializeField] private float damageEachOne;
    [SerializeField] private ParticleSystem _particleHit;
    private float _endHeight;
    private void Awake()
    {
        Vector3 edge = fromEnemy ? Camera.main.ViewportToWorldPoint(Vector3.zero) : Camera.main.ViewportToWorldPoint(Vector3.up);
        _endHeight = edge.y;
    }

    private void Update()
    {
        if (fromEnemy)
        {
            transform.position -= _direction * _speed * Time.deltaTime;
            if (transform.position.y <= _endHeight + transform.localScale.y / 2)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            transform.position += _direction * _speed * Time.deltaTime;
            if (transform.position.y >= _endHeight + transform.localScale.y / 2)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag.Equals("Player") && fromEnemy)
        {
            ParticleSystem ps = Instantiate(_particleHit, transform.position, Quaternion.identity);
            Destroy(ps.gameObject, .3f);
            PlayerHealth player = other.GetComponent<PlayerHealth>();
            player.loosingHealth(damageEachOne);
            Destroy(gameObject);
        }
        else if (other.transform.tag.Equals("Enemies") && !fromEnemy)
        {
            ParticleSystem ps = Instantiate(_particleHit, transform.position, Quaternion.identity);
            Destroy(ps.gameObject, .3f);
            Invader invader = other.GetComponent<Invader>();
            invader.loosingHealth(damageEachOne);
            Destroy(gameObject);
        }
    }
}
