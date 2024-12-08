using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBossFightWalls : MonoBehaviour
{
    // Start is called before the first frame update

    //ORIGINAL CODE
    
    public Transform tilemapTransform;
    public GameObject enemyBoss;
    bool moved = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            if(moved) return;
            tilemapTransform.position = new Vector3(tilemapTransform.position.x,0,tilemapTransform.position.z);
            BossMovement bossMove = enemyBoss.GetComponent<BossMovement>();
            bossMove.changeCanAttack();
            moved = true;

        }
    }

    void OnTriggerStay2D(Collider2D other){
        if(other.tag == "Player"){
            if(moved) return;
            tilemapTransform.position = new Vector3(tilemapTransform.position.x,0,tilemapTransform.position.z);
            BossMovement bossMove = enemyBoss.GetComponent<BossMovement>();
            bossMove.changeCanAttack();
            moved = true;

        }
    }


}
