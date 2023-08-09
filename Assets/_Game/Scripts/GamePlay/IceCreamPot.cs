using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCreamPot : MonoBehaviour
{
    private List<ScoopIceCream> _listScoops = new List<ScoopIceCream>();

    public Action<EIceCream> OnAddScoopCallback;
    
    private Transform _transform;

    private bool _isComplete;
    
    void Start()
    {
        _transform = transform;
    }

    public void Init()
    {
        _isComplete = false;
        gameObject.SetActive(true);
    }

    public void Complete()
    {
        _isComplete = true;
    }

    void Update()
    {
        if (!_isComplete) return;
        _transform.Rotate(0,100 * Time.deltaTime,0);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.gameObject.tag.Equals("IceCreamBall"))
        {
            other.gameObject.GetComponent<SphereFollowMouse>().SetInPot();
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other != null && other.gameObject.tag.Equals("IceCreamBall"))
        {
            other.gameObject.GetComponent<SphereFollowMouse>().SetOutPot();
        }   
    }
}
