using System;
using Runtime.Movables;
using UnityEngine;

public class SmeltableBlock : MonoBehaviour, IBlock
{
    [SerializeField, Range(2, 10)] private float requiredPlayerSmeltRange;
    [SerializeField] private float smeltTime = 2f;

    private GameObject _player;
    private float _smeltedFor;
    private Animator _animator;
    private TorchPower _torchPower;

    private static readonly int Smelting = Animator.StringToHash("Smelting");


    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _animator = GetComponent<Animator>();
        _torchPower = FindObjectOfType<TorchPower>();
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, _player.transform.position);
        if (distance < requiredPlayerSmeltRange && Input.GetKey(KeyCode.E) && _torchPower.IsBurning())
        {
            _animator.SetBool(Smelting, true);
            _smeltedFor += Time.deltaTime;
            if (_smeltedFor > smeltTime)
            {
                OnUpdate();
                Destroy(this.gameObject);
            }
        }
        else
        {
            _animator.SetBool(Smelting, false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, requiredPlayerSmeltRange);
    }

    public void OnUpdate()
    {
        GameControl.Instance.onBlockUpdate.Invoke();
    }
}