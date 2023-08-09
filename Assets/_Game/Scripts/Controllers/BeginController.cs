using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeginController : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(DelayLoadScene());
    }

    IEnumerator DelayLoadScene()
    {
        yield return new WaitForEndOfFrame();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Loading");

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
