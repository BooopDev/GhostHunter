﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health = 100;

    void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void DoDamage(int damage)
    {
        health -= damage;
    }
}
