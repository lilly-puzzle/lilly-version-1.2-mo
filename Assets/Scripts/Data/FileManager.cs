using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileManager
{
    public static bool WriteToFile(string fileName, string data) {
        bool result = true;
        string path = Application.dataPath + "/Resources/Data/Json/" + fileName;

        try {
            File.WriteAllText(path, data);
        }
        catch (Exception e) {
            Debug.LogError("Error: Fail to write json file\n" + e);
            result = false;
        }

        return result;
    }

    public static int LoadFromFile(string fileName, out string json) {
        int result = 1;
        string path = Application.dataPath + "/Resources/Data/Json/" + fileName;
        string a_json = "";

        try {
            a_json = File.ReadAllText(path);
        }
        catch (Exception e) {
            if (File.Exists(path)) {
                Debug.LogError("Error: Fail to read json file\n" + e);
                result = -1;
            } else {
                Debug.LogError("Error: No Json File");
                result = 0;
            }
        }

        json = a_json;

        return result;
    }
}
