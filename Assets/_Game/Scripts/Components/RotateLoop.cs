using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLoop : MonoBehaviour
{
    private Transform _transform;
    
    void Start()
    {
        _transform = transform;
    }
    
    void Update()
    {
        _transform.Rotate(0,0,100 * Time.deltaTime);
    }
}
