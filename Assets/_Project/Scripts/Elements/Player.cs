using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Camera mainCamera;

    public Rigidbody rb;
    public float speed;
    public float jumpForce;
    public float jumpGravityMultiplier;
    public int jumpCount;
    public float maxYVelocity;
    public LayerMask lookAtLayerMask;

    private Vector3 _direction;
    private bool _isTouchingGround;
    private int _currentJumpCount;

    public void RestartPlayer()
    {
        transform.position = Vector3.zero;
    }

    private void Update()
    {
        MovePlayer();
        CheckIfTouchingGround();
        CheckForJump();
        ApplyJumpGravity();
        ClampVelocity();
        LookAtMouse();
    }

    private void LookAtMouse()
    {
        var ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray.origin, ray.direction, out var hitInfo, 50, lookAtLayerMask))
        {
            var lookPos = hitInfo.point;
            lookPos.y = 0;
            transform.LookAt(lookPos);
        }
    }

    private void ClampVelocity()
    {
        if (rb.linearVelocity.y < -maxYVelocity)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x , -maxYVelocity, rb.linearVelocity.z);
        }
    }

    private void CheckIfTouchingGround()
    {
        _isTouchingGround = Physics.Raycast(transform.position + Vector3.up * .5f, Vector3.down, 1);
        if (_isTouchingGround && rb.linearVelocity.y < 0)
        {
            _currentJumpCount = 0;
        }
    }

    private void ApplyJumpGravity()
    {
        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y * jumpGravityMultiplier, rb.linearVelocity.z);
        }
    }

    private void CheckForJump()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame 
            && _isTouchingGround)
        {
            Jump();
        }
        else if (Keyboard.current.spaceKey.wasPressedThisFrame
            && _currentJumpCount < jumpCount)
        {
            Jump();
        }     
        
    }

    private void Jump()
    {
        //rb.AddForce(Vector3.up * jumpForce);
        rb.linearVelocity = new Vector3 (rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
        _currentJumpCount++;
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
