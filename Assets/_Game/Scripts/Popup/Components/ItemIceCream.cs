using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemIceCream : MonoBehaviour
{
    public Image Icon;
    
    public TextMeshProUGUI NameText;

    private EIceCream _iceCreamId;

    public Action<EIceCream> OnPickCallbackEvent;
    
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(HandleClick);
    }

    void HandleClick()
    {
        Debug.Log("==> Pick Ice Cream " + _iceCreamId);
        OnPickCallbackEvent?.Invoke(_iceCreamId);
    }

    public void Init(EIceCream iceCreamId)
    {
        _iceCreamId = iceCreamId;
        
        var data = DataManager.Instance.IceCreamDictionary[iceCreamId];
        Icon.sprite = data.IconSprite;
        NameText.text = "" + data.NameIceCream;
        
        transform.localScale = Vector3.one * 0.8f;
        transform.DOScale(Vector3.one, 0.3f);
    }
}
