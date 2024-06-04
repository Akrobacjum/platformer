using UnityEngine;

public class PlayerJumpVFX : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] GameObject Light;
    

    public Animator JumpVFXAnimator;
    private SpriteRenderer Renderer;

    bool isEnabled = false;
    void Start()
    {

        JumpVFXAnimator = GetComponent<Animator>();
        
        Renderer = GetComponent<SpriteRenderer>();

        Renderer.enabled = false;
        Light.SetActive(false);
    }
    void Update()
    {
        if (isEnabled == false)
        {
            transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y - 0.5f);
        }

    }
    public void LaunchVfx()
    {

        isEnabled = true;
        Renderer.enabled = true;
        JumpVFXAnimator.SetTrigger("JumpVFX");
        Light.SetActive(true);

    }
    public void JumpVFXKiller()
    {
        isEnabled = false;
        Renderer.enabled = false;
        Light.SetActive(false);
        JumpVFXAnimator.ResetTrigger("JumpVFX");
    }
    
}
