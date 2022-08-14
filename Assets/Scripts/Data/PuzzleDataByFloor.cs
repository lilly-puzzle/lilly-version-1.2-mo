using System.Collections;
using System.Collections.Generic;

using SaveDataPerPuzzle.Floor1;
using SaveDataPerPuzzle.Floor2;
using SaveDataPerPuzzle.Floor3;
using SaveDataPerPuzzle.Floor4;

namespace PuzzleDataByFloor
{
    [System.Serializable] public class Floor1 {
        public MainPuzzle1Data saveMainPuzzle1;
        public LPposeData saveLPpose;
        public Floor1() {
            saveMainPuzzle1 = new MainPuzzle1Data();
            saveLPpose = new LPposeData();
        }
    }

    [System.Serializable] public class Floor2 {
        public Floor2() {
            
        }
    }
    
    [System.Serializable] public class Floor3 {
        public Floor3() {
            
        }
    }
    
    [System.Serializable] public class Floor4 {
        public Floor4() {
            
        }
    }
}
