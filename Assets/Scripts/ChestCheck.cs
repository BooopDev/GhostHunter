using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChestCheck : MonoBehaviour
{
    public bool hasOpened = false;
    public bool hasGhost = false;
    [HideInInspector]
    public Animator anim;
    private bool opened = false;
    private GameManager manager;
    [HideInInspector]
    public float energyStored;
    public bool playerOpened;
    private bool audioPlaying;
    private AudioSource source;
    public AudioClip cry;
    public AudioClip scream;
    GameObject player;
    GameObject chase;

    void Start()
    {
        chase = GameObject.FindGameObjectWithTag("Chase");
        player = GameObject.FindGameObjectWithTag("Player");
        source = GetComponent<AudioSource>();
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameOver.hasGameEnded)
        {
            if (hasGhost)
            {
                if (!audioPlaying)
                {
                    StartAudio();
                    audioPlaying = true;
                }

                if (Vector3.Distance(player.transform.position, transform.position) < 3f && !Input.GetKey(KeyCode.LeftShift) && !opened)
                {
                    opened = true;
                    OpenChest();
                }
                if (energyStored < 10)
                {
                    energyStored += Time.deltaTime;
                }
                else
                {
                    energyStored = 10;
                }
            }
            else
            {
                energyStored = 0;
            }
            if (hasOpened && !opened)
            {
                opened = true;
                OpenChest();
            }
        }
    }

    void StartAudio()
    {
        source.clip = cry;
        source.spatialBlend = 1;
        source.Play();
    }

    void OpenChest()
    {
        if(hasGhost)
        {
            source.Stop();
            source.clip = null;
            hasGhost = false;
            if(!playerOpened)
            {
                energyStored += 5;
                source.spatialBlend = 0;
                source.PlayOneShot(scream);
                Invoke("ResetAudio", 5);
            }
            SpawnGhost();
        }
        anim.Play("Lid Open");
        Invoke("CloseChest", 15);
    }

    void ResetAudio()
    {
        audioPlaying = false;
    }

    void CloseChest()
    {
        opened = false;
        hasOpened = false;
        anim.Play("Lid Close");
    }

    void SpawnGhost()
    {
        chase.GetComponent<AudioSource>().Play();
        GameObject ghost = Instantiate(manager.ghost, transform.position - Vector3.forward, transform.rotation);
        GhostAI ghostAI = ghost.GetComponent<GhostAI>();
        ghostAI.energy = energyStored;
        Debug.Log("Ghost Spawned");
    }
}
