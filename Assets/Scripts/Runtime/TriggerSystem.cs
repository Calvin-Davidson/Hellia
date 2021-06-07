using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerSystem : MonoBehaviour
{
   public LayerMask layerMask;
   public UnityEvent onTriggerEvent;
   
   private void OnTriggerEnter(Collider other)
   {
      if (IsInLayer(other.gameObject.layer, layerMask))
      {
         onTriggerEvent?.Invoke();
      }
   }
   
   public static bool IsInLayer(int layer, LayerMask layermask)
   {
      return layermask == (layermask | (1 << layer));
   }

}
