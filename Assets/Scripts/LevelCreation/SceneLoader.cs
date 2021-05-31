using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string inputKey = "space";
    [SerializeField] private string sceneName;
    [SerializeField] private bool useInputKey = true;
    [SerializeField] private bool useNextLevelEvent = false;
    [SerializeField] private bool useResetLevelEvent = false;

    private void Start()
    {
        if (useNextLevelEvent)
        {
            GameControl.Instance.onNextLevel?.AddListener(LoadLevel);
        }
        if (useResetLevelEvent)
        {
            GameControl.Instance.onResetLevel?.AddListener(LoadLevel);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(inputKey) && useInputKey)
        {
            LoadLevel();
        }
    }
    void LoadLevel()
    {
        SceneManager.LoadScene(sceneName);
    }
}
