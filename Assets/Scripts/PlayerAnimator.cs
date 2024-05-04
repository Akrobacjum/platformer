using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEngine.Rendering.DebugUI;

//To solve: Stopping animation plays during changing directions while running.
public class PlayerAnimator : MonoBehaviour
{
    public PlayerController PlayerController;
    public Stats Stats;

    Animator animator;

    bool input = false;
    bool previousInput = false;
    void Start()
    {
        PlayerController = GetComponent<PlayerController>();
        Stats = GetComponent<Stats>();

        animator = GetComponent<Animator>();
    }
    void Update()
    {
        MoveAnimator();
        RunAnimator();
        JumpAnimator();
        Flip();
        previousInput = input;
    }
    void Flip()
    {
        //Flips player sprites according to movement direction.
        if (PlayerController.dirX < 0 && PlayerController.right)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            PlayerController.right = false;
        }
        if (PlayerController.dirX > 0 && !PlayerController.right)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            PlayerController.right = true;
        }
    }
    void MoveAnimator()
    {
        //Sets player speed as absolute speed variable for walk animation.
        animator.SetBool("Input", PlayerController.dirX != 0);
        animator.SetFloat("Speed", Mathf.Abs(PlayerController.rigBody2D.velocity.x));
    }
    public void Jump()
    {
        //Checks if player is able to jump regarding his jump limit and stamina.
        if (Input.GetButtonDown("Jump") && PlayerController.jumpCount < PlayerController.maxJumpCount && Stats.stamina >= 5)
        {
            //Removes required stamina. Sets trigger for jump animation. Uses PlayerController.JumpForce() in animation event.
            Stats.stamina = Stats.stamina - Stats.staminaJump;
            animator.SetTrigger("Jump");
        }
    }
    void RunAnimator()
    {
        //Sets triggers for run and stop animation.
        if (Stats.stamina >= 2)
        {
            if (Mathf.Abs((float)PlayerController.rigBody2D.velocity.x) > 1 && (Input.GetButton("Run")))
            {
                animator.SetBool("Run", true);
            }
            else if (Mathf.Abs((float)PlayerController.rigBody2D.velocity.x) > 0.9)
            {
                animator.SetBool("Run", false);
                input = true;
                animator.SetBool("Input", previousInput);
            }
            else
            {
                animator.SetBool("Run", false);
                input = false;
                animator.SetBool("Input", previousInput);
            }
        }
    }
    void JumpAnimator()
    {
        //Sets triggers for stop animation while jumping.
        if (PlayerController.previousGroundCheck == false && PlayerController.GroundCheckBox())
        {
            animator.SetTrigger("Stop");
        }
        else if (PlayerController.previousGroundCheck == true && PlayerController.GroundCheckBox())
        {
            animator.ResetTrigger("Stop");
        }
    }
}
