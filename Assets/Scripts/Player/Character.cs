﻿using System.Collections;
using System.Collections.Generic;
using DragonBones;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public abstract IEnumerator TakeDamage();

    public bool TakingDamage { get; set; }

    public UnityArmatureComponent myArmature;

    [SerializeField]
    GameObject armatureObject;

    [SerializeField]
    protected int health;

    public int Health
    {
        get
        {
            return health;
        }

        set
        {
            health = value;
        }
    }

    public abstract bool IsDead { get; }

    [SerializeField]
    private PolygonCollider2D attackCollider;

    public PolygonCollider2D AttackCollider
    {
        get
        {
            return attackCollider;
        }
    }

    [SerializeField]
    public List<string> damageSources;

    [SerializeField]
    protected float movementSpeed = 3.0f;

    protected bool facingRight;//chek direction(true if we look right)

    public bool Attack { get; set; }

    public virtual void Start ()
    {
        facingRight = true;
        myArmature = armatureObject.GetComponent<UnityArmatureComponent>();
        //myArmature.Dispose(false);//destroy all child game objects
        //UnityFactory.factory.BuildArmatureComponent(PlayerPrefs.GetString("Skin", "Classic"), null, null, null, armatureObject.gameObject);
        //myArmature.sortingLayerName = myArmature.sortingLayerName;
        //myArmature.sortingOrder = myArmature.sortingOrder;
    }

    public void MeleeAttack()
    {
        AttackCollider.enabled = true;
    }

    public void ChangeDirection()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (damageSources.Contains(other.tag))
        {
            StartCoroutine(TakeDamage());
        }
    }

    
}
