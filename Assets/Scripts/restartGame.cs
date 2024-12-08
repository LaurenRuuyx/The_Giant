using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class restartGame : MonoBehaviour
{
    // Start is called before the first frame update
    
    //CASANIS PLAYS CODE
    public float restartTime;
    bool restartNow = false;
    float resetTime;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(restartNow && resetTime < Time.time){
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        }
    }

    public void resetGame(){
        restartNow = true;
        resetTime = Time.time + restartTime;
    }
}
