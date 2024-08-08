using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingOrb : MonoBehaviour
{
    [SerializeField] private float _speed = 10;
    [SerializeField] private float healthValue = 6;
    private Rigidbody2D rb;
    private float _endHeight;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector3 edge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        _endHeight = edge.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= Vector3.up * _speed * Time.deltaTime;
        if (transform.position.y <= _endHeight + transform.localScale.y / 2)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            PlayerHealth ph = other.gameObject.GetComponent<PlayerHealth>();
            if (ph.Health < ph.MaxHealth)
            {
                ph.setHealthBar(ph.Health + healthValue);
            }
        }
        Destroy(gameObject);
    }
}
