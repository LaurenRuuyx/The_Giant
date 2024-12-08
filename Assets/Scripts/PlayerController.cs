using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // Movement variables

    //INSPIRED BY CASANIS PLAYS PLAYER CONTROLLER, EVOLVED INTO SOMETHING BIGGER
    public float maxSpeed;
    public float acceleration;
    public float deceleration;
    public float velPower;
    public float maxHeight;
    public float dashHorizontalForce;
    public float dashVerticalForce;
    public LayerMask tilemapLayer;
    public Transform tilemapGroundCheck;
    public Transform tilemapRightCheck;
    public Transform tilemapLeftCheck;
    public float fireRate;
    public AudioClip playerRun;


    Rigidbody2D myRigidBody;
    Animator myAnimator;
    bool facingRight;

    bool running = false;
    float runSoundTiming;


    AudioSource playerAs;

    //for jumping
    bool canJump = false;
    float jumpTimeOut;
    bool jumpTimeoutTracker = false;
    bool isGrounded = false;

    //for dashing
    bool hasDashed = false;
    bool canDash = false;

    //for wall jumps
    bool left = false;
    bool right = false;
    float groundCheckRadius = 0.5f;
    float wallCheckRadius = 0.5f;
    bool firstWallTouch = false;
    string lastWall = "none";
    float wallJumpTimer;


    //For Shooting
    public Transform projectileTip;
    public GameObject projectile;
    float nextFire = 0f;
    bool attacking;
    float attackTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        facingRight = true;
        playerAs = GetComponent<AudioSource>();
        runSoundTiming = Time.time;
        wallJumpTimer = Time.time;
    }

    void Update(){
        vertical_checks();
        dash_checks();

        //player shooting
        if(Input.GetAxisRaw("Fire1") > 0) fireProjectile();
        checkAttackAnimation();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool ground = Physics2D.OverlapCircle(tilemapGroundCheck.position,groundCheckRadius,tilemapLayer);
        myAnimator.SetBool("player_grounded",ground || right || left);
        if(ground){
            lastWall = "none";
        }
        if(ground && !canJump && !jumpTimeoutTracker){
            jumpTimeOut = Time.time + 0.1f;
            jumpTimeoutTracker = true;
        }
        right = Physics2D.OverlapCircle(tilemapRightCheck.position,wallCheckRadius,tilemapLayer);
        left = Physics2D.OverlapCircle(tilemapLeftCheck.position,wallCheckRadius,tilemapLayer);
        myAnimator.SetBool("wall_hanging",(right || left) && !isGrounded);
        if(right && lastWall == "right"){
            right = false;
        }
        if(left && lastWall == "left"){
            left = false;
        }
        canJump = ground || right || left;
        if(jumpTimeOut > Time.time){
            canJump = false;
        }
        if(ground && hasDashed){
            hasDashed = false;
        }
        canDash = !ground && !left && !right && !hasDashed;
        isGrounded = ground;
        myRigidBody.gravityScale = (right || left) ? 0.5f : 15f;
        if(!right && !left){
            firstWallTouch = false;
        }
        if((right || left) && !firstWallTouch){
            firstWallTouch = true;
            myRigidBody.linearVelocity = Vector2.zero;
        }
        horizontal_checks();

    }

    void flip_character(){
        facingRight = !facingRight;
        Vector3 scalingVector = transform.localScale;
        scalingVector.x *= -1;
        transform.localScale = scalingVector;
        Vector3 rightVector = tilemapRightCheck.position;
        Vector3 leftVector = tilemapLeftCheck.position;
        tilemapRightCheck.position = leftVector;
        tilemapLeftCheck.position = rightVector;

    }

    void horizontal_checks(){
        if((right || left) && !isGrounded){
            running = false;
            return;
        } 
        float moveDirectionHorizontal = Input.GetAxis("Horizontal");
        myAnimator.SetFloat("player_speed",Mathf.Abs(moveDirectionHorizontal));
        float targetSpeed = moveDirectionHorizontal * maxSpeed;
        float speedDif = targetSpeed - myRigidBody.linearVelocity.x;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : deceleration;
        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);
        myRigidBody.AddForce(movement * Vector2.right);

        if(isGrounded && moveDirectionHorizontal != 0){
            running = true;
        }
        else{
            running = false;
        }

        if(moveDirectionHorizontal > 0 && !facingRight){
            flip_character();
        }
        else if(moveDirectionHorizontal < 0 && facingRight){
            flip_character();
        }
        if(facingRight && right && !isGrounded){
            flip_character();
        }
        if(!facingRight && left && !isGrounded){
            flip_character();
        }
        runSound();

    }

    void vertical_checks(){
        if(Input.GetAxis("Jump") > 0 && canJump){
            float horizontalAxis = Input.GetAxis("Horizontal");
            if(((right && horizontalAxis < 0) || (left && horizontalAxis > 0)) && !isGrounded && horizontalAxis != 0){
                if(wallJumpTimer > Time.time){
                    return;
                }
                if(right){
                    lastWall = "right";
                }
                if(left){
                    lastWall = "left";
                }
                myRigidBody.AddForce(new Vector2(0,maxHeight));
                canJump = !canJump;
                wallJumpTimer = Time.time + 0.3f;

            } 
            else{
                if(!right && !left){
                    myRigidBody.AddForce(new Vector2(0,maxHeight));
                    canJump = !canJump;
                    jumpTimeoutTracker = false;
                }
            } 

        }
    }

    void dash_checks(){
        if(canDash){
            if(Input.GetKeyDown(KeyCode.LeftShift)){
                float moveDirecitonHorizontal = Input.GetAxis("Horizontal");
                float moveDirectionVertical = Input.GetAxis("Jump");
                myRigidBody.AddForce(new Vector2(dashHorizontalForce * moveDirecitonHorizontal,dashVerticalForce * moveDirectionVertical));
                canDash = false;
                hasDashed = true;
            }
        }
    }

    void fireProjectile(){
        if(Time.time> nextFire){
            nextFire = Time.time + fireRate;
            attacking = true;
            myAnimator.SetBool("attacking",attacking);
            attackTime = Time.time + fireRate;
            if(facingRight){
                Instantiate(projectile, new Vector3(projectileTip.position.x + 15,projectileTip.position.y + 5,projectileTip.position.z), Quaternion.Euler (new Vector3(0,0,0)));
            }else{
                Instantiate(projectile, new Vector3(projectileTip.position.x - 15,projectileTip.position.y - 3,projectileTip.position.z), Quaternion.Euler (new Vector3(0,0,180f)));
            }
        }
    }

    void checkAttackAnimation(){
        if(Time.time > attackTime){
            attacking = false;
            myAnimator.SetBool("attacking",attacking);
        }
    }

    void runSound(){
        if(running && runSoundTiming < Time.time){
            playerAs.clip = playerRun;
            playerAs.PlayOneShot(playerRun);
            runSoundTiming = Time.time + 0.4f;
        }

    }
}
