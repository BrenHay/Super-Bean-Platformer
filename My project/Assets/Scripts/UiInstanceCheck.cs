using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiInstanceCheck : MonoBehaviour
{
    private static UiInstanceCheck uiCheck;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
     DontDestroyOnLoad (this);
         
     if(uiCheck == null) 
        {
         uiCheck = this;
        }else 
        {
          DestroyObject(gameObject);
        }
    }
 
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
