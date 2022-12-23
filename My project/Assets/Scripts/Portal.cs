using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Portal : MonoBehaviour
{
    public string sceneToLoad;
    private GameObject UI;
    private GameObject panel;
    public int starReq;
    private bool textShown;
    
    // Start is called before the first frame update
    void Start()
    {
        UI = GameObject.FindGameObjectWithTag("UI");
        DontDestroyOnLoad(UI);  
    }

    // Update is called once per frame
    void Update()
    {
        if(panel == null)
        {
            panel = GameObject.FindGameObjectWithTag("Fade");
        }
        
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            StarsCollected starC = FindObjectOfType<StarsCollected>();
            if (starC.stars < starReq)
            {
                StarReqText();
            }else
            {
                //Gets the panel GameObject and starts the Fade function
                panel.GetComponent<FadeScreen>().Fade();
                //Starts Coroutine to change scenes
                StartCoroutine("WaitToChange");
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("StarReq").GetComponent<TextMeshProUGUI>().text = "";
        }
    }
    void StarReqText()
    {
        int starText = starReq - FindObjectOfType<StarsCollected>().stars;
        string text = "You need: " + starReq + " more Star(s)";
        GameObject.FindGameObjectWithTag("StarReq").GetComponent<TextMeshProUGUI>().text = text;
    }
    IEnumerator WaitToChange()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        //Player cannot move
        player.GetComponent<PlayerMovement>().canMove = false;
        //Wait 0.5 seconds and then change scene
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadSceneAsync(sceneToLoad);
        yield return null;
    }
}
