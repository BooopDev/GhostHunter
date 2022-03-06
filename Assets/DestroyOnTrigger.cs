using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTrigger : MonoBehaviour
{
    public GameObject[] objects;
    public float delay;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Invoke("DestroyObjects", delay);
        }
    }

    void DestroyObjects()
    {
        foreach (GameObject obj in objects)
        {
            Destroy(obj);
        }
    }
}