using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Windows;

public class FilePathTester : MonoBehaviour
{

    #region Variables

    string assetsPath = "Assets/"; // hard coding - - generally not used
    string boardGameFile = "BoardGame.png";

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Streaming assets folder path: [" + Application.streamingAssetsPath + "]"); // this is a more dynamic way of searching folders - - preferred way of navigating

        if (UnityEngine.Windows.File.Exists(Path.Combine(Application.streamingAssetsPath, boardGameFile))) 
        {
            Debug.Log("Path Exists!");
        }
        else
        {
            Debug.LogError("Path Missing!");
        }
    }

    
}
