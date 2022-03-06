using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    public float shootDelay;
    public int bulletDamage;
    public float range;
    public LayerMask collisionMask;
    public Transform muzzleFlashLocation;
    public GameObject muzzleFlash;
    public GameObject bloodSplat;
    public AudioSource audioSource;
    public AudioClip shootSound;

    private RaycastHit hit;
    private GameObject player;
    private bool PlayerLocated;

    private void Start()
    {
        StartCoroutine(Shooting());
    }
    void Update()
    {
        Check();
    }

    void Check()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, range, collisionMask, QueryTriggerInteraction.Collide))
        {
            if(hit.collider.CompareTag("Player"))
            {
                PlayerLocated = true;
            }
            else
            {
                PlayerLocated = false;
            }
        }
        else
        {
            PlayerLocated = false;
        }
    }

    IEnumerator Shooting()
    {
        while (gameObject)
        {
            if (PlayerLocated)
            {
                Fire();
                yield return new WaitForSeconds(shootDelay);
            }
            yield return null;
        }
        yield return null;
    }


    void Fire()
    {
        Debug.Log("HitEnemy");
        Health health = hit.collider.GetComponent<Health>();
        health.DoDamage(bulletDamage);
        audioSource.PlayOneShot(shootSound);
        Instantiate(muzzleFlash, muzzleFlashLocation.position, muzzleFlashLocation.rotation, muzzleFlashLocation);
    }
}
