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

    void Start()
    {
        currentAmmo = maxAmmo;
    }

	// Update is called once per frame
	void Update () {

        if (is_reloading)
            return;

        if(currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }
		
        if(Input.GetMouseButtonDown(0) && currentAmmo > 0)
        {
            Shoot();
        }
	}

    IEnumerator Reload()
    {
        is_reloading = true;
        Debug.Log("Reloading");
        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        Debug.Log("Reloaded");
        is_reloading = false;
        animator.SetBool("Reloading", false);
    }

    void Shoot()
    {
        currentAmmo--;
        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            Power_Ups pw = hit.transform.GetComponent<Power_Ups>();
            if (pw != null)
            {
                time_manager.SlowMotion();
            }

        }
    }
}
