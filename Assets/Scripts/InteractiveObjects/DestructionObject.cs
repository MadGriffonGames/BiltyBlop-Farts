﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionObject : InteractiveObject
{
    [SerializeField]
    private HP hp;

    [SerializeField]
    private Coin coin;

    [SerializeField]
    public GameObject chips;

    public override void Start()
    {
        base.Start();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Sword")
        {

            if (UnityEngine.Random.Range(1, 100) < 40)//spawn or not hearts or coins
            {
                if (UnityEngine.Random.Range(1, 100) < 90)
                    Instantiate(coin, this.gameObject.transform.position, Quaternion.identity);
                else
                    Instantiate(hp, this.gameObject.transform.position, Quaternion.identity);
            }
            CameraEffect.Shake(0.2f, 0.2f);
            Instantiate(chips, this.gameObject.transform.position + new Vector3(0, 0.5f , 0), Quaternion.identity);
			SoundManager.PlaySound ("wooden_box");
            Destroy(this.gameObject);
        }
    }
}
