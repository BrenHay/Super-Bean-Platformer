using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraRotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(transform.rotation.x > 90)
        {
            //transform.eulerAngles = new Vector3(90, transform.rotation.y, transform.rotation.z);
            transform.rotation = Quaternion.Euler(90, transform.rotation.y, transform.rotation.z);
        }
        if(transform.rotation.x < -90)
        {
            //transform.eulerAngles = new Vector3(-90, transform.rotation.y, transform.rotation.z);
            transform.rotation = Quaternion.Euler(-90, transform.rotation.y, transform.rotation.z);
        }
    }
}
