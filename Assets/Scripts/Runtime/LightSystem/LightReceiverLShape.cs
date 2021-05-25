using System.Collections;
using System.Collections.Generic;
using Runtime.LightSystem;
using UnityEngine;

public class LightReceiverLShape : MonoBehaviour, ILightReceiver
{ 
    public void LightReceive(Vector3 from)
    {
    }

    public void LightDisconnect()
    {
        throw new System.NotImplementedException();
    }
}
