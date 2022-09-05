using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugManager : MonoBehaviour
{
    [Header("Script Variables")]
    public NameFinding NFmanager;
    
    [Header("Object Variables")]
    [SerializeField] private Sprite[] sprites;
    
    [Header("Variables")]
    public int gridY;
    public int gridX;
    private NF_Shapes bugShape = NF_Shapes.None;
    private Coroutine moveCoroutine;
    private Coroutine fadeCoroutine;

    [Header("Constant Variables")]
    private const float bugMovingTime = 1.0f;
    private const float fadeTime = 0.7f;


    public void setBug(int bugShape_){
        switch(bugShape_){
            case 1:
                this.bugShape = NF_Shapes.Tri;
                break;
            case 2:
                this.bugShape = NF_Shapes.Inv;
                break;
            case 3:
                this.bugShape = NF_Shapes.Rec;
                break;
            case 4:
                this.bugShape = NF_Shapes.Cir;
                break;
            default:
                break;
        }
        this.GetComponent<SpriteRenderer>().sprite = this.sprites[bugShape_];
        NFmanager.navigateBug(gridY, gridX);
    }

    public int getBug(){
        switch(this.bugShape){
            case NF_Shapes.Tri:
                return 0;
            case NF_Shapes.Inv:
                return 1;
            case NF_Shapes.Rec:
                return 2;
            case NF_Shapes.Cir:
                return 3;
            default:
                return -1;
        }
    }

    public void movePos(Vector3 dest){
        if(moveCoroutine != null){
            StopCoroutine(moveCoroutine);
        }
        moveCoroutine = StartCoroutine(movePosCor(dest));
    }

    IEnumerator movePosCor(Vector3 destPos){
        Vector3 defaultPos = this.transform.position;
        float step_timer = 0.0f;
        while(this.transform.position != destPos){
            float rate = step_timer /bugMovingTime;
            rate = Mathf.Min(rate, 1.0f);
            rate = Mathf.Sin(rate * Mathf.PI / 2.0f);
            this.transform.position =Vector3.Lerp(defaultPos, destPos, rate);
            yield return null;
            step_timer += Time.deltaTime;
        }
        NFmanager.navigateBug(gridY, gridX);
    }

    public void moveGrid(int y, int x){
        gridY = y;
        gridX = x;
    }

    private void OnTriggerEnter2D(Collider2D other){
        // if you need, Add Tag "Spider"
        if(other.gameObject.tag == "Spider"){
            SpiderManager spider = other.GetComponent<SpiderManager>();
            if(spider.getSpider() == NF_Shapes.Spider){
                this.failBug(0);
            }
        }
    }

    public void completeBug(){
        // Need Complete Animation 액자 아래로 없어지는 애니메이션 필요
        Destroy(this.gameObject);
        // Need delete the bug item in map scene
    }

    public void failBug(int failmode){
        switch(failmode){
            case 0:     // be eaten
                Destroy(this.gameObject, fadeTime);
                if(fadeCoroutine != null){
                    StopCoroutine(fadeCoroutine);
                }
                fadeCoroutine = StartCoroutine(fadeOut());
                break;
            case 1:
                Destroy(this.gameObject, fadeTime);
                Debug.Log("failBug1 flyaway Animation not yet made");
                break;
            default:
                break;
        }
    }

    IEnumerator fadeOut(){
        Color fade = GetComponent<SpriteRenderer>().color;
        fade.a = 1.0f;
        float step_timer = 0.0f;
        while(fade.a != 0.0f){
            float rate = step_timer / fadeTime;
            rate = Mathf.Min(rate, 1.0f);
            rate = Mathf.Sin(rate * Mathf.PI / 2.0f);
            fade.a = Mathf.Lerp(1.0f, 0.0f, rate);
            this.GetComponent<SpriteRenderer>().color = fade;
            yield return null;
            step_timer += Time.deltaTime;
        }
    }
}
