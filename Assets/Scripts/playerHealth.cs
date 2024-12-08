using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    
    //CASANIS CODE WITH CHANGES MADE FOR POTIONS, ANIMATIONS, ETC
    public float fullHealth;
    public AudioClip playerHurt;
    public AudioClip playerHealed;
    float currentHealth;
    AudioSource playerAs;
    PlayerController controlMovement;

    //HUD Variables
    public Slider healthSlider;
    public SpriteRenderer spriteRenderer;

    public Text gameOverScreen;
    public restartGame theGameManager;

    void Start()
    {
        currentHealth = fullHealth;
        controlMovement = GetComponent<PlayerController>();
        playerAs = GetComponent<AudioSource>();


        //HUD initialization
        healthSlider.maxValue = fullHealth;
        healthSlider.value = fullHealth;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addDamage(float damage){
        if(damage <= 0) return;
        playerAs.clip = playerHurt;
        playerAs.PlayOneShot(playerHurt);
        currentHealth -= damage;
        healthSlider.value = currentHealth;
        StartCoroutine(FlashRed());


        if(currentHealth<=0){
            makeDead();
        }
    }

    public void addHealth(float health){
        currentHealth += health;
        if(currentHealth > fullHealth){
            currentHealth = fullHealth;
        }
        playerAs.clip = playerHealed;
        playerAs.PlayOneShot(playerHealed);
        healthSlider.value = currentHealth;
        StartCoroutine(FlashGreen());
    }

    public void makeDead(){
        Destroy(gameObject);

        Animator gameOverAnimator = gameOverScreen.GetComponent<Animator>();
        gameOverAnimator.SetTrigger("gameOver");
        theGameManager.resetGame();
    }

    public IEnumerator FlashRed(){
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }

    public IEnumerator FlashGreen(){
        spriteRenderer.color = Color.green;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }

}
