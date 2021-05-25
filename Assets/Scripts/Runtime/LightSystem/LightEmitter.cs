using System;
using System.Collections;
using System.Collections.Generic;
using Runtime.Movables;
using UnityEngine;

public class LightEmitter : MonoBehaviour, IBlock
{
    public GameObject defaultLightBeam;

    private List<GameObject> _beams;

    private void Awake()
    {
        _beams = new List<GameObject>();
        _beams.Add(defaultLightBeam);
        UpdateLightBeams();
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 direction = transform.forward;
        Vector3 pos = transform.position;

        Gizmos.DrawRay(pos, direction);

        Vector3 right = Quaternion.LookRotation(direction) * new Vector3(0, 0, 1);
        Vector3 left = Quaternion.LookRotation(direction) * new Vector3(0, 0, 1);
        Gizmos.DrawRay(pos + direction, right * 5);
        Gizmos.DrawRay(pos + direction, left * 5);
    }

    public void OnUpdate()
    {
    }

    public void UpdateLightBeams()
    {
        foreach (GameObject beam in _beams)
        {
            RaycastHit[] hits = Physics.RaycastAll(beam.transform.position, -beam.transform.up, 100);

            Debug.Log(hits.Length);
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
            Debug.Log(distance);
            
            Vector3 currentScale = defaultLightBeam.transform.localScale;
            currentScale.y = distance / transform.localScale.z / 2;
            defaultLightBeam.transform.localScale = currentScale;
        }
    }
}