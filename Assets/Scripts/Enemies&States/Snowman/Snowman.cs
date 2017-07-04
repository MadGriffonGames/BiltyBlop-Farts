﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class Snowman : MovingMeleeEnemy
{

    private ISnowmanState currentState;
    [SerializeField]
    private GameObject snowmanParticle;
    bool damaged = false;
    public bool walk = false;

    void Awake()
    {
        armature = GetComponent<UnityArmatureComponent>();
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<BoxCollider2D>(), true);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<CapsuleCollider2D>(), true);
    }

    public override void Start()
    {
        base.Start();
        isAttacking = false;
        ChangeState(new SnowmanPatrolState());
    }

    void Update()
    {
        if (!IsDead)
        {
            if (!TakingDamage && !Attack)
            {
                currentState.Execute();
            }
            LookAtTarget();
        }
    }

    public void ChangeState(ISnowmanState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }
        currentState = newState;
        currentState.Enter(this);
    }

    public override IEnumerator TakeDamage()
    {
        if (!damaged)
        {
            damaged = true;
            health -= Player.Instance.damage;
            StartCoroutine(AnimationDelay());
            CameraEffect.Shake(0.2f, 0.3f);
            SetHealthbar();
            if (Target == null)
            {
                ChangeDirection();
            }
            if (IsDead)
            {
                SoundManager.PlaySound("enemyher loud");
                //Instantiate(snowmanParticle, gameObject.transform.position + new Vector3(0, 1f, -1f), Quaternion.identity);
                SpawnCoins(3, 5);
                GameManager.deadEnemies.Add(gameObject);
                gameObject.SetActive(false);
            }
            yield return null;
        }
        yield return new WaitForSeconds(0.05f);
        damaged = false;
    }

    private void OnEnable()
    {
        ResetCoinPack();

        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<BoxCollider2D>(), true);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<CapsuleCollider2D>(), true);

        armature.animation.timeScale = 1;
        Target = null;
        damaged = false;
        AttackCollider.enabled = false;

        if (Health <= 0)
        {
            ChangeState(new SnowmanPatrolState());
            Health = 2;
        }

        SetHealthbar();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        currentState.OnCollisionEnter2D(other);
    }

    public void LocalMove()
    {
        if (!walk)
        {
            walk = true;
            armature.animation.FadeIn("walk", -1, -1);
        }
        transform.Translate(GetDirection() * (movementSpeed * Time.deltaTime));
    }
}