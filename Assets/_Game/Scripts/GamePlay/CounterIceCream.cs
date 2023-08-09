using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterIceCream : MonoBehaviour
{
    public GamePlayController GamePlay;

    public GameObject Type2Slots;
    
    public GameObject Type8Slots;

    public List<GameObject> List8Slots;
    
    void Start()
    {
        
    }

    public void SetPlaneSlots(List<EIceCream> listPlaneIceCreams)
    {
        if (listPlaneIceCreams.Count <= 2)
        {
            Type2Slots.SetActive(true);
            Type8Slots.SetActive(false);
        }
        else
        {
            Type2Slots.SetActive(false);
            Type8Slots.SetActive(true);

            var index = 0;

            foreach (var slot in List8Slots)
            {
                slot.SetActive(true);
            }

            foreach (var iceCream in listPlaneIceCreams)
            {
                List8Slots[7 - index].SetActive(false);
                InitPlane(iceCream, List8Slots[7 - index].transform);
                index += 1;
            }
        }
    }

    void InitPlane(EIceCream iceCreamId, Transform slotTrans)
    {
        var iceCreamData= DataManager.Instance.IceCreamDictionary[iceCreamId];

        var plane = Instantiate(iceCreamData.PlaneIceCream, GamePlay.SlotPlaneLeft.transform);
        plane.transform.position = new Vector3(slotTrans.position.x, GamePlay.SlotPlaneLeft.transform.position.y, slotTrans.position.z);
        plane.transform.localEulerAngles = Vector3.zero;
        plane.transform.localScale = Vector3.one * 0.55f;
            
        GamePlay.AddPlaneIceCream(plane.GetComponent<PlaneIceCream>());
    }
}
