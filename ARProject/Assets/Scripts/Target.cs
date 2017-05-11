using UnityEngine;

public class Target : MonoBehaviour {

    GameManager game_m;

    public float health = 30f;

    // Use this for initialization
    void Start()
    {
        game_m = GameObject.Find("Game_Manager").GetComponent<GameManager>();
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if(health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
        GameObject del = gameObject;
        game_m.Remove_From_List(del);
    }

}
