using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class startTime : MonoBehaviour
{
    private float time;
    private int minutes;
    public bool startTimeing;
    public TextMeshProUGUI timer;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(startTimeing == false && Input.GetKeyDown(KeyCode.T))
        {
            startTimeing = true;
        }else if(startTimeing && Input.GetKeyDown(KeyCode.Tab))
        {
            startTimeing = false;
        }
        if(startTimeing)
        {
            Timeing();
        }
        if(Input.GetKeyDown(KeyCode.L))
        {
            time = 0;
            string currentTime = "Time: " + time.ToString("F2");
        }
    }

    public void Timeing()
    {
        string currentTime;
        time += 1*Time.deltaTime;
        if(time > 60)
        {
            time -= 60.0f;
            minutes += 1;
        }
        if(time < 10)
        {
            currentTime = "Time: " + minutes.ToString() + ":0" + time.ToString("F2");
        }else
        {
            currentTime = "Time: " + minutes.ToString() + ":" + time.ToString("F2");
        }
        timer.text = currentTime;
    }
}
