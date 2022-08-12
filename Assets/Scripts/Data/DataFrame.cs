using System.Collections.Generic;

using PuzzleDataByFloor;

namespace DataFrame
{
    // setting data
    [System.Serializable] public class SoundSetting {
        public bool isMute;
        public int masterVolume;
        public int bgmVolume;
        public int effectVolume;

        public SoundSetting() {
            isMute = false;
            masterVolume = 50;
            bgmVolume = 50;
            effectVolume = 50;
        }
    }

    [System.Serializable] public class SettingData {
        public SoundSetting soundSetting;

        public SettingData() {
            soundSetting = new SoundSetting();
        }
    }

    // character data
    [System.Serializable] public class CharacterPos {
        public int curFloor;
        public float curPosX;

        public CharacterPos() {
            curFloor = 1;
            curPosX = 0.0f;
        }
    }

    [System.Serializable] public class CharacterData {
        public CharacterPos characterPos;
        public List<int> playerInventory;

        public CharacterData() {
            characterPos = new CharacterPos();
            playerInventory = new List<int>();
        }
    }

    // play data
    [System.Serializable] public class PuzzleData {
        public Floor1 floor1Data;
        public Floor2 floor2Data;
        public Floor3 floor3Data;
        public Floor4 floor4Data;

        public PuzzleData() {
            floor1Data = new Floor1();
            floor2Data = new Floor2();
            floor3Data = new Floor3();
            floor4Data = new Floor4();
        }
    }

    [System.Serializable] public class PlayData {
        public int[,] activeObject;
        public int[] clearedPuzzle;
        public PuzzleData puzzleData;

        public PlayData() {
            activeObject = new int[DefaultData.NUM_OF_FLOOR + 1, DefaultData.MAX_NUM_OF_PUZ_PER_FLOOR + 1];
            clearedPuzzle = new int[DefaultData.NUM_OF_FLOOR + 1];
            puzzleData = new PuzzleData();
        }
    }
}