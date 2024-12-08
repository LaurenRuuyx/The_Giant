using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class enemyDamage : MonoBehaviour
{
    // Start is called before the first frame update
    
    //CASANIS PLAYS CODE
    public float damage;
    public float damageRate;
    public float pushbackForce;

    float nextDamage;
    void Start()
    {
        nextDamage = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D other){
        if(other.tag == "Player" && nextDamage < Time.time){
            playerHealth thePlayerHealth = other.gameObject.GetComponent<playerHealth>();
            thePlayerHealth.addDamage(damage);
            nextDamage = Time.time + damageRate;
            pushback(other.transform);
        }
    }

    void pushback(Transform pushedObject){

        Vector2 pushdir = new Vector2((pushedObject.position.x - transform.position.x) * 10f,pushedObject.position.y - transform.position.y).normalized;
        pushdir *= pushbackForce;
        Rigidbody2D pushRB = pushedObject.gameObject.GetComponent<Rigidbody2D>();
        pushRB.linearVelocity = Vector2.zero;
        pushRB.AddForce(pushdir, ForceMode2D.Impulse);
    }
}
