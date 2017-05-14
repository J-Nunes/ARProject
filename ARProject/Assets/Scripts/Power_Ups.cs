using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power_Ups : MonoBehaviour {

    public int player_kills;
    public Time_Manager time_manager;
    public Weapon weapon_go;

    // Use this for initialization
    void Start () {

        player_kills = 0;

    }
	
	// Update is called once per frame
	void Update () {

        if(player_kills == 3)
        {
            time_manager.SlowMotion();
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
        Debug.Log("Reset Kills");
        Debug.Log(player_kills);
    }

    IEnumerator AmmoPowerUp()
    {
        weapon_go.InfiniteAmmo();
        yield return new WaitForSeconds(10f);
        weapon_go.NormalAmmo(); 
    }
}
