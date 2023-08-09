using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class UIButtonDelayShow : MonoBehaviour
{
    public float DelayTime = 1f;
    
    public float FadeInTime = 1f;
    
    private CanvasGroup _canvasGroup;
    private Button _button;

    void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _button = GetComponent<Button>();
    }
    
    void Start()
    {
        
    }

    private void OnEnable()
    {
        _canvasGroup.alpha = 0f;
        _button.interactable = false;
        transform.localScale = Vector3.one * 0.6f;
        
        StopAllCoroutines();
        
        StartCoroutine(DelayShowCoroutine());
    }

    IEnumerator DelayShowCoroutine()
    {
        yield return new WaitForSeconds(DelayTime);
        transform.DOScale(Vector3.one, FadeInTime);
        _canvasGroup.DOFade(1f, FadeInTime).OnComplete(() =>
        {
            
        });
        _button.interactable = true;
    }
}
