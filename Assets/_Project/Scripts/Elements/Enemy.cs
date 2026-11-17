using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int startHealth;
    private int _currentHealth;

    public float speed;

    public ActionState actionState;

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
            var direction = (GameDirector.instance.player.transform.position - transform.position).normalized;
            direction.y = 0;
            transform.position += direction * Time.deltaTime * speed;
        }
    }

    private void MakeDecision()
    {
        var playerDistance = (GameDirector.instance.player.transform.position - transform.position).magnitude;

        //Decision
        if (playerDistance < 2)
        {
            actionState = ActionState.Attack;
        }
        else if (playerDistance < 10)
        {
            actionState = ActionState.Walk;
        }
    }

    public void GetHit(int damage)
    {
        _currentHealth -= damage;
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