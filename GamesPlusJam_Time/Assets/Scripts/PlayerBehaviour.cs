using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {

    Rigidbody2D rgb;
    public float speed;
    public float jumpHeight;
    int dir;

    //Changing sprite delay
    public float delay;
    float startingDelay;

    //PowerUp delay
    public float pwrDelay;
    float startingPwrDelay;

    //Hit delay
    public float hitDelay;
    float startingHitDelay;

    //Sprites
    public Sprite[] states;
    SpriteRenderer own;

    public int index;
    [Space]
    //
    public float[] speeds;
    public float[] heights;

    [Space]

    //Movement
    public bool isGrounded;
    public bool isPwrdUp;
    public bool isHit;
    public bool isOnAir;
    Collider2D col;
    public LayerMask lay;

    //Controls
    public KeyCode left, right;

    //Animation N object catching
    Animator anim;
    GameManager gm;
    int type;

	// Use this for initialization
	void Start () {
        rgb = GetComponent<Rigidbody2D>();
        own = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        gm = FindObjectOfType<GameManager>();
        
        //DelayZone
        startingDelay = delay;
        startingPwrDelay = pwrDelay;
        startingHitDelay = hitDelay;

        own.sprite = states[index];
    }

    // Update is called once per frame
    void Update() {
        isGrounded = Physics2D.IsTouchingLayers(col, lay);

        if (isPwrdUp)
        {
            if(type==1)
            { 
                if (index > 0)
                {
                    index--;
                }
                else if (index <= 0)
                {
                    index = 0;
                }
            }
            pwrDelay -= gm.deltaT;

            if (pwrDelay <= 0)
            {   
                isPwrdUp = false;
                pwrDelay = startingPwrDelay;
            }
            
        }
        else {
            //Change Sprite Mechanic
            delay -= gm.deltaT;
            if (delay <= 0)
            {
                if (index < states.Length - 1)
                {
                    index++;
                }
                else if (index >= states.Length - 1)
                {
                    //Time.timeScale = 0f;
                    index = states.Length - 1;
                }

                //own.sprite = states[index];
                delay = startingDelay;
            }
        }
        own.sprite = states[index];
        anim.SetInteger("State", index);

        //Movement Mechanics
        if (isHit)
        {
            //Time.timeScale = 0.7f;

            if (isOnAir)
            {
                rgb.velocity = new Vector2(rgb.velocity.x, rgb.velocity.y);
                isOnAir =!isGrounded;
            }
            else if(!isOnAir)
            {
                dir = 0;
                rgb.velocity = new Vector2(0f, rgb.velocity.y);
            }

            hitDelay -= gm.deltaT;
            if (hitDelay <= 0)
            {
                isHit = false;
                hitDelay = startingHitDelay;
                Time.timeScale = 1f;
            }
            anim.SetFloat("Speed", Mathf.Abs(rgb.velocity.x));

        }
        else if (!isHit)
        {

            Move();
            Jump();
        }
        
    }


    void Move()
    {
        
        switch (index)
        {
            default:
            case 0:
                speed = speeds[index];
                break;
            case 1:
                speed = speeds[index];
                break;
            case 2:
                speed = speeds[index];
                break;
        }

        if(Input.GetKeyDown(left) || Input.GetKey(left))
        {
            dir = -1;
        }
        else if(Input.GetKeyDown(right) || Input.GetKey(right))
        {
            dir = 1;
        }
        else if(Input.GetKeyUp(left) || Input.GetKeyUp(right))
        {
            dir = 0;
        }
        rgb.velocity = new Vector2(dir * speed, rgb.velocity.y);


        if (rgb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (rgb.velocity.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }


        anim.SetFloat("Speed",Mathf.Abs( rgb.velocity.x));
    }

    void Jump()
    {
        
        switch(index)
        {
            default:
            case 0:
                jumpHeight = heights[index];
                break;
            case 1:
                speed = speeds[index];
                break;
            case 2:
                jumpHeight = heights[index];
                break;
        }

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rgb.velocity = new Vector2(rgb.velocity.x, jumpHeight);
            MusicManager.instance.PlayJump();
        }
        anim.SetBool("isGround", isGrounded);
    }

    void Knockback(int direction,float amount)
    {
        rgb.velocity = new Vector2(direction * -1 *amount, 10);
        isOnAir = true;
    }
    
    void Stop()
    {
        rgb.velocity = new Vector2(0f,rgb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Items"))
        {
            isPwrdUp = true;
            type = collision.GetComponent<ItemBehaviour>().type;
            switch (collision.GetComponent<ItemBehaviour>().type)
            {
                default:
                case 0:
                    gm.score++;
                    break;
                case 1:

                    break;
            }

            //Destroy(collision.gameObject);
        }
        if(collision.tag.Equals("Enemy"))
        {
            if(collision.GetComponent<Enemy>() !=null)
                Knockback(collision.GetComponent<Enemy>().dir, collision.GetComponent<Enemy>().impactAmount);
            if(collision.GetComponent<HazardBehaviour>() != null)
                Knockback((int)transform.localScale.x, collision.GetComponent <HazardBehaviour>().amount);
            MusicManager.instance.PlayHurt();
        }
    }
    
}
