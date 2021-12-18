using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class Spikes : MonoBehaviour
{
    private Vector3 endPosition;
    private Vector3 startPosition;
    
    private void OnEnable()
    {
        startPosition = transform.position;
        startPosition.y = -2.7f;
        endPosition = startPosition;
        endPosition.y = -0.1f;
        transform.position = startPosition;
        MoveUp();
    }

    private void MoveUp()
    {
        transform.DOMove(endPosition, 0.6f).SetEase(Ease.OutExpo).OnComplete(MoveDown);
    }
    
    private void MoveDown()
    {
        transform.DOMove(startPosition, 1f).SetEase(Ease.InExpo).OnComplete(Disable);
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }
}
