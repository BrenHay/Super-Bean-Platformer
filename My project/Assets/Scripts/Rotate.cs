using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float rotateSpeedX;
    public float rotateSpeedY;
    public float rotateSpeedZ;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotateSpeedX*Time.deltaTime,rotateSpeedY*Time.deltaTime,rotateSpeedZ*Time.deltaTime);
    }
}
