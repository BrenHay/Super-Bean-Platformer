using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public GameObject timer;
    private float time;
    private int minutes;
    public bool startTimeing;
    private TextMeshProUGUI timerText;

    // Start is called before the first frame update
    void Start()
    {
        timerText = timer.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(startTimeing == false && Input.anyKey && !Input.GetKeyDown(KeyCode.P))
        {
            startTimeing = true;
        }
        if(startTimeing)
        {
            Timeing();
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            timer.SetActive(true);
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
        timerText.text = currentTime;
    
    }
}
