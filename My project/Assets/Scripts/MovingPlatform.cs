using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;

    public bool active;
    private Vector3 direction;

    [SerializeField] private bool moveToA;
    [SerializeField] private bool moveToB;
    private Rigidbody rb;

    public float velocity;

    public Vector3 position;

    public Vector3 lastPos;
    public Vector3 lastMoveDirection;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        direction = Vector3.zero;
        lastPos = transform.position;
        lastMoveDirection = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {   
        //If transform.position = PointaA's transform and is active, then move to pointB
        
        if(transform.position == pointA.transform.position && active == true && pointB != null)
        {
            moveToB = true;
            moveToA = false;
        }
        //If the reverse is true, then move to point A
        
        if(transform.position == pointB.transform.position && active == true)
        {
            moveToA = true;
            moveToB = false;
        }
        
        //Movement Code
        if(moveToA && active)
        {
            //direction = pointA.transform.position - transform.position;
            //direction = direction.normalized*5000.0f*Time.deltaTime;
            position = Vector3.MoveTowards(transform.position, pointA.transform.position, 10.0f*Time.deltaTime);
            transform.position = position;
            //rb.MovePosition(position);
        }else if(moveToB && active)
        {
            //direction = pointB.transform.position - transform.position;
            //direction = direction.normalized*5000.0f*Time.deltaTime;
            position = Vector3.MoveTowards(transform.position, pointB.transform.position, 10.0f*Time.deltaTime);
            transform.position = position;
            //rb.MovePosition(position);
        }
    }

    void FixedUpdate()
    {
        //This calculates the speed of the platform by taking its current position and subtracting it by
        //its last position
        
        if(transform.position != lastPos)
        {
            lastMoveDirection = (transform.position - lastPos);
            lastPos = transform.position;
        }
    }

    void OnTriggerEnter(Collider other)
    {

    }

    void OnTriggerExit(Collider other)
    {

    }

    void OnCollisionEnter(Collision coll)
    {
        if(coll.gameObject.tag == "Player" && active == false)
        {
            active = true;
            moveToA = true;
            Debug.Log("getting touched");
        }
    }
}
