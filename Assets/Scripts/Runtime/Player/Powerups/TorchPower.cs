using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchPower : MonoBehaviour
{
    [SerializeField] private float requiredChargingStationRange;
    [SerializeField, Header("Hoe hoger dit getal hoe langer de torch brand")] private float burnDuration = 1f;
    [SerializeField] private float tileSize = 4f;
    
    private TorchChargingStation[] _chargingStations;
    private float _currentTorchCharge;

    private void Awake()
    {
        _chargingStations = FindObjectsOfType<TorchChargingStation>();
        ;
    }

    private void Update()
    {
        _currentTorchCharge += Time.deltaTime;
        
        if (IsInChargeStationTile() && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("you torch has been charged");
            _currentTorchCharge = burnDuration;
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
