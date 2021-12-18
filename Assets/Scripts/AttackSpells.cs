using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpells : MonoBehaviour
{
    [SerializeField] GameObject spikeObject;
    [SerializeField] float spikeRange = 10f;
    
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip spikeSound;
    [SerializeField] AudioClip healSound;

    [SerializeField] GameObject spike;

    void Start()
    {

    }
    
    public void SpikeAttack()
    {
        spike.SetActive(true);
        audioSource.clip = spikeSound;
        audioSource.Play();

        spikeObject.SetActive(true);
        var enemies = FindObjectsOfType<follow>();
        foreach (var enemy in enemies)
        {
            var distance = Vector3.Distance(enemy.transform.position, transform.position);
            Debug.Log(distance);
            if (distance < spikeRange)
            {
                Debug.Log("damage");
                enemy.GetComponent<Health>().Damage(50);
            }
        }
    }
}
