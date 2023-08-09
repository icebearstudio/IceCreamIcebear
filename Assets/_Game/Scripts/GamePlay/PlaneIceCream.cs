using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneIceCream : MonoBehaviour
{
    public EIceCream IceCreamId;
    
    [HideInInspector] public Action<EIceCream> OnCreateScoopEvent;
    [HideInInspector] public Action<EIceCream, bool> OnScoopDoneEvent;
    
    void Start()
    {
        
    }
    
    public void CreateScoop()
    {
        OnCreateScoopEvent?.Invoke(IceCreamId);
    }

    public void ScoopDone(bool isExitPlane = false)
    {
        OnScoopDoneEvent?.Invoke(IceCreamId, isExitPlane);
    }
}
