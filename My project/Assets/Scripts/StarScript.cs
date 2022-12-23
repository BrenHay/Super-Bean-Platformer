using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class StarScript : MonoBehaviour
{
    public bool beenCollected = false;

    public string starName;
    public int starNum;
    private GameObject Ui;
    private TextMeshProUGUI starGotText;
    private TextMeshProUGUI starNameText;

    public Material transparent;
    
    // Start is called before the first frame update
    void Start()
    {
        //If star manager's script has the stars ID in its list, turn "beenCollected" to true
        if (FindObjectOfType<StarManager>().starNums.Contains(starNum))
        {
            beenCollected = true;
            GetComponent<Renderer>().material = transparent;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        
        //When the player overlaps with the star trigger add a star to a ui counter and
        //make sure it can't be collected again
        if(other.tag == "Player" && !beenCollected)
        {
            Ui = GameObject.FindGameObjectWithTag("UI");
            starGotText = GameObject.FindGameObjectWithTag("StarGot").GetComponent<TextMeshProUGUI>();
            starNameText = GameObject.FindGameObjectWithTag("StarName").GetComponent<TextMeshProUGUI>();
            
            beenCollected = true;
            FindObjectOfType<StarsCollected>().stars += 1;
            FindObjectOfType<StarManager>().starNums.Add(starNum);
            GetComponent<Renderer>().material = transparent;
            StartCoroutine("StarCutscene");

            //Display star flavor text and refresh airdash
            starGotText.text = "You Got a Star!";
            starNameText.text = starName;
            FindObjectOfType<PlayerMovement>().canAirDash = true;
        }
    }

    IEnumerator StarCutscene()
    {
        float t = 0;
        Time.timeScale = 0.0f;

        //Cutscene lasts for 2 seconds and pauses the game
        while(t < 2)
        {
            t += 1*Time.unscaledDeltaTime;
            yield return null;
        }
        Time.timeScale = 1.0f;

        //Remove the flavor text and resume the game
        starGotText.text = "";
        starNameText.text = "";

        yield return null;
    }
}
