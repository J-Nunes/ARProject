using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Power_Ups : MonoBehaviour {

    public int player_kills;
    public Time_Manager time_manager;
    public Shoot player;
    bool slow_motion_used = false;
    bool infinite_ammo_used = false;
    bool increase_life_used = false;
    public bool bomb_used = false;

    public GameObject explosion;

    public Image image_ammo;
    public Image image_life;
    public Image image_slowmotion;
    public Image image_explosion;

    public Text kill_list_ui;

    // Use this for initialization
    void Start () {
        player_kills = 0;

        //Change the alhpa of the UI images
        Decrease_Alpha();


    }
	
	// Update is called once per frame
	void Update ()
    {

        if (player_kills == 3 && infinite_ammo_used == false)
        {
            Debug.Log("Ammo Power Activated");

            Color ammo = image_ammo.color;
            ammo.a = 1.0f;
            image_ammo.color = ammo;

            StartCoroutine(AmmoPowerUp());
            infinite_ammo_used = true;

        }
        else if (player_kills == 5 && increase_life_used == false)
        {
            Debug.Log("Increase Life");

            Color life = image_life.color;
            life.a = 1.0f;
            image_life.color = life;


            player.live += 10;
            increase_life_used = true;

        }
        else if (player_kills == 7 && slow_motion_used == false)
        {
            Color slow_mot = image_slowmotion.color;
            slow_mot.a = 1.0f;
            image_slowmotion.color = slow_mot;

            time_manager.SlowMotion();
            slow_motion_used = true;
        }
        else if (player_kills == 10 && bomb_used == false)
        {
            Color barrel = image_explosion.color;
            barrel.a = 0.5f;
            image_explosion.color = barrel;

            Explosion bomb = explosion.GetComponent<Explosion>();
            bomb.col.enabled = true;
            bomb.render_mesh.enabled = true;
            Reset_Kills();
        }

    }

    public void Increase_kills()
    {
        player_kills++;
        kill_list_ui.text = player_kills.ToString();
        Debug.Log(" Kills");
        Debug.Log(player_kills);
    }

    public void Reset_Kills()
    {
        player_kills = 0;
        kill_list_ui.text = player_kills.ToString();
        Decrease_Alpha();
        slow_motion_used = false;
        infinite_ammo_used = false;
        bomb_used = false;
        increase_life_used = false;
        Debug.Log("Reset Kills");
        Debug.Log(player_kills);
    }

    IEnumerator AmmoPowerUp()
    {
        player.InfiniteAmmo();
        yield return new WaitForSeconds(10f);
        player.NormalAmmo();
        Debug.Log("Deactivate Ammo Power");
    }


    void Decrease_Alpha()
    {
        //Change the alhpa of the UI images
        Color ammo = image_ammo.color;
        ammo.a = 0.5f;
        image_ammo.color = ammo;

        Color life = image_life.color;
        life.a = 0.5f;
        image_life.color = life;

        Color slow_mot = image_slowmotion.color;
        slow_mot.a = 0.5f;
        image_slowmotion.color = slow_mot;

        Color barrel = image_explosion.color;
        barrel.a = 0.5f;
        image_explosion.color = barrel;
    }
}
