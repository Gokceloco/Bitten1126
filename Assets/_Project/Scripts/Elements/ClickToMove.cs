using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClickToMove : MonoBehaviour
{
    public LayerMask clickMask;

    private Vector3 _targetPos;
    public float speed;

    private void Update()
    {
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            SetTargetPos();
        }

    }

    private void FixedUpdate()
    {
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        if ((_targetPos - transform.position).magnitude > .2f)
        {
            var direction = (_targetPos - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
            transform.LookAt(_targetPos);
        }
        
    }

    private void SetTargetPos()
    {
        var ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray.origin, ray.direction, out var hitInfo, 50, clickMask))
        {
            var movePos = hitInfo.point;
            movePos.y = 0;
            _targetPos = movePos;
        }
    }
}
