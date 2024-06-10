using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public float health;
    public float healthMax;

    public float stamina;
    public float staminaMax;
    public int staminaRegenWhileWalking;
    public int staminaRegenWhileStanding;
    public int regen;
    
    public int staminaRunPerSec;
    public int staminaJump;

    [SerializeField] ProgressBar StaminaBar;
    [SerializeField] ProgressBar HealthBar;

    public int staminaHardAttak;
    public int staminaSoftAttak;

    public bool staminaRegen;
    public bool staminaRun;
    bool isDead = false;
    void Start()
    {
        //Sets player statistics to maximum at scene's starts.
        health = healthMax;
        stamina = staminaMax;

        StaminaBar.SetMaxValue((int)stamina);
        HealthBar.SetMaxValue((int)health);
        regen = staminaRegenWhileStanding;

    }
    void Update()
    {

        StatsCheck();
    }


    void StatsCheck()
    {
        //Checks if statistics are mathematically correct and restrained every frame. Also checks if entity should be slain.
        health = Mathf.Clamp(health, 0, healthMax);
        stamina = Mathf.Clamp(stamina, 0, staminaMax);

        StaminaBar.SetCurrentValue((int)stamina);
        HealthBar.SetCurrentValue((int)health);


        if (health <= 0)
        {
            
            if(tag == "Entity")
            {
                EntitySlay();
            }
        }
    }
    public IEnumerator StaminaRegen()
    {
        //Regenerates stamina, checks if stamina number is correct and waits for one second before allowing next stamina regen coroutine. Turns off stamina run coroutine.
        staminaRegen = true;
        if (staminaRun == true)
        {
            staminaRun = false;
        }
        stamina = stamina + regen * Time.deltaTime;
        StatsCheck();
        //Debug.Log("Entity Stamina: " + stamina);
        yield return null;
        staminaRegen = false;
    }
    public IEnumerator StaminaRun()
    {

        StopCoroutine(StaminaRegen());
        staminaRun = true;
        stamina = stamina - staminaRunPerSec * Time.deltaTime;
        StatsCheck();

        //Debug.Log("Entity Stamina: " + stamina);
        yield return null; ;

        staminaRun = false;
    }

    public void DoDamage(int damageAmmount)
    {
        health = health - damageAmmount;
    }
    void EntitySlay()
    {
        if (isDead== false)
        {
            Debug.Log("Entity Slain");
            EntityAnim entityAnim = gameObject.GetComponent<EntityAnim>();
            entityAnim.Death();
            isDead = true;
        }
        
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    //Checks tag of trigger's object.
    //    string coliderTag = collision.gameObject.tag;

    //    switch (coliderTag)
    //    {
    //        case "Entity":
    //            //Deals damage to player.
    //            health--;

    //            Debug.Log("Entity Health: " + health);
    //            break;
    //    }
    //}
}
