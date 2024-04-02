# Time_Bullet

1 - Bullet slowdown ( general code for all bullets)    

2 - Player mouvement -- (WASD) force based      
+ Dash mecanic (invulnerability)    
+ Holding weapon mecanic

3 - Enemy behavior -- Tracking player, spawn rate and position    

4 - Weapon Variety --    
+ Pistol   
+ AR   
+ Sniper    
+ SMG    
+ Missile Launcher    
+ Shotgun     

5 - Map generation (IMPORANT)     
- Define map's caracteristic for random generation
- Grid map and tiles
- 



using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_mvt : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float dashDistance = 5f;
    private bool DashWasPressed;
    private Vector3 _input;
    private bool isMoving = false;


    void Update()
    {
        GatherInput();
        Look();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DashWasPressed = true;
        }
    }

    private void FixedUpdate()
    {
        Move();
        if (DashWasPressed)
        {
            Dash();
            DashWasPressed = false;
        }
    }

    private void GatherInput()
    {
        _input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        isMoving = _input.magnitude > 0;
    }

    private void Look()
    {
        if (_input != Vector3.zero)
        {
            var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
            var skewedInput = matrix.MultiplyPoint3x4(_input);
            var relative = (transform.position + skewedInput) - transform.position;
            var rot = Quaternion.LookRotation(relative, Vector3.up);

            transform.rotation = rot; // Set rotation instantly without any interpolation
        }
    }
    private void Move()
    {
        if (isMoving)
        {
            // Vérifie si le personnage peut se déplacer dans la direction donnée
            if (!CheckObstacle(transform.forward))
            {
                // Si pas d'obstacle, déplace le personnage
                _rb.MovePosition(transform.position + transform.forward * _speed * Time.deltaTime);
            }
        }
    }

    private bool CheckObstacle(Vector3 direction)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, 1f))
        {
            // S'il y a un obstacle devant le personnage, retourne vrai
            return true;
        }
        return false;
    }
    private void Dash()
    {
        Vector3 dashDestination = transform.position + transform.forward * dashDistance;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, dashDistance))
        {
            // Teleport just in front of the obstacle
            dashDestination = hit.point - transform.forward * 0.5f; // Adjust distance from the obstacle
        }

        transform.position = dashDestination;
    }
}
