using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class PotionScript : MonoBehaviour
{
    // Start is called before the first frame update

    //ORIGINAL CODE
    public float healthAdded;

    public float floatingSpeed;

    float changeDirectionTime;
    bool down = true;
    Rigidbody2D rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(0, -1 * floatingSpeed));
        changeDirectionTime = Time.time + 0.5f;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(changeDirectionTime < Time.time){
            SwapDireciton();
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            playerHealth thePlayerHealth = other.gameObject.GetComponent<playerHealth>();
            thePlayerHealth.addHealth(healthAdded);
            Destroy(gameObject);
        }
    }
    
    void OnTriggerStay2D(Collider2D other){
        if(other.tag == "Player"){
            playerHealth thePlayerHealth = other.gameObject.GetComponent<playerHealth>();
            thePlayerHealth.addHealth(healthAdded);
            Destroy(gameObject);
        }
    }

    void SwapDireciton(){
        if(down){
            rb.AddForce(Vector2.zero);
            rb.AddForce(new Vector2(0,2 *floatingSpeed));
            down = false;
        }
        else{
            rb.AddForce(Vector2.zero);
            rb.AddForce(new Vector2(0, -2 * floatingSpeed));
            down = true;
        }
        changeDirectionTime = Time.time + 0.5f;
    }
}
