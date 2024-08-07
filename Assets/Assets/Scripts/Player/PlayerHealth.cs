using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float health = 100;
    [SerializeField] private HealthBar healthBar;
    // Start is called before the first frame update

    void Start()
    {
        healthBar.setMaxValue(health);
        healthBar.setValue(health);
    }

    public void loosingHealth(float health)
    {

        this.health -= health;
        if (this.health <= 0)
        {
            Destroy(gameObject);
            GameController.instance.EndGame("we loose the nebula!! :(");
        }
        healthBar.setValue(this.health);
    }
}
