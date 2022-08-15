using System.Collections;
using System.Collections.Generic;

namespace SaveDataPerPuzzle
{
    namespace Floor1
    {
        [System.Serializable] public class PlainHangerData {
            public List<List<int>> hangingClothesCode;
            public bool hasPaper;

            public PlainHangerData() {
                hangingClothesCode = new List<List<int>> {
                    new List<int> { },
                    new List<int> { -1, -1, -1, -1 },
                    new List<int> { -1, -1, -1, -1 }
                };
                hasPaper = true;
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
