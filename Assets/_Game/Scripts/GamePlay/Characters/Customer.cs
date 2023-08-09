using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Customer : Character
{
    public ParticleSystem FxPoofHead;
    
    public ParticleSystem FxPoofFoot;
    
    void Start()
    {
        
    }

    public void Happy()
    {
        FxPoofHead.Play();

        DOVirtual.DelayedCall(0.2f, () =>
        {
            GetComponentInChildren<Animator>().SetTrigger("Happy");
        });
    }
}
