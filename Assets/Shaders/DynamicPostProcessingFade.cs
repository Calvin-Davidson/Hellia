using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DynamicPostProcessingFade : MonoBehaviour
{
    [SerializeField] private float lerpSpeed = 0.5f;
    [SerializeField] private PostProcessingShader postProcessingEffect = null;
    private Material materialReference = null;
    [SerializeField] private List<string> keys = new List<string>();
    [SerializeField] private List<float> valuesMin = new List<float>();
    [SerializeField] private List<float> valuesMax = new List<float>();
    static float t = 0.0f;
    void Start()
    {
        if (postProcessingEffect == null)
        {
            return;
        }
        materialReference = postProcessingEffect.customRenderPass.materialReference;
        if (t >= 0.2)
        {
            StartCoroutine("DelayedReset");
        }
        else
        {
            ResetPostProcessing();
        }
    }

        // Update is called once per frame
        void Update()
    {
        if (t >= 1) { return; }
        for (int i = 0; i < keys.Count; i++)
        {
            materialReference.SetFloat(keys[i], Mathf.Lerp(valuesMin[i], valuesMax[i], t));
        }
        t += lerpSpeed * Time.deltaTime;
    }
    public void ResetPostProcessing()
    {
        if (postProcessingEffect == null)
        {
            return;
        }
        materialReference = postProcessingEffect.customRenderPass.materialReference;
        for (int i = 0; i < keys.Count; i++)
        {
            materialReference.SetFloat(keys[i], valuesMin[i]);
        }
        t = 0.0f;
    }

    IEnumerator DelayedReset()
    {
        yield return new WaitForSeconds(0.7f);
        ResetPostProcessing();
    }
}
