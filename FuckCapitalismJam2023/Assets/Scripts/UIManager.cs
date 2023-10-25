using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    Text txtCoins;
    Text txtClear;
    Text txtStage;

    public static UIManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        txtCoins = GameObject.Find("txtCoins").GetComponent<Text>();
        txtClear = GameObject.Find("txtClear").GetComponent<Text>();
        txtStage = GameObject.Find("txtStage").GetComponent<Text>();

        txtStage.text = "STAGE " + SceneManager.GetActiveScene().buildIndex;
        txtClear.gameObject.SetActive(false);

        Invoke("StartLevel", 3);
    }

    // Update is called once per frame
    void Update()
    {
        txtCoins.text = "x" + GameManager.instance.coins.ToString();
    }

    public void StartLevel()
    {
        txtStage.gameObject.SetActive(false);
    }

    public void EndLevel()
    {
        txtClear.gameObject.SetActive(true);
        AudioManager.instance.Play("StageClear");
        Invoke("NextLevel", 3);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
