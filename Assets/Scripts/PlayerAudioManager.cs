using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    AudioSource player;
    [SerializeField] AudioClip playerDeathSound;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Soundmanager").GetComponent<AudioSource>();
    }


    public void DeathSound()
    {
        player.PlayOneShot(playerDeathSound);
    }
}
