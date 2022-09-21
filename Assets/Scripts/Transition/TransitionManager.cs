using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager instance;

    [Header("Const Variables")]
    private const float blinkTime = 1.0f;

    [Header("Object Variables")]
    [SerializeField] private GameObject filterObject;
    private Color filterColor;

    [Header("Variables")]
    private string destSceneName;
    private AsyncOperation op;


    void Awake(){
        instance = this;
        filterColor = filterObject.GetComponent<Image>().color;
        DontDestroyOnLoad(this.gameObject);
    }

    public void SceneTransition(string destScene){
        SceneManager.sceneLoaded += OnSceneLoaded;
        destSceneName = destScene;
        filterObject.SetActive(true);
        StartCoroutine(SceneTransitionCor());
    }

    private IEnumerator SceneTransitionCor(){
        AsyncLoad();
        yield return StartCoroutine(Fade(true));
        while(!CheckIfDataLoaded()){
            yield return null;
        }
        StartCoroutine(ActivateScene());
        yield break;
    }

    private void AsyncLoad(){
        op = SceneManager.LoadSceneAsync(destSceneName);
        op.allowSceneActivation = false;
    }

    private IEnumerator Fade(bool isFadeIn){
        float step_timer = 0.0f;
        while(step_timer <= blinkTime){
            yield return null;
            step_timer += Time.unscaledDeltaTime;
            float rate = step_timer / blinkTime;
            rate = Mathf.Min(rate, 1.0f);
            rate = Mathf.Sin(rate * Mathf.PI / 2.0f);
            filterColor.a = isFadeIn ? Mathf.Lerp(0.0f, 1.0f, rate) : Mathf.Lerp(1.0f, 0.0f, rate);
            filterObject.GetComponent<Image>().color = filterColor;
        }
        
        if(!isFadeIn){
            PrepareScene();
        }
    }

    private bool CheckIfDataLoaded(){
        // if(!isDataLoaded){
        //     return false;
        // }
        return true;
    }

    private IEnumerator ActivateScene(){
        op.allowSceneActivation = true;
        
        yield return null;
        if(destSceneName == "MapScene"){
            InventoryManager.instance.ActivateInventory(true, 2);
            INMoveControl.instance.SetSceneNum(2);
        }
        else if(destSceneName == "PuzzleScene"){
            InventoryManager.instance.ActivateInventory(true, 3);
            INMoveControl.instance.SetSceneNum(3);
            PuzzleManager.instance.AwakePuzzle();
        }
        else{
            InventoryManager.instance.ActivateInventory(false, -1);
        }
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1){
        if(arg0.name == destSceneName){
            StartCoroutine(Fade(false));
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    private void PrepareScene(){
        filterObject.SetActive(false);
    }
}
