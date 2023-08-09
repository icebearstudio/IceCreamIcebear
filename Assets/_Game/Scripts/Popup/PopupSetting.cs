using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupSetting : BasePopup
{
    [Header("Popup Other")]
    public Button BtnPolicy;
    
    public override void Start()
    {
        base.Start();

        BtnPolicy.onClick.AddListener(() => { Application.OpenURL(""); });
    }
    
    public override void Show()
    {
        base.Show();
        Time.timeScale = 0f;
    }
    
    public override void Hide()
    {
        base.Hide();
        Time.timeScale = 1f;
    }
}
