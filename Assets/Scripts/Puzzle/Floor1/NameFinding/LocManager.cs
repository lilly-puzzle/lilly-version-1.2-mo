using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocManager : SimpleOneTouch
{
    [Header("Script Variables")]
    public NameFinding NFmanager;
    

    [Header("Constant & ReadOnly Variables")]
    // 1 = Tri  2 = Inv  3 = Rec 4 = Cir    -1 = Wall
    private readonly int[] START_BUG_CRITERIA = new int[] {-1, 1, 2, 3, 4, -1};
    private const float SIZE_DOWN_OFFSET = 0.8f;
    private const float FADE_TIME = 0.7f;
    public readonly Vector3 DEFAULT_SIZE = new Vector3(1.0f, 1.0f, 1.0f);

    [Header("Variables")]
    public int gridY;
    public int gridX;
    private Coroutine fadeCoroutine;

    protected override void FuncWhenTouchEnded() {
        // Suppose this puzzle code 102
        int itemCode = InventoryManager.instance.curSelectedItem;
        if(itemCode >= 10201 && itemCode <= 10204){
            int selectBug = itemCode % 10;
            if(START_BUG_CRITERIA[gridX] == selectBug){
                NFmanager.makeBug(gridX, selectBug);
            }
        }
        else if(itemCode == 10205){
            NFmanager.installSauce(gridY, gridX);
            setSauce(true);
        }
        if(gridY == 5){
            NFmanager.zoomTouch(gridX);
        }
    }

    public void setTransparent(bool animation){
        if(animation){
            if(fadeCoroutine != null){
                StopCoroutine(fadeCoroutine);
            }
            fadeCoroutine = StartCoroutine(Fade(false));
        }
        else{
            Color color = this.GetComponent<SpriteRenderer>().color;
            color.a = 0.0f;
            this.GetComponent<SpriteRenderer>().color = color;
        }
    }

    public void setSauce(bool animation){
        if(animation){
            if(fadeCoroutine != null){
                StopCoroutine(fadeCoroutine);
            }
            fadeCoroutine = StartCoroutine(Fade(true));
        }
        else{
            Color color = this.GetComponent<SpriteRenderer>().color;
            color.a = 1.0f;
            this.GetComponent<SpriteRenderer>().color = color;
        }
    }

    IEnumerator Fade(bool fadeIn){
        Color color = this.GetComponent<SpriteRenderer>().color;
        
        Vector3 destSize;
        Vector3 startSize;
        float destA;
        float startA;
        if(fadeIn){
            destA = 1.0f;
            startA = 0.0f;
            color.a = 0.0f;
            startSize = DEFAULT_SIZE * SIZE_DOWN_OFFSET;
            destSize = DEFAULT_SIZE;
        }
        else{
            destA = 0.0f;
            startA = 1.0f;
            color.a = 1.0f;
            startSize = DEFAULT_SIZE;
            destSize = DEFAULT_SIZE * SIZE_DOWN_OFFSET;
        }

        float step_timer = 0.0f;
        while(color.a != destA){
            float rate = step_timer / FADE_TIME;
            rate = Mathf.Min(rate, 1.0f);
            rate = Mathf.Sin(rate * Mathf.PI / 2.0f);
            color.a = Mathf.Lerp(startA, destA, rate);
            this.GetComponent<SpriteRenderer>().color = color;
            this.transform.localScale = Vector3.Lerp(startSize, destSize, rate);

            yield return null;
            step_timer += Time.deltaTime;
        }
        this.transform.localScale = DEFAULT_SIZE;
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.name == "bug"){
            this.setTransparent(true);
            NFmanager.removeSauce(gridY, gridX);
        }
    }
}
