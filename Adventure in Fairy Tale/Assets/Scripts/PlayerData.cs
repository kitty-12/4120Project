using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance {get;private set;}
    public int Level {get;set;}
    public float curHealth {get; set;}
    public float maxHealth {get;set;}

    public int HerbNum {get;set;}
    public int Money {get;set;}

    public int exp {get;set;}

    public int next_exp {get;set;}

    // Start is called before the first frame update
    void Awake()
    {
        Level = 5;
        curHealth = 100;
        maxHealth = 100;
        HerbNum = 0;
        Money = 500;
        exp = 0;
        next_exp = 500;
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
