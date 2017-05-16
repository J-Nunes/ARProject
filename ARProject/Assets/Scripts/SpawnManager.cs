using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<Transform> attack_pos;
    public List<Transform> suicide_pos;
    public List<Transform> spawners;
    public Transform bunker;

    public List<WavesResources> waves;
    WavesResources current_wave;
    bool wave_starting;
    bool spawning;
    float timer;

    public GameObject soldier;
    public GameObject hostage;

    // Use this for initialization
    void Start ()
    {
        wave_starting = true;
        spawning = true;
        current_wave = waves[0];
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (spawning)
        {
            if (wave_starting)
            {
                InitialSpawn();
            }

            else
            {

            }
        }

        else if(current_wave.units.Count <= 0)
        {

        }
	}

    void InitialSpawn()
    {
        for (int i = 0;  i < current_wave.initial_soldiers; i++)
            Spawn(0);        
        
        for (int i = 0; i < current_wave.initial_suicides; i++)
            Spawn(1);
        
        for (int i = 0; i < current_wave.initial_hostages; i++)
            Spawn(2);

        wave_starting = false;
    }

    void Spawn(int type)
    {
        int spawner = Random.Range(0, spawners.Count);
        GameObject new_unit;

        switch (type)
        {
            //Soldiers
            case 0:
                new_unit = ((GameObject)Instantiate(soldier, spawners[spawner].position, new Quaternion()));

                int attack_id = Random.Range(0, attack_pos.Count);
                new_unit.GetComponent<Enemy>().position_attack = attack_pos[attack_id];
                current_wave.units.Add(new_unit);
                break;

            //Suicides
            case 1:
                new_unit = ((GameObject)Instantiate(soldier, spawners[spawner].position, new Quaternion()));

                int suicide_id = Random.Range(0, suicide_pos.Count);
                new_unit.GetComponent<Enemy>().position_attack = attack_pos[suicide_id];
                current_wave.units.Add(new_unit);
                break;

            //Hostages
            case 2:
                new_unit = ((GameObject)Instantiate(hostage, spawners[spawner].position, new Quaternion()));

                new_unit.GetComponent<Hostage_Script>().destination = bunker.position;
                current_wave.units.Add(new_unit);
                break;
        }        
    }

    public void RemoveUnit(GameObject unit_to_remove)
    {
        current_wave.units.Remove(unit_to_remove);
    }
}
