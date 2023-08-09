using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameController : MonoBehaviour
{
    public UIGameController UIGame;
    public GamePlayController GamePlay;

    [Header("Camera")]
    public Camera CameraHome;

    public Camera CameraCustomer;

    public Camera CameraGamePlay;

    public Camera CameraTopping;

    [Header("Environment")]
    public GameObject IceCreamCarObj;
    public GameObject TableObj;

    [Header("Characters")] public Customer CustomerOrder;

    [Header("Tools Make Ice Cream")]
    public IceCreamPot Pot;

    public GameObject TargetToppingPot;
    public GameObject CompleteToppingPot;

    private Vector3 _originPot;

    private Camera _cameraMain;

    private EStateGame _stateGame;

    void Awake()
    {
        _cameraMain = Camera.main;
        _originPot = Pot.transform.position;

        SetStateHome();
        // GameInfo.ListPickedIceCreams = new List<EIceCream>() {EIceCream.Caramel, EIceCream.Vanilla};
        // SetStateGamePlay();
    }

    void Start()
    {
        _stateGame = EStateGame.HOME;

        Pot.OnAddScoopCallback += HandlePotAddScoop;
    }

    void HandlePotAddScoop(EIceCream iceCream)
    {
        Debug.Log("==> Callback Ice Cream " + iceCream);
    }

    public void SetStateHome()
    {
        _stateGame = EStateGame.HOME;
        
        _cameraMain.transform.position = CameraHome.transform.position;
        _cameraMain.transform.rotation = CameraHome.transform.rotation;

        TableObj.SetActive(false);
        IceCreamCarObj.SetActive(true);

        ClearAllScoops();

        Debug.Log("Check Table Obj " + TableObj.activeSelf);

        UIGame.SetUIStateHome();
    }

    public void SetStateCustomer()
    {
        Pot.transform.position = _originPot;

        _cameraMain.transform.position = CameraCustomer.transform.position;
        _cameraMain.transform.rotation = CameraCustomer.transform.rotation;

        //_cameraMain.transform.DOMove(CameraCustomer.transform.position, 0.3f);
        //_cameraMain.transform.DORotate(CameraCustomer.transform.eulerAngles, 0.3f);
        
        UIGame.SetUIStateCustomer();
    }

    public void SetStateGamePlay()
    {
        //_cameraMain.transform.position = CameraGamePlay.transform.position;
        //_cameraMain.transform.rotation = CameraGamePlay.transform.rotation;
        
        GameInfo.DictionaryDropValue.Clear();
        GameInfo.DictionaryPickedValue.Clear();
        
        Pot.Init();
        
        foreach (var pickedIceCream in GameInfo.ListPickedIceCreams)
        {
            if (GameInfo.DictionaryPickedValue.ContainsKey(pickedIceCream))
            {
                GameInfo.DictionaryPickedValue[pickedIceCream] += 1;
            }
            else
            {
                GameInfo.DictionaryPickedValue.Add(pickedIceCream, Random.Range(1, 3));
                GameInfo.DictionaryDropValue.Add(pickedIceCream, 0);
            }
        }
        
        GamePlay.InitPlaneScoops();

        _cameraMain.transform.DOMove(CameraGamePlay.transform.position, 0.3f);
        _cameraMain.transform.DORotate(CameraGamePlay.transform.eulerAngles, 0.3f);

        TableObj.SetActive(true);
        IceCreamCarObj.SetActive(false);
        
        UIGame.SetUIStateGamePlay();

        StartCoroutine(DelayActiveStateMakeCone());
    }

    IEnumerator DelayActiveStateMakeCone()
    {
        yield return new WaitForSeconds(0.5f);
        GameInfo.IsStateMakeConeIceCream = true;
    }

    public void SetTopping()
    {
        GameInfo.IsStateMakeConeIceCream = false;

        float timeEffect = 0.15f;
        Pot.transform.DOMove(TargetToppingPot.transform.position, timeEffect);
        // Pot.transform.position = TargetToppingPot.transform.position;
        
        _cameraMain.transform.DOMove(CameraTopping.transform.position, timeEffect);
        _cameraMain.transform.DORotate(CameraTopping.transform.eulerAngles, timeEffect);
        
        UIGame.SetUIStateTopping();
    }

    public void SetStateComplete()
    {
        if (_stateGame == EStateGame.COMPLETE) return;
        
        _stateGame = EStateGame.COMPLETE;
        
        Debug.Log("==> Set State Complete");
        _cameraMain.transform.DOMove(CameraCustomer.transform.position, 0.3f);
        _cameraMain.transform.DORotate(CameraCustomer.transform.eulerAngles, 0.3f);

        foreach (var rb in Pot.GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = true;
        }
        
        Pot.transform.DOMove(CompleteToppingPot.transform.position, 0.3f).OnComplete(() =>
        {
            Pot.Complete();
        });
        
        // Pot.transform.position = CompleteToppingPot.transform.position;
        
        UIGame.SetUIComplete();
    }

    public void MakeCharacterAction()
    {
        Pot.Hide();
        CustomerOrder.Happy();
    }

    void ClearAllScoops()
    {
        foreach (var child in Pot.GetComponentsInChildren<ScoopIceCream>())
        {
            Destroy(child.gameObject);
        }
        
        GamePlay.ClearGamePlay();
    }
}
