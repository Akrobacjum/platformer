using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//To solve: Interaction and unlocking is working, but with significant delay.

public class Firecamp : MonoBehaviour
{
    [SerializeField] GameObject FireCampMapButton;
    [SerializeField] GameObject Spawnpoint;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject UIManager;
    [SerializeField] ParticleSystem Effect;

    PlayerAnimator PlayerAnimator;

    [SerializeField] GameObject AudioManager;
    AudioManager AudioScript;

    //[SerializeField] TextMeshPro fireText;

    UIManager UIManagerScript;
    Stats Stats;
    Animator Animator;

    public bool isUnlocked = false;
    public bool isInteracted = false;
    bool PlayerTrigger;

    //float dirX = 0f; 

    bool isHit;
    bool isPlaying;

    void Start()
    {
        Effect.Pause();
        Effect.Clear();
        UIManagerScript = UIManager.GetComponent<UIManager>();
        Stats = Player.GetComponent<Stats>();
        Animator = GetComponent<Animator>();

        AudioScript = AudioManager.GetComponent<AudioManager>();
        PlayerAnimator = Player.GetComponent<PlayerAnimator>();

        //Unlocking firecamp should be loaded from player prefs, turn on before creating a build.
        //isUnlocked = PlayerPrefs.GetInt("isUnlocked") == 1 ? true : false;
        isHit = false;
    }
    void Update()
    {
        PlayerInteraction();
        //Checks if firecamp is unlocked and controls if player can teleport to it from map menu.
        if (isUnlocked)
        {
            SoundCheck();
            FireCampMapButton.SetActive(true);
            //fireText.SetText("Active");
        }
        else
        {
            FireCampMapButton.SetActive(false);
        }
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
        if (PlayerTrigger)
        {
            if (isUnlocked && Input.GetButtonDown("Interact"))
            {
                Debug.Log("Campfire: Interaction");

            }
            else if (Input.GetButtonDown("Interact") && isInteracted == false)
            {
                isInteracted = true;
                PlayerAnimator.Interaction();
                Invoke("FirecampUnlock", 3.0f);
            }
        }
    }
    private void SoundCheck()
    {
        var hit = Physics2D.Raycast(transform.position, Player.transform.position);
        AudioScript.firecampSource.volume = 1 - hit.distance * 0.33f;
        AudioScript.musicSource.volume = 1 - hit.distance * 0.33f;
        //Debug.Log(hit.distance);
        isHit = hit.collider;
        Debug.DrawRay(transform.position, Player.transform.position - transform.position, Color.green);
        if (hit.distance > 3 && isPlaying == true)
        {
            AudioScript.firecampSource.Stop();
            AudioScript.musicSource.Stop();
            isPlaying = false;
        }
        else if (isPlaying == false)
        {
            AudioScript.firecampSource.Play();
            AudioScript.musicSource.Play();
            isPlaying = true;
        }
    }
    void FirecampUnlock()
    {
        AudioScript.PlayMusic("FirecampUnlock");
        Invoke("PlayOST", 3.5f);
        AudioScript.PlayFirecamp("Firecamp");
        isUnlocked = true;
        Animator.SetBool("isUnlocked", true);
        Effect.Play();
        Debug.Log("Campfire: " + isUnlocked);
    }
    void PlayOST()
    {
        AudioScript.PlayMusic("Peace");
    }
    private void OnApplicationQuit()
    {
        //Saves state of firecamp (unlocked or not).
        PlayerPrefs.SetInt("isUnlocked", isUnlocked ? 1 : 0);
    }
    public void OnClickFirecamp()
    {
        //Teleports player to firecamp, resumes game and refills his stats.
        Debug.Log("Firecamp: Teleport");
        Player.transform.position = Spawnpoint.transform.position;
        Stats.health = Stats.healthMax;
        Stats.stamina = Stats.staminaMax;
        UIManagerScript.Resume();
    }
}
