using System;
using System.Collections;
using System.Collections.Generic;
using Runtime.LightSystem;
using Runtime.Movables;
using UnityEngine;

public class LightEmitter : MonoBehaviour, ILightComponent
{
    public GameObject defaultLightBeam;
    [SerializeField] private LayerMask playerLayerMask;

    private List<GameObject> _beams;
    private ILightReceiver lightReceiver;
    private void Awake()
    {
        _beams = new List<GameObject> {defaultLightBeam};
        UpdateLightBeams();
    }

    private void Start()
    {
        GameControl.Instance.onBlockUpdate?.AddListener(UpdateLightBeams);
    }

    public void UpdateLightBeams()
    {
        foreach (GameObject beam in _beams)
        {
            RaycastHit[] hits = Physics.RaycastAll(beam.transform.position, -beam.transform.up, 1000, ~playerLayerMask);

            if (hits.Length == 0) return;

            RaycastHit closest = hits[0];
            foreach (RaycastHit hit in hits)
            {
                if (Vector3.Distance(hit.point, beam.transform.position) < Vector3.Distance(beam.transform.position, closest.point))
                {
                    closest = hit;
                }
            }
            ILightReceiver receiver = closest.collider.gameObject.GetComponent<ILightReceiver>();
            if (receiver != null)
            {
                if (this.lightReceiver != null && this.lightReceiver != receiver) 
                    this.lightReceiver.LightDisconnect(this);

                receiver.LightReceive(this);
                this.lightReceiver = receiver;
            }
            else
            {
                this.lightReceiver?.LightDisconnect(this);
                this.lightReceiver = null;
            }
            
            if (closest.collider.gameObject.TryGetComponent(out SmeltableBlock smeltableBlock))
            {
                smeltableBlock.forceSmelt = true;
                if (closest.collider.gameObject.TryGetComponent(out MovableBlock movableBlock)) movableBlock.canBePushed = false;
            }

            float distance = Vector3.Distance(closest.point, beam.transform.position);
            Vector3 currentScale = defaultLightBeam.transform.localScale;
            currentScale.y = distance / transform.localScale.z / 2;
            defaultLightBeam.transform.localScale = currentScale;
        }
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}