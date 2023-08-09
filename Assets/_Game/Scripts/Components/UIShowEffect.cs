using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class UIShowEffect : MonoBehaviour
{
    public float StartScaleFactor = 0.6f;
    
    public float DelayTime = 0;
    
    public float EffectTime = 0.5f;

    public Ease EffectEase = Ease.OutElastic;
    
    private CanvasGroup _canvasGroup;
    
    private Vector3 _originScale = Vector3.one;

    void Awake()
    {
        // _originScale = transform.localScale;
        
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        StopAllCoroutines();
        transform.DOKill();

        // if (GetComponent<Button>() != null)
        // {
        //     GetComponent<Button>().interactable = false;
        // }
        
        _canvasGroup.alpha = 0;
        transform.localScale = _originScale * StartScaleFactor;
        
        StartCoroutine(DelayShowCoroutine());
    }

    IEnumerator DelayShowCoroutine()
    {
        yield return new WaitForSeconds(DelayTime);
        _canvasGroup.alpha = 1f;
        
        transform.DOScale(_originScale, EffectTime).SetEase(EffectEase).OnComplete(() =>
        {
            // if (GetComponent<Button>() != null)
            // {
            //     GetComponent<Button>().interactable = false;
            // }
        });
    }
}
