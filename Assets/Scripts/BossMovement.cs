using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    // Start is called before the first frame update

    //ORIGINAL CODE

    public float enemySpeed;
    public float jumpForce;
    public float timeoutDuration;
    public Transform tilemapGroundCheck;
    public LayerMask tilemapLayer;
    public Transform projectileTip;
    public GameObject projectile;


    Animator enemyAnimator;

    public GameObject enemyGraphic;
    public GameObject player;
    float groundCheckRadius = 0.5f;
    bool facingRight = false;

    bool canAttack = false;
    float restPhase;

    //attacking
    public float chargeTime;
    public float chargeFallOff;
    bool inAttackAnimation;
    Rigidbody2D enemyRB;
    int currentAction;
    float attackDuration;

    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponentInChildren<Animator>();
        currentAction = 0;
        inAttackAnimation = false;
        attackDuration = 0f;
        restPhase = Time.time + 3f;

    }

    // Update is called once per frame
    void Update()
    {
        if(!canAttack){
            restPhase = Time.time + 3f;
            return;
        }
        if(Time.time > restPhase){
            if(facingRight && player.transform.position.x < transform.position.x) flipFacing();
            else if(!facingRight && player.transform.position.x > transform.position.x) flipFacing();
            if(!inAttackAnimation){
                currentAction = getRandomAction();
            }
            switch (currentAction){
                case 0:
                    first_pattern();
                    break;

                case 1:
                    second_pattern();
                    break;

                case 2:
                    third_pattern();
                    break;

            }

        }

    }

    void first_pattern(){
        float time = Time.time;
        if(!inAttackAnimation){
            attackDuration = time + 0.4f;
            inAttackAnimation = true;
            enemyAnimator.SetBool("running",true);
            if(!facingRight){
                enemyRB.AddForce(new Vector2(-1,0) * enemySpeed);
            }
            else{
                enemyRB.AddForce(new Vector2(1,0) * enemySpeed);
            }
        }

        if(attackDuration < time){
            enemyRB.AddForce(Vector2.zero);
            inAttackAnimation = false;
            enemyAnimator.SetBool("running",false);
            restPhase = Time.time + timeoutDuration;
        }

    }

    int getRandomAction(){
        float x = Random.Range(0,100);
        int answer = 0;
        if(x >= 33.33 && x < 66.66) answer = 1;
        if(x >= 66.66) answer = 2;
        return answer;
    }

    void second_pattern(){
        if(!inAttackAnimation){
            inAttackAnimation = true;
            attackDuration = Time.time + 0.5f;
            if(facingRight){
                enemyRB.AddForce(new Vector2(5,5) * jumpForce);
            }
            else{
                enemyRB.AddForce(new Vector2(-5,5) * jumpForce);
            }
        }
        if(attackDuration < Time.time){
            bool ground = Physics2D.OverlapCircle(tilemapGroundCheck.position,groundCheckRadius,tilemapLayer);
            if(ground){
                inAttackAnimation = false;
                restPhase = Time.time + timeoutDuration;
            }
        }


    }

    void third_pattern(){
        float time = Time.time;
        if(!inAttackAnimation){
            enemyAnimator.SetBool("shoot",true);
            inAttackAnimation = true;
            attackDuration = time + 0.5f;
        }
        if(attackDuration < time){
            inAttackAnimation = false;
            enemyAnimator.SetBool("shoot",false);
            fireEnemyProjectile();
            restPhase = Time.time + timeoutDuration;
        }
        
    }


    void flipFacing(){
        float facingX = enemyGraphic.transform.localScale.x;
        facingX *= -1f;
        enemyGraphic.transform.localScale = new Vector3(facingX, enemyGraphic.transform.localScale.y, enemyGraphic.transform.localScale.z);
        facingRight = !facingRight;
    }

    void fireEnemyProjectile(){
        if(facingRight){
            Instantiate(projectile, new Vector3(projectileTip.position.x + 35,projectileTip.position.y,projectileTip.position.z), Quaternion.Euler (new Vector3(0,0,0)));
        }else{
            Instantiate(projectile, new Vector3(projectileTip.position.x - 15,projectileTip.position.y - 8,projectileTip.position.z), Quaternion.Euler (new Vector3(0,0,180f)));
        }
    }

    public void changeCanAttack(){
        canAttack = true;
    }

}
