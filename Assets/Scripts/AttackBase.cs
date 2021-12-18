using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBase : MonoBehaviour
{
    float range = 10f;

    AttackSpells _base;
    Health baseHealth;

    void Start()
    {
        _base = FindObjectOfType<AttackSpells>();
        baseHealth = _base.GetComponent<Health>();
        StartCoroutine(Attack_base());
    }
    IEnumerator Attack_base()
    {
        while (true)
        {
            Attack();
            yield return new WaitForSeconds(2);
        }
    }

    void Attack()
    {
        var distance = Vector3.Distance(_base.transform.position, transform.position);
        if (distance <= range)
        {
            baseHealth.Damage(10);
        }
    }
}
