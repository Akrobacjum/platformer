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
    int SoftAttackCounter = 1;
    int HardAttackCounter;
    int SoftHitCounter = 1;
    int HardHitCounter = 1;

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
        playerSource.Play();
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
        else
        {
            DamageCounter++;
            PlayPlayer("Damage1");
        }
    }
    public void SoftAttack()
    {
        if (SoftAttackCounter == 6)
        {
            SoftAttackCounter = 1;
            PlaySoftAttack("SoftAttack6");
        }
        else if (SoftAttackCounter == 5)
        {
            SoftAttackCounter++;
            PlaySoftAttack("SoftAttack5");
        }
        else if (SoftAttackCounter == 4)
        {
            SoftAttackCounter++;
            PlaySoftAttack("SoftAttack4");
        }
        else if (SoftAttackCounter == 3)
        {
            SoftAttackCounter++;
            PlaySoftAttack("SoftAttack3");
        }
        else if (SoftAttackCounter == 2)
        {
            SoftAttackCounter++;
            PlaySoftAttack("SoftAttack2");
        }
        else
        {
            SoftAttackCounter++;
            PlaySoftAttack("SoftAttack1");
        }
    }
    public void HardAttack()
    {
        if (HardAttackCounter == 3)
        {
            HardAttackCounter = 1;
            PlayHardAttack("HardAttack3");
        }
        else if (HardAttackCounter == 2)
        {
            HardAttackCounter++;
            PlayHardAttack("HardAttack2");
        }
        else
        {
            HardAttackCounter++;
            PlayHardAttack("HardAttack1");
        }
    }
    public void SoftHit()
    {
        if (SoftHitCounter == 6)
        {
            SoftHitCounter = 1;
            PlayEnemy("SoftHit6");
        }
        else if (SoftHitCounter == 5)
        {
            SoftHitCounter++;
            PlayEnemy("SoftHit5");
        }
        else if (SoftHitCounter == 4)
        {
            SoftHitCounter++;
            PlayEnemy("SoftHit4");
        }
        else if (SoftHitCounter == 3)
        {
            SoftHitCounter++;
            PlayEnemy("SoftHit3");
        }
        else if (SoftHitCounter == 2)
        {
            SoftHitCounter++;
            PlayEnemy("SoftHit2");
        }
        else
        {
            SoftHitCounter++;
            PlayEnemy("SoftHit1");
        }
    }

    public void HardHit()
    {
        if (HardHitCounter == 3)
        {
            HardHitCounter = 1;
            PlayEnemy("HardHit3");
        }
        else if (HardHitCounter == 2)
        {
            HardHitCounter++;
            PlayEnemy("HardHit2");
        }
        else
        {
            HardHitCounter++;
            PlayEnemy("HardHit1");
        }
    }
    public void PlayerDeath()
    {
        playerSource.Play();
        PlayPlayer("Death");
    }
    public void PlayerJumpSFX()
    {
        playerSource.Play();
        PlayPlayer("PlayerJumpSFX");
    }
    public void PlayerWalk()
    {
        Debug.Log("S");
        playerSource.Play();
        PlayPlayer("Walk");
    }
    public void PlayerRun()
    {
        playerSource.Play();
        PlayPlayer("Run");
    }
    public void PlayerFall()
    {
        playerSource.Play();
        PlayPlayer("Fall");
    }
    public void PlayerStop()
    {
        playerSource.Stop();
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


