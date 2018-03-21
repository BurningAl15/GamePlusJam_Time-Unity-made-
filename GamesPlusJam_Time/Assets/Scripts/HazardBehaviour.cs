using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardBehaviour : MonoBehaviour {

    public float amount;

    PlayerBehaviour player;
    // Use this for initialization
    void Start () {
        player = FindObjectOfType<PlayerBehaviour>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            if (!player.isHit)
                player.isHit = true;
        }
    }

}
