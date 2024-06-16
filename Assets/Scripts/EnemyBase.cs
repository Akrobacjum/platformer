using UnityEngine;

public enum AiState
{
    Patrol,
    Chase,
    Attack
}

public class EnemyBase : MonoBehaviour
{
    AiState aiState;
    Stats stats;
    [SerializeField] private GameObject[] wayPoints;
    GameObject Player;
    Rigidbody rgbd;
    [SerializeField] float damgageToPlayer;
   
    EntityAnim entityAnim;

    float velY = -9.8f;

    int currentPoint = 0;
    public float speed = 1.0f;

    public LayerMask playerMask;

    [SerializeField] float moveSpeed;
    [SerializeField] float rayDistance;
    [SerializeField] float searchLenght;

    Vector2 dir;
    Vector3 lastPosition;


    public int attackStamina = 7;


    public bool attacked = false;
    public bool rightSide;
    void Start()
    {
        stats = GetComponent<Stats>();
        entityAnim =GetComponent<EntityAnim>();
        aiState = AiState.Patrol;

        Player = GameObject.Find("Player");

        
    }



    // Update is called once per frame
    void Update()
    {
        if (stats.health > 0)
        {
            Sight();
           // transform.position = Vector2.down * velY;
            StartCoroutine(stats.StaminaRegen());
            Debug.Log(Player.transform.position);

            //Debug.DrawLine(transform.position,  *10,Color.red);
            if (attacked == false)
            {
                switch (aiState)
                {
                    case AiState.Patrol:

                        MoveToPoints();
                        PointManager();
                        break;
                    case AiState.Chase:
                        ChasePlayer();
                        break;
                    case AiState.Attack:
                        if (attacked == false && stats.stamina > attackStamina)
                        {
                            entityAnim.Attack();
                            stats.stamina -= attackStamina;
                            attacked = true;
                        }


                        break;
                }
            }

            //GETTING VELOCITY HERE
            if (dir.x > 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                rightSide = false;
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
                rightSide = true;
            }
            lastPosition = transform.position;
        }
        else
        {
            transform.position = lastPosition;
        }
    }



    void PointManager()
    {
        Vector2 myPosition = transform.position;
        Vector2 pointPostion = wayPoints[currentPoint].transform.position;

        Vector2 newPostion = new Vector2(pointPostion.x, myPosition.y);
        if (Vector2.Distance(myPosition, newPostion) <= 0.1f)
        {
            currentPoint++;
        }
        if (currentPoint >= wayPoints.Length)
        {
            currentPoint = 0;
        }
    }

    void MoveToPoints()
    {
        Vector2 myPosition = transform.position;
        Vector2 pointPostion = wayPoints[currentPoint].transform.position;

        Vector2 newPosition = new Vector2(pointPostion.x, myPosition.y);
        dir = (pointPostion - myPosition);

        transform.position = Vector2.MoveTowards(myPosition, newPosition, speed * Time.deltaTime);
    }

    

    void Sight()
    {
        float distanceToPlayerX = Mathf.Abs(Player.transform.position.x - transform.position.x);
        //Debug.Log(distanceToPlayerX);

        if (distanceToPlayerX > 4)
        {
            aiState = AiState.Patrol;
        }
        else if(distanceToPlayerX < 4 && distanceToPlayerX > 1.2f)
        {
            aiState = AiState.Chase;
        }
        else
        {
            aiState = AiState.Attack;
        }
    }

    void ChasePlayer()
    {
        Vector2 myPosition = transform.position;
        Vector2 playerPostionX = new Vector2(Player.transform.position.x, myPosition.y);
        dir = (playerPostionX - myPosition);

        transform.position = Vector2.MoveTowards(myPosition, playerPostionX, speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") &&  attacked == true)
        {
            Debug.Log("AttackingPlayer");
            Stats player = collision.gameObject.GetComponent<Stats>();
            if (player != null)
            {
                player.health -= damgageToPlayer;
            }
        }
    }

    
}


