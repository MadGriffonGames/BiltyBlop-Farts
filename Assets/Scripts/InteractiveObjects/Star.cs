﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : InteractiveObject
{
	[SerializeField]
	private GameObject crystalParticle;

	public override void Start ()
	{
		base.Start();
	}

	public void OnTriggerEnter2D(Collider2D other)
	{
		if(other.transform.CompareTag("Player") && !other.transform.CompareTag("Sword"))
		{
			animator.SetTrigger("collected");
            Player.Instance.collectables++;
		}   
	}

	public void MakeFX()
	{
		Instantiate(crystalParticle, this.gameObject.transform.position + new Vector3(0, 0.2f, 1), Quaternion.Euler(new Vector3 (0, 0 , 0)));
	}

    public void ShowStar()
    {
        CollectsUI.Instance.ShowStar(Player.Instance.collectables);
    }

	public void DestroyObject()
	{
		SoundManager.PlaySound ("star_collect");
        ShowStar();
		Destroy(this.gameObject);
	}
}