using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int startHealth;
    private int _currentHealth;

    public ActionState actionState;

    public NavMeshAgent navAgent;

    private void Start()
    {
        _currentHealth = startHealth;
       
    }

    private void Update()
    {
        MakeDecision();

        //Execution
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
            if (actionState != ActionState.Attack)
            {
                GetComponentInChildren<Animator>().CrossFade("Idle", .1f);
            }
            actionState = ActionState.Attack;
        }
        else if (playerDistance < 10)
        {
            if (actionState != ActionState.Walk)
            {
                GetComponentInChildren<Animator>().CrossFade("Walk", .1f);
            }
            actionState = ActionState.Walk;
        }
    }

    public void GetHit(int damage)
    {
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
}


public enum ActionState
{
    Idle,
    Walk,
    Attack,
}