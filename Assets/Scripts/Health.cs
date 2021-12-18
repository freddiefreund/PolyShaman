using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Health : MonoBehaviour
{
    public static Action<GameObject> onDeath;
    int health = 100;

    public void Damage(int amount)
    {
        health -= amount;
        CheckDeath();
    }

    void CheckDeath()
    {
        if (health <= 0)
        {
            onDeath?.Invoke(gameObject);
        }
    }
}
