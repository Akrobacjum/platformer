using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

enum AiState
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
    [SerializeField] GameObject Player;
    int currentPoint = 0;
    public float speed = 1.0f;

    public LayerMask playerMask;

    [SerializeField] float moveSpeed;
    [SerializeField] float rayDistance;
    [SerializeField] float searchLenght;
    private bool lineOfSight = false;
    private bool previousLineOfSight = false;

    void Start()
    {
        stats = GetComponent<Stats>();
        aiState = AiState.Patrol;
    }



    // Update is called once per frame
    void Update()
    {
        RayCast();
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

                break;
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

        transform.position = Vector2.MoveTowards(myPosition, newPosition, speed * Time.deltaTime);
    }

    void RayCast()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, Player.transform.position - transform.position, rayDistance, playerMask);
        if (ray.collider != null)
        {
            Debug.Log(ray.collider.name);
            lineOfSight = ray.collider.CompareTag("Player");
            if (lineOfSight)
            {
                aiState = AiState.Chase;
                Debug.DrawRay(transform.position, Player.transform.position - transform.position, Color.green);
            }
            else
            {
                aiState = AiState.Patrol;
                
            }
        }
        else
        {
            aiState = AiState.Patrol;
            lineOfSight = false;
        }
    }
    
    void ChasePlayer()
    {
        Vector2 myPosition = transform.position;
        Vector2 playerPostionX = new Vector2(Player.transform.position.x, myPosition.y);

        transform.position = Vector2.MoveTowards(myPosition, playerPostionX, speed * Time.deltaTime);
    }


}

