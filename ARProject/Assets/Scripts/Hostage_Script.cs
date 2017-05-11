using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Hostage_Script : MonoBehaviour {

    public NavMeshAgent agent;
    public Vector3 destination;

    // Use this for initialization
    void Start () {
        agent.SetDestination(destination);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
