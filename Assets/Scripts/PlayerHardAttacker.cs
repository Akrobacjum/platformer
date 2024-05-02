using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class PlayerHardAttacker : MonoBehaviour
{
    public Animator Animator;
    private SpriteRenderer Renderer;

    [SerializeField] GameObject Light;

    bool hardAttackTrue = false;
    bool coroutineStarted = false;
    void Start()
    {
        Animator = GetComponent<Animator>();
        Renderer = GetComponent<SpriteRenderer>();

        Renderer.enabled = false;
        Light.SetActive(false);
    }
    void Update()
    {
        HardAttack();
    }
    public void HardAttack()
    {
        if (Input.GetButtonDown("Hard Attack"))
        {
            if (hardAttackTrue == true)
            {
                if (coroutineStarted == false)
                {
                    coroutineStarted = true;
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
                hardAttackTrue = true;
            }

        }
    }
    private IEnumerator NextSwing()
    {
        Animator.SetBool("Attacking", true);
        yield return new WaitForSeconds(1.5f);
        Animator.SetBool("Attacking", false);
        coroutineStarted = false;
    }
    public void HardAttackFinish()
    {
        Animator.ResetTrigger("AttackingStart");
        hardAttackTrue = false;
        Renderer.enabled = false;
        Light.SetActive(false);
    }
}
