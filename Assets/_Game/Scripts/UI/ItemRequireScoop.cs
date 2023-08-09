using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemRequireScoop : ItemRequireStep
{
    void Start()
    {
        
    }

    public void Init(EIceCream iceCreamId, int numberRequire)
    {
        gameObject.SetActive(true);
        var data = DataManager.Instance.IceCreamDictionary[iceCreamId];
        UpdateInfo(data.IconSprite, numberRequire);
        Show();
    }
}
