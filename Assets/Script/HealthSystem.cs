using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem
{
    public static HealthSystem healthSystem;
    public int health;
    public HealthSystem()
    {
        ResetGame();
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
        }
    }
    public void Heal(int hp)
    {
        health += hp;
        if (health >= 100)//if health is greater than 100
        {
            health = 100; //Set to 100
        }
    }
    public void ResetGame()
    {
        health = 100;
    }
    public bool Death()
    {
        if (health == 0)
        {
            return true;
        }
        return false;
    }
}
