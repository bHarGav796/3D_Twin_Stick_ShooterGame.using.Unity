using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour, IRewardListener
{
    public delegate void OnHealthChange(float health, float delta, float maxHealth);
    public delegate void OnTakeDamage(float health, float delta, float maxHealth, GameObject Instigator);
    public delegate void OnHealthEmpty(GameObject Killer);

    [SerializeField] float health = 100;
    [SerializeField] float maxhealth = 100;
    
    public event OnHealthChange onHealthChange;
    public event OnTakeDamage onTakeDamage;
    public event OnHealthEmpty onHealthEmpty;

    [Header("Audio")]
    [SerializeField] AudioClip HitAudio;
    [SerializeField] AudioClip DeathAudio;
    [SerializeField] float volume;
    AudioSource audioSrc;

    private void Awake()
    {
        audioSrc = GetComponent<AudioSource>();
    }
    public void BroadcastHealthValueImmeidately()
    {
        onHealthChange?.Invoke(health, 0, maxhealth);
    }

    public void changeHealth(float amt, GameObject Instigator)
    {
        if(amt == 0 || health == 0)
        {
            return;
        }

        health += amt;

        if(amt < 0)
        {
            onTakeDamage?.Invoke(health, amt, maxhealth, Instigator);
            Vector3 loc = transform.position;
            if(!audioSrc.isPlaying)
            {
                audioSrc.PlayOneShot(HitAudio, volume);
            }
        }

        onHealthChange?.Invoke(health, amt, maxhealth);

        if(health <= 0)
        {
            health = 0;
            onHealthEmpty?.Invoke(Instigator);
            Vector3 loc = transform.position;
            GameplayStatics.PlayAudioAtLoc(DeathAudio, loc, 1);
        }

        //Debug.Log($"{gameObject.name}, taking damage: {amt}, health is now: {health}");
    }

    public void Reward(Reward reward)
    {
        health = Mathf.Clamp(health + reward.healthReward, 0, maxhealth);
        onHealthChange?.Invoke(health, reward.healthReward, maxhealth);
    }
}
