using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class JsonParse<T>
{
    public static bool Save(T data, string fileName)
    {
        //try
        {
            string newText = JsonUtility.ToJson(data);
            string path = $"{Application.dataPath}/{fileName}.json";
            File.WriteAllText(path, newText);

            return true;
        }
        //catch
        //{
        //    return false;
        //}
    }

    public static T Load(T dataType, string fileName)
    {
        TextAsset textAsset = Resources.Load<TextAsset>(fileName);

        return JsonUtility.FromJson<T>(textAsset.text);
    }
}
