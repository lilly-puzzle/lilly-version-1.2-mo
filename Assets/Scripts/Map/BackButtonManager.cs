using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButtonManager : MonoBehaviour
{
    [Header("Object Variable")]
    [SerializeField] private GameObject backButtonObject;
    private Image buttonImg;

    [Header("Constant Variable")]
    private float fadeTime = 0.5f;
    private Coroutine fadeCoroutine;

    void Awake(){
        buttonImg = backButtonObject.GetComponent<Image>();
        backButtonObject.SetActive(false);
    }
    
    public void MakeBackButton(){
        backButtonObject.SetActive(true);
        
        if(fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(Fade(true));
    }

    private IEnumerator Fade(bool isFadeIn){
        float initA = isFadeIn ? 0.0f : 1.0f;
        float destA = isFadeIn ? 1.0f : 0.0f;

        Color color = buttonImg.color;
        float step_timer = 0.0f;
        float rate;
        while(color.a != destA){
            rate = step_timer / fadeTime;
            rate = Mathf.Min(rate, 1.0f);
            rate = Mathf.Sin(rate * Mathf.PI / 2.0f);
            color.a = Mathf.Lerp(initA, destA, rate);
            buttonImg.color = color;
            
            step_timer += Time.unscaledDeltaTime;
            yield return null;
        }
        if(!isFadeIn){
            backButtonObject.SetActive(false);
        }

    }

    public void DeleteBackButton(){
        Color color_ = buttonImg.color;
        color_.a = 0.0f;
        buttonImg.color = color_;
    }

    public void PressBackButton(){
        MapCameraManager.instance.ZoomOut();
    }
}
