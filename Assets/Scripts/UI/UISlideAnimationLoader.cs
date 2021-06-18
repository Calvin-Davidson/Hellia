using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UISlideAnimationLoader : MonoBehaviour
{
    [SerializeField] private GameObject canvasObject;
    private void Start()
    {
        var newCanvasObject = Instantiate(canvasObject, this.transform.position, Quaternion.identity);
        newCanvasObject.transform.parent = this.gameObject.transform;
    }
}
