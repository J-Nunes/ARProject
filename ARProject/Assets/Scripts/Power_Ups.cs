using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power_Ups : MonoBehaviour {

    public int player_kills;
    public Time_Manager time_manager;
    public Shoot weapon_go;
    bool slow_motion_used = false;
    bool infinite_ammo_used = false;
    bool increase_life_used = false;
    public bool bomb_used = false;

    public GameObject explosion;

    // Use this for initialization
    void Start () {
        player_kills = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (player_kills == 1 && bomb_used == false)
        {
            Explosion bomb = explosion.GetComponent<Explosion>();
            bomb.col.enabled = true;
            bomb.render_mesh.enabled = true;            
        }

        else if (player_kills == 4 && infinite_ammo_used == false)
        {
            Debug.Log("Ammo Power Activated");
            StartCoroutine(AmmoPowerUp());
            infinite_ammo_used = true;

        }

        else if (player_kills == 10 && slow_motion_used == false)
        {
            time_manager.SlowMotion();
            slow_motion_used = true;
        }

    }

    public void Increase_kills()
    {
        player_kills++;
        Debug.Log(" Kills");
        Debug.Log(player_kills);
    }

    public void Reset_Kills()
    {
        player_kills = 0;
        slow_motion_used = false;
        infinite_ammo_used = false;
        bomb_used = false;
        Debug.Log("Reset Kills");
        Debug.Log(player_kills);
    }

    IEnumerator AmmoPowerUp()
    {
        weapon_go.InfiniteAmmo();
        yield return new WaitForSeconds(10f);
        weapon_go.NormalAmmo();
        Debug.Log("Deactivate Ammo Power");
    }
}
