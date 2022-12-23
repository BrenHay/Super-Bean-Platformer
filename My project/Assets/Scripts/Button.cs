using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject[] platforms;
    private bool beenPressed = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision coll)
    {
        if(coll.gameObject.tag == "Player" && !beenPressed)
        {
            foreach(GameObject g in platforms)
            {
                g.SetActive(true);
            }
            beenPressed = true;
            GetComponent<Renderer>().material.color = Color.green;
        }
    }
}
