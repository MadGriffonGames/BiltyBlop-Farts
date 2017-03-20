﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : InteractiveObject
{
    [SerializeField]
    GameObject lightPillar;

    bool activated = false;

    public override void Start()
    {
        base.Start();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            if (!activated)
            {
                lightPillar.SetActive(true);
                Player.Instance.CheckpointPosition = this.gameObject.transform.position;
            }
        }
    }
}