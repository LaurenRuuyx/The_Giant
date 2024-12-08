using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketHIt : MonoBehaviour
{
    // Start is called before the first frame update
    
    //CASANIS PLAYS CODE
    
    public float weaponDamage;
    projectileController myPC;
    float timeHit;

    void Start()
    {
        myPC = GetComponentInParent<projectileController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){

    }

    void OnTriggerStay2D(Collider2D other){
        if(other.gameObject.layer == LayerMask.NameToLayer("Tilemaps") || other.gameObject.layer == LayerMask.NameToLayer("Shootable")){
            myPC.removeForce();
            Destroy(gameObject);
            if(other.tag == "Enemy" && timeHit < Time.time){
                enemyHealth hurtEnemy = other.gameObject.GetComponent<enemyHealth>();
                hurtEnemy.addDamage(weaponDamage);
                timeHit = Time.time + 0.1f;
            }
        }
    }
}
