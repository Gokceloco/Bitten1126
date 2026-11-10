using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Rigidbody rb;
    public float speed;

    private Vector3 _direction;

    public void RestartPlayer()
    {
        transform.position = Vector3.zero;
    }

    private void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        _direction = Vector3.zero;

        if (Keyboard.current.wKey.isPressed)
        {
            _direction += Vector3.forward;
        }
        if (Keyboard.current.aKey.isPressed)
        {
            _direction += Vector3.left;
        }
        if (Keyboard.current.sKey.isPressed)
        {
            _direction += Vector3.back;
        }
        if (Keyboard.current.dKey.isPressed)
        {
            _direction += Vector3.right;
        }

        var yVelocity = rb.linearVelocity;
        yVelocity.x = 0;
        yVelocity.z = 0;

        rb.linearVelocity = _direction.normalized * speed + yVelocity;
    }    
}
