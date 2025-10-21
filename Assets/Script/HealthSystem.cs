using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem
{
    private int health;
    private int maxHealth;

    public int Health
    {
        get { return health; }
        set { health = Mathf.Clamp(value, 0, maxHealth); }
    }

    public int MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = Mathf.Max(1, value);}
    }
    public HealthSystem()
    {
        ResetGame();
    }
    public void TakeDamage(int damage)
    {
        Health -= damage;
    }
    public void Heal(int hp)
    {
        Health += hp;
    }
    public void ResetGame()
    {
        Health = MaxHealth;
    }
    public bool Death()
    {
        //if the health is 0 than return true else returns false 
        if (Health == 0)
        {
            return true;
        }
        return false;
    }
}
