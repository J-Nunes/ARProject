using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

    Transform pos;
    public bool explosion = false;
    int vel = 40;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
        if(explosion && transform.position.y > 0)
        {
            Vector3 new_pos = transform.position;
            new_pos.y -= vel * Time.deltaTime;
            transform.position = new_pos;

        }
	}


    private void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.name == "Power_Up")
        {
            Debug.Log("Hostage die power up");
        }
    }


}
