using System.Collections;
using UnityEngine;

public class PassThroughPlatform : MonoBehaviour
{

    private Collider2D _colider;
    private bool playerOnPlatform;
    // Start is called before the first frame update
    void Start()
    {
        _colider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerOnPlatform && Input.GetAxisRaw("Vertical") < 0)
        {
            _colider.enabled = false;
            StartCoroutine(EnableCollider());
        }
    }

    private IEnumerator EnableCollider()
    {
        yield return new WaitForSeconds(0.7f);
        _colider.enabled = true;
    }
    void SetPlayerOnPlatform(Collision2D other, bool value)
    {
        var player = other.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            playerOnPlatform = value;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SetPlayerOnPlatform(collision, true);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        SetPlayerOnPlatform(collision, true);
    }
}
