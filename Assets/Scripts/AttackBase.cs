using Unity.VisualScripting;
using UnityEngine;

public class AttackBase : MonoBehaviour
{
    public PlayerController player;
    public Transform attackSlot;
    public Animator animator;
    public SpriteRenderer renderer;
    public Stats Stats;

    Transform lastPosition;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        Stats = player.GetComponent<Stats>();
    }

    // Update is called once per frame
    public void Update()
    {


        Debug.Log(player.isAttacking);
        if (player.isAttacking)
        {
            transform.position = lastPosition.position;
        }
        else
        {

            if (player.side)
            {
                transform.localScale = new Vector3(-1, 1, 1);

            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }

            transform.position = attackSlot.position;
            lastPosition = transform;
        }
            
    }

    public void AttackDebug()
    {
        Debug.Log("Attack");
    }


}
