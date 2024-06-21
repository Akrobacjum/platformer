using UnityEngine;

public class Void : MonoBehaviour
{
  
   
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
