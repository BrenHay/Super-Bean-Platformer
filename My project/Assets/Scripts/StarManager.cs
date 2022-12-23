using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarManager : MonoBehaviour
{
    
    //List persists between scenes to keep a list of star IDs
    public List<int> starNums;

    private static StarManager managerCheck;

    //Instance Check
    void Awake()
    {
        DontDestroyOnLoad(this);

        if (managerCheck == null)
        {
            managerCheck = this;
        }
        else
        {
            DestroyObject(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
