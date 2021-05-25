using System;
using Runtime.LightSystem;
using UnityEngine;

public class LightReceiverTShape : MonoBehaviour, ILightReceiver
{
    public GameObject[] lightBeams;
    
    public void LightReceive()
    {
        
    }

    public void LightDisconnect()
    {
    }
}
