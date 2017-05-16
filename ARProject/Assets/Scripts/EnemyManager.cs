using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public List<GameObject> enemies = new List<GameObject>();
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
    }

  

    
}
