using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Projectile : MonoBehaviour
{
    GameObject Player;
    Stats Stats;
    float speed = 1;
    [SerializeField] float damage = 2;

    [SerializeField] GameObject AudioManager;
    AudioManager AudioScript;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        Stats = Player.GetComponent<Stats>();

        AudioManager = GameObject.FindWithTag("Soundmanager");
        AudioScript = AudioManager.GetComponent<AudioManager>();
    }
    private void Awake()
    {
        Destroy(gameObject, 4);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 myPosition = transform.position;
        Vector2 playerPostionX = new Vector2(Player.transform.position.x, Player.transform.position.y);
        //dir = (playerPostionX - myPosition);

        transform.position = Vector2.MoveTowards(myPosition, playerPostionX, speed * Time.deltaTime);
    }
    
    public void OnTriggerEnter2D(Collider2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Player"))
        {
            Stats.health = Stats.health - damage;
            Destroy(gameObject);
            AudioScript.PlayerDamage();
        }else if(collision2D.gameObject.CompareTag("Entity"))
        {

        }
        else
        {
            Destroy(gameObject);
        }

    }
}
