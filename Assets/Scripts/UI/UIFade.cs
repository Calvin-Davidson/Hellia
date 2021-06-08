using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIFade : MonoBehaviour
{
    [SerializeField] private float lerpSpeed;
    [SerializeField] private CanvasGroup canvas;
    [SerializeField] private Vector2 minMaxOpacity;
    [SerializeField] private bool DestroyAfterFade = true;
    static float t = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<CanvasGroup>();
        ResetFade();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ResetFade();
    }
    void Update()
    {
        if (t >= 1)
        {
            if (DestroyAfterFade)
            {
                Destroy(this.gameObject);
            }
            return;
        }
        canvas.alpha = Mathf.Lerp(minMaxOpacity.y, minMaxOpacity.x, t);
        t += lerpSpeed * Time.deltaTime;
    }

    void ResetFade()
    {
        t = 0.0f;
    }
}
