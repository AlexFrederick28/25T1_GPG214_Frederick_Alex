using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WebRequests : MonoBehaviour
{

    [SerializeField] private string webAddress;

    public Image myWebTexture;
    private Sprite mySpriteFromWeb;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        // show a loading screen
        yield return StartCoroutine(LoadTextureFromWeb());

        myWebTexture.sprite = mySpriteFromWeb;

        // hide the loading screen and show the image
        // wait until the above is done... Then do more
        yield return null;
    }

   

    IEnumerator LoadTextureFromWeb()
    {

        UnityWebRequest imageRequest = UnityWebRequest.Get(webAddress);

        AsyncOperation downloadOperation = imageRequest.SendWebRequest();

        while (!downloadOperation.isDone)
        {
            Debug.Log("Download Progress " + ((downloadOperation.progress / 1f) * 100) + "%");

            yield return new WaitForEndOfFrame();
        }

        if (imageRequest.result == UnityWebRequest.Result.ConnectionError || imageRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error with loading file");
            yield break;
        }

        Debug.Log("Download Complete");

        byte[] allDataDownloaded = imageRequest.downloadHandler.data;

        Texture2D myTexture = new Texture2D(2, 2);

        myTexture.LoadImage(allDataDownloaded);

        mySpriteFromWeb = Sprite.Create(myTexture, new Rect(0, 0, myTexture.width, myTexture.height), Vector2.zero);

        yield return null;  
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

        while (asyncLoad != null && !asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);

            Debug.Log("Loading progress " + (progress * 100) + "%");

            yield return null; // yield control back to the main thread
        }

        // Loading is complete
        Debug.Log("Scene Loaded Successfully");

        asyncLoad.allowSceneActivation = true;

        yield return null;
    }

}
