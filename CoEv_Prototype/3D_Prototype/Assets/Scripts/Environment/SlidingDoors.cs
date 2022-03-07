using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoors : MonoBehaviour
{

    public GameObject door;

    public float maxOpen = 4f;
    public float maxClose = 1f;
    public float moveSpeed = 10f;

    private bool sensePlayer;

    private void Start()
    {
        sensePlayer = false;
    }

    void Update()
    {
        if (sensePlayer)
        {
            if (door.transform.position.y < maxOpen)
            {
                door.transform.Translate(0f, moveSpeed * Time.deltaTime, 0f);
            }
        }
        else
        {
            if (door.transform.position.y > maxClose)
            {
                door.transform.Translate(0f, -moveSpeed * Time.deltaTime, 0f);
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            sensePlayer = true;
        }
    }
    
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            sensePlayer = false;
        }
    }
}
