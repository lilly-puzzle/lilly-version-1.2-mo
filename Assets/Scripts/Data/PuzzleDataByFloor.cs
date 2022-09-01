using System.Collections;
using System.Collections.Generic;

using SaveDataPerPuzzle.Floor1;
using SaveDataPerPuzzle.Floor2;
using SaveDataPerPuzzle.Floor3;
using SaveDataPerPuzzle.Floor4;

namespace PuzzleDataByFloor
{
    [System.Serializable] public class Floor1 {
        public PlainHangerData savePlainHanger;
        public TVfrequencyData saveTVfrequency;

        public Floor1() {
            savePlainHanger = new PlainHangerData();
            saveTVfrequency = new TVfrequencyData();
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
