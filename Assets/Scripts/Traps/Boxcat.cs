using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boxcat : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        float maxDistance = 10f;
        RaycastHit hit;

        bool isHIt = Physics.BoxCast(center: transform.position, halfExtents: transform.lossyScale / 2, direction: transform.forward, out hit, transform.rotation, maxDistance);

        if (isHIt)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(from: transform.position, direction: transform.forward * hit.distance);
            Gizmos.DrawWireCube(center: transform.position + transform.forward * hit.distance, size: transform.lossyScale);
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(from: transform.position, direction: transform.forward * maxDistance);
        }


    }
}
