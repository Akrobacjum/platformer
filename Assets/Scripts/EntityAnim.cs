using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAnim : MonoBehaviour
{
    Animator entityAnimator;
    // Start is called before the first frame update
    void Start()
    {
        entityAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Death()
    {
        Debug.Log("Death");
        entityAnimator.SetTrigger("Death");
        
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
