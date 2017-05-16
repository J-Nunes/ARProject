﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Target : MonoBehaviour {

    GameManager game_m;
    EnemyManager enemy_manager;
    public NavMeshAgent agent;
    public float health = 30f;

    // Use this for initialization
    void Start()
    {
        game_m = GameObject.Find("GameManager").GetComponent<GameManager>();
        enemy_manager = GameObject.Find("GameManager").GetComponent<EnemyManager>();
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if(health <= 0f)
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        agent.Stop();

        //Check if is an enemy or a hostage
        if (gameObject.GetComponent<Enemy>() != null)
        {
            //Is an enemy
            Enemy soldier = gameObject.GetComponent<Enemy>();
            soldier.animator.SetBool("Die", true);
            GameObject enemy = gameObject;
            enemy_manager.enemies.Remove(enemy);
        }
        else
        {
            //Is a hostage
            Hostage_Script hostage = gameObject.GetComponent<Hostage_Script>();
            hostage.animator.SetBool("Hostage_Die", true);
            GameObject host = gameObject;
            enemy_manager.hostages.Remove(host);
        }

        yield return new WaitForSeconds(2.5f);

        Destroy(gameObject);
        GameObject del = gameObject;
        game_m.Remove_From_List(del);
           
    }

}
