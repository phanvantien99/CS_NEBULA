using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvaderShoot : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(frequencySpawn());
    }

    private IEnumerator frequencySpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(3.5f);
            Instantiate(bullet, transform.position, Quaternion.identity);
        }
    }

}
