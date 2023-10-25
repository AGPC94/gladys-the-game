using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    [SerializeField] Text txtCoins;
    // Start is called before the first frame update
    void Start()
    {
        //Musica final
    }

    // Update is called once per frame
    void Update()
    {
        txtCoins.text = "x" + GameManager.instance.coins.ToString();

        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
            SceneManager.LoadScene("Menu");
    }
}
