using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public float damage = 10f;
    public float range = 400f;

    public int maxAmmo = 10;
    private int currentAmmo;
    public float reloadTime = 1f;
    public bool is_reloading = false;

    public Animator animator;

    public Camera fpsCam;

    public Time_Manager time_manager;

    public Scope_Rifle scoped_rifle;
    public Power_Ups powerup_go;

    bool is_infinite_ammo = false;

    void Start()
    {
        currentAmmo = maxAmmo;
    }

    // Update is called once per frame
    void Update() {

        if (is_reloading)
            return;

        if (currentAmmo <= 0 && is_infinite_ammo == false)
        {
            deactivate_animation();
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetMouseButtonDown(0) && currentAmmo > 0)
        {
            Shoot();
            
        }
        else 
        {
            if (currentAmmo > 0)
                deactivate_animation();
        }   
       
    }

    void deactivate_animation()
    {
        if (scoped_rifle.Is_Scoped() == false)
        {
            animator.SetBool("Shoot", false);
        }
    }

    IEnumerator Reload()
    {
        is_reloading = true;
        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        is_reloading = false;
        animator.SetBool("Reloading", false);
    }

    void Shoot()
    {
            
        animator.SetBool("Shoot", true);

        if (is_infinite_ammo == false)
        {
            currentAmmo--;
        }

        RaycastHit hit;

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Target target = hit.transform.GetComponent<Target>();

            if (target != null)
            {
                target.TakeDamage(damage);
            }

            Enemy enemy = hit.transform.GetComponent<Enemy>();

            if (enemy != null)
            {
                powerup_go.Increase_kills();
            }
            else
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
