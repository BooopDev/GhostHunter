using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public float pickupRadius;
    public LayerMask layer;
    private GameObject pickup;

    void Update()
    {
        if (Physics.CheckSphere(transform.position, pickupRadius, layer, QueryTriggerInteraction.Collide))
        {
            Collider[] pickups = Physics.OverlapSphere(transform.position, pickupRadius, layer, QueryTriggerInteraction.Collide);
            pickup = pickups[0].gameObject;
        }
        else
        {
            pickup = null;
        }

        if(pickup != null && Input.GetKeyDown(KeyCode.Space))
        {
            ChestCheck chest = pickup.GetComponent<ChestCheck>();
            chest.playerOpened = true;
            chest.hasOpened = true;
        }
    }
}
