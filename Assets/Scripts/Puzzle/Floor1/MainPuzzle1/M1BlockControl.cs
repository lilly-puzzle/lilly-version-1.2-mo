using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum M1move{
    UptoDown,
    DowntoUp,
    LefttoRight,
    RighttoLeft,
}

public class M1BlockControl : SimpleOneTouch
{
    [Header("Constant Variables")]
    private const float move_time = 0.5f;

    [Header("Variables")]
    public int grid_r;
    public int grid_c;
    private Vector3 start_offset;
    private Coroutine moveCoroutine;

    [Header ("Object Variables")]
    public Sprite[] sprites;    // 0 is Transparent

    [Header ("Script Variables")]
    public MainPuzzle1 block_root = null;

    public void setSprite(int n){
        this.GetComponent<SpriteRenderer>().sprite = this.sprites[n];
    }

    protected override void FuncWhenTouchEnded() {
        if(!block_root.getismoving()){
            block_root.touchBlock(grid_r, grid_c);
        }
    }

    public void move(M1move movecase){
        switch(movecase){
            case M1move.UptoDown:     // Up -> Down
                start_offset = new Vector3(0.0f, MainPuzzle1.BLOCK_SIZE * 1.0f, 0.0f);
                break;
            case M1move.DowntoUp:     // Down -> Up
                start_offset = new Vector3(0.0f, MainPuzzle1.BLOCK_SIZE * -1.0f, 0.0f);
                break;
            case M1move.LefttoRight:     // Left -> Right
                start_offset = new Vector3(MainPuzzle1.BLOCK_SIZE *-1.0f, 0.0f, 0.0f);
                break;
            case M1move.RighttoLeft:    // Right -> Left
                start_offset = new Vector3(MainPuzzle1.BLOCK_SIZE * 1.0f, 0.0f, 0.0f);
                break;
            default:
                Debug.Log("Move Block Error");
                break;
        }
        if(moveCoroutine != null){
            StopCoroutine(moveCoroutine);
        }
        moveCoroutine = StartCoroutine(moveBlock());
    }

    IEnumerator moveBlock(){
        float step_timer = 0.0f;
        Vector3 dest_offset = new Vector3(0.0f, 0.0f, 0.0f);
        Vector3 current_offset = start_offset;
        Vector3 default_pos = this.transform.position;
        
        block_root.setismoving(true);
        while(current_offset != dest_offset){
            float rate = step_timer / move_time;
            rate = Mathf.Min(rate, 1.0f);
            rate = Mathf.Sin(rate * Mathf.PI / 2.0f);
            current_offset = Vector3.Lerp(start_offset, dest_offset, rate);
            this.transform.position = default_pos + current_offset;
            yield return null;
            step_timer += Time.deltaTime;
        }
        block_root.setismoving(false);
    }
}
