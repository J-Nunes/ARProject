using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public SpawnManager spawn_manager;
    public Button button_start_game;
    public Shoot player;
    public int hostages_can_die;
    public int death_hostages;
    [HideInInspector] public bool lose;
    [HideInInspector] public bool win;

    // Use this for initialization
    void Start()
    {
        spawn_manager.enabled = false;
        lose = false;
        win = false;
        death_hostages = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(death_hostages >= hostages_can_die)
            lose = true;

        if(win)
            WinGame();

        else if(lose)
            LoseGame();
    }

    public void StartGame()
    {
        win = false;
        lose = false;
        spawn_manager.CleanUpUnits();
        spawn_manager.StartWaves();
        spawn_manager.enabled = true;
        button_start_game.gameObject.SetActive(false);
        player.live = 100;
        death_hostages = 0;
    }

    public void FinishGame(string message)
    {
        spawn_manager.enabled = false;
        button_start_game.GetComponentInChildren<Text>().text = message;
        button_start_game.gameObject.SetActive(true);        
    }

    void WinGame()
    {
        FinishGame("You win");
    }

    void LoseGame()
    {
        FinishGame("You Lose");
    }
}