using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Helper
{
    public static List<EIceCream> SufferIceCreams(List<EIceCream> excludes = null)
    {
        var tempList = (from item in DataManager.Instance.IceCreamDictionary where item.Value.Active select item.Key).ToList();

        if (tempList != null)
        {
            foreach (var exclude in excludes)
            {
                tempList.Remove(exclude);
            }    
        }
        
        var sufferList = tempList.OrderBy(x => Random.value).ToArray();
        return sufferList.ToList();
    }

    public static bool IsCanDropToPot(EIceCream iceCreamId)
    {
        if (!GameInfo.DictionaryDropValue.ContainsKey(iceCreamId)
        || GameInfo.DictionaryDropValue[iceCreamId] >= GameInfo.DictionaryPickedValue[iceCreamId]
        ) return false;

        return true;
    }

    public static bool CheckPotDone()
    {
        var flag = true;
        foreach (var dop in GameInfo.DictionaryDropValue)
        {
            if (dop.Value < GameInfo.DictionaryPickedValue[dop.Key])
            {
                flag = false;
                break;
            }
        }

        return flag;
    }
}
