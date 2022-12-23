using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScreen : MonoBehaviour
{
    private GameObject player;
    public bool inAnimation;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if(Input.GetKeyDown(KeyCode.R) && !inAnimation|| player.transform.position.y < -player.GetComponent<PlayerMovement>().maxFallDistance && !inAnimation)
        {
            Fade();
        }
    }
    public void Fade()
    {
        if(!inAnimation)
        {
            GetComponent<Animator>().SetTrigger("fade");
            inAnimation = true;
            StartCoroutine("GetOut");
        }
    }

    IEnumerator GetOut()
    {
        yield return new WaitForSeconds(0.5f);
        inAnimation = false;
        yield return null;
    }
}
