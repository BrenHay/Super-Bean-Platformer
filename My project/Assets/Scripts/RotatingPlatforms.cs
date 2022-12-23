using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatforms : MonoBehaviour
{
    public float rotateSpeedX;
    public float rotateSpeedY;
    public float rotateSpeedZ;

    public GameObject[] connectedObjects;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject g in connectedObjects)
        {
            g.transform.rotation = Quaternion.Euler(0,0,0);
        }
        
        transform.Rotate(rotateSpeedX*Time.deltaTime,rotateSpeedY*Time.deltaTime,rotateSpeedZ*Time.deltaTime);
    }
}
