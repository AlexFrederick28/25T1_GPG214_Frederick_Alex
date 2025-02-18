using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class JSONFiles : MonoBehaviour
{

    public PlayerStats stats = new PlayerStats();

    public string playerJsonFileName = "Player.json";
    public string folderPath = Application.streamingAssetsPath;
    private string fullFilePath = string.Empty;

    // Start is called before the first frame update
    void Start()
    {
        fullFilePath = Path.Combine(folderPath, playerJsonFileName);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveJSON();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadJSON();
        }
    }

    void SaveJSON()
    {

        stats.SetPlayerPosition(transform.position);

        string jsonData = JsonUtility.ToJson(stats);

        File.WriteAllText(fullFilePath, jsonData);
    }

    void LoadJSON()
    {

        if (File.Exists(fullFilePath))
        {
            string jsonData = File.ReadAllText(fullFilePath);

            stats = JsonUtility.FromJson<PlayerStats>(jsonData);

            if (stats != null)
            {
                Debug.Log("Player save position was: " + stats.ReturnPlayerPosition());
                transform.position = stats.ReturnPlayerPosition();
            }
            else
            {
                Debug.LogError("Json found, but can't convert to class.");
            }
        }
        else
        {
            Debug.LogError("Player not found");
        }

    }
}
