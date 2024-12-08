using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyHealth : MonoBehaviour
{
    // Start is called before the first frame update
    
    //CASANIS PLAYS CODE WITH CHANGES MADE LIKE DROPPING POTIONS OR FLASHING RED WHEN TAKING DAMAGE
    public float enemyMaxHealth;
    float enemyCurrentHealth;
    public SpriteRenderer sprite;
    public Text gameWonScreen;
    public Slider healthSlider;
    public GameObject potion;
    void Start()
    {
        enemyCurrentHealth = enemyMaxHealth;
        if(healthSlider != null){
            healthSlider.maxValue = enemyMaxHealth;
            healthSlider.value = enemyMaxHealth;
        } 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addDamage(float damage){
        enemyCurrentHealth -= damage;
        StartCoroutine(FlashRed());
        if(enemyCurrentHealth <= 0) makeDead();
        if(healthSlider != null) healthSlider.value = enemyCurrentHealth;
        if(transform.parent.gameObject.tag == "Boss"){
            if(enemyCurrentHealth == 60 || enemyCurrentHealth == 40 || enemyCurrentHealth == 20){
                Instantiate(potion,new Vector3(transform.position.x,transform.position.y - 10,transform.position.z),Quaternion.Euler (new Vector3(0,0,0)));
            }
        }

    }

    void makeDead(){
        if(transform.parent.gameObject.tag == "Boss"){
            Animator gameOverAnimator = gameWonScreen.GetComponent<Animator>();
            gameOverAnimator.SetTrigger("gameWon");
        }
        else{
            if(Random.Range(0,10) >=5) Instantiate(potion,new Vector3(transform.position.x,transform.position.y,transform.position.z),Quaternion.Euler (new Vector3(0,0,0)));
        }
        Destroy(transform.parent.gameObject);
    }

    public IEnumerator FlashRed(){
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }

}
