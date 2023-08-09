using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ProgressTopping : MonoBehaviour
{
    public Image Fill;
    
    void Start()
    {
        
    }

    public void Init()
    {
        Fill.fillAmount = 0f;
    }

    public void IncreaseFill(float val)
    {
        Fill.DOFillAmount(val, 0.3f);
    }
}
