using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotSphereDetect : MonoBehaviour
{
    void Start()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.gameObject.tag.Equals("IceCreamBall"))
        {
            other.gameObject.GetComponent<SphereFollowMouse>().PutInPot();
        }
    }
}
