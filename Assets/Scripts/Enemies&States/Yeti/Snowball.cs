﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Snowball : MonoBehaviour
{

    public Vector3 startPosition;

    [SerializeField]
    public GameObject particle;

    private Vector2 direction;



    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("GroundYeti"))
        {
            Instantiate(particle, this.gameObject.transform.position + new Vector3(0, 0, 0), Quaternion.identity);
            this.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Instantiate(particle, this.gameObject.transform.position + new Vector3(0, -0.7f, 0), Quaternion.identity);
            this.gameObject.SetActive(false);
        }
    }

    public void Throw(Vector3 startPos, Vector2 power)
    {
        this.transform.position = startPos;
        gameObject.SetActive(true);
        this.GetComponent<Rigidbody2D>().velocity += power;
    }
}
