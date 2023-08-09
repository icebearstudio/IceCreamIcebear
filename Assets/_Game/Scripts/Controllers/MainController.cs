using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    [SerializeField] private GameObject _inGameDebugObj;
    
    private static MainController instance;

    public static MainController Instance => instance;

    void Awake()
    {
        DontDestroyOnLoad(this);

        instance = GetComponent<MainController>();
    }

    void Start()
    {
        Debug.Log("Init Main Controller");
        
        HandleSomeDebugTools();
    }

    void HandleSomeDebugTools()
    {
        _inGameDebugObj.SetActive(false);
        
        //TODO: More tools add below.
    }

    void HandleFirebaseInitDone(bool isSuccess)
    {
        ///
    }

    public bool SpendCoin(int val)
    {
        if (val <= 0) return false;

        if (DataManager.Instance.UserData.Coin < val) return false;
        
        DataManager.Instance.UserData.Coin -= val;
        return true;
    }
    
    public int AddCoin(int val)
    {
        if (val > 0)
        {
            DataManager.Instance.UserData.Coin += val;    
        }

        return DataManager.Instance.UserData.Coin;
    }
    
    public bool SpendGem(int val)
    {
        if (val <= 0) return false;

        if (DataManager.Instance.UserData.Gem < val) return false;
        
        DataManager.Instance.UserData.Gem -= val;
        return true;
    }
    
    public int AddGem(int val)
    {
        if (val > 0)
        {
            DataManager.Instance.UserData.Gem += val;    
        }

        return DataManager.Instance.UserData.Gem;
    }
    
    void OnApplicationPause(bool isPause) {
        if (isPause) OnAppInactive();
        else OnResume();
    }

    void OnApplicationQuit() {
        OnAppInactive();
    }

    void OnResume()
    {
        // add some function when app resume
    }

    void OnAppInactive() {
        DataManager.Instance.SaveAllData();
    }

    
    public void OpenAppInStore()
    {
        var url = "market://details?id=" + Application.identifier;
        #if UNITY_IOS || UNITY_IPHONE
        url ="itms-apps://itunes.apple.com/app/" + ISCONST.PackageNameIOS;
        #endif
        Application.OpenURL(url);
    }
}
