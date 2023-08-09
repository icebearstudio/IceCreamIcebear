using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemRequireStep : MonoBehaviour
{
    public Image Icon;
    
    public TextMeshProUGUI NumRequireText;

    protected int _numberRequire;

    public void UpdateInfo(Sprite iconSprite, int numberRequire)
    {
        _numberRequire = numberRequire;
        
        Icon.sprite = iconSprite;
        NumRequireText.text = numberRequire + "";
        
        NumRequireText.transform.parent.gameObject.SetActive(numberRequire > 1);
    }

    public void UpdateValue(int numberRequire)
    {
        if (_numberRequire < 0) _numberRequire = 0;
        
        if(numberRequire != _numberRequire)
        {
            transform.DOKill();
            transform.DOPunchScale(Vector3.one * 0.1f, 0.3f, 1, 0.5f);
            _numberRequire = numberRequire;
            NumRequireText.text = numberRequire + "";
        }

        if (_numberRequire <= 0)
        {
            Hide();
        }
    }

    public void Hide()
    {
        transform.DOKill();
        transform.DOScale(Vector3.one * 0.5f, 0.2f).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }
    
    public void Show()
    {
        transform.DOKill();
        gameObject.SetActive(true);
        transform.localScale = Vector3.one * 0.6f;
        transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBounce);
    }
}
