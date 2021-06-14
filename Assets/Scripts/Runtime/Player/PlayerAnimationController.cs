using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator _animator;
    private static int KickAnimationID = Animator.StringToHash("KickBlock");

    private void Awake()
    {
        _animator = this.GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        GameControl.Instance.onBlockStartMove.AddListener(OnBlockMove);
    }

    public void OnBlockMove()
    {
        _animator.SetTrigger(KickAnimationID);
    }
}
