using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    public float damage = 10f;
    public float range = 400f;

    public int maxAmmo = 10;
    private int currentAmmo;
    public float reloadTime = 1f;
    public bool is_reloading = false;

    public Time_Manager time_manager;
    public Camera cam;
    public Power_Ups powerup_go;

    bool is_infinite_ammo = false;

    void Start()
    {
        currentAmmo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {

        if (is_reloading)
            return;

        if (currentAmmo <= 0 && is_infinite_ammo == false)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetMouseButtonDown(0) && currentAmmo > 0)
        {
            Shot();

        }

    }

    IEnumerator Reload()
    {
        is_reloading = true;

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        is_reloading = false;
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
            Debug.Log(hit.transform.name);

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
