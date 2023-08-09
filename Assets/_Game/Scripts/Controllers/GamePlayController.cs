using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Wacki.IndentSurface;

public class GamePlayController : MonoBehaviour
{
    public UIGameController UIGame;
    
    public GameObject SpawnScoopObj;
    
    public GameObject SlotPlaneLeft;
    public GameObject SlotPlaneRight;

    public CounterIceCream Counter;

    private List<PlaneIceCream> _planeIceCreams = new List<PlaneIceCream>();
    private GameObject _currentSphereScoop;
    
    void Start()
    {
        
    }

    public void InitPlaneScoops()
    {
        var list = new List<EIceCream>();
        foreach (var pickedIceCream in GameInfo.ListPickedIceCreams)
        {
            if (!list.Contains(pickedIceCream))
            {
                list.Add(pickedIceCream);
                Debug.Log("==> Add List Plane " + pickedIceCream);
            }
        }

        var listSuffer = Helper.SufferIceCreams(list);
        var rand = Random.Range(0, 100);
        
        Debug.Log("==> List Count " + list.Count + " " + rand);
        if (list.Count == 1)
        {
            list.Add(listSuffer[0]);
        }
        // If list count == 2
        else
        {
            
            // var rand = 70;
            
            
            
            if (rand > 90)
            {
                list.Add(listSuffer[4]);
            } 
            
            if (rand > 80)
            {
                list.Add(listSuffer[3]);
            }
            
            if (rand > 70)
            {
                list.Add(listSuffer[2]);
            }
            
            if (rand > 60)
            {
                list.Add(listSuffer[1]);
            }
        }

        Init(list);
    }

    void Init(List<EIceCream> pickIceCreams)
    {
        Debug.Log("==> Pick Ice Creams " + pickIceCreams.Count);
        if (pickIceCreams.Count < 2)
        {
            Debug.LogError("Pick IceCream < 2!");
            return;
        }

        _planeIceCreams.Clear();
        RemovePlaneIceCream();
        
        if (pickIceCreams.Count <= 2)
        {
            var iceCreamDataLeft= DataManager.Instance.IceCreamDictionary[pickIceCreams[0]];
            var iceCreamDataRight= DataManager.Instance.IceCreamDictionary[pickIceCreams[1]];

            var planeLeft = Instantiate(iceCreamDataLeft.PlaneIceCream, SlotPlaneLeft.transform);
            planeLeft.transform.localPosition = Vector3.zero;
            planeLeft.transform.localEulerAngles = Vector3.zero;
            
            var planeRight = Instantiate(iceCreamDataRight.PlaneIceCream, SlotPlaneRight.transform);
            planeRight.transform.localPosition = Vector3.zero;
            planeRight.transform.localEulerAngles = Vector3.zero;
            
            _planeIceCreams.Add(planeLeft.GetComponent<PlaneIceCream>());
            _planeIceCreams.Add(planeRight.GetComponent<PlaneIceCream>());
            
            // _planeIceCreams.AddRange(GetComponentsInChildren<PlaneIceCream>());
            
            foreach (var planeIceCream in _planeIceCreams)
            {
                planeIceCream.OnCreateScoopEvent += HandleCreateScoop;
                planeIceCream.OnScoopDoneEvent += HandleScoopDone;
            }
        }

        Counter.SetPlaneSlots(pickIceCreams);
    }

    public void AddPlaneIceCream(PlaneIceCream planeIceCream)
    {
        _planeIceCreams.Add(planeIceCream);
        planeIceCream.OnCreateScoopEvent += HandleCreateScoop;
        planeIceCream.OnScoopDoneEvent += HandleScoopDone;
    }

    void RemovePlaneIceCream()
    {
        foreach (Transform child in SlotPlaneLeft.transform)
        {
            Destroy(child.gameObject);
        }
        
        foreach (Transform child in SlotPlaneRight.transform)
        {
            Destroy(child.gameObject);
        }
    }

    Transform PlaneIceCream(EIceCream iceCream)
    {
        foreach (var planeIceCream in _planeIceCreams)
        {
            if (iceCream == planeIceCream.IceCreamId) return planeIceCream.transform;
        }

        return null;
    }

    void HandleCreateScoop(EIceCream iceCreamId)
    {
        Debug.Log("==> Create Scoop Ice Cream Id " + iceCreamId);
        CreateScoop(iceCreamId);
    }

    void HandleScoopDone(EIceCream iceCreamId, bool isExitPlane = false)
    {
        Debug.Log("==> Scoop Done Ice Cream Id " + iceCreamId);
        if(_currentSphereScoop != null) _currentSphereScoop.GetComponent<SphereFollowMouse>().SetStateCanPick(isExitPlane);
    }

    void CreateScoop(EIceCream iceCreamId)
    {
        _currentSphereScoop = null;
        
        var iceCreamData = DataManager.Instance.IceCreamDictionary[iceCreamId];
        GameObject obj = Instantiate(iceCreamData.ScoopIceCream, SpawnScoopObj.transform);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localScale = Vector3.one;
        var sphereScoop = obj.GetComponent<SphereFollowMouse>(); 
        
        var planeIceCream = PlaneIceCream(iceCreamId);
        if(planeIceCream != null){
            sphereScoop.planeTransform = planeIceCream;
            sphereScoop.Init(iceCreamId);
            sphereScoop.OnDropOnPotEvent += HandleDropScoop;

            _currentSphereScoop = obj;
        }
        else
        {
            Debug.LogError("Not Found Plane Ice Cream Create Scoop "  + iceCreamId);   
        }
    }

    void HandleDropScoop(EIceCream iceCreamId, GameObject obj)
    {
        Debug.Log("==> Drop Scoop " + iceCreamId);
        
        obj.transform.SetParent(UIGame.Game.Pot.transform);
        
        // Check if Id Valid Allow Drop 
        if (GameInfo.DictionaryDropValue.ContainsKey(iceCreamId))
        {
            GameInfo.DictionaryDropValue[iceCreamId] += 1;
            Debug.Log($"==> Drop Scoop {iceCreamId} {GameInfo.DictionaryDropValue[iceCreamId]}");
            
            if (Helper.CheckPotDone())
            {
                DOVirtual.DelayedCall(1.5f, () =>
                {
                    UIGame.Game.SetStateComplete();
                });

            }
            
            UIGame.UpdateInfoListItemRequireScoop();
        }
    }

    public void ClearGamePlay()
    {
        foreach (Transform child in SpawnScoopObj.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
