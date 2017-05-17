using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Target : MonoBehaviour {

    SpawnManager game_m;
    public NavMeshAgent agent;
    public float health = 30f;

    // Use this for initialization
    void Start()
    {
        game_m = GameObject.Find("GameManager").GetComponent<SpawnManager>();
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if(health <= 0f)
        {
            StartCoroutine(Die());
        }
    }

    public IEnumerator Die()
    {
        agent.Stop();

        //Check if is an enemy or a hostage
        if (gameObject.GetComponent<Enemy>() != null)
        {
            //Is an enemy
            Enemy soldier = gameObject.GetComponent<Enemy>();
            soldier.animator.SetBool("Die", true);
        }
        else
        {
            //Is a hostage
            Hostage_Script hostage = gameObject.GetComponent<Hostage_Script>();
            hostage.animator.SetBool("Hostage_Die", true);
            GameObject hostages = gameObject;
            game_m.Del_Hostages_Units(hostages);
        }

        yield return new WaitForSeconds(2.5f);

        Destroy(gameObject);
        GameObject del = gameObject;
        game_m.RemoveUnit(del);
           
    }

}
