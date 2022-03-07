using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    private bool sensePlayer;

    public GameObject player;
    public int increment = 5;

    private void Start()
    {
        sensePlayer = false;
    }

    void Update()
    {
        if (sensePlayer)
        {
            player.GetComponent<PlayerStats>().UpdateStat(gameObject.tag, increment);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            sensePlayer = true;
        }
    }
}