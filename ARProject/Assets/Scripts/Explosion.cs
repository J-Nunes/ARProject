using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

    public bool activate_bomb = false;
    public Renderer render_mesh;
    public Collider col;
    public ParticleSystem explsion_effect;
    public SpawnManager spawn_manager;

    public AudioClip explosion;
    public AudioSource audio_source;

    // Use this for initialization
    void Start ()
    {
        
    }
	
	// Update is called once per frame
	void Update ()
    {
		
        if(activate_bomb)
        {
            audio_source.PlayOneShot(explosion);
            StartCoroutine(Explode());        
        }

        if(!spawn_manager.enabled)
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
