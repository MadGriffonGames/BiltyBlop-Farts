﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSpikes : MonoBehaviour
{
    Rigidbody2D MyRigidbody;
    bool shake = false;

    void Start()
    {
        MyRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (shake)
        {
            Vector3 rnd = Random.insideUnitCircle * 0.03f;
            transform.localPosition += new Vector3(rnd.x, rnd.y, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Fall());
        }
    }

    IEnumerator Fall()
    {
        shake = true;
        yield return new WaitForSeconds(0.3f);
        shake = false;
        MyRigidbody.bodyType = RigidbodyType2D.Dynamic;
        MyRigidbody.gravityScale = 4;
    }

    public void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}