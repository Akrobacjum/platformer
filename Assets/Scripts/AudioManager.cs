using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] musicSound, sfxSound;
    public AudioSource playerSource, hardAttackSource, softAttackSource, firecampSource, musicSource, ambienceSource, enemySource, playerDamageSource, skeletonSource, zombieSource, neuromancerSource;

    [SerializeField] GameObject UIManager;
    UIManager UIManagerScript;
    
    int DamageCounter = 1;
    int SoftAttackCounter = 1;
    int HardAttackCounter;
    int SoftHitCounter = 1;
    int HardHitCounter = 1;
    int SkeletonCounter = 1;
    int ZombieCounter = 1;
    int NeuromancerCounter = 1;

    private void Start()
    {
        UIManagerScript = UIManager.GetComponent<UIManager>();
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
        if (UIManagerScript.MenuActive == false)
        {
            if (DamageCounter == 7)
            {
                DamageCounter = 1;
                PlayPlayerDamage("Damage7");
            }
            else if (DamageCounter == 6)
            {
                DamageCounter++;
                PlayPlayerDamage("Damage6");
            }
            else if (DamageCounter == 5)
            {
                DamageCounter++;
                PlayPlayerDamage("Damage5");
            }
            else if (DamageCounter == 4)
            {
                DamageCounter++;
                PlayPlayerDamage("Damage4");
            }
            else if (DamageCounter == 3)
            {
                DamageCounter++;
                PlayPlayerDamage("Damage3");
            }
            else if (DamageCounter == 2)
            {
                DamageCounter++;
                PlayPlayerDamage("Damage2");
            }
            else
            {
                DamageCounter++;
                PlayPlayerDamage("Damage1");
            }
        }
    }
    public void SoftAttack()
    {
        if (UIManagerScript.MenuActive == false)
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
    public void Jump()
    {
        playerSource.Play();
        PlayPlayer("JumpSFX");
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
        musicSource.Play();
        PlayMusic("Death");
    }
    public void ElfDeath()
    {
        playerSource.Play();
        PlayPlayer("ElfDeath");
    }
    public void PlayerJumpSFX()
    {
        playerSource.Play();
        PlayPlayer("PlayerJumpSFX");
    }
    public void PlayerWalk()
    {
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
    public void SkeletonDMG()
    {
        if (SkeletonCounter == 5)
        {
            SkeletonCounter = 1;
            PlaySkeleton("SkeletonDMG5");
        }
        else if (SkeletonCounter == 4)
        {
            SkeletonCounter++;
            PlaySkeleton("SkeletonDMG4");
        }
        else if (SkeletonCounter == 3)
        {
            SkeletonCounter++;
            PlaySkeleton("SkeletonDMG3");
        }
        else if (SkeletonCounter == 2)
        {
            SkeletonCounter++;
            PlaySkeleton("SkeletonDMG2");
        }
        else if (SkeletonCounter == 1)
        {
            SkeletonCounter++;
            PlaySkeleton("SkeletonDMG1");
        }
    }
    public void ZombieDMG()
    {
        if (ZombieCounter == 2)
        {
            ZombieCounter = 1;
            PlayZombie("ZombieDMG2");
        }
        else if (ZombieCounter == 1)
        {
            ZombieCounter++;
            PlayZombie("ZombieDMG1");
        }
    }
    public void NeuromancerDMG()
    {
        if (NeuromancerCounter == 2)
        {
            NeuromancerCounter = 1;
            PlayNeuromancer("NeuromancerDMG2");
        }
        else if (NeuromancerCounter == 1)
        {
            NeuromancerCounter++;
            PlayNeuromancer("NeuromancerDMG1");
        }
    }
    public void SkeletonThrust()
    {
        PlaySkeleton("SkeletonThrust");
    }
    public void SkeletonSwing()
    {
        PlaySkeleton("SkeletonSwing");
    }
    public void ZombieClaw()
    {
        PlayZombie("ZombieClaw");
    }
    public void NeuromancerSpell()
    {
        PlayNeuromancer("NeuromancerSpell");
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
    public void PlayPlayerDamage(string name)
    {
        Sound s = Array.Find(sfxSound, x => x.name == name);

        if (s != null)
        {
            playerDamageSource.clip = s.clip;
            playerDamageSource.Play();
        }
    }
    public void PlaySkeleton(string name)
    {
        Sound s = Array.Find(sfxSound, x => x.name == name);

        if (s != null)
        {
            skeletonSource.clip = s.clip;
            skeletonSource.Play();
        }
    }
    public void PlayZombie(string name)
    {
        Sound s = Array.Find(sfxSound, x => x.name == name);

        if (s != null)
        {
            zombieSource.clip = s.clip;
            zombieSource.Play();
        }
    }
    public void PlayNeuromancer(string name)
    {
        Sound s = Array.Find(sfxSound, x => x.name == name);

        if (s != null)
        {
            neuromancerSource.clip = s.clip;
            neuromancerSource.Play();
        }
    }
}


