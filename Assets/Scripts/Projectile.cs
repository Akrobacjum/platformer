using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Projectile : MonoBehaviour
{
    GameObject Player;
    Stats Stats;
    float speed = 1;
    float damage = 2;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        Stats = Player.GetComponent<Stats>();
    }
    private void Awake()
    {
        Destroy(gameObject, 2);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 myPosition = transform.position;
        Vector2 playerPostionX = new Vector2(Player.transform.position.x, Player.transform.position.y);
        //dir = (playerPostionX - myPosition);

        transform.position = Vector2.MoveTowards(myPosition, playerPostionX, speed * Time.deltaTime);
    }
    public void OnCollisionEnter2D(Collision2D collision2D)
    {
        if(collision2D.gameObject.CompareTag ("Player"))
        {
            Stats.health = Stats.health - damage;
            Destroy(gameObject);
        }
    }
}
