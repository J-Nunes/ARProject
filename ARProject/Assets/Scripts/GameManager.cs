using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public SpawnManager spawn_manager;
    public Button button_start_game;
    public Text text_amount_death_hostages;
    public Shoot player;
    public int hostages_can_die;
    public int death_hostages;
    int hostages_die_before;
    [HideInInspector] public bool lose;
    [HideInInspector] public bool win;

    // Use this for initialization
    void Start()
    {
        spawn_manager.enabled = false;
        lose = false;
        win = false;
        death_hostages = 0;
        hostages_die_before = 0;
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

        if(hostages_die_before < death_hostages)
        {
            text_amount_death_hostages.text = death_hostages.ToString();
            hostages_die_before = death_hostages;
        }
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
        player.currentAmmo = player.maxAmmo;
        death_hostages = 0;
        text_amount_death_hostages.text = death_hostages.ToString();
        hostages_die_before = death_hostages;        
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