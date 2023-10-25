using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float coins;
    public float nBoats;
    public float timeToNextLevel;

    public static GameManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    /*
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Application.platform != RuntimePlatform.WebGLPlayer)
           /Application.Quit();
    }
    */

    public void AddCoin()
    {
        coins++;
    }

    public void AddBoat()
    {
        nBoats++;
    }

    public void SubstractBoat()
    {
        nBoats--;
        if (nBoats <= 0)
            UIManager.instance.EndLevel();
    }

    public void Reset()
    {
        coins = 0;
    }

}
