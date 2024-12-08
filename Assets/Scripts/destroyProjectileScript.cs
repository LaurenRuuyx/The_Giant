using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyProjectileScreipt : MonoBehaviour
{
    // Start is called before the first frame update
    
    //CASANIS PLAYS CODE
    public float aliveTime;
    void Awake()
    {
        Destroy(gameObject,aliveTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
