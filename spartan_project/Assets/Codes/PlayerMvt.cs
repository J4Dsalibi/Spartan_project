using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMvt : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _speed = 7f;
    [SerializeField] private float dashDistance = 5f;
    [SerializeField] private float dashCooldown = 2f;

    private float dashCountdown = 0f;
    private Vector3 _input;
    private bool isMoving = false;

    void Update()
    {
        GatherInput();
        Look();

        // Check if the dash is allowed based on the countdown timer
        if (Input.GetKeyDown(KeyCode.Space) && dashCountdown <= 0)
        {
            Dash(); // Perform the dash
            dashCountdown = dashCooldown; // Set the cooldown
        }

        // Update the countdown timer
        if (dashCountdown > 0)
        {
            dashCountdown -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        Move(); // Perform movement based on gathered input
    }

    private void GatherInput()
    {
        _input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        isMoving = _input.magnitude > 0; // Determine if the player is moving
    }

    private void Look()
    {
        if (_input != Vector3.zero)
        {
            var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
            var skewedInput = matrix.MultiplyPoint3x4(_input);
            var relative = (transform.position + skewedInput) - transform.position;
            var rot = Quaternion.LookRotation(relative, Vector3.up);

            transform.rotation = rot; // Instantly adjust the rotation
        }
    }

    private void Move()
    {
        if (isMoving)
        {
            if (!CheckObstacle(transform.forward)) // Prevent collisions
            {
                _rb.MovePosition(transform.position + transform.forward * _speed * Time.deltaTime);
            }
        }
    }

    private bool CheckObstacle(Vector3 direction)
    {
        RaycastHit hit;
        return Physics.Raycast(transform.position, direction, out hit, 1f);
    }

    private void Dash()
    {
        Vector3 dashStartPosition = transform.position;
        Vector3 dashDestination = dashStartPosition + transform.forward * dashDistance;

        // Check for obstacles to avoid dashing into them
        RaycastHit hit;
        if (Physics.Raycast(dashStartPosition, transform.forward, out hit, dashDistance))
        {
            dashDestination = hit.point - transform.forward * 0.5f; // Adjust for obstacle
        }

        // Perform the dash
        transform.position = dashDestination;
    }
}