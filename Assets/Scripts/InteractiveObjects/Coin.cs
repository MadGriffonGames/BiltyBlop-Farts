﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : InteractiveObject
{

	public override void Start ()
    {
        base.Start();
	}

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" && other.gameObject.tag != "Sword")
        {
            animator.SetTrigger("collected");
            GameManager.CollectedCoins++;
			SoundManager.PlaySound ("coin_collect");
        }   
    }

    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }
}
