using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoboCount : MonoBehaviour
{
    public int robotMax = 6;
    public GameObject Jambi;
    public GameObject theCanvas;
  
    public Text fixNum;
    private int numFixed = 0;


    // Start is called before the first frame update
    void Start()
    {
        numFixed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RobotFixed()
    {
        numFixed++;

        fixNum.text = numFixed.ToString();

        if (numFixed >= robotMax)
        {
            Jambi.SendMessage("LevelWon");
            theCanvas.SendMessage("WinScreen");
        }
    }

}
