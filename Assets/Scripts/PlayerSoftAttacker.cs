using System.Collections;
using UnityEngine;

public class PlayerSoftAttacker : AttackBase
{
    [SerializeField] GameObject Light;
    GameObject AudioManager;
    AudioManager AudioScript;

    bool softAttackTrue = false;
    bool coroutineStarted = false;

    bool keepPosition;
    void Start()
    {
        renderer.enabled = false;
        Light.SetActive(false);
        AudioManager = GameObject.FindWithTag("Soundmanager");
        AudioScript = AudioManager.GetComponent<AudioManager>();
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

        
        if (softAttackTrue)
        {
            if (coroutineStarted == false)
            {
                StartCoroutine(NextSwing());
            }
        }
        else
        {
            AudioScript.SoftAttack();
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
        yield return new WaitForSeconds(1f);
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
    public void SoftSound()
    {
        AudioScript.SoftAttack();
    }
}
