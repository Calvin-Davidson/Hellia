using System;
using Runtime.Movables;
using UnityEngine;

public class SmeltableBlock : MonoBehaviour, IBlock
{
    [SerializeField, Range(2, 10)] private float requiredPlayerSmeltRange;
    [SerializeField] private float smeltTime = 2f;
    [SerializeField] private ParticleSystem smeltParticle;
    [NonSerialized] public bool forceSmelt = false;

    private GameObject _player;
    private float _smeltedFor;
    private MeshMorphingDynamic _meshMorphingDynamic;
    private TorchPower _torchPower;


    private static readonly int Smelting = Animator.StringToHash("Smelting");


    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _meshMorphingDynamic = GetComponentInChildren<MeshMorphingDynamic>();
        _torchPower = FindObjectOfType<TorchPower>();
        
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, _player.transform.position);
        if ((distance < requiredPlayerSmeltRange && Input.GetKey(KeyCode.E) && _torchPower.IsBurning()) || forceSmelt)
        {
            _meshMorphingDynamic.StartUpdating();
            _meshMorphingDynamic.UnPauseUpdating();
            _smeltedFor += Time.deltaTime;
            if (!smeltParticle.isPlaying) smeltParticle.Play();

            if (_smeltedFor > smeltTime)
            {
                OnUpdate();
                Destroy(this.gameObject);
            }
        }
        else
        {
            smeltParticle.Stop();
            _meshMorphingDynamic.PauseUpdating();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, requiredPlayerSmeltRange);
    }

    public void OnUpdate()
    {
        GameControl.Instance.BlockUpdateNextFrame();
    }

}