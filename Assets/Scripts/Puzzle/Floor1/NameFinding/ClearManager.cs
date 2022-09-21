using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearManager : MonoBehaviour
{
    [Header("Script Variables")]
    public NameFinding NFmanager;

    [Header("Object Variables")]
    [SerializeField] private Sprite[] sprites;  // [2n] Graffiti Version [2n +1] Name Engraved Bug, n = 1 Tri, n = 2 Rev, n = 3 Rec, n = 4 Cir
    
    [Header("Variables")]
    public int gridX;
    public Vector3 defaultPos;
    private int spriteNum;
    private Coroutine moveCoroutine;

    [Header("Constant Variables")]
    private const float zoomSize = 3.0f;
    private readonly Vector3 moveOffset = new Vector3(0.0f, 0.5f, 0.0f);
    private const float bugMovingTime = 1.0f;

    public void setVisible(){
        Color color = this.GetComponent<SpriteRenderer>().color;
        color.a = 1.0f;
        this.GetComponent<SpriteRenderer>().color = color;
    }

    public void setTransparent(){
        Color color = this.GetComponent<SpriteRenderer>().color;
        color.a = 0.0f;
        this.GetComponent<SpriteRenderer>().color = color;
    }

    public void setSprite(int num){
        this.GetComponent<SpriteRenderer>().sprite = sprites[num];
        spriteNum = num;
    }

    public void clearAnimation(){
        if(moveCoroutine != null){
            StopCoroutine(moveCoroutine);
        }
        moveCoroutine = StartCoroutine(moveDown());
    }

    IEnumerator moveDown(){
        this.transform.position = defaultPos + moveOffset;
        Vector3 initPos = this.transform.position;
        setVisible();
        
        float step_timer = 0.0f;
        while(this.transform.position != defaultPos){
            float rate = step_timer / bugMovingTime;
            rate = Mathf.Min(rate, 1.0f);
            rate = Mathf.Sin(rate * Mathf.PI / 2.0f);
            this.transform.position = Vector3.Lerp(initPos, defaultPos, rate);
            yield return null;
            step_timer += Time.deltaTime;
        }
    }

    public void zoomBugCrystal(){
        // Need CallBack Function Or Function Pointer
        setSprite(spriteNum + 1);

        ZoomManager.instance.Zoom(true,this.transform.position.x, this.transform.position.y, zoomSize);
    }
}
