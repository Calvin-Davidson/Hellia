using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameControl : MonoBehaviour
{
    private static GameControl instance;
    public UnityEvent onBlockUpdate = new UnityEvent();
    public UnityEvent onBlockPushed = new UnityEvent();
    public UnityEvent onNextLevel = new UnityEvent();
    public UnityEvent onResetLevel = new UnityEvent();
    public UnityEvent onBlockStartMove = new UnityEvent();
    private void Awake()
    {
        if (instance != null) return;
        instance = this;
    }

    public void BlockUpdateNextFrame()
    {
        StartCoroutine(RunNextFixedUpdate(() => onBlockUpdate?.Invoke()));
    }

    private IEnumerator RunNextFixedUpdate(Action a)
    {
        yield return new WaitForFixedUpdate();
        a?.Invoke();
    }

    public void LoadLevel(int id)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(id);
    }

    public void QuiteGame()
    {
        Application.Quit();
    }

    public static GameControl Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject gameObject = new GameObject();
                gameObject.AddComponent<GameControl>();
            }
            return instance;
        }
    }
}
