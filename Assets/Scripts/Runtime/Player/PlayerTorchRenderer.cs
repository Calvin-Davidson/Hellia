using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTorchRenderer : MonoBehaviour
{
    [SerializeField] private Material activeMaterial;
    [SerializeField] private Material inactiveMaterial;
    
    private TorchPower torchPower;
    private SkinnedMeshRenderer meshRenderer;
    private void Awake()
    {
        torchPower = GetComponentInParent<TorchPower>();
        meshRenderer = GetComponent<SkinnedMeshRenderer>();
    }

    private void Start()
    {
        torchPower.onTorchDeplete.AddListener(OnDeplete);
        torchPower.onTorchRefuel.AddListener(OnIgnite);
    }

    private void OnIgnite()
    {
        meshRenderer.material = activeMaterial;
    }

    private void OnDeplete()
    {
        meshRenderer.material = inactiveMaterial;
    }
}
