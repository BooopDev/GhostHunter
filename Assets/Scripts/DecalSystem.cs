using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecalSystem : MonoBehaviour
{
    public GameObject decal;
    [Range(0.01f, 0.1f)]
    public float decalOffset = 0.01f;
    public float decalProximity = 0.2f;
    public ParticleSystem laser;
    private bool foundDecal;
    public LayerMask decalLayerMask;
    public AudioSource decalSource;
    public Slider slider;
    private List<ParticleCollisionEvent> particleCollisions = new List<ParticleCollisionEvent>();

    private void Start()
    {
        fill.color = startColour;
    }
    private void OnParticleCollision(GameObject other)
    {
        int numCollisions = laser.GetCollisionEvents(other, particleCollisions);
        int i = 0;
        while (i < numCollisions)
        {
            decalSource.volume = 1;
            Collider[] decalColliders = Physics.OverlapSphere(particleCollisions[i].intersection, decalProximity, decalLayerMask ,QueryTriggerInteraction.Collide);
            for (int i2 = 0; i2 < decalColliders.Length; i2++)
            {
                if(decalColliders[i2].CompareTag("Decal"))
                {
                    foundDecal = true;
                    Debug.Log("foundDecal");
                }
            }
            if (!foundDecal)
            {
                Damage();
                Vector3 particlePos = particleCollisions[i].intersection;
                Vector3 offsetPoint = particlePos += particleCollisions[i].normal * decalOffset;
                Instantiate(decal, offsetPoint, Quaternion.LookRotation(-particleCollisions[i].normal));
                Debug.Log("hit");
                decalSource.transform.position = offsetPoint;
            }
            foundDecal = false;
            i++;
        }
    }

    private void Update()
    {
        if (decalSource.volume < 0.1f)
        {
            decalSource.volume = 0;
        }
        else
        {
            decalSource.volume -= 0.5f * Time.deltaTime;
        }
    }

    float value = 0;
    public Image fill;
    public Color startColour;
    public Color endColour;
    public GameObject danger;
    public AudioClip beep;

    private void Damage()
    {
        if(value > 0.8f)
        {
            startColour = Color.red / 2;
            danger.SetActive(true);
            if(!GetComponent<AudioSource>())
            {
                AudioSource source = gameObject.AddComponent<AudioSource>();
                source.volume = 0.1f;
                source.pitch = 0.7f;
                source.loop = true;
                source.clip = beep;
                source.Play();
            }
        }
        else
        {
            danger.SetActive(false);
        }
        value = Mathf.Clamp(value + 0.003f, 0, 1);
        slider.value = value;
        fill.color = Color.Lerp(startColour, endColour, value);
        if(value > 0.99f)
        {
            Destroy(GetComponent<AudioSource>());
            GameOver.EndGame("Too much Damage");
        }
    }
}
