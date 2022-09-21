using System.Collections.Generic;

public class DefaultData
{
    public const int NUM_OF_FLOOR = 4;
    public const int MAX_NUM_OF_PUZ_PER_FLOOR = 16;
    public const int MAX_NUM_OF_ITEMS_PER_PUZ = 32;
    public const int MAP_SCENE_IDX_NUM = 2;

    public const int SIZE_OF_INVENTORY = 24;
    public static readonly List<int> NUM_OF_INVENTORY_SLOT = new List<int> { 6, 4 };

    public static readonly List<List<int>> ZOOM_OR_NOT = new List<List<int>>
    {
        new List<int> { },
        new List<int> { 0, 2, 0, 4, 0, 0, 0, 0 }
    };
}