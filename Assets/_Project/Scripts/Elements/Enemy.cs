using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int startHealth;
    private int _currentHealth;

    public ActionState actionState;

    public NavMeshAgent navAgent;

    private AnimationState _curAnimationState;

    public float hitSuccessAngle;

    private void Start()
    {
        _currentHealth = startHealth;
        GetComponentInChildren<Healthbar>().SetHealth(1);
    }

    private void Update()
    {
        MakeDecision();
        Execute();
    }

    private void Execute()
    {
        if (actionState == ActionState.Walk)
        {
            var playerPos = GameDirector.instance.player.transform.position;

            navAgent.isStopped = false;
            navAgent.SetDestination(playerPos);
        }
        else if (actionState == ActionState.Attack)
        {
            navAgent.isStopped = true;
        }
    }

    private void MakeDecision()
    {
        var playerDistance = (GameDirector.instance.player.transform.position - transform.position).magnitude;

        //Decision
        if (playerDistance < 2)
        {
            ChangeAnimationState(AnimationState.Attack);
            actionState = ActionState.Attack;
        }
        else if (playerDistance < 10)
        {
            ChangeAnimationState(AnimationState.Walk);
            actionState = ActionState.Walk;
        }
    }

    void ChangeAnimationState(AnimationState desiredAnimationState)
    {
        if (desiredAnimationState != _curAnimationState)
        {
            if (desiredAnimationState == AnimationState.Walk)
            {
                GetComponentInChildren<Animator>().CrossFade("Walk", .1f);
            }
            else if (desiredAnimationState == AnimationState.Idle)
            {
                GetComponentInChildren<Animator>().CrossFade("Idle", .1f);
            }
            else if (desiredAnimationState == AnimationState.Attack)
            {
                GetComponentInChildren<Animator>().CrossFade("Attack", .1f);
            }
            _curAnimationState = desiredAnimationState;
        }
    }

    public void GetHit(int damage)
    {
        if (actionState == ActionState.Idle)
        {
            ChangeAnimationState(AnimationState.Walk);
            actionState = ActionState.Walk;
        }

        _currentHealth -= damage;

        GetComponentInChildren<Healthbar>().SetHealth((float)_currentHealth / startHealth);

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }

    public void TryHitPlayer()
    {
        var angle = Vector3.Angle(transform.forward, GameDirector.instance.player.transform.position - transform.position);
        if (angle < hitSuccessAngle)
        {
            GameDirector.instance.player.GetHit();
        }
    }

    public void AttackStarted()
    {
        transform.DOLookAt(GameDirector.instance.player.transform.position, .2f);
    }
}


public enum ActionState
{
    Idle,
    Walk,
    Attack,
}

public enum AnimationState
{
    Idle,
    Walk,
    Attack,
}