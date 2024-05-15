using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

//To solve: GroundCheckBox() always returning false.
public class PlayerController : MonoBehaviour
{
    PlayerAnimator PlayerAnimator;
    public Stats Stats;

    public PlayerJumpVFX PlayerJumpVFX;
    public Rigidbody2D rigBody2D;
    private new Collider2D collider;

    public float dirX;

    public LayerMask groundMask;
    public Transform Spawn;

    public float walkSpeed;
    [SerializeField] float maxSpeed;
    [SerializeField] float acceleration;
    [SerializeField] float jumpForce;

    public float speed;
    public bool right = true;
    public float jumpCount;
    public float maxJumpCount;
    public bool previousGroundCheck;

    private float minJumpTime = 0.3f;
    private float currentJumpTime;
    void Start()
    {
        PlayerAnimator = GetComponent<PlayerAnimator>();
        Stats = GetComponent<Stats>();

        rigBody2D = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();

        jumpCount = 0f;
        currentJumpTime = 0f;

        rigBody2D.MovePosition(Spawn.position);

        Stats.staminaRegen = false;
    }
    void Update()
    {
        
        Move();
        Jump();
        if(GroundCheckBox() == true)
        {
            jumpCount = 1;
        }
        
        
        //Checks if there was ground beneath player in previous frame. For JumpAnimator() and stop animation.
        previousGroundCheck = GroundCheckBox();
    }
    void Move()
    {
       
        dirX = Input.GetAxisRaw("Horizontal");

       
        if (Input.GetButton("Run") && Stats.stamina >= 2)
        {
            PlayerAnimator.Run();
            //Checks if there is already an existing stamina running coroutine.
            if (Stats.staminaRun == false)
            {
                StartCoroutine(Stats.StaminaRun());
            }
            //Calculates acceleration. Restricts actual speed to speed limit.
            speed = speed + (maxSpeed * Time.deltaTime * acceleration);
            speed = Mathf.Clamp(speed, 0, maxSpeed);
            //Calculates running speed.
            float rigVY = rigBody2D.velocity.y;
            rigBody2D.velocity = new Vector2(dirX * speed, rigVY);
        }
        else
        {
            //Checks if there is already an existing stamina regeneration running coroutine.
            PlayerAnimator.StopRun();   
            if (Stats.staminaRegen == false)
            {
                StartCoroutine(Stats.StaminaRegen());
            }
            speed = walkSpeed;
            //Calculates walking speed.
            float rigVY = rigBody2D.velocity.y;
            rigBody2D.velocity = new Vector2(dirX * walkSpeed, rigVY);

        }
    }
    void Jump()
    {

        if (currentJumpTime <= 0)
        {
            //Checks if there is ground beneath player.
            if (Input.GetButtonDown("Jump") &&  GroundCheckBox() == true)
            {
                
                PlayerAnimator.Jump();
                JumpForce();    
            }
            else if(Input.GetButtonDown("Jump") && GroundCheckBox() == false && jumpCount> 0)
            {
                PlayerAnimator.Jump();
                JumpForce();
                PlayerJumpVFX.LaunchVfx();
                jumpCount -= 1;
            }
            
        }
        else
        {
            currentJumpTime += Time.deltaTime;
            if (currentJumpTime >= minJumpTime)
            {
                currentJumpTime = 0;
            }
        }
    }
    public void JumpForce()
    {
        //Sets jump force. Used through PlayerAnimator in animation event.
        currentJumpTime += Time.deltaTime;
        float rigVX = rigBody2D.velocity.x;
        rigBody2D.velocity = new Vector2(rigVX, jumpForce);
        
    }
    public bool GroundCheckBox()
    {

        Vector2 sizeVector = new Vector2(collider.bounds.size.x, collider.bounds.size.y - 1f);
        Vector2 originVector = new Vector2(collider.transform.position.x, collider.transform.position.y - 0.9f);

        return Physics2D.BoxCast(originVector, sizeVector, 0f, Vector2.down, 0.1f, groundMask);
    }
}
