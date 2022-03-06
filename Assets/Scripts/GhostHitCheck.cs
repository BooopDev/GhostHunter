using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostHitCheck : MonoBehaviour
{
    public LayerMask mask;
    public static bool hasGhostBeenHit;

    public void Update()
    {
        if (!GameOver.hasGameEnded)
        {
            RaycastHit hit;
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            if (Physics.Raycast(transform.position, forward, out hit, 10, mask, QueryTriggerInteraction.Collide) && Input.GetMouseButton(0) && !LaserToggle.overHeated)
            {
                if (hit.collider.CompareTag("Ghost"))
                {
                    hasGhostBeenHit = true;
                }
                else
                {
                    hasGhostBeenHit = false;
                }
            }
            else
            {
                hasGhostBeenHit = false;
            }
        }
    }
}
