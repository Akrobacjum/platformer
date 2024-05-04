using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class PlayerHardAttacker : MonoBehaviour
{
    public Animator Animator;
    private SpriteRenderer Renderer;
    Stats Stats;

    [SerializeField] GameObject Player;
    [SerializeField] GameObject Light;

    bool hardAttackTrue = false;
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
        HardAttack();
    }
    public void HardAttack()
    {
        if (Input.GetButtonDown("Hard Attack") && Stats.stamina >= Stats.staminaHardAttak)
        {
            if (hardAttackTrue == true)
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
                Animator.SetTrigger("AttackingStart");
                Animator.SetBool("Attacking", false);
                hardAttackTrue = true;
                Stats.stamina = Stats.stamina - Stats.staminaHardAttak;
            }

        }
    }
    private IEnumerator NextSwing()
    {
        coroutineStarted = true;
        Animator.SetBool("Attacking", true);
        Stats.stamina = Stats.stamina - Stats.staminaHardAttak;
        yield return new WaitForSeconds(3);
        Animator.SetBool("Attacking", false);
        coroutineStarted = false;
    }
    public void HardAttackKiller()
    {
        Animator.ResetTrigger("AttackingStart");
        hardAttackTrue = false;
        Renderer.enabled = false;
        Light.SetActive(false);
    }
}
