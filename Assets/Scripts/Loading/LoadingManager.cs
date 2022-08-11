using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadingManager : MonoBehaviour
{
    [Header("Const Variables")]
    private const float fadeTime = 1.5f;
    private const float rotateTime = 2.6f;

    [Header("Object Variables")]
    [SerializeField]private TextMeshProUGUI text;   // Fade In Effect
    [SerializeField]private GameObject[] gears;     // Fade In Effect + Rotation
    void Awake(){
        CheckDataReset();
        StartCoroutine(LoadingAnimation());
    }
    private void CheckDataReset(){
        if(DataManager.gameData.newGame){
            DataManager.instance.ResetGame();
            DataManager.instance.SetNewGame(false);
        }
    }

    private IEnumerator LoadingAnimation(){
        yield return StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn(){
        float timer = 0.0f;
        Color color = gears[0].GetComponent<Image>().color;
        while (timer <= fadeTime)
        {
            color.a = Mathf.Lerp(0.0f, 1.0f, timer/fadeTime);
            text.color = color;
            gears[0].GetComponent<Image>().color = color;
            gears[1].GetComponent<Image>().color = color;
            gears[2].GetComponent<Image>().color = color;
            yield return new WaitForSeconds(0.01f);
            timer += 0.01f;
        }
        StartCoroutine(Rotation());
    }

    private IEnumerator Rotation(){
        float timer = 0.0f;
        float[] angles = new float[] {gears[0].transform.rotation.z, gears[1].transform.rotation.z, gears[2].transform.rotation.z};
        float[] dest_angles  = new float[] {210.0f, -210.0f, -210.0f};
        while(timer <= rotateTime){
            float rate = timer / rotateTime;
            rate = Mathf.Min(rate, 1.0f);
            rate = Mathf.Sin(rate * Mathf.PI / 2.0f);
            for( int i = 0; i < 3; i++){
                float current_angle = Mathf.Lerp(angles[i], angles[i] + dest_angles[i], rate);
                gears[i].transform.rotation = Quaternion.Euler(0,0, current_angle);
            }
            yield return new WaitForSeconds(0.01f);
            timer += 0.01f;
        }
        GoToMap();
    }

    private void GoToMap(){
        TransitionManager.instance.SceneTransition("MapScene");
    }
}
