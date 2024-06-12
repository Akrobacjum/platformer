using UnityEngine;

//To solve: Stopping animation plays during changing directions while running.
public class PlayerAnimator : MonoBehaviour
{
    public PlayerController PlayerController;
    public Stats Stats;

    public Animator animator;

    float playerSpeed;
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
        playerSpeed = PlayerController.playerSpeed;
        MoveAnimator();
        
        StopAnimator();
        
        previousInput = input;

    }
    public void Flip(bool side /* true == flip*/)
    {
        //Flips player sprites according to movement direction.
        if (side)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            PlayerController.right = false;
        }
        else 
        { 
            transform.eulerAngles = new Vector3(0, 0, 0);
            PlayerController.right = true;
        }
    }
    void MoveAnimator()
    {
        //Sets player speed as absolute speed variable for walk animation.
        animator.SetBool("Input", PlayerController.dirX != 0);
        animator.SetFloat("Speed", playerSpeed);
    }
    public void Jump()
    {
        animator.SetTrigger("Jump");

    }
    public void Run()
    {
        //Sets triggers for run and stop animation.
        animator.SetBool("Run",true);   

    }
    public void StopRun()
    {
        animator.SetBool("Run",false);
    }
    void StopAnimator()
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
    public void Interaction()
    {
        animator.SetTrigger("Interact");
    }
}
