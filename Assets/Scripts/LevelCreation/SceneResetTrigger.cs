using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneResetTrigger : MonoBehaviour
{
    [SerializeField] private LayerMask playerMask;
    void OnTriggerEnter(Collider collision)
    {
        if (IsInLayer(collision.gameObject.layer, playerMask))
        {
            GameControl.Instance.onResetLevel?.Invoke();
        }
    }
    public static bool IsInLayer(int layer, LayerMask layermask)
    {
        return layermask == (layermask | (1 << layer));
    }
}
