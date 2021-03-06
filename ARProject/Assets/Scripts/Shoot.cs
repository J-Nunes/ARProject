﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shoot : MonoBehaviour
{
    public float live = 100;
    public float damage = 10f;
    public float range = 400f;

    public int maxAmmo = 10;
    public int currentAmmo;
    public float reloadTime = 2f;
    public bool is_reloading = false;

    public GameManager game_manager;
    public Time_Manager time_manager;
    public Camera cam;
    public Power_Ups powerup_go;

    bool is_infinite_ammo = false;

    public AudioClip shot;
    public AudioClip reload;
    public AudioSource audio_source;

    void Start()
    {
        currentAmmo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if (game_manager.spawn_manager.enabled)
        {
            if (is_reloading)
                return;

            if (currentAmmo <= 0 && is_infinite_ammo == false)
            {
                audio_source.PlayOneShot(reload);
                StartCoroutine(Reload());
                return;
            }

            if (Input.GetMouseButtonDown(0) && currentAmmo > 0)
            {
                Shot();
                audio_source.PlayOneShot(shot);
            }

            if (live <= 0)
                game_manager.lose = true;
        }

    }

    IEnumerator Reload()
    {
        is_reloading = true;
        Color tmp = gameObject.GetComponent<Image>().color;
        tmp.g = 0.8f;
        tmp.a = 0.3f;
        gameObject.GetComponent<Image>().color = tmp;

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        is_reloading = false;
        tmp.g = 0.0f;
        tmp.a = 0.8f;
        gameObject.GetComponent<Image>().color = tmp;
    }

    void Shot()
    {
        if (is_infinite_ammo == false)
        {
            currentAmmo--;
        }

        RaycastHit hit;

        Ray r = cam.ScreenPointToRay(transform.position);
        if (Physics.Raycast(r, out hit))
        {
            Target target = hit.transform.GetComponent<Target>();
            Explosion bomb = hit.transform.GetComponent<Explosion>();
            Enemy enemy = hit.transform.GetComponent<Enemy>();

            if (target != null)
            {
                target.TakeDamage(damage);
            }

            if (bomb != null)
            {
                bomb.Activate_Explosion();
                //powerup_go.bomb_used = true;
            }

            if (enemy != null)
            {
                powerup_go.Increase_kills();
            }

            if (enemy == null && enemy == null && bomb == null) 
            {
                //Restart kills
                powerup_go.Reset_Kills();
            }
        }


    }

    public void InfiniteAmmo()
    {
        is_infinite_ammo = true;
    }

    public void NormalAmmo()
    {
        is_infinite_ammo = false;
    }
}
