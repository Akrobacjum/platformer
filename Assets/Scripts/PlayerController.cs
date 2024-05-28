using UnityEngine;

//To solve: GroundCheckBox() always returning false.
public class PlayerController : MonoBehaviour
{
    PlayerAnimator PlayerAnimator;
    PlayerAudioManager playerAudioManager;
    public Stats Stats;
    public UIManager Manager;
    public PlayerJumpVFX JumpVFX;
    public Rigidbody2D rigBody2D;
    private new Collider2D collider;

    public float dirX;

    public LayerMask groundMask;

    public LayerMask platformMask;
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
    public float playerSpeed;
    private float minJumpTime = 0.3f;
    private float currentJumpTime;

    bool isDead = false;
    void Start()
    {
        playerAudioManager = GetComponent<PlayerAudioManager>();    
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
        if (Stats.health <= 0f)
        {
            Dead();

        }
        else
        {
            playerSpeed = Mathf.Abs((float)rigBody2D.velocity.x);
            RegenType();
            Move();
            Jump();
        }




        if (GroundCheckBox() == true)
        {
            jumpCount = 1;
        }


        //Checks if there was ground beneath player in previous frame. For JumpAnimator() and stop animation.
        previousGroundCheck = GroundCheckBox();
    }
    void RegenType()
    {
        if (playerSpeed != 0f)
        {
            Stats.regen = Stats.staminaRegenWhileWalking;
        }
        else
        {
            Stats.regen = Stats.staminaRegenWhileStanding;
        }
    }
    void Move()
    {

        dirX = Input.GetAxisRaw("Horizontal");


        if (Input.GetButton("Run") && Stats.stamina >= 2) //RUNING
        {
            PlayerAnimator.Run();


            if (Stats.staminaRun == false)
            {
                StartCoroutine(Stats.StaminaRun());
            }

            //Acceleration
            speed = speed + (maxSpeed * Time.deltaTime * acceleration);
            speed = Mathf.Clamp(speed, 0, maxSpeed);

            //Running speed
            float rigVY = rigBody2D.velocity.y;
            rigBody2D.velocity = new Vector2(dirX * speed, rigVY);
        }
        else //NOT RUNING
        {

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
            if (Stats.stamina > Stats.staminaJump)
            {
                MoveThroughPlatform();
                if (Input.GetButtonDown("Jump") && GroundCheckBox() == true)
                {

                    PlayerAnimator.Jump();
                    Stats.stamina -= Stats.staminaJump;
                    JumpForce();
                }
                else if (Input.GetButtonDown("Jump") && GroundCheckBox() == false && jumpCount > 0)
                {
                    PlayerAnimator.Jump();
                    Stats.stamina -= Stats.staminaJump;
                    JumpForce();
                    //Instantiate(JumpVFX, transform.position, Quaternion.Euler(0, 0, 0));
                    JumpVFX.LaunchVfx();
                    jumpCount -= 1;
                }


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

        Vector2 sizeVector = new Vector2(collider.bounds.size.x - 0.2f, collider.bounds.size.y - 1f);
        Vector2 originVector = new Vector2(collider.transform.position.x, collider.transform.position.y - 0.9f);

        return Physics2D.BoxCast(originVector, sizeVector, 0f, Vector2.down, 0.1f, groundMask);
    }
    public bool PlatformCheckBox()
    {

        Vector2 sizeVector = new Vector2(collider.bounds.size.x - 0.2f, collider.bounds.size.y - 1f);
        Vector2 originVector = new Vector2(collider.transform.position.x, collider.transform.position.y - 0.9f);

        return Physics2D.BoxCast(originVector, sizeVector, 0f, Vector2.down, 0.1f, platformMask);
    }
    void MoveThroughPlatform()
    {
        if (Input.GetButtonDown("Jump") && Input.GetKeyDown(KeyCode.S) && GroundCheckBox() == false && PlatformCheckBox() == true)
        {
            Debug.Log("Platforma");
            collider.isTrigger = true;
        }

    }
    public void Dead()
    {
        if (isDead == false)
        {
            Debug.Log("You Died");
            playerAudioManager.DeathSound();
            Manager.DeathScreen();
            isDead = true;
        }
    }
}
