using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
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
    private bool EnemyLocated;

    void Fire()
    {
        if (EnemyLocated)
        {
            Debug.Log("HitEnemy");
            Health health = hit.collider.GetComponent<Health>();
            health.DoDamage(bulletDamage);
            GameObject splat = Instantiate(bloodSplat, hit.point, hit.transform.rotation, hit.collider.transform);
            splat.transform.rotation = Quaternion.FromToRotation(transform.forward, hit.normal);
            splat.transform.position += (splat.transform.forward * 0.05f);
        }
        audioSource.PlayOneShot(shootSound);
        Instantiate(muzzleFlash, muzzleFlashLocation.position, muzzleFlashLocation.rotation, muzzleFlashLocation);
    }


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
            if(hit.collider.CompareTag("Enemy"))
            {
                EnemyLocated = true;
            }
            else
            {
                EnemyLocated = false;
            }
        }
        else
        {
            EnemyLocated = false;
        }
    }

    IEnumerator Shooting()
    {
        while (gameObject)
        {
            if (Input.GetMouseButton(0))     //Player Logic
            {
                Fire();
                yield return new WaitForSeconds(shootDelay);
            }
            yield return null;
        }
        yield return null;
    }

    
}
