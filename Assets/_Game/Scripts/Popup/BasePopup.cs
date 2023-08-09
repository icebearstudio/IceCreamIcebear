using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class BasePopup : MonoBehaviour
{
    [Header("Popup Base")]
    public Button BtnClose;

    public GameObject MainObj;

    [HideInInspector] public UnityEvent CloseCallback;

    private Vector3 _originMainScale;

    public virtual void Awake()
    {
        _originMainScale = MainObj.transform.localScale;
    }
    
    public virtual void Start()
    {
        Debug.Log("Popup Start " + BtnClose);
        
        if(BtnClose != null){
            Debug.Log("Popup Start " + BtnClose);
            BtnClose.onClick.AddListener(() =>
            {
                Debug.Log("Call back click button close");
                Hide();
                CloseCallback?.Invoke();
            });
        }
    }

    public virtual void Show()
    {
        gameObject.SetActive(true);
        MainObj.transform.DOKill();
        MainObj.transform.localScale = _originMainScale;
        MainObj.transform.DOPunchScale(new Vector3(0.1f, 0.1f, 1f), 0.3f, 5, 1).SetEase(Ease.OutBounce).SetUpdate(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
}
