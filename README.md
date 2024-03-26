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
    // Basic Mvt
    [SerializeField] private RigidBody _rb;
    [SerializeField] private float speed = 5;
    [SerializeField] private float turnSpeed = 360;
    private Vector3 _input;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
    }
    
    void FixedUpdate{
        Move();
    }
    
    void GatherInput
    {
        _input = new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical"));
    }
    
    void Look(){
        if (_input != Vector3.zero){
            var relative = (transfrom.position + _input) - transform.position;
            var rot = Quaternion.LookRotation(relative,Vector3.up);
        
            transform.rotation = Quaternion.RotateTowards(transform.rotation,rot,turnSpeed * Time.deltaTime);
        }
    }
    void Move()
    {
        rb.MovePosition(transform.position + transform.forward * _speed * Time.deltaTime);
    }
    
}
