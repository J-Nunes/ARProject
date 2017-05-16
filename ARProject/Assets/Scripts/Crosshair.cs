using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        Cursor.visible = false;		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = Input.mousePosition;		
	}
}
