using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AsyncLevelLoader : MonoBehaviour
{
    Action OnLevelLoadedAction;

    private void OnEnable()
    {
        OnLevelLoadedAction += OnLoadLevel;
    }

    private void OnDisable()
    {
        OnLevelLoadedAction -= OnLoadLevel;
    }

    private void Start()
    {
        StartCoroutine(CoLoadLevel("Week 3 - Test Load", OnLevelLoadedAction));
    }


    IEnumerator CoLoadLevel(string sceneName, Action onLevelLoadedCallBack)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;

        while (asyncLoad.progress < 0.9f)
        {
            Debug.Log("Loading... ");
            yield return null;
        }

        if (onLevelLoadedCallBack != null)
        {
            onLevelLoadedCallBack.Invoke();
            yield return null;
        }

        asyncLoad.allowSceneActivation = true;  
        yield return null;
    }
    void OnLoadLevel()
    {
        Debug.Log("Scene loaded successfully!");
    }
}
