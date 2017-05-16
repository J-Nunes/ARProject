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
    public float radius_attack_pos;
    public float radius_explosion_pos;
    public float time_to_change_position_attack = 0.5f;

    public bool attack_hostages = true;
    public Animator animator;

    //Player
    Transform camera;
    Shoot player;
    public bool kill_player = false;
    public float damage = 1;

    void Awake()
    {
        position_enemy = GetComponent<Transform>();
        camera = GameObject.Find("Camera").GetComponent<Transform>();
        player = GameObject.Find("Crosshair").GetComponent<Shoot>();
    }

    // Use this for initialization
    void Start()
    {
        //After the respawn of the character, this goes to the attack position
        CalcRandomPos();
    }

    // Update is called once per frame
    void Update()
    {
        //When the soldier arrive to the attack position
        if (Vector3.Distance(position_enemy.position, destination) <= 0.05f)
        {
            animator.SetBool("Run", false);

            animator.SetBool("Shoot Player", true);
            //Orient Soldier to the Player
            Vector3 target_pos = camera.position;
            target_pos.y = position_enemy.position.y;
            position_enemy.LookAt(target_pos);
            player.live -= damage * Time.deltaTime;
        }

    }

    public void CalcRandomPos()
    {
        Vector2 random;
        if (kill_player)
        {
            random = Random.insideUnitCircle * radius_attack_pos;
        }
        else
        {
            random = Random.insideUnitCircle * radius_explosion_pos;
        }

        destination = position_attack.position;
        destination.x += random.x;
        destination.z += random.y;

        animator.SetBool("Run", true);

        agent.SetDestination(destination);

        if (kill_player == false)
        {
            StartCoroutine(Enemy_Explode());
        }
    }

    IEnumerator Enemy_Explode()
    {
        int time = Random.Range(3, 8); ;
        yield return new WaitForSeconds(time);

        //Die
        Target die = gameObject.GetComponent<Target>();
        StartCoroutine(die.Die());

    }
}
