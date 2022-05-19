using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueCounter
{
    private int clueNum = 0;
    private Dictionary<string, int> clues = new Dictionary<string, int>();
    private int totalNum = 7;
    private static ClueCounter _instance;
    public static ClueCounter Instance
    {
        get
        {
            Debug.Log("Check whether is instance");
            if (_instance==null)
            {
                Debug.Log("create the instance");
                _instance = new ClueCounter();
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
        double res = 100.0 * clueNum / totalNum;
        //string res = " ("+clueNum+" / "+totalNum+" )";
        return " "+ res.ToString("0.00")+"%";
    }
}