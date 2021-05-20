using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string inputKey = "space";
    [SerializeField] private string sceneName;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(inputKey))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
