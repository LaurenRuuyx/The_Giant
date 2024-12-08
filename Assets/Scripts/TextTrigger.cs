using UnityEngine;
using UnityEngine.UI;

public class TextTrigger : MonoBehaviour
{
    // Start is called before the first frame update

    //ORIGINAL CODE

    public Text textToTrigger;
    TextTriggerManager triggerManager;
    AudioSource source;
    public AudioClip voiceLine;
    bool triggered;
    void Start()
    {
        triggerManager = gameObject.GetComponentInParent<TextTriggerManager>();
        if(gameObject.GetComponent<AudioSource>() != null){
            source = gameObject.GetComponent<AudioSource>();
        }
        triggered = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player" && !triggerManager.getTrigger() && !triggered){
            Animator textAnim = textToTrigger.GetComponent<Animator>();
            textAnim.SetTrigger("textTrigger");
            triggerManager.triggerTextScene();
            if(source != null){
                source.clip = voiceLine;
                source.PlayOneShot(voiceLine);
            }
            triggered = true;
        }
    }
    
    void OnTriggerStay2D(Collider2D other){
        if(other.tag == "Player" && !triggerManager.getTrigger() && !triggered){
            Animator textAnim = textToTrigger.GetComponent<Animator>();
            textAnim.SetTrigger("textTrigger");
            triggerManager.triggerTextScene();
            if(source != null){
                source.clip = voiceLine;
                source.PlayOneShot(voiceLine);
            }
            triggered = true;

        }
    }
}
