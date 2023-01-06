using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class JsonParse<T>
{
    public static bool Save(T data, string fileName)
    {
        string newText = JsonUtility.ToJson(data, true);
        string path = $"{Application.dataPath}/Resources/{fileName}.json";
        File.WriteAllText(path, newText);

        return true;
    }

    public static T Load(T dataType, string fileName)
    {
        TextAsset textAsset = Resources.Load<TextAsset>(fileName);

        return JsonUtility.FromJson<T>(textAsset.text.ToString());
    }
}
