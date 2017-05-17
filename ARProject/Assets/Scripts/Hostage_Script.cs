using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Hostage_Script : MonoBehaviour
{
    SpawnManager game_m;
    public NavMeshAgent agent;
    public Vector3 destination;
    public EnemyManager enemy_manager;

    public bool have_killer = false;

    public Animator animator;

    //Bomb
    Explosion explosion_go;

    // Use this for initialization
    void Start()
    {
        game_m = GameObject.Find("GameManager").GetComponent<SpawnManager>();
        enemy_manager = game_m.GetComponent<EnemyManager>();
        explosion_go = GameObject.Find("Bomb").GetComponent<Explosion>();
        agent.SetDestination(destination);
    }

    // Update is called once per frame
    void Update()
    {
        if (explosion_go.activate_bomb)
        {

            if (Vector3.Distance(transform.position, explosion_go.transform.position) <= 0.3f)
            {
                Target hostage_target = gameObject.GetComponent<Target>();
                StartCoroutine(hostage_target.Die());
            }
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.name == "Bunker")
        {
            GameObject del = gameObject;  
            Destroy(gameObject);
            game_m.Del_Hostages_Units(del);
            game_m.RemoveUnit(del);

        }
    }
}
