using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehaviour : MonoBehaviour {

    Rigidbody2D rgb;
    public float rotationSpeed;
    public int type;
    public GameObject effect;
    

	// Use this for initialization
	void Start () {
        rgb = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        switch(type)
        {
            default:
            case 0:
                rgb.MoveRotation(rgb.rotation + rotationSpeed * Time.deltaTime);
                break;
            case 1:
                rgb.velocity = new Vector2(0f, rgb.velocity.y);
                break;
        }

	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Player"))
        {
            Instantiate(effect, transform.position, transform.rotation);
            MusicManager.instance.PlayPwrUp();
            Destroy(gameObject);
        }
    }

}
