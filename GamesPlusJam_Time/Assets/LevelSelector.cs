using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour {

    public string[] levels;

    public void Tutorial()
    {
        SceneManager.LoadScene(levels[0]);
    }

    public void Lvl1()
    {
        SceneManager.LoadScene(levels[1]);
    }

    public void Lvl2()
    {
        SceneManager.LoadScene(levels[2]);
    }

    public void Lvl3()
    {
        SceneManager.LoadScene(levels[3]);
    }
}
