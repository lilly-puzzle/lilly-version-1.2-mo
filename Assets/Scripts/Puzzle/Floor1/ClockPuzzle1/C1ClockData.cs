using System.Collections.Generic;

namespace C1ClockData
{
    public class CLOCK_DATA {
        public static readonly List<int> NUM_OF_IDX_LIST = new List<int> {
            4, 2, 12
        };

        public static readonly List<int> ANSWER_IDX_LIST = new List<int> {
            2, 1, 6
        };

        public static readonly List<List<float>> ROTATION_ANGLE_LIST = new List<List<float>> {
            new List<float> { -45f, -135f, -225f, -315f },
            new List<float> { 0f, -180f },
            new List<float> { 0f, -30f, -60f, -90f, -120f, -150f, -180f, -210f, -240f, -270f, -300f, -330f},
        };
    }
}