using System;
using System.Collections;
using System.Collections.Generic;
using Runtime.LightSystem;
using Runtime.Movables;
using UnityEngine;

public class LightEmitter : MonoBehaviour
{
    public GameObject defaultLightBeam;

    private List<GameObject> _beams;

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
            RaycastHit[] hits = Physics.RaycastAll(beam.transform.position, -beam.transform.up, 100);

            if (hits.Length == 0) return;

            RaycastHit closest = hits[0];
            foreach (RaycastHit hit in hits)
            {
                if (Vector3.Distance(hit.point, beam.transform.position) < Vector3.Distance(beam.transform.position, closest.point))
                {
                    closest = hit;
                }
            }
            float distance = Vector3.Distance(closest.point, beam.transform.position);
            Vector3 currentScale = defaultLightBeam.transform.localScale;
            currentScale.y = distance / transform.localScale.z / 2;
            defaultLightBeam.transform.localScale = currentScale;

            Debug.Log("updating beam");
            if (closest.collider.gameObject.TryGetComponent(out ILightReceiver lightReceiver))
            {
                Debug.Log("has the script");
                lightReceiver.LightReceive(transform.position);
            }
        }
    }
}