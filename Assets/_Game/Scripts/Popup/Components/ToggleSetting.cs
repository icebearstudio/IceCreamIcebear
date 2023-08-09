using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleSetting : MonoBehaviour
{
    public SettingEnum SettingType;
    public GameObject IconOn;
    public GameObject IconOff;

    public enum SettingEnum
    {
        MUSIC,
        SOUND,
        VIBRATION
    }

    private Toggle _toggle;


    private void Awake()
    {
        _toggle = GetComponent<Toggle>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetDefault();

        _toggle.onValueChanged.AddListener((val) => {
            SetOnOff(val);
        });
    }

    void SetOnOff(bool flag = true)
    {
        IconOn.gameObject.SetActive(!flag);
        IconOff.gameObject.SetActive(flag);

        GameObject bg = transform.Find("Background").gameObject;
        bg.GetComponent<Image>().color = new Color(1f, 1f, 1f, flag ? 0f : 1f);

        if (SettingType == SettingEnum.SOUND)
        {
            DataManager.Instance.SettingData.Sound = !_toggle.isOn;
            // MainController.AnalyticController.LogEvent(ANALYTIC_EVENT.SETTING_SOUND, "value", _toggle.isOn ? 0 : 1);
        }
        else if (SettingType == SettingEnum.MUSIC)
        {
            DataManager.Instance.SettingData.Music = !_toggle.isOn;
            // MainController.AnalyticController.LogEvent(ANALYTIC_EVENT.SETTING_MUSIC, "value", _toggle.isOn ? 0 : 1);
        }
        else if (SettingType == SettingEnum.VIBRATION)
        {
            DataManager.Instance.SettingData.Vibration = !_toggle.isOn;
            // MainController.AnalyticController.LogEvent(ANALYTIC_EVENT.SETTING_VIBRATE, "value", _toggle.isOn ? 0 : 1);
        }
    }

    public void SetDefault()
    {
        if (SettingType == SettingEnum.SOUND) _toggle.isOn = !DataManager.Instance.SettingData.Sound;
        if (SettingType == SettingEnum.MUSIC) _toggle.isOn = !DataManager.Instance.SettingData.Music;
        if (SettingType == SettingEnum.VIBRATION) _toggle.isOn = !DataManager.Instance.SettingData.Vibration;
        
        IconOn.gameObject.SetActive(!_toggle.isOn);
        IconOff.gameObject.SetActive(_toggle.isOn);

        GameObject bg = transform.Find("Background").gameObject;
        bg.GetComponent<Image>().color = new Color(1f, 1f, 1f, _toggle.isOn ? 0f : 1f);
    }
}
