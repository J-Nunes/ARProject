using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Hostage_Script : MonoBehaviour
{
    GameManager game_m;
    public NavMeshAgent agent;
    public Vector3 destination;
    public EnemyManager enemy_manager;

    public bool have_killer = false;

    public Animator animator;

    // Use this for initialization
    void Start()
    {
        game_m = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        agent.SetDestination(destination);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.name == "Bunker")
        {
            Debug.Log("Hostage die");
            
            GameObject del = gameObject;  
            Destroy(gameObject);
            game_m.Remove_From_List(del);

        }

        if (col.gameObject.name == "Power_Up")
        {
            Debug.Log("Hostage die");

        
        }
    }
}
