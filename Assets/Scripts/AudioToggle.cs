using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioToggle : MonoBehaviour
{
    private AudioSource[] source;
    [Range(0, 2f)] public float volume = 1f;
    public AudioClip[] beamClips;
    public AudioClip beamStart;
    public AudioClip beamEnd;
    void Start()
    {
        source = new AudioSource[beamClips.Length];
        for (int i = 0; i < source.Length; i++)
        {
            source[i] = gameObject.AddComponent<AudioSource>();
            source[i].volume = volume;
            source[i].playOnAwake = false;
            source[i].loop = true;
            source[i].clip = beamClips[i];
        }
    }

    private bool shooting;
    private bool startedShooting;
    private bool hasOverheated;

    void Update()
    {
        if(!GameOver.hasGameEnded)
        {
            if (!LaserToggle.overHeated)
            {
                hasOverheated = false;
            }

            if (Input.GetMouseButton(0) && !hasOverheated)
            {
                shooting = true;
            }

            else
            {
                startedShooting = false;
                shooting = false;
            }

            if (shooting && !startedShooting)
            {

                hasOverheated = false;
                startedShooting = true;
                source[0].PlayOneShot(beamStart);
                for (int i = 0; i < source.Length; i++)
                {
                    source[i].Play();
                }
            }

            if (LaserToggle.overHeated && !hasOverheated)
            {
                shooting = false;
                startedShooting = false;
                hasOverheated = true;
                for (int i = 0; i < source.Length; i++)
                {
                    source[i].Stop();
                }
                source[0].PlayOneShot(beamEnd);
            }

            if (Input.GetMouseButtonUp(0) && !hasOverheated)
            {
                for (int i = 0; i < source.Length; i++)
                {
                    source[i].Stop();
                }
                source[0].PlayOneShot(beamEnd);
            }
        }
        else
        {
            if(GetComponent<AudioSource>())
            {
                Destroy(GetComponent<AudioSource>());
            }
        }
    }
}
