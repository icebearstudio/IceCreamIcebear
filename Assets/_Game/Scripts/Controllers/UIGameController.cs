using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIGameController : MonoBehaviour
{
    public GameController Game;
    
    [Header("Group Panel")] 
    public GameObject PanelComplete;

    [Header("Buttons")]
    public Button BtnSetting;    
    public Button BtnPlay;
    public Button BtnStart;
    public Button BtnNext;
    public Button BtnComplete;

    [Header("Group UI Step")]
    public GameObject GroupRequireScoop;
    public GameObject GroupRequireTopping;
    public ProgressTopping ProgressTopping;

    private List<ItemRequireScoop> _listItemRequireScoops;
    private List<ItemRequireTopping> _listItemRequireToppings;

    private Dictionary<EIceCream, ItemRequireScoop> _dictionaryItemRequireScoops;
    private Dictionary<EIceCream, ItemRequireTopping> _dictionaryItemRequireToppings;

    void Awake()
    {
        _dictionaryItemRequireScoops = new Dictionary<EIceCream, ItemRequireScoop>();
        _dictionaryItemRequireToppings = new Dictionary<EIceCream, ItemRequireTopping>();
        
        _listItemRequireScoops = GroupRequireScoop.GetComponentsInChildren<ItemRequireScoop>(true).ToList();
        _listItemRequireToppings = GroupRequireScoop.GetComponentsInChildren<ItemRequireTopping>(true).ToList();
    }
    
    void Start()
    {
        BtnPlay.onClick.AddListener(HandlePlay);

        BtnStart.onClick.AddListener(HandleStart);

        BtnNext.onClick.AddListener(HandleNext);

        BtnComplete.onClick.AddListener(HandleComplete);

        BtnSetting.onClick.AddListener(HandleSetting);
        
        HideAllPanel();

        // HideAllGroupRequireStep();
    }

    void HandlePlay()
    {
        Game.SetStateCustomer();
    }

    void HandleStart()
    {
        // Game.SetStateGamePlay();
        
        BtnStart.gameObject.SetActive(false);

        GameInfo.ListOrderIceCreams.Clear();
        
        // Get Random Two Order 
        var tempList = (from item in DataManager.Instance.IceCreamDictionary where item.Value.Active select item.Key).ToList();
        var sufferList = tempList.OrderBy(x => Random.value).ToArray();
        
        GameInfo.ListOrderIceCreams.Add(sufferList[0]);
        GameInfo.ListOrderIceCreams.Add(sufferList[1]);

        PopupController.Instance.popupPickIceCream.Show(GameInfo.ListOrderIceCreams, () =>
        {
            DOVirtual.DelayedCall(0.5f, () =>
            {
                Game.SetStateGamePlay();
            });
        });
    }

    void HandleNext()
    {
        // Game.SetTopping();
    }

    void HandleComplete()
    {
        BtnComplete.gameObject.SetActive(false);
        PanelComplete.SetActive(false);
        Game.MakeCharacterAction();

        DOVirtual.DelayedCall(2f, () =>
        {
            Game.SetStateHome();
        });
    }

    void HandleSetting()
    {

    }

    public void SetUIStateHome()
    {
        HideAllButton();
        HideAllGroupRequireStep();
        HideAllPanel();
        
        BtnPlay.gameObject.SetActive(true);
    }

    public void SetUIStateCustomer()
    {
        HideAllButton();
        BtnStart.gameObject.SetActive(true);
    }

    public void SetUIStateGamePlay()
    {
        HideAllButton();
        // BtnNext.gameObject.SetActive(true);
        
        HideAllGroupRequireStep();
        GroupRequireScoop.SetActive(true);
        
        _dictionaryItemRequireScoops.Clear();

        // Hide all item require scoops
        foreach (var item in _listItemRequireScoops)
        {
            item.gameObject.SetActive(false);
        }

        var index = 0;
        foreach (var pickedIceCream in GameInfo.DictionaryPickedValue)
        {
            Debug.Log("==> Picked Ice Cream " + pickedIceCream.Key + " " + pickedIceCream.Value);
            _listItemRequireScoops[index].Init(pickedIceCream.Key, pickedIceCream.Value);
            _dictionaryItemRequireScoops.Add(pickedIceCream.Key, _listItemRequireScoops[index]);

            index += 1;
        }
    }

    public void SetUIComplete()
    {
        HideAllGroupRequireStep();
        PanelComplete.SetActive(true);
        
        HideAllButton();
        BtnComplete.gameObject.SetActive(true);
    }

    public void UpdateInfoListItemRequireScoop()
    {
        foreach (var picked in GameInfo.DictionaryPickedValue)
        {
            if (_dictionaryItemRequireScoops.ContainsKey(picked.Key))
            {
                _dictionaryItemRequireScoops[picked.Key].UpdateValue(picked.Value - GameInfo.DictionaryDropValue[picked.Key]);
            }
        }
    }

    public void SetUIStateTopping()
    {
        HideAllButton();
        BtnComplete.gameObject.SetActive(true);
    }

    void HideAllButton()
    {
        BtnPlay.gameObject.SetActive(false);
        BtnStart.gameObject.SetActive(false);
        BtnNext.gameObject.SetActive(false);
        BtnComplete.gameObject.SetActive(false);
    }

    void HideAllGroupRequireStep()
    {
        GroupRequireScoop.SetActive(false);
        GroupRequireTopping.SetActive(false);
        ProgressTopping.gameObject.SetActive(false);
    }

    void HideAllPanel()
    {
        PanelComplete.SetActive(false);
    }
}
