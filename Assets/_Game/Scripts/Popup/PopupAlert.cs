using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupAlert : BasePopup
{
    [Header("Popup Alert")]
    public TextMeshProUGUI TitleText;
    
    public TextMeshProUGUI DescText;

    public Button BtnOk;
    
    public override void Start()
    {
        base.Start();
        
        BtnOk.onClick.AddListener(Hide);
    }

    public void Show(string msg, string title = "")
    {
        DescText.text = msg;
        
        if (!string.IsNullOrEmpty(title))
        {
            TitleText.text = title;
        }
        
        Show();
    }

    public override void Show()
    {
        base.Show();
    }
}
