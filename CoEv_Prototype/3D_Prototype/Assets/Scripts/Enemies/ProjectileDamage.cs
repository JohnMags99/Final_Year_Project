using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    public int damage;
    public Transform player;

    private void Update()
    {
        Collider col = gameObject.GetComponent<Collider>();
        
        if (col.gameObject.CompareTag("Player"))
        {
            player.GetComponent<PlayerStats>().TakeDamage(damage);
        }
    }
}
