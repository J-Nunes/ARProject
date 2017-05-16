﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

    public bool activate_bomb = false;
    public Renderer render_mesh;
    public Collider col;
    public ParticleSystem explsion_effect;
    

	// Use this for initialization
	void Start () {

        render_mesh = GetComponent<Renderer>();
        col = GetComponent<Collider>();
        
    }
	
	// Update is called once per frame
	void Update () {
		
        if(activate_bomb)
        {    
            StartCoroutine(Explode());        
        }
        else
        {
            render_mesh.enabled = false;
            col.enabled = false;
        }
	}

    IEnumerator Explode()
    {

        render_mesh.enabled = true;
        explsion_effect.Play();
        yield return new WaitForSeconds(0.1f);

        activate_bomb = false;
        render_mesh.enabled = false;
        col.enabled = false;

    }

    public void Activate_Explosion()
    {
        activate_bomb = true;
    }


}
