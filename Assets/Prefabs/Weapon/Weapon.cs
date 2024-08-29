using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] string AttachSlotTag;
    [SerializeField] float AttackRateMult = 1f;
    [SerializeField] AnimatorOverrideController overrideController;

    [SerializeField] AudioClip WeaponAudio;
    [SerializeField] float volume = 1f;
    AudioSource WeaponAudioSource;
    private void Awake()
    {
        WeaponAudioSource = GetComponent<AudioSource>();
    }

    public void PlayWeaponAudio()
    {
        WeaponAudioSource.PlayOneShot(WeaponAudio, volume);
    }
    public abstract void Attack();

    public string GetAttachSlotTag()
    {
        return AttachSlotTag;
    }
    public GameObject Owner
    {
        get;
        private set;
    }

    public void Init(GameObject owner)
    {
        Owner = owner;
        UnEquip();
    }

    public void Equip()
    {
        gameObject.SetActive(true);
        Owner.GetComponent<Animator>().runtimeAnimatorController = overrideController;
        Owner.GetComponent<Animator>().SetFloat("AttackRateMult", AttackRateMult);
    }

    public void UnEquip()
    {
        gameObject.SetActive(false);
    }

    public void DamageGameObject(GameObject objToDamage, float amt)
    {
        HealthComponent healthComp = objToDamage.GetComponent<HealthComponent>();
        if(healthComp != null)
        {
            healthComp.changeHealth(-amt, Owner);
        }
    }
}
