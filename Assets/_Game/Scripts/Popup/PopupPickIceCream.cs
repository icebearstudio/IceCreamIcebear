using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PopupPickIceCream : BasePopup
{
    [Header("Popup Pick")]
    public GameObject PanelCover;
    public TextMeshProUGUI TitleText;
    
    public TextMeshProUGUI DescText;

    [Header("Order Pick")] 
    public Transform OrderPick;
    public Image IconOrder;

    private List<EIceCream> _listOrderIceCreams;
    private int _countOrder;

    private Action _pickDoneCallback;

    public override void Start()
    {
        base.Start();

        foreach (var item in GetComponentsInChildren<ItemIceCream>())
        {
            item.OnPickCallbackEvent += HandlePickIceCream;
        }
    }

    void HandlePickIceCream(EIceCream iceCreamId)
    {
        PanelCover.SetActive(true);
        
        if (!GameInfo.ListPickedIceCreams.Contains(iceCreamId))
        {
            GameInfo.ListPickedIceCreams.Add(iceCreamId);
        }

        if (_countOrder < _listOrderIceCreams.Count - 1)
        {
            DOVirtual.DelayedCall(0.5f, () =>
            {
                _countOrder += 1;
                ShowStepOrder();
            });
        }
        else
        {
            _pickDoneCallback?.Invoke();
            Hide();
        }
    }

    public void Show(List<EIceCream> orderIceCreams, Action doneCallback)
    {
        GameInfo.ListPickedIceCreams.Clear();
        
        _pickDoneCallback = doneCallback;
        
        _listOrderIceCreams = orderIceCreams;

        _countOrder = 0;
        ShowStepOrder();
        Show();
    }

    void ShowStepOrder()
    {
        PanelCover.SetActive(false);
        
        var firstPick = _listOrderIceCreams[_countOrder];
        
        Debug.Log($"==> Step Order {_countOrder} || {firstPick}");
        
        ShowOrderOfCustomer(firstPick);

        var optionPick = OptionForPick(firstPick);
        ShowOptionForPick(optionPick);
    }

    void ShowOrderOfCustomer(EIceCream orderIceCream)
    {
        OrderPick.gameObject.SetActive(false);
        var data = DataManager.Instance.IceCreamDictionary[orderIceCream];
        IconOrder.sprite = data.IconSprite;

        DOVirtual.DelayedCall(0.5f, () =>
        {
            OrderPick.gameObject.SetActive(true);
        
            OrderPick.localScale = Vector3.one * 0.3f;
            OrderPick.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBounce);
        });
    }

    void ShowOptionForPick(EIceCream[] iceCreams)
    {
        int index = 0;
        foreach (var item in GetComponentsInChildren<ItemIceCream>(true))
        {
            Debug.Log($"==> Option Pick [{index}] " + iceCreams[index]);
            
            if (iceCreams.Length > index)
            {
                item.gameObject.SetActive(true);
                item.Init(iceCreams[index]);    
            }
            else
            {
                item.gameObject.SetActive(false);
            }

            index += 1;
        }
    }

    EIceCream[] OptionForPick(EIceCream orderIceCream)
    {
        var tempList = new List<EIceCream>();
        foreach (var item in DataManager.Instance.IceCreamDictionary)
        {
            if (item.Value.Active)
            {
                tempList.Add(item.Key);
            }
        }

        var tempPick = new List<EIceCream>();
        tempPick.Add(orderIceCream);
        tempList.Remove(orderIceCream);
        
        var pick1 = tempList[Random.Range(0, tempList.Count)];
        tempPick.Add(pick1);
        tempList.Remove(pick1);

        // Debug.Log("Pick 1 " + pick1 + " " + tempList.Count);
        
        var pick2 = tempList[Random.Range(0, tempList.Count)];
        tempPick.Add(pick2);
        tempList.Remove(pick2);
        
        // Debug.Log("Pick 2 " + pick2 + " " + tempList.Count);
        
        var pick3 = tempList[Random.Range(0, tempList.Count)];
        tempPick.Add(pick3);
        tempList.Remove(pick3);
        
        // Debug.Log("Pick 3 " + pick3 + " " + tempList.Count);


        return tempPick.OrderBy( x => Random.value ).ToArray();
    }

    public override void Show()
    {
        base.Show();
    }    
}
