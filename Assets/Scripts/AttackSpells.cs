using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AttackSpells : MonoBehaviour
{
    [SerializeField] GameObject spikeObject;
    [SerializeField] float spikeRange = 10f;

    [SerializeField] private GameObject barrier;
    [SerializeField] private Color healColor;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip spikeSound;
    [SerializeField] AudioClip healSound;

    private Color barrierDefaultColor;

    public void SpikeAttack()
    {
        spikeObject.SetActive(true);
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

    public void Heal()
    {
        audioSource.clip = healSound;
        audioSource.Play();
        FadeInBarrierColor();
    }

    private void FadeInBarrierColor()
    {
        barrierDefaultColor = barrier.GetComponent<Renderer>().material.color;
        barrier.GetComponent<Renderer>().material.DOColor(healColor, 0.8f).OnComplete(FadeOutBarrierColor);
    }

    private void FadeOutBarrierColor()
    {
        barrier.GetComponent<Renderer>().material.DOColor(barrierDefaultColor, 0.8f);
    }
}
