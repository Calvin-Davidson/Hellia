using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneLoadingTrigger : MonoBehaviour
{
    [SerializeField] private LayerMask playerMask;
    void OnTriggerEnter(Collider collision)
    {
        if (IsInLayer(collision.gameObject.layer, playerMask))
        {
            LevelSystem.SetLevelCompleted(SceneManager.GetActiveScene().name);
            GameControl.Instance.onNextLevel?.Invoke();
        }
    }
    public static bool IsInLayer(int layer, LayerMask layermask)
    {
        return layermask == (layermask | (1 << layer));
    }
}
