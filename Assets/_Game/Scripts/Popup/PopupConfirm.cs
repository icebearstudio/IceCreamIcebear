using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupConfirm : BasePopup
{
    [Header("Popup Alert")]
    public TextMeshProUGUI TitleText;
    
    public TextMeshProUGUI DescText;

    public Button BtnOk;
    
    public Button BtnCancel;

    private Action _acceptCallback;
    private Action _cancelCallback;
    
    public override void Start()
    {
        base.Start();
        
        BtnOk.onClick.AddListener(() =>
        {
            _acceptCallback?.Invoke();
            Hide();
        });
        
        BtnCancel.onClick.AddListener(() =>
        {
            _cancelCallback?.Invoke();   
            Hide();
        });
    }
    
    public void Show(string msg, Action acceptCallback, Action cancelCallback = null)
    {
        DescText.text = msg;

        _acceptCallback = acceptCallback;
        _cancelCallback = cancelCallback;
        
        Show();
    }

    public void Show(string msg, string title, Action acceptCallback, Action cancelCallback = null)
    {
        DescText.text = msg;
        
        if (!string.IsNullOrEmpty(title))
        {
            TitleText.text = title;
        }
        
        _acceptCallback = acceptCallback;
        _cancelCallback = cancelCallback;
        
        Show();
    }
}
