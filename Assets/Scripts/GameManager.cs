using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void HandleDeath(GameObject obj)
    {
        if (obj.GetComponent<AttackSpells>())
        {
            Debug.Log("game over");
        }
        else
        {
            Destroy(obj.gameObject);
        }
    }

    void OnEnable()
    {
        Health.onDeath += HandleDeath;
    }

    void OnDisable()
    {
        Health.onDeath -= HandleDeath;
    }
}
