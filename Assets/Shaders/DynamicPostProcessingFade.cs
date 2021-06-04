using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicPostProcessingFade : MonoBehaviour
{
    [SerializeField] private float lerpSpeed;
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
        for (int i = 0; i < keys.Count; i++)
        {
            materialReference.SetFloat(keys[i], valuesMin[i]);
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
    }
}
