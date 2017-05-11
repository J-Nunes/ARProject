using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
   
   // Transform position_enemy;
    public Transform position_attack;

    public NavMeshAgent agent;
    Vector3 destination;

    GameManager g_manager;
    public float radius;

    void Awake()
    {
        //position_enemy = GetComponent<Transform>();
        //position_attack = GameObject.Find("Sphere").GetComponent<Transform>();
    }

    // Use this for initialization
    void Start ()
    {      
        //After the respawn of the character, this goes to the attack position
        CalcRandomPos();
    }
	
	// Update is called once per frame
	void Update ()
    {
     
    }

    void CalcRandomPos()
    {
        Vector2 random = Random.insideUnitCircle * radius;
       

        destination = position_attack.position;
        destination.x += random.x;
        destination.z += random.y;

        agent.SetDestination(destination);
    }

   

  
}
