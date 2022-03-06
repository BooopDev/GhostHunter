using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject ghost;
    private GameObject[] chests;
    private ChestCheck ghostChest;
    void Start()
    {
        chests = GameObject.FindGameObjectsWithTag("Chest");
        ResetChest();
    }

    public void ResetChest()
    {
        ghostChest = chests[Random.Range(0, chests.Length - 1)].GetComponent<ChestCheck>();
        ghostChest.energyStored = 10;
        ghostChest.hasGhost = true;
    }

}
