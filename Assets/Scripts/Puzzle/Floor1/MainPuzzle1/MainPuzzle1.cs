using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DataFrame;
using SaveDataPerPuzzle.Floor1;

public class MainPuzzle1 : PuzzleMainController
{
    [Header ("Object Variables")]
    [SerializeField] private GameObject BlockPrefab = null;
    [SerializeField] private GameObject ClearSpriteObject;

    [Header ("Script Variables")]
    private M1BlockControl[,] Blocks = new M1BlockControl[BLOCK_ROW, BLOCK_COL];
    
    [Header("Constant Variables")]
    public const float BLOCK_SIZE = 1.0f;
    private const int BLOCK_COL = 4;
    private const int BLOCK_ROW = 4;
    private readonly Vector3 blockStartPos = new Vector3(BLOCK_SIZE * -1.5f, BLOCK_SIZE * 1.5f, 0.0f);
    private readonly int[,] correctblockGrid = { // 0 is transparent
        {  1,  2,  3,  4 },
        {  5,  6,  7,  8 },
        {  9, 10, 11, 12 },
        { 13, 14,  15, 0 }
    };

    [Header("Variables")]
    private bool newStart;
    private int[,] blockGrid = { // 0 is transparent
        {  1,  2,  3,  4 },
        {  5,  6,  7,  8 },
        {  9, 10, 11, 12 },
        { 13, 14,  0, 15 }
    };
    private bool ismoving = false;

    protected override void LoadEachPuzzleData() {
        MainPuzzle1Data saveData = PuzzleManager.instance.puzzleData.floor1Data.saveMainPuzzle1;

        blockGrid = saveData.blockGrid;
    }
    
    public override void SaveEachPuzzleData(PuzzleData a_puzzleSaveData) {
        a_puzzleSaveData.floor1Data.saveMainPuzzle1.blockGrid = blockGrid;
    }

    protected override void SetupPuz(){
        if(PuzzleManager.instance.CheckIfPuzzleClear(108)){
            ClearSpriteObject.SetActive(true);
        }
        else{
            ClearSpriteObject.SetActive(false);
            Blocks = new M1BlockControl[BLOCK_ROW, BLOCK_COL];
            for (int r = 0; r < BLOCK_ROW; r++){
                for (int c = 0; c < BLOCK_COL; c++){
                    GameObject instantiatedObject = Instantiate(BlockPrefab, this.transform);

                    instantiatedObject.transform.position = CalcBlockPosition(r, c);

                    M1BlockControl Block = instantiatedObject.GetComponent<M1BlockControl>();
                    Blocks[r, c] = Block;

                    Block.block_root = this;
                    Block.grid_r = r;
                    Block.grid_c = c;
                    Block.setSprite(blockGrid[r, c]);
                    Block.name = "Block(" + r.ToString() + ", " + c.ToString() + ")";

                }
            }
        }
    }

    private Vector3 CalcBlockPosition(int a_r, int a_c){
        return new Vector3(blockStartPos.x + BLOCK_SIZE * a_c, blockStartPos.y - BLOCK_SIZE * a_r, 0.0f);
    }

    public void touchBlock(int a_r, int a_c){
        if(a_r != 0){
            if(blockGrid[a_r -1, a_c] == 0){
                int temp = blockGrid[a_r, a_c];
                blockGrid[a_r, a_c] = 0;
                Blocks[a_r, a_c].setSprite(0);
                blockGrid[a_r - 1, a_c] = temp;
                Blocks[a_r - 1, a_c].setSprite(temp);
                Blocks[a_r - 1, a_c].move(M1move.DowntoUp);    // Down -> Up
                checkBlock();
                return;
            }
        }
        if(a_r != BLOCK_ROW -1){
            if(blockGrid[a_r +1, a_c] == 0){
                int temp = blockGrid[a_r, a_c];
                blockGrid[a_r, a_c] = 0;
                Blocks[a_r, a_c].setSprite(0);
                blockGrid[a_r + 1, a_c] = temp;
                Blocks[a_r + 1, a_c].setSprite(temp);
                Blocks[a_r + 1, a_c].move(M1move.UptoDown);    // Up -> Down
                checkBlock();
                return;
            }
        }
        if(a_c != 0){
            if(blockGrid[a_r, a_c-1] == 0){
                int temp = blockGrid[a_r, a_c];
                blockGrid[a_r, a_c] = 0;
                Blocks[a_r, a_c].setSprite(0);
                blockGrid[a_r, a_c - 1] = temp;
                Blocks[a_r, a_c - 1].setSprite(temp);
                Blocks[a_r, a_c - 1].move(M1move.RighttoLeft);     // Right -> Left
                checkBlock();
                return;
            }
        }
        if(a_c != BLOCK_COL - 1){
            if(blockGrid[a_r, a_c+1] == 0){
                int temp = blockGrid[a_r, a_c];
                blockGrid[a_r, a_c] = 0;
                Blocks[a_r, a_c].setSprite(0);
                blockGrid[a_r, a_c + 1] = temp;
                Blocks[a_r, a_c + 1].setSprite(temp);
                Blocks[a_r, a_c + 1].move(M1move.LefttoRight);     // Left -> Right
                checkBlock();
                return;
            }
        }
    }

    private void checkBlock(){
        for(int r = 0; r < BLOCK_ROW; r++){
            for(int c = 0; c < BLOCK_COL; c++){
                if(blockGrid[r, c] != correctblockGrid[r, c])
                    return;
            }
        }
        clearM1();
    }

    private void clearM1(){
        Debug.Log("ClearM1");
        ClearSpriteObject.SetActive(true);
        PuzzleClear();
    }

    public bool getismoving(){
        return ismoving;
    }
    public void setismoving(bool state){
        this.ismoving = state;
    }
}
