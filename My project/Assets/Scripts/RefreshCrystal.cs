using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefreshCrystal : MonoBehaviour
{
    private bool hasRefresh = true;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && hasRefresh)
        {
            FindObjectOfType<PlayerMovement>().canAirDash = true;
            hasRefresh = false;
            GetComponent<Renderer>().material.color = Color.clear;
            StartCoroutine("Refresh");
        }
    }

    IEnumerator Refresh()
    {
        yield return new WaitForSeconds(4.0f);
        hasRefresh = true;
        GetComponent<Renderer>().material.color = Color.cyan;
    }
}
