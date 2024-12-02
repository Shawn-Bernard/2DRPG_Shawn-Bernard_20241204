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
    public void ResetGame()
    {
        health = 100;
    }
    public bool DIE()
    {
        if (health == 0)
        {
            return true;
        }
        return false;
    }
}
