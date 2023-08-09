using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo
{
    public static bool IsStateMakeConeIceCream;
    
    public static List<EIceCream> ListOrderIceCreams = new List<EIceCream>();
    
    public static List<EIceCream> ListPickedIceCreams = new List<EIceCream>();
    
    public static Dictionary<EIceCream, int> DictionaryPickedValue = new Dictionary<EIceCream, int>();
    
    public static Dictionary<EIceCream, int> DictionaryDropValue = new Dictionary<EIceCream, int>();
}
