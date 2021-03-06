﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    Transform position_enemy;
    public Transform position_attack;

    public NavMeshAgent agent;
    public Vector3 destination;
    bool shooting;

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

    //Bomb
    Explosion explosion_go;
    public ParticleSystem explosion_effect;
    public AudioClip explosion;
    public AudioClip shoot_loop;
    public AudioSource audio_source;

    public SpawnManager spawner;

    void Awake()
    {
        position_enemy = GetComponent<Transform>();
        camera = GameObject.Find("Camera").GetComponent<Transform>();
        player = GameObject.Find("Crosshair").GetComponent<Shoot>();
        explosion_go = GameObject.Find("Bomb").GetComponent<Explosion>();
        spawner = GameObject.Find("GameManager").GetComponent<SpawnManager>();
        explosion_effect = GameObject.Find("EnemyExplosionEffect").GetComponent<ParticleSystem>();       
    }

    // Use this for initialization
    void Start()
    {
        //After the respawn of the character, this goes to the attack position
        CalcRandomPos();
        shooting = false;
    }

    // Update is called once per frame
    void Update()
    {
        //When the soldier arrive to the attack position
        if (Vector3.Distance(position_enemy.position, destination) <= 0.05f)
        {
            if (!shooting)
            {
                audio_source.loop = true;
                audio_source.clip = shoot_loop;
                audio_source.Play();
            }
            shooting = true;
            animator.SetBool("Run", false);
            
            animator.SetBool("Shoot Player", true);
            //Orient Soldier to the Player
            Vector3 target_pos = camera.position;
            target_pos.y = position_enemy.position.y;
            position_enemy.LookAt(target_pos);
            if (player.live > 0)
                player.live -= damage * Time.deltaTime;
            else
                player.live = 0;
        }

        if (explosion_go.activate_bomb)
        {
            
            if (Vector3.Distance(position_enemy.position, explosion_go.transform.position) <= 0.3f)
            {
                Target enemy_target = gameObject.GetComponent<Target>();
                StartCoroutine(enemy_target.Die());
            }
        }

        if (!spawner.enabled)
            audio_source.Stop();
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
        int time = Random.Range(3, 7); 

        yield return new WaitForSeconds(time);

        List<GameObject> hostages_list = spawner.Get_Hostages_Units();
        List<GameObject> hostages_must_die = new List<GameObject>();  
        for (int i = 0; i < hostages_list.Count; i++)
        {
            if (Vector3.Distance(position_enemy.position, hostages_list[i].transform.position) <= 0.17f)
            {
                hostages_must_die.Add(hostages_list[i]);
            }
        }

        for (int i = 0; i < hostages_must_die.Count; i++)
        {
            Target target = hostages_must_die[i].GetComponent<Target>();
            StartCoroutine(target.Die());
        }

        hostages_must_die.Clear();

        //Die
        Target die = gameObject.GetComponent<Target>();
        audio_source.PlayOneShot(explosion);
        explosion_effect.transform.position = transform.position;
        explosion_effect.Play();
        StartCoroutine(die.Die());

    }
}
