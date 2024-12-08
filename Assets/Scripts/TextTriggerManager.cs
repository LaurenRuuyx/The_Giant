using UnityEngine;

public class TextTriggerManager : MonoBehaviour
{
    // Start is called before the first frame update

    //ORIGINAL CODE
    
    bool triggered = false;
    float timing;
    void Start()
    {
        timing = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(timing < Time.time){
            triggered = false;
        }
    }

    public void triggerTextScene(){
        if(triggered == false){
            triggered = true;
            timing = Time.time + 6.0f;
        }

    }

    public bool getTrigger(){
        return triggered;
    }
}
