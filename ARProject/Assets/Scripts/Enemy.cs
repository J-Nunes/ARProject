using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
   
    Transform position_enemy;
    public Transform position_attack;

    public NavMeshAgent agent;
    public Vector3 destination;

   // GameManager g_manager;
    public float radius;
    public float time_to_change_position_attack = 0.5f;

    public bool attack_hostages = true;
    public Animator animator;

    //Player
    Transform player;
    public bool kill_player = false;

    public Hostage_Script hostage_target;
    public EnemyManager enemy_manager;

    void Awake()
    {
        position_enemy = GetComponent<Transform>();
        player = GameObject.Find("Camera").GetComponent<Transform>();

    }

    // Use this for initialization
    void Start ()
    {      
        //After the respawn of the character, this goes to the attack position
        CalcRandomPos();
        Debug.Log(player.position);
    }
	
	// Update is called once per frame
	void Update ()
    {

        if (Vector3.Distance(position_enemy.position, destination) <= 0.05f)
        {
           // Debug.Log("Look to die");
            animator.SetBool("Run", false);

            if (kill_player)
            {
                animator.SetBool("Shoot Player", true);

                //Orient Soldier to the Player
                Vector3 target_pos = player.position;
                target_pos.y = position_enemy.position.y;
                position_enemy.LookAt(target_pos);
            }
            else
            {
                

               /* if (hostage_target.GetComponent<Target>().health > 0)
                {
                    animator.SetBool("Shoot", true);

                    //Orient Soldier to hostage
                    Vector3 target_pos = hostage_target.GetComponent<Transform>().position;
                    target_pos.y = position_enemy.position.y;
                    position_enemy.LookAt(target_pos);
                    Debug.Log("Look to die");
                }
                else
                {
                    kill_player = true;
                    animator.SetBool("Shoot", false);
                }*/
            }

         
            /* for (int i = 0; i < g_manager.soldiers.cou; i++)
             {
                 if (g_manager.soldiers[i].gameObject.GetComponent<Hostage_Script>() != null)
                 {
                     Hostage_Script hostage = g_manager.soldiers[i].gameObject.GetComponent<Hostage_Script>();

                 if (hostage.have_killer == false)
                 {

                     hostage.have_killer = true;
                     //Look to the hostage
                     Vector3 target_pos = g_manager.soldiers[i].gameObject.GetComponent<Transform>().position;
                     target_pos.y = position_enemy.position.y;
                     position_enemy.LookAt(target_pos);
                     Debug.Log("Look to die");
                     break;


                 }

        
                 }
             }*/

        }
    }

    public void CalcRandomPos()
    {
        Vector2 random = Random.insideUnitCircle * radius;
        
        destination = position_attack.position;
        destination.x += random.x;
        destination.z += random.y;

        animator.SetBool("Run", true);
 

        agent.SetDestination(destination);
       
    }

    public void Assign_Hostage()
    {
        Enemy enemy = this;
        //Go to enemy manager and assign to this enemy a target
        enemy_manager.Assign_Target_To_Enemy(enemy);
    }

}
