using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInCircle : MonoBehaviour
{

    [SerializeField] Transform rotationCenter;

    [SerializeField] float RotationRadius = 2f, angularSpeed = 2f;

    float posX, posY, posZ, angle = 0f;

    public bool moveX, moveY, moveZ;

    // Update is called once per frame
    void Update()
    {
        if(moveX)
        {
            posX = rotationCenter.position.x + Mathf.Cos(angle) * RotationRadius;
        }else
        {
            posX = transform.position.x;
        }
        if(moveY)
        {
            posY = rotationCenter.position.y + Mathf.Sin(angle) * RotationRadius;
        }else
        {
            posY = transform.position.y;
        }
        if(moveZ)
        {
            posZ = rotationCenter.position.x + Mathf.Cos(angle) * RotationRadius;
        }else
        {
            posZ = transform.position.z;
        }
        
        transform.position = new Vector3(posX, posY, posZ);
        angle = angle+Time.deltaTime*angularSpeed;

        if(angle >= 360)
        {
            angle = 0;;
        }
    }
}
