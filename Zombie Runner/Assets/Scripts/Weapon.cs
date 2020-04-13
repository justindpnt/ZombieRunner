using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 30f;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitEffect;
    [SerializeField] GameObject hitBlood;
    [SerializeField] Ammo ammoSlot;
    [SerializeField] AmmoType ammoType;
    [SerializeField] float timeBetweenShots = 0.5f;
    [SerializeField] TextMeshProUGUI ammoText;


    bool canShoot = true;

    AudioSource fireSoundOne;
    AudioSource fireSoundTwo;
    AudioSource fireSoundThree;

    private void Start()
    {
        AudioSource[] soundFiles = GetComponents<AudioSource>();
        fireSoundOne = soundFiles[0];
        fireSoundTwo = soundFiles[1];
        fireSoundThree = soundFiles[2];
    }


    void Update()
    {
        DisplayAmmo();
        if (Input.GetMouseButtonDown(0) && (canShoot == true))
        {
            StartCoroutine(Shoot());
        }
    }

    private void DisplayAmmo()
    {
        int currentAmmo = ammoSlot.GetCurrentAmmo(ammoType);
        ammoText.text = currentAmmo.ToString();
    }
 
    IEnumerator Shoot()
    {
        canShoot = false;
        if (ammoSlot.GetCurrentAmmo(ammoType) > 0)
        {
            PlayMuzzleFlash();
            PlayGunFireSound();
            ProcessRayCast();
            ammoSlot.ReduceCurrentAmmo(ammoType);
        }
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }

    private void PlayGunFireSound()
    {
        float soundindex = UnityEngine.Random.Range(0f, 10f);
        if (soundindex < 3)
        {
            Debug.Log("1");
            fireSoundOne.PlayOneShot(fireSoundOne.clip);
        }
        else if (soundindex >= 3 && soundindex <= 7)
        {
            Debug.Log("2");
            fireSoundTwo.PlayOneShot(fireSoundTwo.clip);
        }
        else if (soundindex > 7)
        {
            Debug.Log("3");
            fireSoundThree.PlayOneShot(fireSoundThree.clip);
        }
    }

    private void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
    }

    private void ProcessRayCast()
    {
        RaycastHit hit;
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {
            if(hit.transform.tag == "Enemy")
            {
                GameObject impact = Instantiate(hitBlood, hit.point, Quaternion.LookRotation(hit.normal));
                AudioSource.PlayClipAtPoint(hit.transform.gameObject.GetComponent<EnemyAI>().bulletImpact.clip, hit.transform.position, .1F);
                Destroy(impact, 3f);
            }
            else
            {
                CreateHitImpact(hit);
            }
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target == null) return;
            target.takeDamage(damage);
        }
        else
        {
            return;
        }
    }

    private void CreateHitImpact(RaycastHit hit)
    {
        GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, .1f);
    }
}
