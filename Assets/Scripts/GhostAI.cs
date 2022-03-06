using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostAI : MonoBehaviour
{
    public TMPro.TextMeshPro text;
    private GameObject player;
    public ParticleSystem[] wrapSystems;
    private bool hasCaptureStarted;
    private bool attemptEscape;
    private float[] oldSystemValues;
    private NavMeshAgent nav;
    public Collider lasercol;
    public Material runMat;
    public Material normMat;
    private ChestCheck ghostChest = null;
    private Animator anim;
    GameObject chase;
    private void Start()
    {
        chase = GameObject.FindGameObjectWithTag("Chase");
        anim = GetComponent<Animator>();
        text.text = energy.ToString();
        MeshRenderer[] meshes = GetComponentsInChildren<MeshRenderer>();
        foreach(MeshRenderer mat in meshes)
        {
            if(!mat.gameObject.CompareTag("Text"))
            {
                mat.material = normMat;
            }
        }
        nav = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        oldSystemValues = new float[wrapSystems.Length];

        for (int i = 0; i < wrapSystems.Length; i++)
        {
            var emission = wrapSystems[i].emission;
            oldSystemValues[i] = emission.rateOverTime.constant;
            emission.rateOverTime = 0;
        }
    }
    private void Update()
    {
        if(energy > 0)
        {
            if(isHiding)
            {
                if(Vector3.Distance(nav.destination, transform.position) < 1)
                {
                    Reappear();
                }
            }
            if (GhostHitCheck.hasGhostBeenHit && !hasCaptureStarted)
            {
                hasCaptureStarted = true;
                attemptEscape = true;
                for (int i = 0; i < wrapSystems.Length; i++)
                {
                    var emission = wrapSystems[i].emission;
                    emission.rateOverTime = oldSystemValues[i];
                }
            }
            if (attemptEscape)
            {
                EscapeMovement();
            }
            else
            {
                Movement();
            }
            if (!attemptEscape && hasCaptureStarted)
            {
                hasCaptureStarted = false;
                Escape();
            }
        }
        else
        {
            Captured();
        }
    }

    public float rotateSpeed = 1;
    public float speed = 1;

    private void EscapeMovement()
    {
        hasRun = false;
        energy -= Time.deltaTime;
        Debug.Log(energy);
        text.text = Mathf.RoundToInt(energy).ToString();
        if (!GhostHitCheck.hasGhostBeenHit)
        {
            attemptEscape = false;
        }
        float distance = Mathf.Clamp(Vector3.Distance(transform.position, player.transform.position), 1, Mathf.Infinity);
        Vector3 target = transform.position + (player.transform.forward * 5);
        nav.destination = target;
    }

    bool hasRun = false;

    private void Movement()
    {
        if(!hasRun)
        {
            hasRun = true;
            GameObject[] chests = GameObject.FindGameObjectsWithTag("Chest");
            ghostChest = chests[Random.Range(0, chests.Length - 1)].GetComponent<ChestCheck>();
        }
        nav.destination = ghostChest.transform.position;
        if (Vector3.Distance(ghostChest.transform.position, transform.position) < 1)
        {
            Reappear();
        }
    }

    bool isHiding;
    private void Escape()
    {
        MeshRenderer[] meshes = GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer mat in meshes)
        {
            if (!mat.gameObject.CompareTag("Text"))
            {
                mat.material = runMat;
            }
            else
            {
                mat.gameObject.SetActive(false); 
            }
        }
        for (int i = 0; i < wrapSystems.Length; i++)
        {
            var emission = wrapSystems[i].emission;
            emission.rateOverTime = 0;
        }
        lasercol.gameObject.SetActive(false);
        GameObject[] chests = GameObject.FindGameObjectsWithTag("Chest");
        ghostChest = chests[Random.Range(0, chests.Length - 1)].GetComponent<ChestCheck>();
        if(!ghostChest.hasOpened)
        {
            nav.destination = ghostChest.transform.position;
            isHiding = true;
        }
        else
        {
            Invoke("Escape", 1f);
        }
    }

    private void Reappear()
    {
        isHiding = false;
        ghostChest.hasGhost = true;
        ghostChest.energyStored = energy;
        hasRun = false;
        anim.Play("Shrink");
        chase.GetComponent<AudioSource>().Stop();
        ghostChest.anim.Play("Lid Close");
        Destroy(gameObject, 0.8f);
    }

    private void Captured()
    {
        GameOver.EndGame("You Captured The Ghost!");
        chase.GetComponent<AudioSource>().Stop();
        Destroy(gameObject);
    }

    [HideInInspector]
    public float energy;


}
