using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public GameObject spawnPoint;
    public bool isCurrentCheckpoint;
    public GameObject flag;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       //When isCurrentCheckpoint is true, change the position of the "Spawnpoint" GameObject

       if(isCurrentCheckpoint)
       {
         spawnPoint.transform.position = transform.position;
        
         flag.GetComponent<Renderer>().material.color = Color.cyan;
       }else
       {
           flag.GetComponent<Renderer>().material.color = Color.red;
       }
    }

    //Changes isCurrentCheckpoint to true when the player triggers it
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !isCurrentCheckpoint && other.GetComponent<PlayerMovement>().currentCheckpoint != gameObject)
        {
            if(other.GetComponent<PlayerMovement>().currentCheckpoint != null)
            {
                other.GetComponent<PlayerMovement>().currentCheckpoint.GetComponent<Checkpoint>().isCurrentCheckpoint = false;
            }
            isCurrentCheckpoint = true;
            other.GetComponent<PlayerMovement>().currentCheckpoint = gameObject;
        }
    }
}
