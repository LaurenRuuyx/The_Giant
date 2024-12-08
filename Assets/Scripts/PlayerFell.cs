using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFell : MonoBehaviour
{
    // Start is called before the first frame update
    //CASANIS CODE
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            playerHealth playerFell = other.GetComponent<playerHealth>();
            playerFell.makeDead();
        }
    }
}
