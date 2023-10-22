
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;




//followed a youtube video for this code: https://www.youtube.com/watch?v=KZft1p8t2lQ
public static class FileHandler
{
    //other scripts will call this method to save data - LoginManager
    public static void SaveToJSON<T>(T[] toSave, string filename)
    {
        Debug.Log(GetPath(filename));
        string content = JsonHelper.ToJson<T>(toSave);
        WriteFile(GetPath(filename), content);
    }

    public static T[] ReadFromJSON<T>(string filename)
    {
        string content = ReadFile(GetPath(filename));

        if (string.IsNullOrEmpty(content))
        {
            return null;
        }
        else
        {
            T[] res = JsonHelper.FromJson<T>(content);
            return res;
        }

        //Note to MR B - I tried to implement a try catch and excetion handling here but in order to do that I need to download a JSON package but unity is a bit weird with that and it didnt let the package work


    }

    private static string GetPath(string filename)
    {
        return Application.persistentDataPath + "/" + filename;
    }

    private static void WriteFile(string path, string content)
    {
        FileStream fileStream = new FileStream(path, FileMode.Create);

        using (StreamWriter writer = new StreamWriter(fileStream)) 
        { 
            writer.Write(content); 
        }
    }

    private static string ReadFile(string path)
    {
        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string content = reader.ReadToEnd();
                return content;
            }
        }
        return null;
    }
}


//this code is from stack overflow: https://stackoverflow.com/questions/36239705/serialize-and-deserialize-json-and-json-array-in-unity
public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}
