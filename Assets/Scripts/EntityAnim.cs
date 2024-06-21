using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class EntityAnim : MonoBehaviour
{
    Animator entityAnimator;
    EnemyBase enemyBase;

    public GameObject Projectile;
    private Vector2 ShootPosition;
    [SerializeField] Transform FirePoint;
    float maxDistance = 1;
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
    public void Thrust()
    {
        entityAnimator.SetTrigger("Thrust");


    }
    public void ThrustStart()
    {
        entityAnimator.SetTrigger("ThrustStart");
    }

    public void Shoot()
    {
        if (enemyBase.DistanceProjectile < 6)
        {
            ShootPosition = FirePoint.position;
            Instantiate(Projectile, ShootPosition, Quaternion.identity);
        }
        
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
