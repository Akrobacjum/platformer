using UnityEngine;

public class Void : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("EnterCollision");
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("smierc");
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            player.Dead();

        }
    }
}
