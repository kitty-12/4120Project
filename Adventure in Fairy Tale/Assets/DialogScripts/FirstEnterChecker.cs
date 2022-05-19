using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstEnterChecker
{
    private static List<string> records = new List<string>();
    private static FirstEnterChecker _instance;
    public static FirstEnterChecker Instance
    {
        get
        {
            Debug.Log("Check whether is instance");
            if (_instance==null)
            {
                Debug.Log("create the instance");
                _instance = new FirstEnterChecker();
            }
            return _instance;
        }
    }

    public void AddRecord(string scene)
    {
        records.Add(scene);
    }
    public bool isFirst(string scene){
        return !(records.Contains(scene));
    }
}
