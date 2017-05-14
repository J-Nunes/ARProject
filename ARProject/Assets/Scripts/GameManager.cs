﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    public static GameManager g_manager;
    public EnemyManager enemy_manager;
    public Transform Respawn_1;
    public Transform Respawn_2;

    //Amount
    public int max_soldiers_capacity = 10;
    public int min_soliders_Capacity = 5;
    private int amount_enemies_on_level = 0;

    int soldier_respawn;

    //List of Soldiers
    public List<GameObject> soldiers = new List<GameObject>();
    public GameObject[] people;

    //Attack Positions
    public Transform attack_position_1;
    public Transform attack_position_2;
    public Transform attack_hostage_1;
    public Transform attack_hostage_2;

    //Bunker
    public Transform bunker_position;
    Vector3 position_soldier;

    //Levels
    bool are_all_soldiers_spawned = false;
    int current_level = 0;
    public int max_levels = 5;


    void Awake()
    {
        g_manager = this;
    }

    // Use this for initialization
    void Start()
    {
        current_level++;
        amount_enemies_on_level = Random.Range(min_soliders_Capacity, max_soldiers_capacity);
        Debug.Log("Amount Enemies");
        Debug.Log(amount_enemies_on_level);
    }

    // Update is called once per frame
    void Update()
    {
        if (current_level < max_levels)
        {
            //Check if all the soldiers have been created at the beginning
            if (soldiers.Count == amount_enemies_on_level)
            {
                are_all_soldiers_spawned = true;
            }

            //Create the soldiers
            if (soldiers.Count < amount_enemies_on_level && are_all_soldiers_spawned == false)
            {
                Spwan_Character();
            }

            //When all the soldiers have been removed
            if (soldiers.Count == 0 && are_all_soldiers_spawned)
            {
                are_all_soldiers_spawned = false;
                current_level++;
                max_soldiers_capacity += 5;
                min_soliders_Capacity += 3;
                Debug.Log(current_level);
                amount_enemies_on_level = Random.Range(min_soliders_Capacity, max_soldiers_capacity);
                Debug.Log("Amount Enemies");
                Debug.Log(amount_enemies_on_level);
            }


        }
    }

    void Choose_Respawn()
    {

        int value_respawn = Random.Range(1, 10);


        if (value_respawn <= 5)
        {
            position_soldier = Respawn_2.position;
            soldier_respawn = 2;
        }
        else
        {
            position_soldier = Respawn_1.position;
            soldier_respawn = 1;
        }
    }

    void Spwan_Character()
    {
        //Wich character we will chose
        int value = Random.Range(0, people.Length);

        //Respawn
        Choose_Respawn();

        //Creatio of the new object
        GameObject soldier_agent = ((GameObject)Instantiate(people[value], position_soldier, new Quaternion()));

        //Attack destination
        if (soldier_agent.GetComponent<Enemy>() != null)
        {
            Enemy sold_1 = soldier_agent.GetComponent<Enemy>();

            int value_type_attack = Random.Range(1, 10);

            //Attack player
            if (value_type_attack <= 5)
            {
                int attack_pos = Random.Range(1, 10);

                if (attack_pos <= 5)
                {
                    sold_1.position_attack = attack_position_1;
                }
                else
                {
                    sold_1.position_attack = attack_position_2;
                }

                sold_1.kill_player = true;
            }
            //Attack hostages
            else if (value_type_attack <= 10 && value_type_attack > 5)
            {
                int attack_pos = Random.Range(1, 10);

                if (attack_pos <= 5)
                {
                    sold_1.position_attack = attack_hostage_1;
                }
                else
                {
                    sold_1.position_attack = attack_hostage_2;
                }

                sold_1.kill_player = false;

            }
            enemy_manager.Add_Enemy(soldier_agent);
        
        }

        //Go to bunker
        if (soldier_agent.GetComponent<Hostage_Script>() != null)
        {
            Hostage_Script sold_1 = soldier_agent.GetComponent<Hostage_Script>();
            sold_1.destination = bunker_position.position;
        }

        soldiers.Add(soldier_agent);

        soldier_respawn = 0;
        Debug.Log(soldiers.Count);
    }

    public void Remove_From_List(GameObject go)
    {
        soldiers.Remove(go);
        Debug.Log("Remove");
        Debug.Log(soldiers.Count);
    }

    public int Get_Amount_Soldiers()
    {
        return soldiers.Count;
    }

    public List<GameObject> Get_List()
    {
        return soldiers;
    }

}
