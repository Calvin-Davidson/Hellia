using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UISlideAnimationLoader : MonoBehaviour
{
    [SerializeField] private GameObject canvasObject;
    [SerializeField] private bool playOnStart = false;
    private void Start()
    {
        if (!playOnStart) { return; }
        var newCanvasObject = Instantiate(canvasObject, this.transform.position, Quaternion.identity);
        newCanvasObject.transform.parent = this.gameObject.transform;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        var newCanvasObject = Instantiate(canvasObject, this.transform.position, Quaternion.identity);
        newCanvasObject.transform.parent = this.gameObject.transform;
    }
}
