using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class LoadingLocalContentAsync : MonoBehaviour
{

    public Texture texture;
    public string jsonData;
    public AudioClip audioClip;
    public AssetBundle bundle;

    public string jsonFileName;

    public string streamingAssetsFolderPath = Application.streamingAssetsPath;


    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return StartCoroutine(LoadLocalJsonAsync());
        // wait and do the next one
    }

   

    IEnumerator LoadLocalJsonAsync()
    {
        UnityWebRequest jsonLoadingRequest = UnityWebRequest.Get(Path.Combine(streamingAssetsFolderPath, jsonFileName));

        AsyncOperation downloadOperation = jsonLoadingRequest.SendWebRequest();

        while (!downloadOperation.isDone)
        {
            Debug.Log("Download Progress " + ((downloadOperation.progress / 1f) * 100) + "%");
        }

        if (jsonLoadingRequest.result == UnityWebRequest.Result.ConnectionError || jsonLoadingRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error with loading file");
            yield break;
        }

        Debug.Log("Download Complete");

        // Access web requests access the downloaded handler and grab the text
        jsonData = jsonLoadingRequest.downloadHandler.text;

        yield return null;
    }

   
}
