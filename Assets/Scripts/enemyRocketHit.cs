using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyRocketHit : MonoBehaviour
{
    // Start is called before the first frame update

    //CASANIS PLAYS CODE
    public float weaponDamage;
    projectileController myPC;
    void Start()
    {
        myPC = GetComponentInParent<projectileController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.layer == LayerMask.NameToLayer("Tilemaps") || other.gameObject.layer == LayerMask.NameToLayer("Hurtable")){
            myPC.removeForce();
            Destroy(gameObject);
            if(other.tag == "Player"){
                playerHealth hurtPlayer = other.gameObject.GetComponent<playerHealth>();
                hurtPlayer.addDamage(weaponDamage);
            }
        }
    }

    void OnTriggerStay2D(Collider2D other){
        if(other.gameObject.layer == LayerMask.NameToLayer("Tilemaps") || other.gameObject.layer == LayerMask.NameToLayer("Hurtable")){
            myPC.removeForce();
            Destroy(gameObject);
        }
    }
}
