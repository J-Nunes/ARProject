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
    bool waiting_to_start;

    public AudioClip win_jingle;
    public AudioClip lose_jingle;
    public AudioClip music;
    public AudioSource jingle_source;
    public AudioSource music_source;

    public Power_Ups power_up;

    // Use this for initialization
    void Start()
    {
        spawn_manager.enabled = false;
        lose = false;
        win = false;
        waiting_to_start = true;
        death_hostages = 0;
        hostages_die_before = 0;

        music_source.clip = music;
    }

    // Update is called once per frame
    void Update()
    {
        if(death_hostages >= hostages_can_die)
            lose = true;

        if (!waiting_to_start)
        {
            if (win)
                WinGame();

            else if (lose)
                LoseGame();
        }

        if(hostages_die_before < death_hostages)
        {
            text_amount_death_hostages.text = death_hostages.ToString();
            hostages_die_before = death_hostages;
        }
    }

    public void StartGame()
    {
        music_source.Stop();
        music_source.Play();
        win = false;
        lose = false;
        waiting_to_start = false;
        spawn_manager.CleanUpUnits();
        spawn_manager.StartWaves();
        spawn_manager.enabled = true;
        button_start_game.gameObject.SetActive(false);
        player.live = 100;
        player.currentAmmo = player.maxAmmo;
        death_hostages = 0;
        text_amount_death_hostages.text = death_hostages.ToString();
        hostages_die_before = death_hostages;
        power_up.Reset_Kills();
    }

    public void FinishGame(string message)
    {
        waiting_to_start = true;
        music_source.volume = 0.2f;
        spawn_manager.enabled = false;
        button_start_game.GetComponentInChildren<Text>().text = message;
        button_start_game.gameObject.SetActive(true);        
    }

    void WinGame()
    {
        FinishGame("You win");
        jingle_source.PlayOneShot(win_jingle);
    }

    void LoseGame()
    {
        FinishGame("You Lose");
        jingle_source.PlayOneShot(lose_jingle);
        
    }
}