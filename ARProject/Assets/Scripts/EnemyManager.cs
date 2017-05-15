using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public List<GameObject> enemies = new List<GameObject>();
    public List<GameObject> hostages = new List<GameObject>();
    Enemy enemy_go;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Add_Enemy(GameObject new_enemy)
    {
        enemies.Add(new_enemy);

        enemy_go = new_enemy.GetComponent<Enemy>();

        if (enemy_go.kill_player)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (Vector3.Distance(enemy_go.destination, enemies[i].GetComponent<Transform>().position) <= 1f)
                {
                    Debug.Log("Same_pos");
                    enemy_go.CalcRandomPos();

                }
            }
        }
        else
        {
            Hostage_Script hostage_go;
            for (int i = 0; i < hostages.Count; i++)
            {
                hostage_go = hostages[i].GetComponent<Hostage_Script>();
                if (hostage_go.have_killer == false)
                {
                    enemy_go.hostage_target = hostage_go;
                    hostage_go.have_killer = true;
                    break;
                }
            }

            if (enemy_go.hostage_target == null)
            {
                enemy_go.kill_player = true;
            }
        }
    }

    public void Add_Hostage(GameObject new_hostage)
    {
        hostages.Add(new_hostage);
    }

    public void Assign_Target_To_Enemy( Enemy killer)
    {
        Hostage_Script hostage_go;
        for (int i = 0; i < hostages.Count; i++)
        {
            hostage_go = hostages[i].GetComponent<Hostage_Script>();
            if(hostage_go.have_killer == false)
            {
                killer.hostage_target = hostage_go;
                hostage_go.have_killer = true;
                break;
            }
        }

        if(killer.hostage_target == null)
        {
            killer.kill_player = true;
        }
    }
}
