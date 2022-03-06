using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject playerCamera;
    public int damage;
    public AudioSource shootSound;
    public AudioClip shooting;
    public GameObject decal;
    public float decalOffset;

    void Update()
    {
        RaycastHit hit;
        if(Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.TransformDirection(Vector3.forward), out hit))
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    Health enemyHealth = hit.collider.gameObject.GetComponent<Health>();
                    enemyHealth.health -= damage;
                    shootSound.PlayOneShot(shooting);
                    Vector3 offsetPoint = Vector3.Lerp(hit.point, playerCamera.transform.position, decalOffset);
                    Instantiate(decal, offsetPoint, Quaternion.LookRotation(hit.normal));
                }
            }
        }
    } 
}
