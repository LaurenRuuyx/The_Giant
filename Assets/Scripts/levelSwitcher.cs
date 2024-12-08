using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelSwitcher : MonoBehaviour
{
    // Start is called before the first frame update
    
    //ORIGINAL CODE
    public String nextLevel;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            SceneManager.LoadScene(sceneName: nextLevel);
        }

    }

    
}
