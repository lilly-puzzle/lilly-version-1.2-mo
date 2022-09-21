using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Family{
    Cecilly = 0,
    Lilly,
    Mom,
    Dad,
}

struct KTScript {
    public Family speaker;
    public string ment;

    public KTScript(Family speaker_, string ment_){
        speaker = speaker_;
        ment = ment_;
    }
};

public class KTEvent : MonoBehaviour
{
    [Header("Script Variable")]
    [SerializeField] private KitchenTable ktManager;
    [SerializeField] private KTDish[] ktDish = new KTDish[4];
    
    [Header("Object Variable")]
    [SerializeField] private GameObject[] dishObjects = new GameObject[4];  // [0] LeftTop [1] RightTop [2] LeftBottom [3] RightBottom

    [Header("ReadOnly Variable")]
    [SerializeField] private readonly KTScript[,] eventTalking = new KTScript[2,4]{
        {
            new KTScript(Family.Cecilly, "Mom, I'm Hungry"),
            new KTScript(Family.Mom, "Wait"),
            new KTScript(Family.Cecilly, "Dad hates me"),
            new KTScript(Family.Mom, "Because Dad is tired"),
        },
        {
            new KTScript(Family.Cecilly, "Mom, Why I can't eat Poision"),
            new KTScript(Family.Mom, "You can't eat"),
            new KTScript(Family.Cecilly, "why?"),
            new KTScript(Family.Mom, "Because I love you"),
        }
    };

    private const float typingSpeed = 2.0f;

    [Header("Variable")]
    private bool isShaking = false;
    private bool isNextMent = false;


    void Update(){
        if(Input.GetMouseButtonUp(0)){
            isNextMent = true;
        }
    }

    public void Progress0Event(){
        StartCoroutine(TalkEvent(0, 4));
    }

    public void Progress1Event(){
        StartCoroutine(Progress1EventCor());
    }

    private IEnumerator Progress1EventCor(){
        TouchBlockManager.instance.TouchBlock();
        ZoomManager.instance.Fade(true);
        // Play Sound
        yield return new WaitForSeconds(0.8f);
        ktDish[1].SetDish(0);
        
        ZoomManager.instance.Fade(false);
        TouchBlockManager.instance.TouchBlockEnd();
    }

    public void Progress2Event(){
        StartCoroutine(Progress2EventCor());
    }

    private IEnumerator Progress2EventCor(){
        TouchBlockManager.instance.TouchBlock();
        yield return StartCoroutine(TalkEvent(1, 4));
        ZoomManager.instance.Fade(true);
        // Play Sound
        yield return new WaitForSeconds(0.8f);
        // Mom Poision Soup SetActive(true)
        ktDish[0].SetDish(0);
        ktDish[2].SetDish(0);
        ktDish[3].SetDish(0);
        
        ZoomManager.instance.Fade(false);
        TouchBlockManager.instance.TouchBlockEnd();
    }

    IEnumerator TalkEvent(int eventNum, int mentNum){
        TouchBlockManager.instance.TouchBlock();
        for(int i = 0; i < mentNum; i++){
            yield return StartCoroutine(KTTalk(eventTalking[eventNum, i].speaker, eventTalking[eventNum, i].ment));
            
            while(!isNextMent){
                yield return null;
            }
            isNextMent = false;
        }
        NarrationManager.instance.NarrationEnd();
        TouchBlockManager.instance.TouchBlockEnd();
    }

    IEnumerator KTTalk(Family speaker, string ment){
        NarrationManager.instance.Narration(ment, typingSpeed,NarrationDone);
        isShaking = true;
        while(isShaking){
            yield return StartCoroutine(DishShaking(speaker, new Vector3(0.0f, 0.1f, 0.0f), 0.3f));
        }
    }

    private void NarrationDone(){
        isShaking = false;
    }


    // Not Script Animation, Use 2 Frame Animation
    IEnumerator DishShaking(Family speaker, Vector3 shakeOffset, float shakeTime){
        GameObject Dish = dishObjects[(int)speaker];
        Vector3 initPos = Dish.gameObject.transform.position;
        Vector3 currentPos = initPos;
        Vector3 shakePos = initPos + shakeOffset;
        float rate;
        float step_timer = 0.0f;
        while(currentPos != shakePos){
            rate = step_timer / shakeTime;
            rate = Mathf.Min(rate, 1.0f);
            rate = Mathf.Sin(rate * Mathf.PI / 2.0f);
            currentPos = Vector3.Lerp(initPos, shakePos, rate);
            Dish.gameObject.transform.position = currentPos;
            yield return null;
            step_timer += Time.deltaTime;
        }

        step_timer = 0.0f;
        while(currentPos != initPos){
            rate = step_timer / shakeTime;
            rate = Mathf.Min(rate, 1.0f);
            rate = Mathf.Sin(rate * Mathf.PI / 2.0f);
            currentPos = Vector3.Lerp(shakePos, initPos, rate);
            Dish.gameObject.transform.position = currentPos;
            yield return null;
            step_timer += Time.deltaTime;
        }
    }

    

}
