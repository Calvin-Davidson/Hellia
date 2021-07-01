using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallableBlock
{
   public bool CheckShouldFall(Transform transform)
    {
        RaycastHit[] hits = new RaycastHit[10];
        Physics.RaycastNonAlloc(transform.position, new Vector3(0, -10, 0), hits, 10f);

        float closestDistance = 100000.0f;

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider == null) continue;

            if (Vector3.Distance(hits[i].collider.gameObject.transform.position, transform.position) < closestDistance)
                closestDistance = Vector3.Distance(hits[i].collider.gameObject.transform.position, transform.position);
        }
        
        return (closestDistance > 4f);
    }
}
