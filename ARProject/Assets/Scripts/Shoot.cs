using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shoot : MonoBehaviour
{
    public float live = 100;
    public float damage = 10f;
    public float range = 400f;

    public int maxAmmo = 10;
    private int currentAmmo;
    public float reloadTime = 2f;
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
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            Explosion bomb = hit.transform.GetComponent<Explosion>();
            Enemy enemy = hit.transform.GetComponent<Enemy>();

            if (target != null)
            {
                target.TakeDamage(damage);
            }

            if (bomb != null)
            {
                Debug.Log("Bomb");
                bomb.Activate_Explosion();
                powerup_go.bomb_used = true;
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
