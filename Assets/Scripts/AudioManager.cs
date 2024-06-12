using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] musicSound, sfxSound;
    public AudioSource playerSource, hardAttackSource, softAttackSource, firecampSource, musicSource, ambienceSource, enemySource;

    int DamageCounter = 1;

    private void Start()
    {
        playerSource.Play();
        hardAttackSource.Play();
        softAttackSource.Play();
        firecampSource.Play();
        musicSource.Play();
        ambienceSource.Play();
        SceneCheck();
    }
    public void SceneCheck()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "MainMenu")
        {
            PlayMusic("MainMenu");
        }
        else if (sceneName == "DungeonLevel")
        {
            PlayAmbience("Ambience");
        }
    }
    public void PlayerDamage()
    {
        if (DamageCounter == 7)
        {
            DamageCounter = 1;
            PlayPlayer("Damage7");
        }
        else if (DamageCounter == 6)
        {
            DamageCounter++;
            PlayPlayer("Damage6");
        }
        else if (DamageCounter == 5)
        {
            DamageCounter++;
            PlayPlayer("Damage5");
        }
        else if (DamageCounter == 4)
        {
            DamageCounter++;
            PlayPlayer("Damage4");
        }
        else if (DamageCounter == 3)
        {
            DamageCounter++;
            PlayPlayer("Damage3");
        }
        else if (DamageCounter == 2)
        {
            DamageCounter++;
            PlayPlayer("Damage2");
        }
        else if (DamageCounter == 1)
        {
            DamageCounter++;
            PlayPlayer("Damage1");
        }
    }
    public void SoftAttack()
    {
        PlaySoftAttack("SoftAttack");
    }
    public void HardAttack()
    {
        PlayHardAttack("HardAttack");
    }
    public void FirecampUnlock()
    {
        PlayMusic("FirecampUnlock");
    }
    public void PlayerDeath()
    {
        PlayPlayer("Death");
    }
    public void Hit()
    {
        PlayEnemy("Hit");
    }
    public void PlayPlayer(string name)
    {
        Sound s = Array.Find(sfxSound, x => x.name == name);

        if (s != null)
        {
            playerSource.clip = s.clip;
            playerSource.Play();
        }
    }
    public void PlayHardAttack(string name)
    {
        Sound s = Array.Find(sfxSound, x => x.name == name);

        if (s != null)
        {
            hardAttackSource.clip = s.clip;
            hardAttackSource.Play();
        }
    }
    public void PlaySoftAttack(string name)
    {
        Sound s = Array.Find(sfxSound, x => x.name == name);

        if (s != null)
        {
            softAttackSource.clip = s.clip;
            softAttackSource.Play();
        }
    }
    public void PlayFirecamp(string name)
    {
        Sound s = Array.Find(sfxSound, x => x.name == name);

        if (s != null)
        {
            firecampSource.clip = s.clip;
            firecampSource.Play();
        }
    }
    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSound, x => x.name == name);

        if (s != null)
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }
    public void PlayAmbience(string name)
    {
        Sound s = Array.Find(sfxSound, x => x.name == name);

        if (s != null)
        {
            ambienceSource.clip = s.clip;
            ambienceSource.Play();
        }
    }
    public void PlayEnemy(string name)
    {
        Sound s = Array.Find(sfxSound, x => x.name == name);

        if (s != null)
        {
            enemySource.clip = s.clip;
            enemySource.Play();
        }
    }
}


