using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateClueNum
{
    private int clueNum = 0;
    private Dictionary<string, int> clues = new Dictionary<string, int>();
    private int totalNum = 7;
    private static UpdateClueNum _instance;
    public static UpdateClueNum Instance
    {
        get
        {
            Debug.Log("Check whether is instance");
            if (_instance==null)
            {
                Debug.Log("create the instance");
                _instance = new UpdateClueNum();
            }
            return _instance;
        }
    }
 
    public void Increace(string name)
    {
        clueNum++;
        clues.Add(name, 1);
        Debug.Log("Current clue found="+ clueNum);
    }

    public int GetNum()
    {
        Debug.Log("Get clues="+ clueNum);
        return clueNum;
    }

    public bool IsFound(string name)
    {
        Debug.Log(clues.Keys);
        return clues.ContainsKey(name);
    }

    public bool IsAllFound()
    {
        return (clueNum == totalNum);
    }

    public string GetProgress()
    {
        string res = " ("+clueNum+"/"+totalNum+")";
        return res;
    }
}