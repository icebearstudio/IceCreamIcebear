using UnityEngine;
using System.Collections;

public static class Vibration
{

#if UNITY_ANDROID && !UNITY_EDITOR
    public static AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    public static AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
    public static AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
#else
    public static AndroidJavaClass unityPlayer;
    public static AndroidJavaObject currentActivity;
    public static AndroidJavaObject vibrator;
#endif

    public static void Vibrate()
    {
        if(!DataManager.Instance.SettingData.Vibration) return;
        if (isAndroid())
            vibrator.Call("vibrate");
        else if(isIOS())
            Handheld.Vibrate();
    }


    public static void Vibrate(long milliseconds)
    {
        if(!DataManager.Instance.SettingData.Vibration) return;
        if (isAndroid())
            vibrator.Call("vibrate", milliseconds);
        else if(isIOS())
            Handheld.Vibrate();
    }

    public static void Vibrate(long[] pattern, int repeat)
    {
        if(!DataManager.Instance.SettingData.Vibration) return;
        if (isAndroid())
            vibrator.Call("vibrate", pattern, repeat);
        else if(isIOS())
            Handheld.Vibrate();
    }

    public static bool HasVibrator()
    {
        return isAndroid();
    }

    public static void Cancel()
    {
        if (isAndroid())
            vibrator.Call("cancel");
    }

    private static bool isAndroid()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
	return true;
#else
        return false;
#endif
    }
    
    private static bool isIOS()
    {
#if UNITY_IOS && !UNITY_EDITOR
	return true;
#else
        return false;
#endif
    }
    
}