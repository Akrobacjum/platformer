using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ember : MonoBehaviour
{
    GameObject Player;
    [SerializeField] GameObject UIManager;
    [SerializeField] ParticleSystem Effect;

    PlayerAnimator PlayerAnimator;

    [SerializeField] GameObject AudioManager;
    AudioManager AudioScript;

    //[SerializeField] TextMeshPro fireText;

    UIManager UIManagerScript;
    Animator Animator;

    public bool isUnlocked = false;
    public bool isInteracted = false;
    bool PlayerTrigger;

    //float dirX = 0f; 

    bool isHit;
    bool isPlaying;
    // Start is called before the first frame update
    void Start()
    {
        Effect.Pause();
        Effect.Clear();
        UIManagerScript = UIManager.GetComponent<UIManager>();
        Animator = GetComponent<Animator>();

        AudioScript = AudioManager.GetComponent<AudioManager>();
        PlayerAnimator = Player.GetComponent<PlayerAnimator>();

        //Unlocking firecamp should be loaded from player prefs, turn on before creating a build.
        //isUnlocked = PlayerPrefs.GetInt("isUnlocked") == 1 ? true : false;
        isHit = false;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInteraction();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerTrigger = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerTrigger = false;
    }
    private void PlayerInteraction()
    {
        if (PlayerTrigger && Input.GetButtonDown("Interact") && isInteracted == false)
        {
            isInteracted = true;
            PlayerAnimator.Interaction();
            Invoke("FirecampUnlock", 3.0f);
            UIManagerScript.WinScreen();
        }
    }
}
