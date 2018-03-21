using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    Rigidbody2D rgb;
    public float speed,impactAmount;
    public int dir;

    PlayerBehaviour player;
	// Use this for initialization
	void Start () {
        rgb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerBehaviour>();
        dir = -1;
    }
	
	// Update is called once per frame
	void Update () {
        Move();


	}

    void Move()
    {
        rgb.velocity = new Vector2(speed*dir,rgb.velocity.y);

        if (dir<0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        { 
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Wall"))
        {
            dir *= -1;
        }
        if(collision.tag.Equals("Player"))
        {
            if(!player.isHit)
                player.isHit=true;
        }
    }

}

