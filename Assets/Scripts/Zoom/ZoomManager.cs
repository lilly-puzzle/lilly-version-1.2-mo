using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoomManager : MonoBehaviour
{
    public static ZoomManager instance;

    [Header("Object Variable")]
    [SerializeField] private GameObject mainCameraObject;
    private Camera mainCamera;
    [SerializeField] private GameObject filterObject;
    private Image filterImage;

    [Header("Constant Variable")]
    private const float zoomInOffset = 0.05f;
    private const float zoomOutOffset = 0.08f;
    private const float zoomTime = 0.4f;
    private const float blinkTime = 0.3f;

    [Header("Variable")]
    private Coroutine blinkCoroutine;
    private Coroutine zoomCoroutine;
    private Coroutine fadeCoroutine;

    // Callback function
    public delegate void Callback();


    private void Awake(){
        instance = this;
        initialSetUp();
    }

    private void initialSetUp(){
        filterImage = filterObject.GetComponent<Image>();
        mainCamera = mainCameraObject.GetComponent<Camera>();
        filterObject.SetActive(false);
    }

    public void Zoom(bool isZoomIn, float posX, float posY, float zoomSize, Callback curtainFunction = null){
        if(zoomCoroutine != null){
            StopCoroutine(zoomCoroutine);
        }
        zoomCoroutine = StartCoroutine(ZoomCor(isZoomIn, posX, posY, zoomSize, curtainFunction));
    }

    private IEnumerator ZoomCor(bool isZoomIn, float posX, float posY, float zoomSize, Callback curtainFunction = null){
        if(blinkCoroutine != null){
            StopCoroutine(blinkCoroutine);
        }
        yield return blinkCoroutine = StartCoroutine(Blink(true));
        
        mainCameraObject.transform.position = new Vector3(posX, posY, -10);
        if(curtainFunction != null){
            curtainFunction();
        }

        if(blinkCoroutine != null){
            StopCoroutine(blinkCoroutine);
        }
        blinkCoroutine = StartCoroutine(Blink(false));
        

        // For zoom animation
        float init_zoomsize = isZoomIn ? zoomSize + zoomSize * zoomInOffset : zoomSize - zoomSize * zoomOutOffset;
        float current_zoomsize = init_zoomsize;
        float steptimer = 0.0f;
        while(current_zoomsize != zoomSize){
            float rate = steptimer / zoomTime;
            rate = Mathf.Min(rate, 1.0f);
            rate = Mathf.Sin(rate * Mathf.PI / 2.0f);
            current_zoomsize = Mathf.Lerp(init_zoomsize, zoomSize, rate);
            mainCamera.orthographicSize = current_zoomsize;
            
            steptimer += Time.unscaledDeltaTime;
            yield return null;
        }
    }

    private IEnumerator Blink(bool isBlinkIn){
        filterObject.SetActive(true);
        Color blinkC = filterImage.color;
        float initA;
        float destA;
        if(isBlinkIn){
            initA = 0.0f;
            destA = 1.0f;
        }
        else{
            initA = 1.0f;
            destA = 0.0f;
        }

        float steptimer = 0.0f;
        while(blinkC.a != destA){
            float rate = steptimer /blinkTime;
            rate = Mathf.Min(rate, 1.0f);
            rate = Mathf.Sin(rate * Mathf.PI / 2.0f);
            blinkC.a = Mathf.Lerp(initA, destA, rate);
            filterImage.color = blinkC;
            
            steptimer += Time.unscaledDeltaTime;
            yield return null;
        }
        if(!isBlinkIn) filterObject.SetActive(false);
    }
}
