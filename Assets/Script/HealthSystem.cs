using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

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

        // Implement damage logic
    }
    public void ResetGame()
    {
        // Reset all variables to default values
        health = 100;
    }
}
