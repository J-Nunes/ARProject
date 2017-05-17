using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameManager game_manager;

    public List<Transform> attack_pos;
    public List<Transform> suicide_pos;
    public List<Transform> spawners;
    public Transform bunker;

    public List<WavesResources> waves;
    WavesResources current_wave;
    bool wave_starting;
    bool spawning;
    float timer;
    int current_subwave;

    public GameObject soldier;
    public GameObject hostage;

    // Use this for initialization
    void Start ()
    {
        StartWaves();  
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
                if(timer > current_wave.delay_time)
                {
                    SubWave();                    
                    timer = 0.0f;
                }
                else
                {
                    timer += Time.deltaTime;
                }
            }
        }
        
        else if(current_wave.units.Count <= 0)
        {
            int next_wave = waves.IndexOf(current_wave) + 1;
            if (next_wave < waves.Count)
            {
                current_wave = waves[next_wave];
                spawning = true;
                wave_starting = true;
                current_subwave = 0;
            }
            else
                game_manager.game = false;
        }
	}

    public void StartWaves()
    {
        wave_starting = true;
        spawning = true;
        current_wave = waves[0];
        timer = 0.0f;
        current_subwave = 0;
    }

    public void CleanUpUnits()
    {
        for(int i = 0; i < current_wave.units.Count; i++)
        {
            Destroy(current_wave.units[i]);
        }
        current_wave.units.Clear();

        for (int i = 0; i < current_wave.hostages.Count; i++)
        {
            Destroy(current_wave.hostages[i]);
        }
        current_wave.hostages.Clear();
    }

    void SubWave()
    {
        if(current_subwave < current_wave.sub_waves)
        {
            for(int i = 0; i < current_wave.units_number; i++)
            {
                int type = Random.Range(0, 3);
                Spawn(type);
            }
            current_subwave++;
        }

        else
            spawning = false;
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
                new_unit.GetComponent<Enemy>().kill_player = true;

                current_wave.units.Add(new_unit);
                break;

            //Suicides
            case 1:
                new_unit = ((GameObject)Instantiate(soldier, spawners[spawner].position, new Quaternion()));

                int suicide_id = Random.Range(0, suicide_pos.Count);
                new_unit.GetComponent<Enemy>().position_attack = attack_pos[suicide_id];
                new_unit.GetComponent<Enemy>().kill_player = false;

                current_wave.units.Add(new_unit);
                break;

            //Hostages
            case 2:
                new_unit = ((GameObject)Instantiate(hostage, spawners[spawner].position, new Quaternion()));

                new_unit.GetComponent<Hostage_Script>().destination = bunker.position;
                current_wave.units.Add(new_unit);
                current_wave.hostages.Add(new_unit);
                break;
        }        
    }

    public void RemoveUnit(GameObject unit_to_remove)
    {
        current_wave.units.Remove(unit_to_remove);
    }

    public List<GameObject> Get_Hostages_Units()
    {
        return current_wave.hostages;
    }

    public void Del_Hostages_Units(GameObject del)
    {
         current_wave.hostages.Remove(del);
    }
}
