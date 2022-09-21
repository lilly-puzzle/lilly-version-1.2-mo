using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MHEnum;

public class MHCat : MonoBehaviour
{  
    [Header("Script Variable")]
    [SerializeField] private MouseHole mhManager;
    
    [Header("Sprite Variable")]
    [SerializeField] private Sprite[] catSprites = new Sprite[4];    // [0] Hungry  [1] Eating  [2]  Hunting  [3]  HasMouse
    
    public void Init(MHCatCondition condition){
        switch((int)condition){
            case (int)MHCatCondition.Hungry:
                // Hungry Animation
                this.GetComponent<SpriteRenderer>().sprite = catSprites[0];
                break;
            case int n when(1 <= n && n <= 3):
                // Eating, Hunting will go Hunting
                mhManager.mhCatCondition = MHCatCondition.HasMouse;
                this.GetComponent<SpriteRenderer>().sprite = catSprites[3];
                
                break;
            default:
                break;
        }
    }

    public void EatMilk(){
        StartCoroutine(EatMilkCor());
    }
    
    IEnumerator EatMilkCor(){
        yield return null;
        // while(true){
        //     // Eat Milk Animation
        //     yield return null;
        // }
        mhManager.mhCatCondition = MHCatCondition.Hunting;
        StartCoroutine(HuntMouse());
    }

    IEnumerator HuntMouse(){
        yield return null;
        // while(true){
        //     // Hunt Mouse Animation
        //     yield return null;
        // }
        mhManager.mhCatCondition = MHCatCondition.HasMouse;
        StartCoroutine(HasMouse());
    }

    IEnumerator HasMouse(){
        
        // while(true){
        //     // Has mOUSE Animation
        //     yield return null;
        // }
        yield return null;

        mhManager.GetMouse();
    }
}
