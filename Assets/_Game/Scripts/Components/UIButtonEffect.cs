using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;

public class UIButtonEffect : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Vector3 _originScale = Vector3.one;

    void Awake()
    {
        // _originScale = transform.localScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        EffectOnDown();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        EffectOnExit();
    }
    
    void EffectOnDown()
    {
        if (GetComponent<Button>() != null && !GetComponent<Button>().interactable) return;
        
        transform.DOKill();
        transform.DOScale(_originScale * 0.90f, 0.2f).SetUpdate(true);
        
        // if(AudioController.Instance != null) AudioController.Instance.PlayOneShot(DATA_RESOURCES.AUDIO.CLICK);
    }

    void EffectOnExit() {
        if (GetComponent<Button>() != null && !GetComponent<Button>().interactable) return;
        transform.DOScale(_originScale, 0.2f).SetUpdate(true);
    }
}
