using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float health = 100;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private float maxHealth = 100;
    public float Health { get => health; set => health = value; }
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }

    void Start()
    {
        setMaxHealthBar();
    }

    public void loosingHealth(float health)
    {

        this.Health -= health;
        if (this.Health <= 0)
        {
            Destroy(gameObject);
            GameController.instance.EndGame("we loose the nebula!! :(");
        }
        healthBar.setValue(this.Health);
    }

    public void setMaxHealthBar()
    {
        healthBar.setMaxValue(MaxHealth);
        healthBar.setValue(this.health);
    }

    public void setHealthBar(float health)
    {
        this.health = health;
        healthBar.setValue(this.health);
    }
}
