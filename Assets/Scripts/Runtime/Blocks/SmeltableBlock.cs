using System;
using Runtime.Movables;
using UnityEngine;
using UnityEngine.Events;

public class SmeltableBlock : MonoBehaviour, IBlock
{
    [SerializeField, Range(2, 10)] private float requiredPlayerSmeltRange;
    [SerializeField] private float smeltTime = 2f;
    [SerializeField] private ParticleSystem smeltParticle;
    [SerializeField] private GameObject meltSoundObject;
    [NonSerialized] public bool forceSmelt = false;

    public UnityEvent onBlockMeltComplete = new UnityEvent();
    
    private GameObject _player;
    private float _smeltedFor;
    private MeshMorphingDynamic _meshMorphingDynamic;
    private TorchPower _torchPower;
    private AudioSource _meltSoundSource;


    private static readonly int Smelting = Animator.StringToHash("Smelting");
    private static readonly float ForceSmeltMultiplier = 2.8f;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _meshMorphingDynamic = GetComponentInChildren<MeshMorphingDynamic>();
        _torchPower = FindObjectOfType<TorchPower>();
        
        meltSoundObject.transform.parent = null;
        _meltSoundSource = meltSoundObject.GetComponent<AudioSource>();
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, _player.transform.position);
        if ((distance < requiredPlayerSmeltRange && Input.GetKey(KeyCode.E) && _torchPower.IsBurning()) || forceSmelt)
        {

            _meshMorphingDynamic.StartUpdating();
            _meshMorphingDynamic.UnPauseUpdating();
            _smeltedFor += (forceSmelt) ? Time.deltaTime * ForceSmeltMultiplier : Time.deltaTime;

            if (!smeltParticle.isPlaying) smeltParticle.Play();
            if (!_meltSoundSource.isPlaying) _meltSoundSource.Play();

            if (_smeltedFor > smeltTime)
            {
                OnUpdate();
                onBlockMeltComplete?.Invoke();
                Destroy(this.gameObject);
            }
        }
        else
        {
            smeltParticle.Stop();
            _meshMorphingDynamic.PauseUpdating();
            if (_meltSoundSource.clip != null) _meltSoundSource.Stop();
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


    private void OnDestroy()
    {
        
    }
}