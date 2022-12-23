using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StarsCollected : MonoBehaviour
{
    public int stars;
    public TextMeshProUGUI starsText;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        starsText.text = "Stars: " + stars;
    }
}
