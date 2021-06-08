using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSystem : MonoBehaviour
{
    public static bool HasFinishedLevel(string levelName)
    {
        return PlayerPrefs.HasKey(levelName) && PlayerPrefs.GetString(levelName) == "true";
    }

    public static void SetLevelCompleted(string levelName)
    {
        PlayerPrefs.SetString(levelName, "true");
    }

    public static int GetLevelCount()
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings;     
        string[] scenes = new string[sceneCount];
        for( int i = 0; i < sceneCount; i++ )
        {
            scenes[i] = System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i) );
        }

        int counter = 0;
        int levels = 0;
        while (true)
        {
            counter++;
            if (!scenes.Contains("Level_" + counter)) return levels;
            levels++;
        }
    }
}