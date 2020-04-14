using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;

    AudioSource hitAudio;
    AudioSource gameOverAudio;

    public void Start()
    {
        AudioSource[] sounds = GetComponents<AudioSource>();
        hitAudio = sounds[0];
        gameOverAudio = sounds[1];
    }

    public void takeDamage(float damage)
    {
        hitPoints -= damage;
        if (hitPoints <= 0)
        {
            gameOverAudio.PlayOneShot(gameOverAudio.clip);
            GetComponent<DeathHandler>().HandleDeath();
        }
        else{
            hitAudio.PlayOneShot(hitAudio.clip);
        }
    }
}
