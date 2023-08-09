using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UIButtonPingPong : MonoBehaviour
{
    public Ease EaseFirstTurn = Ease.Linear;
    public Ease EaseSecondTurn = Ease.OutElastic;
    
    public float TimeFirstTurn = 0.5f;
    public float TimeSecondTurn = 1f;
    
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    void Start()
    {
        Sequence sq = DOTween.Sequence();
        sq
            .Append(_transform.DOScale(Vector3.one * 0.9f, TimeFirstTurn).SetEase(EaseFirstTurn))
            .Append(_transform.DOScale(Vector3.one , TimeSecondTurn).SetEase(EaseSecondTurn))
            .SetLoops(-1, LoopType.Restart);
    }
}
