using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Scope_Rifle : MonoBehaviour
{
    public Animator animator;
    public GameObject scopeOverlay;
    public GameObject weaponCamera;
    public Camera mainCamera;

    public float scopedFOV = 15f;
    private float normalFOV;

    public Weapon gun;
    private bool is_scope = false;


     // Use this for initialization
    void Start ()
    {
        normalFOV = mainCamera.fieldOfView;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (gun.is_reloading == false)
        {
            if (Input.GetMouseButtonDown(1))
            {
                StartCoroutine(OnScoped());
            }
            if (Input.GetMouseButtonUp(1))
            {
                OnUnscoped();
            }
        }
        else
        {
            if (is_scope)
            {
                is_scope = false;
                animator.SetBool("Scoped", false);
                scopeOverlay.SetActive(is_scope);
                weaponCamera.SetActive(true);

                mainCamera.fieldOfView = normalFOV;
            }
        }
    }

    void OnUnscoped()
    {
        is_scope = false;
        animator.SetBool("Scoped", false);
        scopeOverlay.SetActive(is_scope);
        weaponCamera.SetActive(true);

        mainCamera.fieldOfView = normalFOV;
    }

    IEnumerator OnScoped()
    {
        is_scope = true;
        animator.SetBool("Scoped", true);
        yield return new WaitForSeconds(.15f);
        scopeOverlay.SetActive(is_scope);
        weaponCamera.SetActive(false);

        mainCamera.fieldOfView = scopedFOV;
    }

  
}
