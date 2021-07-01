using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TorchPower : MonoBehaviour
{
    [SerializeField, Header("Hoe hoger dit getal hoe langer de torch brand")] private float burnDuration = 1f;
    [SerializeField] private float tileSize = 4f;

    [Header("UnityEvent for the torch")] public UnityEvent onTorchRefuel;
    public UnityEvent onTorchDeplete;
    
    private TorchChargingStation[] _chargingStations;
    private float _currentTorchCharge;
    private Animator _animator;
    private static readonly int HasTorch = Animator.StringToHash("HasTorch");

    private void Awake()
    {
        _chargingStations = FindObjectsOfType<TorchChargingStation>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        float previousCharge = _currentTorchCharge;
        _currentTorchCharge -= Time.deltaTime;

        if (_currentTorchCharge < 0 && previousCharge >= 0)
        {
            onTorchDeplete?.Invoke();
            _animator.SetBool(HasTorch, false);
        }

        if (IsInChargeStationTile() && Input.GetKey(KeyCode.E))
        {
            _currentTorchCharge = burnDuration;
            onTorchRefuel?.Invoke();
            _animator.SetBool(HasTorch, true);
        }
        
    }


    private bool IsInChargeStationTile()
    {
        foreach (var torchChargingStation in _chargingStations)
        {
            Vector3 chargeStationPosition = torchChargingStation.gameObject.transform.position;
            Vector3 myPosition = transform.position;
            float xDiff = Math.Abs(myPosition.x - chargeStationPosition.x);
            float yDiff = Math.Abs(myPosition.y - chargeStationPosition.y);
            float zDiff = Math.Abs(myPosition.z - chargeStationPosition.z);

            if (xDiff < tileSize && yDiff < tileSize && zDiff < tileSize) return true;
        }

        return false;
    }

    public bool IsBurning() => _currentTorchCharge > 0;
}
