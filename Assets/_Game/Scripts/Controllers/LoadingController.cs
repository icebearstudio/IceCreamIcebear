using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingController : MonoBehaviour
{
    public TextMeshProUGUI LoadingText;
    
    public Image ImageFill;
    
    private bool _allTaskLoadDone;
    private bool _callLoadNextScene;
    private float _tickWaitData;
    
    void Start()
    {
        _tickWaitData = 0f;
        
        StartCoroutine(LoadYourAsyncScene());
    }
    
    IEnumerator LoadYourAsyncScene()
    {  
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("GamePlay");
        asyncLoad.allowSceneActivation = false;

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            
            if(ImageFill != null) ImageFill.DOFillAmount(progress, 0.1f);
            if (LoadingText != null)
            {
                if (progress >= 1f) _tickWaitData += 0.1f;
                LoadingText.text = _tickWaitData > 1f ? $"Waiting Data..." : $"{progress * 100f:n0}%";
            }
            
            yield return new WaitForSeconds(0.1f);

            if (asyncLoad.progress >= 0.9f ){
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
