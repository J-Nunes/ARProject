using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public SpawnManager spawn_manager;
    public Button button_start_game;
    [HideInInspector] public bool game;

    // Use this for initialization
    void Start()
    {
        spawn_manager.enabled = false;
        game = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!game)
        {
            FinishGame();
        }
    }

    public void StartGame()
    {
        game = true;
        spawn_manager.StartWaves();
        spawn_manager.enabled = true;
        button_start_game.gameObject.SetActive(false);
    }

    public void FinishGame()
    {
        spawn_manager.enabled = false;
        button_start_game.GetComponentInChildren<Text>().text = "Restart";
        button_start_game.gameObject.SetActive(true);        
    }
}