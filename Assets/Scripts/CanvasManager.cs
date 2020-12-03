using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;


public class CanvasManager : MonoBehaviour
{
    public int levelNum;

    static float gameTimer = 60;
    static float levelTwoStartTime;
    static bool timeAttackOn = false;

    public GameObject winScreen;
    public GameObject loseScreen;
    public GameObject timeLoseScreen;
    public GameObject jambiMessage;
    public GameObject titleMenu;

    public GameObject timeAttack;
    public Text timerText;


    public AudioClip winBGM;
    public AudioClip loseBGM;
    public AudioClip gameBGM;
    public AudioClip clockSound;

    public AudioSource BGM;



    bool paused = false;
    bool timeLow = false;
    private bool won = false;
    private bool timeLost = false;
    // Start is called before the first frame update
    void Start()
    {
        if(levelNum == 1)
        {
            titleMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            titleMenu.SetActive(false);
            Time.timeScale = 1f;
        }

        if(timeAttackOn)
        {
            timeAttack.SetActive(true);
        }
        paused = false;

        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        BGM.Stop();
        BGM.PlayOneShot(gameBGM);
    }

    // Update is called once per frame
    void Update()
    {

        if (timeAttackOn)
        {
            gameTimer -= Time.deltaTime;
        }
        if (gameTimer < 0)
        {
            TimeLoseScreen();
        }
        else
        {
            timerText.text = gameTimer.ToString("F0");
        }

        if(gameTimer < 20 & !timeLow)
        {
            timeLow = true;
            BGM.PlayOneShot(clockSound);
        }


        if (paused)
        {
            if (won && Input.GetKeyDown("r"))
            {
                SceneManager.LoadScene("MainScene");

                gameTimer = 60f;
                timeLow = false;

            }
            else if (timeLost && Input.GetKeyDown("r"))
            {
                SceneManager.LoadScene("MainScene");
                gameTimer = 60f;
                timeLow = false;

            }
            else if (Input.GetKeyDown("r") && levelNum == 1)
            {
                SceneManager.LoadScene("MainScene");

                gameTimer = 60f;
                timeLow = false;

            }
            else if (Input.GetKeyDown("r") && levelNum == 2)
            {
                SceneManager.LoadScene("Level2");
                gameTimer = levelTwoStartTime;
                timeLow = false;

            }
        }

    }         


    



    public void PauseToggle()
    {
        if(paused)
        {
            Time.timeScale = 1f;
            paused = false;
           
        }
        else if(!paused)
        {
            Time.timeScale = 0f;
            paused = true;
        }
    }
    public void WinScreen()
    {
        if(levelNum == 2)
        {
            won = true;
            winScreen.SetActive(true);
            BGM.Stop();
            BGM.PlayOneShot(winBGM);
            PauseToggle();
        }
        else
        {
            jambiMessage.SetActive(true);
        }

    }
    public void LoseScreen()
    {
        loseScreen.SetActive(true);
        PauseToggle();
        BGM.Stop();
        BGM.PlayOneShot(loseBGM);
    }


    public void TimeLoseScreen()
    {

        timeLost = true;
        timeLoseScreen.SetActive(true);
        PauseToggle();
        BGM.Stop();
        BGM.PlayOneShot(loseBGM);
        Time.timeScale = 0;
    }


    public void JambiTalk()
    {
        jambiMessage.SetActive(true);
    }


    public void SaveTime()
    {
        levelTwoStartTime = gameTimer;
        //Debug.Log
    }

    public void playNormal()
    {
        Time.timeScale = 1f;
        titleMenu.SetActive(false);
        timeAttack.SetActive(false);
        timeAttackOn = false;
    }

    public void playTimeAttack()
    {
        Time.timeScale = 1f;
        titleMenu.SetActive(false);
        timeAttack.SetActive(true);
        timeAttackOn = true;
    }

}

