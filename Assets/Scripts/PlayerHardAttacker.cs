using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class PlayerHardAttacker : AttackBase
{
   
    [SerializeField] GameObject Light;

    [SerializeField] GameObject AudioManager;
    AudioManager AudioScript;

    bool hardAttackTrue = false;
    bool coroutineStarted = false;
    void Start()
    {
        renderer.enabled = false;
        Light.SetActive(false);

        AudioScript = AudioManager.GetComponent<AudioManager>();
    }
    
    public void HardAttack()
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
                AudioScript.HardAttack();
                Debug.Log("Attack");
                renderer.enabled = true;
                Light.SetActive(true);
                animator.SetTrigger("AttackingStart");
                animator.SetBool("Attacking", false);
                hardAttackTrue = true;
                Stats.stamina = Stats.stamina - Stats.staminaHardAttak;
            }

        
    }
    private IEnumerator NextSwing()
    {
        coroutineStarted = true;
        animator.SetBool("Attacking", true);
        Stats.stamina = Stats.stamina - Stats.staminaHardAttak;
        AudioScript.HardAttack();
        yield return new WaitForSeconds(1);
        animator.SetBool("Attacking", false);
        coroutineStarted = false;
    }
    public void HardAttackKiller()
    {
        animator.ResetTrigger("AttackingStart");
        hardAttackTrue = false;
        renderer.enabled = false;
        Light.SetActive(false);
        player.isAttacking = false;
    }
}
