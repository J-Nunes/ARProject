using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Target : MonoBehaviour {

    SpawnManager spawn_manager;
    GameManager game_manager;
    public NavMeshAgent agent;
    public float health = 30f;
    
    public AudioClip death;
    public AudioSource audio_source;

    // Use this for initialization
    void Start()
    {
        spawn_manager = GameObject.Find("GameManager").GetComponent<SpawnManager>();
        game_manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if(health <= 0f)
        {
            audio_source.PlayOneShot(death);
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
        }

        yield return new WaitForSeconds(0.3f);

        //Check if is an enemy or a hostage
        if (gameObject.GetComponent<Enemy>() != null)
        {
            //Delete an enemy
            Destroy(gameObject);
            GameObject del = gameObject;
            spawn_manager.RemoveUnit(del);
        }
        else
        {
            //Delete a hostage
            Destroy(gameObject);
            GameObject hostages = gameObject;
            spawn_manager.Del_Hostages_Units(hostages);
            spawn_manager.RemoveUnit(hostages);
            game_manager.death_hostages++;
        }

        
    

        

    }

}
