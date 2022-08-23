using System.Collections;
using System.Collections.Generic;

namespace SaveDataPerPuzzle
{
    namespace Floor1
    {
        [System.Serializable] public class MainPuzzle1Data{
            public int[,] blockGrid;

            public MainPuzzle1Data(){
                blockGrid = new int[,] {
                    {  1,  2,  3,  4 },
                    {  5,  6,  7,  8 },
                    {  9, 10, 11, 12 },
                    { 13, 14,  0, 15 }
                };
            }
        }

        [System.Serializable] public class LPposeData {
            public int installedLP;     // 0 is None, 1 ~ 4 LP
            public float[] DollRigging;    // [0] Left Shoulder, [1] Left Elbow,  [2] RS, [3] RE, [4] Left Hip Joint [5] Left Knee [6] Right Hip Joint [7] Right Knee
            public int[] DollRiggingDir;      // 1 == + , -1 == -
            public float[] LPSoundProgress; // [0] is None, [1] ~ [4] LP
            public bool isCorrect;
            public bool isDollExit;
            public bool isNeedleDown;
            public LPposeData() {
                installedLP = 0;
                DollRigging = new float[] {145.0f, 270.0f, 140.0f, 280.0f, 105.0f, 105.0f, 105.0f, 90.0f}; // need Value Modification
                DollRiggingDir = new int[] {-1, 1, -1, 1, 1, -1, 1, -1};
                LPSoundProgress = new float[] {0.0f, 0.0f, 0.0f, 0.0f};
                isCorrect = false;
                isDollExit = true;
                isNeedleDown = false;
            }
        }

        [System.Serializable] public class NameFindingData {
            public int[,] hotsauceGrid;
            public int[] completeBugLoc;
            public int[] completeBugShape;
            public NameFindingData(){
                // 0 = not installed, 1 = installed    -1 = None(wall)
                hotsauceGrid = new int[,] {
                    { -1, 0, 0, 0, 0, -1 },
                    { -1, 0, 0, 0, 0, -1 },
                    { -1, 0, 0, 0, 0, -1 },
                    { -1, 0, 0, 0, 0, -1 },
                    { -1, 0, 0, 0, 0, -1 },
                    { -1, 0, 0, 0, 0, -1 } 
                };
                // 0 = not complete, 1 = complete    -1 = None(wall)
                completeBugLoc = new int[] { -1, 0, 0, 0, 0, -1 };
                // [1] Triangle [2] Inverse Triangle [3] Rectangle [4] Circle   0 == None, 1 == Complete
                completeBugShape = new int[5] { -1, 0, 0, 0, 0};
            }
        }

        [System.Serializable] public class MouseHoleData{
            public int mhCatCondition;
            public int mhMouseCondition;
            public int mhPlateCondition;

            public MouseHoleData(){
                mhCatCondition = 0;
                mhMouseCondition = 0;
                mhPlateCondition = 0;
            }
        }
    }

    namespace Floor2
    {

    }

    namespace Floor3
    {

    }

    namespace Floor4
    {
        
    }
}
