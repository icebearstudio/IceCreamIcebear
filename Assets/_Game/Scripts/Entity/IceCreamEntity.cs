using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "Assets/Create IceCream", fileName = "IceCream")]
public class IceCreamEntity : ScriptableObject
{
    [Serializable]
    public class Param
    {
        [HideInInspector] public string name;
        public EIceCream ID;
        public string NameIceCream;
        public Sprite IconSprite;
        public GameObject IconIceCream;
        public GameObject PlaneIceCream;
        public GameObject ScoopIceCream;
        public bool Active;
    }
    
    public List<Param> Params;

    public IceCreamEntity()
    {
        Params = new List<Param>();
    }
    
    private void OnValidate()
    {
        foreach (var t in Params)
        {
            var active = t.Active ? "Active" : "";
            t.name = t.ID.ToString() + $"[{active}]";
        }
    }
}