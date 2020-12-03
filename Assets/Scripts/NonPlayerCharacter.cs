using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NonPlayerCharacter : MonoBehaviour
{


    public float displayTime = 4.0f;
    public GameObject dialogBox;

    public GameObject theCanvas;

    float timerDisplay;

    private bool levelWon = false;
    // Start is called before the first frame update
    void Start()
    {
        dialogBox.SetActive(false);
        timerDisplay = -1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerDisplay >= 0)
        {
            timerDisplay -= Time.deltaTime;
            if (timerDisplay < 0)
            {
                dialogBox.SetActive(false);
            }
        }
    }

    public void DisplayDialog()
    {
        if(levelWon)
        {

            theCanvas.SendMessage("SaveTime");

            SceneManager.LoadScene("Level2");
        }
        timerDisplay = displayTime;
        dialogBox.SetActive(true);
    }
    public void LevelWon()
    {
        levelWon = true;
    }
}
