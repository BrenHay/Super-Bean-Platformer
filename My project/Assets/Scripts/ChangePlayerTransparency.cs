using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayerTransparency : MonoBehaviour
{
    public Material transparent;
    private float maxDistanceFromPlayer;
    private GameObject player;
    private Renderer pRenderer;
    private PlayerMovement pMov;
    public bool isTransparent;

    public float alphaChange;
    private float distanceFromPlayer;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pRenderer = player.GetComponent<Renderer>();
        pMov = player.GetComponent<PlayerMovement>();
    }

    void update()
    {
        if(isTransparent)
        {
            player.GetComponent<PlayerMovement>().canChangeColor = false;
            player.GetComponent<Renderer>().material = transparent;
        }else if(isTransparent == false)
        {
            player.GetComponent<PlayerMovement>().canChangeColor = true;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Player Detected");
            other.GetComponent<PlayerMovement>().canChangeColor = false;
            other.GetComponent<Renderer>().material = transparent;
            isTransparent = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            isTransparent = false;
            other.GetComponent<PlayerMovement>().canChangeColor = true;
        }
    }
}
