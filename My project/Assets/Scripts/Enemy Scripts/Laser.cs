using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float timeActive;
    public float speed;
    public Vector3 goTowards;
    public bool goesThroughWalls;
    public GameObject turret;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Destroy(timeActive));
        transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
        goTowards = turret.transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += goTowards*speed*Time.deltaTime;
    }
    IEnumerator Destroy(float tA)
    {
        yield return new WaitForSeconds(tA);
        Destroy(gameObject);
        yield return null;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6 && !goesThroughWalls)
        {
            Destroy(gameObject);
        }
        if(other.tag == "Player")
        {
            other.GetComponent<PlayerMovement>().health -= 1;
            Debug.Log("Health = " + other.GetComponent<PlayerMovement>().health);
        }
    }
}
