using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovementController : MonoBehaviour
{
    // Start is called before the first frame update
    
    //CASANIS PLAYS CODE WITH SLIGHT CHANGES
    public float enemySpeed;
    Animator enemyAnimator;

    public GameObject enemyGraphic;
    bool canFlip = true;
    bool facingRight = false;
    float flipTime = 0.3f;
    float nextFlipChance = 0f;

    //attacking
    public float chargeTime;
    bool justCharged;
    public float chargeFallOff;
    float startChargeTime;
    bool charging;
    Rigidbody2D enemyRB;
    void Start()
    {
        enemyAnimator = GetComponentInChildren<Animator>();
        enemyRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextFlipChance){
            if(Random.Range(0,10) >=5) flipFacing();
            nextFlipChance = Time.time + flipTime;
        }

    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            if(facingRight && other.transform.position.x < transform.position.x) flipFacing();
            else if(!facingRight && other.transform.position.x > transform.position.x) flipFacing();
            canFlip = false;
            charging = true;
            startChargeTime = Time.time + chargeTime;
        }
    }

        void OnTriggerStay2D(Collider2D other){
            if(other.tag == "Player"){
                if(startChargeTime < Time.time){
                    if(!facingRight){
                        enemyRB.AddForce(new Vector2(-1,0) * enemySpeed);
                    }
                    else{
                        enemyRB.AddForce(new Vector2(1,0) * enemySpeed);
                    }
                    enemyAnimator.SetBool("is_charging",charging);
                }
            }
        }

        void OnTriggerExit2D(Collider2D other){
            if(other.tag == "Player"){
                canFlip = true;
                charging = false;
                if(!facingRight){
                    enemyRB.linearVelocity = new Vector2(-10f,0f);
                }
                else{
                    enemyRB.linearVelocity = new Vector2(10f,0f);
                }
                enemyAnimator.SetBool("is_charging",charging);
            }
        }

    void flipFacing(){
        if(!canFlip) return;
        float facingX = enemyGraphic.transform.localScale.x;
        facingX *= -1f;
        enemyGraphic.transform.localScale = new Vector3(facingX, enemyGraphic.transform.localScale.y, enemyGraphic.transform.localScale.z);
        facingRight = !facingRight;
    }
}
