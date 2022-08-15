using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DataFrame;
using SaveDataPerPuzzle.Floor1;

// Type for the shape of the bugs
public enum NF_Shapes
{
    // Triangle(1), Inverted Triangle(2), Rectangle(3), Circle(4), Sauce(5) Spider(6)
    None = 0,
    Tri,
    Inv,
    Rec,
    Cir,
    Sauce,
    Spider
};

public class NameFinding : PuzzleMainController
{
    [Header("Object Variables")]
    [SerializeField] private GameObject LocPrefab = null;
    [SerializeField] private GameObject BugPrefab = null;
    [SerializeField] private GameObject SpiderPrefab = null;
    [SerializeField] private GameObject ClearPrefab = null;

    [Header("Script Variables")]
    private LocManager[,] locs;
    private BugManager bug;
    private SpiderManager[,] spiders;
    private ClearManager[] clears;  // Start at [1], End [4]

    [Header("Constant & ReadOnly Variables")]
    private const int LADDER_ROW = 6;
    private const int LADDER_COL = 6;
    private const int BUG_NUM = 4;
    private readonly Vector2 start = new Vector2(-2.0f, 3.333344f);
    private readonly Vector2 end = new Vector2(2.0f,-3.333344f);
    private readonly int[,] spiderGrid = new int[LADDER_ROW + 5, LADDER_COL + 1] {     // 0 = None  1 = Spider
     {0, 0, 0, 0, 0, 0, 0},
     {0, 0, 0, 0, 0, 0, 0}, 
     {0, 1, 0, 0, 0, 0, 0}, 
     {0, 0, 0, 0, 0, 0, 0}, 
     {0, 1, 0, 1, 0, 0, 0}, 
     {0, 0, 0, 0, 0, 0, 0}, 
     {0, 0, 0, 0, 0, 1, 0}, 
     {0, 0, 0, 0, 0, 0, 0}, 
     {0, 0, 0, 1, 0, 1, 0}, 
     {0, 0, 0, 0, 0, 0, 0}, 
     {0, 0, 0, 0, 0, 0, 0}};

    // 1 = Tri   2 = Inv   3 = Rec   4 = Cir    -1 = None(wall)    
    private readonly int[] BUG_COMPLETE_CRITERIA = new int[LADDER_COL] {-1, 4, 1, 3, 2, -1};

    [Header("Variables")]
    // 0 = not installed, 1 = installed    -1 = None(wall)
    private int[,] hotsauceGrid = new int[LADDER_ROW, LADDER_COL] { 
     { -1, 0, 0, 0, 0, -1 },
     { -1, 0, 0, 0, 0, -1 },
     { -1, 0, 0, 0, 0, -1 },
     { -1, 0, 0, 0, 0, -1 },
     { -1, 0, 0, 0, 0, -1 },
     { -1, 0, 0, 0, 0, -1 } };

     // 0 = not complete, 1 = complete    -1 = None(wall)
    private int[] completeBugLoc = new int[LADDER_COL] { -1, 0, 0, 0, 0, -1 };
    // [1] Triangle [2] Inverse Triangle [3] Rectangle [4] Circle   0 == None, 1 == Complete
    private int[] completeBugShape = new int[5] { -1, 0, 0, 0, 0};

    //  -------------------------------- Setting Puzzle ---------------------------------------------------------    
    protected override void LoadEachPuzzleData() {
        NameFindingData saveData = PuzzleManager.instance.puzzleData.floor1Data.saveNameFinding;

        hotsauceGrid = saveData.hotsauceGrid;
        completeBugLoc = saveData.completeBugLoc;
        completeBugShape = saveData.completeBugShape;
    }
    
    public override void SaveEachPuzzleData(PuzzleData a_puzzleSaveData) {
        a_puzzleSaveData.floor1Data.saveNameFinding.hotsauceGrid = hotsauceGrid;
        a_puzzleSaveData.floor1Data.saveNameFinding.completeBugLoc = completeBugLoc;
        a_puzzleSaveData.floor1Data.saveNameFinding.completeBugShape = completeBugShape;
    }

    protected override void SetupPuz(){
        spidersInit();
        locsInit();     // if player touch loc with hotsauce, hotsauce installed in loc
        clearsInit();
    }

    private void spidersInit(){
        this.spiders = new SpiderManager[LADDER_ROW + 5, LADDER_COL + 1];
        for(int y = 0; y < LADDER_ROW + 5; y++){
            for(int x = 0; x < LADDER_COL + 1; x++){
                GameObject instantiatedObject = Instantiate(this.SpiderPrefab, this.transform);
                SpiderManager spider = instantiatedObject.GetComponent<SpiderManager>();
                this.spiders[y, x] = spider;
                spider.gridY = y;
                spider.gridX = x;
                spider.NFmanager = this;
                instantiatedObject.transform.position = calcSpiderPos(y, x);
                spider.name = "spider(" + spider.gridY.ToString() + ", " + spider.gridX.ToString() + ")";
                
                if(spiderGrid[y, x] == 1){
                    spider.setSpider();
                }
                else{
                    spider.setTransparent();
                }
            }
        }
    }

    private Vector3 calcSpiderPos(int y, int x){
        float ratioX = (x) / (float)(LADDER_COL);
        float sizeX = Mathf.Abs((float) (end.x - start.x));
        float spiderX = start.x + ratioX * sizeX;

        float ratioY = y / (float)(LADDER_ROW +4);
        float sizeY = Mathf.Abs((float)(end.y - start.y));
        float spiderY = start.y - ratioY * sizeY;

        return new Vector3(spiderX, spiderY, 0.0f);
    }

    private void locsInit(){
        this.locs = new LocManager[LADDER_ROW, LADDER_COL];
        for (int y = 0; y < LADDER_ROW; y++){
            for (int x = 1; x < LADDER_COL - 1; x++){
                GameObject instantiatedObject = Instantiate(this.LocPrefab, this.transform);
                LocManager loc = instantiatedObject.GetComponent<LocManager>();
                this.locs[y, x] = loc;
                loc.gridY = y;
                loc.gridX = x;
                loc.NFmanager = this;
                instantiatedObject.transform.position = calcLocPos(y, x);
                instantiatedObject.transform.localScale = loc.DEFAULT_SIZE;
                loc.name = "loc(" + loc.gridY.ToString() + ", " + loc.gridX.ToString() + ")";
                
                if(hotsauceGrid[y, x] == 1){
                    loc.setSauce(false);
                }
                else{
                    loc.setTransparent(false);
                }
                
            }
        }
    }

    private Vector3 calcLocPos(int y, int x){
        float ratioX = (x - 1) / (float)(LADDER_COL - 3);
        float sizeX = Mathf.Abs((float)(end.x - start.x));
        float posX = start.x + ratioX * sizeX;

        float ratioY = y / (float)(LADDER_ROW - 1);
        float sizeY = Mathf.Abs((float)(end.y - start.y));
        float posY = start.y - ratioY * sizeY;

        return new Vector3(posX, posY, 0.0f);
    }

     private void clearsInit(){
        this.clears = new ClearManager[LADDER_COL];
        for(int x = 1; x < LADDER_COL - 1; x++){
            GameObject instantiatedObject = Instantiate(this.ClearPrefab, this.transform);
            ClearManager clear = instantiatedObject.GetComponent<ClearManager>();
            this.clears[x] = clear;
            clear.gridX = x;
            clear.NFmanager = this;
            instantiatedObject.transform.position = calcLocPos(LADDER_ROW -1, x);
            clear.defaultPos = calcLocPos(LADDER_ROW -1, x);
            clear.name = "clear(" + x.ToString() + ")";

            clear.setSprite(BUG_COMPLETE_CRITERIA[x]*2);
            if(completeBugLoc[x] == 1){
                clear.setVisible();
            }
            else{
                clear.setTransparent();
            }
        }
    }

    // ------------------------------------ Interaction With Other Scripts-----------------------------------
    // Interaction With LocManager
    public void installSauce(int y, int x){
        hotsauceGrid[y, x] = 1;
    }
    public void removeSauce(int y, int x){
        hotsauceGrid[y, x] = 0;
    }

    // Interaction With LocManager & BugManager
    public void makeBug(int bugLoc, int bugShape){
        GameObject instantiatedObject = Instantiate(this.BugPrefab, this.transform);
        bug = instantiatedObject.GetComponent<BugManager>();
        bug.gridY = 0;
        bug.gridX = bugLoc;
        bug.NFmanager = this;
        instantiatedObject.transform.position = calcLocPos(0, bugLoc);
        bug.name = "bug";
        bug.setBug(bugShape);
    }

     public void navigateBug(int y, int x){
        if(y == 5){
            if(BUG_COMPLETE_CRITERIA[x] == bug.getBug()){
                bug.completeBug();
                // Need delete the bug item in map scene
                clears[x].clearAnimation();
            }
            else{
                bug.failBug(1);
            }
        }
        else{
            //hosauceGrid[y, x] = 0;의 경우 자연스러운 애니메이션을 위해 소스와 벌레의 충돌로 처리
            if (hotsauceGrid[y, x - 1] == 1 && hotsauceGrid[y, x + 1] == 1){
                bug.moveGrid(y + 1, x);
                bug.movePos(calcLocPos(y+1, x));
            }
            else if (hotsauceGrid[y, x - 1] == 1){
                bug.moveGrid(y, x - 1);
                bug.movePos(calcLocPos(y, x - 1));
            }
            else if (hotsauceGrid[y, x + 1] == 1){
                bug.moveGrid(y, x + 1);
                bug.movePos(calcLocPos(y, x + 1));
            }
            else{
                bug.moveGrid(y + 1, x);
                bug.movePos(calcLocPos(y + 1, x));
            }
        }
    }

    public void zoomTouch(int x){
        if(completeBugLoc[x] == 1){
            clears[x].zoomBugCrystal();
            completeBugShape[BUG_COMPLETE_CRITERIA[x]] = 1;
        }
    }
}
