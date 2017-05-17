using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundsUI : MonoBehaviour
{
    public SpawnManager spawn_manager;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        int current_round = spawn_manager.waves.IndexOf(spawn_manager.current_wave) + 1;
        string rounds = current_round.ToString();
        rounds += " / ";
        rounds += spawn_manager.waves.Count.ToString();

        GetComponent<Text>().text = rounds;
    }
}
