using System.Collections;
using UnityEngine;

public class PlayerSoftAttacker : AttackBase
{


    [SerializeField] GameObject Light;

    bool softAttackTrue = false;
    bool coroutineStarted = false;

    bool keepPosition;
    void Start()
    {
        renderer.enabled = false;
        Light.SetActive(false);

    }
    //public override void Update()
    //{

    //    base.Update();
    //    //if (keepPosition == false)
    //    //{
    //    //    transform.position = attackSlot.position;
    //    //    lastPostion = transform;
    //    //}
    //    //else
    //    //{
    //    //    transform.position = lastPostion.position;  
    //    //}

    //}
    public void SoftAttack()
    {
        keepPosition = true;

        Debug.Log(player.side);
        if (softAttackTrue)
        {
            if (coroutineStarted == false)
            {
                StartCoroutine(NextSwing());
            }
        }
        else
        {

            
            renderer.enabled = true;
            Light.SetActive(true);
            animator.ResetTrigger("FinalSwingDone");
            animator.SetTrigger("AttackingStart");
            animator.SetBool("Attacking", false);
            softAttackTrue = true;
            Stats.stamina = Stats.stamina - Stats.staminaSoftAttak;
        }

    }
    private IEnumerator NextSwing()
    {
        coroutineStarted = true;
        animator.SetBool("Attacking", true);
        Stats.stamina = Stats.stamina - Stats.staminaSoftAttak;
        yield return new WaitForSeconds(0.75f);

        animator.SetBool("Attacking", false);
        coroutineStarted = false;
    }
    public void SoftAttackKiller()
    {

        animator.ResetTrigger("AttackingStart");
        softAttackTrue = false;
        renderer.enabled = false;
        Light.SetActive(false);
        keepPosition = false;
        player.isAttacking = false;
    }

}
