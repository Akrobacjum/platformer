using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoftAttacker : MonoBehaviour
{
    public Animator Animator;
    private SpriteRenderer Renderer;
    Stats Stats;

    [SerializeField] GameObject Player;
    [SerializeField] GameObject Light;

    bool softAttackTrue = false;
    bool coroutineStarted = false;
    void Start()
    {
        Animator = GetComponent<Animator>();
        Renderer = GetComponent<SpriteRenderer>();
        Stats = Player.GetComponent<Stats>();

        Renderer.enabled = false;
        Light.SetActive(false);
    }
    void Update()
    {
        SoftAttack();
    }
    public void SoftAttack()
    {
        if (Input.GetButtonDown("Soft Attack") && Stats.stamina >= Stats.staminaSoftAttak)
        {
            if (softAttackTrue == true)
            {
                if (coroutineStarted == false)
                {
                    StartCoroutine(NextSwing());
                }
            }
            else
            {
                Debug.Log("Attack");
                Renderer.enabled = true;
                Light.SetActive(true);
                Animator.ResetTrigger("FinalSwingDone");
                Animator.SetTrigger("AttackingStart");
                Animator.SetBool("Attacking", false);
                softAttackTrue = true;
                Stats.stamina = Stats.stamina - Stats.staminaSoftAttak;
            }
        }
    }
    private IEnumerator NextSwing()
    {
        coroutineStarted = true;
        Animator.SetBool("Attacking", true);
        Stats.stamina = Stats.stamina - Stats.staminaSoftAttak;
        yield return new WaitForSeconds(1);
        Animator.SetBool("Attacking", false);
        coroutineStarted = false;
    }
    public void SoftAttackKiller()
    {
        Animator.ResetTrigger("AttackingStart");
        softAttackTrue = false;
        Renderer.enabled = false;
        Light.SetActive(false);
    }
}
