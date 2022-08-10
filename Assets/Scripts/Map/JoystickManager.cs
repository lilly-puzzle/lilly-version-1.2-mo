using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoystickManager : MonoBehaviour
{
    [Header("Script Variable")]
    [SerializeField] private MapCameraManager mapCameraManager;
    
    [Header("Object Variable")]
    [SerializeField] private RectTransform touchArea;
    [SerializeField] private GameObject joystickObject;
    [SerializeField] private Image outerPad;
    [SerializeField] private Image innerPad;

    [Header("Constant Variable")]
    private float fadeTime = 0.4f;
    private float minRadius = 30.0f;
    private float maxRadius = 100.0f;
    
    [Header("Variable")]
    private Vector2 joystickVector;
    private Vector2 initialTouchPos;
    private Vector2 presentTouchPos;
    private float distance = 0.0f;
    private bool isJoystickAct = false;
    private bool touchMode = true;
    private Coroutine fadeCoroutine;

    void Awake(){
        joystickObject.SetActive(false);
    }

    void Update(){
        if(mapCameraManager.cameraOnPlayer){
            if(Input.touchCount > 0){
                FollowInput();
            }
        }
    }

    private void FollowInput(){
        Touch touch = Input.GetTouch(0);
        if (touch.phase != TouchPhase.Canceled){
            if (touch.phase == TouchPhase.Began){
                touchMode = true;
                initialTouchPos = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended){
                HideJoystick();
            }
            else {
                presentTouchPos = touch.position;
                distance = Vector2.Distance(initialTouchPos, presentTouchPos);
                if(isJoystickAct == false && distance > minRadius){
                    Debug.Log("ShowJoystick");
                    ShowJoystick(initialTouchPos);
                }
            }

            if (isJoystickAct && touch.phase != TouchPhase.Began && touch.phase != TouchPhase.Ended){
                joystickVector = presentTouchPos - initialTouchPos;
                joystickVector = (joystickVector.magnitude > maxRadius) ? joystickVector.normalized * maxRadius : joystickVector;
                innerPad.rectTransform.anchoredPosition = joystickVector;
            }
        }
    }

    IEnumerator Fade(bool isFadeIn){
        Color fade = outerPad.color;
        float destA;
        float initA;
        destA = isFadeIn ? 1.0f : 0.0f;
        initA = isFadeIn ? 0.0f : 1.0f;
        float step_timer = 0.0f;
        while(step_timer <= fadeTime){
            float rate = step_timer / fadeTime;
            rate = Mathf.Min(rate, 1.0f);
            rate = Mathf.Sin(rate * Mathf.PI / 2.0f);
            fade.a = Mathf.Lerp(initA, destA, rate);
            outerPad.color = fade;
            innerPad.color = fade;
            step_timer += Time.unscaledDeltaTime;
            yield return null;
        }
        if(!isFadeIn){
            joystickObject.SetActive(false);
        }
    }

    private void ShowJoystick(Vector2 createPos){
        isJoystickAct = true;
        touchMode = false;
        joystickObject.transform.position = createPos;
        joystickObject.SetActive(true);
        if(fadeCoroutine != null){
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(Fade(true));
    }

    private void HideJoystick(){
        isJoystickAct = false;
        if(fadeCoroutine != null){
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(Fade(false));
        joystickVector = Vector2.zero;
    }

    public int GetJoystickDir(){
        if(joystickVector.x > maxRadius * (2.0f / 3.0f)){
            return 2;
        }
        else if(joystickVector.x > maxRadius * (1.0f / 3.0f)){
            return 1;
        }
        else if(joystickVector.x < maxRadius * (-2.0f / 3.0f)){
            return -2;
        }
        else if(joystickVector.x < maxRadius * (-1.0f / 3.0f)){
            return -1;
        }
        else{
            return 0;
        }
    }

    public bool GetisJoystickAct(){
        return isJoystickAct;
    }

    public bool GettouchMode(){
        return touchMode;
    }
}
