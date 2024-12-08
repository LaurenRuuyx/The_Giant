using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    
    //CASANIS PLAYS CAMERA CODE WITH CHANGES MADE FOR THIS GAME
    public Transform target;
    public float smoothing;
    public float highYAddition;
    public float highXAddition;
    Vector3 offset;
    float lowY;
    float highY;
    float lowX;
    float highX;
    void Start()
    {
        offset = new Vector3(0,transform.position.y - target.position.y - 10,transform.position.z - target.position.z);
        lowY = transform.position.y - 10;
        highY = lowY + 7 + highYAddition;
        lowX = transform.position.x;
        highX =  lowX + 200 + highXAddition;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetCamPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position,targetCamPos,smoothing * Time.deltaTime);
        if(transform.position.y < lowY) transform.position = new Vector3(transform.position.x,lowY,transform.position.z);
        if(transform.position.y > highY) transform.position = new Vector3(transform.position.x,highY,transform.position.z);
        if(transform.position.x < lowX) transform.position = new Vector3(lowX,transform.position.y,transform.position.z);
        if(transform.position.x > highX) transform.position = new Vector3(highX,transform.position.y,transform.position.z);

    }
}
