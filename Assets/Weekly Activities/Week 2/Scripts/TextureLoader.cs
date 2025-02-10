using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


/// <summary>
/// This script will load textures from streaming assets
/// </summary>
public class TextureLoader : MonoBehaviour
{
    #region Variables



    #endregion

    // Start is called before the first frame update
    void Start()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "texture.png");

        if (File.Exists(filePath))
        {
            byte[] imageBytes = File.ReadAllBytes(filePath);

            Texture2D textureWeHaveLoaded = new Texture2D(2, 2);
            textureWeHaveLoaded.LoadImage(imageBytes);

            GetComponent<Renderer>().material.mainTexture = textureWeHaveLoaded;
        }
        else
        {
            Debug.LogError("Texture file not found at file path! " + filePath);
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
