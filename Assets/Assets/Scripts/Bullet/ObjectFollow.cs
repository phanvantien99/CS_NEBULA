using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollow : MonoBehaviour
{
    public Transform target;
    public float rotateSpeed = 200f; // Rotation speed of the missile
    public float searchRadius = 100f; // Adjust according to your game needs
    public float stopDistance = 0.1f;
    private bool isActive;
    public float speed;

    private float _endHeight;


    private void OnEnable()
    {
        isActive = true;
        FindNearestEnemy();
        Vector3 edge = Camera.main.ViewportToWorldPoint(Vector3.up);
        _endHeight = edge.y;
    }


    private void OnDisable()
    {
        isActive = false;
    }


    private void Update()
    {
        if (isActive && target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            float distance = Vector3.Distance(transform.position, target.position);


            // Rotate towards the target
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
            Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);


            // Move towards the target if not yet reached
            if (distance > stopDistance)
            {
                transform.position += transform.up * speed * Time.deltaTime;
            }
            else
            {
                transform.position = target.position;
                isActive = false;
            }
        }
        else
        {
            transform.position += transform.up * speed * Time.deltaTime;
            if (transform.position.y >= _endHeight + transform.localScale.y / 2)
            {
                Destroy(this.gameObject);
            }
        }
    }


    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }


    void FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemies");
        float closestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;


        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);


            if (distanceToEnemy < closestDistance && distanceToEnemy <= searchRadius)
            {
                closestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }


        if (nearestEnemy != null)
        {
            target = nearestEnemy.transform;
        }
    }

}
