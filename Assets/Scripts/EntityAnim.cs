using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAnim : MonoBehaviour
{
    Animator entityAnimator;
    EnemyBase enemyBase;
    // Start is called before the first frame update
    void Start()
    {
        entityAnimator = GetComponent<Animator>();
        enemyBase = GetComponent<EnemyBase>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Swing()
    {
        entityAnimator.SetTrigger("Swing");
        

    }
    public void Attack()
    {
        entityAnimator.SetTrigger("SwingStart");
    }
    public void Death()
    {
        Debug.Log("Death");
        entityAnimator.SetTrigger("Death");
        
    }

    public void AttackReset()
    {
        enemyBase.attacked = false; 
    }
    public void DestroyObject()
    {
        Destroy(gameObject.transform.parent.gameObject);
        
    }
}
