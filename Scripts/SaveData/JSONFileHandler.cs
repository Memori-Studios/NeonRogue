using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public static class JSONFileHandler 
{
    public static void SaveToJSON<T> (T toSave, string filename) {
        string content = JsonUtility.ToJson (toSave);
        // Debug.Log($"GetPath (filename) {GetPath (filename)}");
        WriteFile (GetPath (filename), content);
    }
    public static T ReadListFromJSON<T> (string filename) {
        string content = ReadFile (GetPath (filename));

        return JsonUtility.FromJson<T>(content);
    }
    private static string GetPath (string filename) {
        return Application.persistentDataPath + "/" + filename;
    }
    private static void WriteFile (string path, string content) {
        FileStream fileStream = new FileStream (path, FileMode.Create);

        using (StreamWriter writer = new StreamWriter (fileStream)) {
            writer.Write (content);
        }
    }
    private static string ReadFile (string path) {
        if (File.Exists (path)) {
            using (StreamReader reader = new StreamReader (path)) {
                string content = reader.ReadToEnd ();
                return content;
            }
        }
        return "";
    }
}
