using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public float delay;
    bool paused;
    public GameObject pauseMenu,timer,endTime;
    public GameObject Goal,Won;
    [Space]
    public GameObject pMenu, iMenu;
    [Space]
    float delaycpy;
    public float m, s;

    public int score;
    [SerializeField]
    int maxScore;

    public Image[] imgs;
    Image current;
    PlayerBehaviour player;

    public GameObject powerUp;
    public GameObject damaged;

    int index;
    bool change;

    public float deltaT;
  
    // Use this for initialization
	void Start () {
        player = FindObjectOfType<PlayerBehaviour>();

        paused = false;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        endTime.SetActive(false);

        delaycpy = delay;
        if(delaycpy % 60>0)
        {
            m = (int) delaycpy / 60;
            delaycpy -= m * 60;
            s = (int) delaycpy;
        }
        //Debug.Log(m + " - " + s);

        current = imgs[player.index];
        index = player.index;
        current.color = new Color(1, 0, 0);
        iMenu.SetActive(false);
        Goal.GetComponent<Text>().text = score + " / " + maxScore;
        Won.SetActive(false);

        deltaT = Time.deltaTime;
        
    }

    // Update is called once per frame
    void Update() {
       
        Debug.Log(deltaT);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused();
            deltaT = 0;
        }
        else
        {
            deltaT = Time.deltaTime;
        }
        
        //Counter
        if (m >= 0)
        {
            s -= deltaT ;
            if (s <= 0)
            {
                m--;
                s = 60;
            }
        }
        else if (m <= 0)
        {
            Time.timeScale = 0f;
            timer.SetActive(false);
            endTime.SetActive(true);
        }

        //Power Up time.
        if (player.isPwrdUp)
        {
            powerUp.SetActive(true);
            powerUp.GetComponent<Text>().text = "Ends in... " + (int)player.pwrDelay;
        }
        else
        {
            powerUp.SetActive(false);
        }

        if (player.isHit)
        {
            damaged.SetActive(true);
            damaged.GetComponent<Text>().text = "Pain ends in..." + (int)player.hitDelay;
        }
        else
        {
            damaged.SetActive(false);
        }

        //State system
        if (index != player.index)
        {
            for(int i=0;i<imgs.Length;i++)
            {
                imgs[i].color = Color.white;
            }
            index = player.index;
        }
        else
        {
            current = imgs[player.index];
            current.color = new Color(0.8f, 0.4f, 0.2f);
        }

        //imgs
        timer.GetComponent<Text>().text = "Time: " + (int)m + ": "+ (int)s;
        Goal.GetComponent<Text>().text = score + " / " + maxScore;

        if (score==maxScore)
        {
            Time.timeScale = 0f;
            Won.SetActive(true);
        }

	}

    public void isPaused()
    {
        paused = !paused;
        if(paused)
        {
            Time.timeScale = 0f;
            
        }
        else 
        {
            Time.timeScale = 1f;
            //deltaT = Time.deltaTime;
        }

        pauseMenu.SetActive(paused);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Exit(string mainMenu)
    {
        SceneManager.LoadScene(mainMenu);
    }

    public void Win(string nextLvl)
    {
        SceneManager.LoadScene(nextLvl);
    }
    
    public void Instructions()
    {
        pMenu.SetActive(false);
        iMenu.SetActive(true);
    }

    public void ReturnToMenu()
    {
        pMenu.SetActive(true);
        iMenu.SetActive(false);
    }

}
