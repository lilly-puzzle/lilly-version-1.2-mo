using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderManager : MonoBehaviour
{
    [Header("Variables")]
    public int gridY;
    public int gridX;
    private NF_Shapes spiderShape = NF_Shapes.None;
    
    [Header("Script Variables")]
    public NameFinding NFmanager;

    public void setSpider(){
        Color color = this.GetComponent<SpriteRenderer>().color;
        color.a = 1.0f;
        this.GetComponent<SpriteRenderer>().color = color;
        spiderShape = NF_Shapes.Spider;
    }

    public void setTransparent(){
        Color color = this.GetComponent<SpriteRenderer>().color;
        color.a = 0.0f;
        this.GetComponent<SpriteRenderer>().color = color;
        spiderShape = NF_Shapes.None;
    }

    public NF_Shapes getSpider(){
        return this.spiderShape;
    }
}
