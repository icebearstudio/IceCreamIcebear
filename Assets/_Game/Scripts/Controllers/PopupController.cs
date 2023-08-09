using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupController : MonoBehaviour
{
    public PopupAlert PopupAlert;
    
    public PopupConfirm PopupConfirm;
    
    public PopupSetting PopupSetting;
    
    public PopupPickIceCream popupPickIceCream;
    
    private static PopupController instance;

    public static PopupController Instance => instance;

    void Awake()
    {
        DontDestroyOnLoad(this);

        instance = GetComponent<PopupController>();
    }

    void Start()
    {
        HideAllPopup();
    }
    
    void HideAllPopup()
    {
        PopupAlert.Hide();
        PopupConfirm.Hide();
        PopupSetting.Hide();
    }
}
