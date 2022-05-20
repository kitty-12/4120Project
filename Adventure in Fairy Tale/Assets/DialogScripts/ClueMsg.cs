using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;


public class ClueMsg : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI textDisplay;
    [TextArea(1,3)] public string message;
    public GameObject panel;
    public string clueName;
    public float stayTime = 3.0f;

    // Item
    public Item thisItem;
    public Inventory playerInventory;


    private bool isFound = false;
    private AudioSource source;
    void Start()
    {
        source=GetComponent<AudioSource>();
        panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Appear()
    {
        source.Play();
        if((!isFound) && (!ClueCounter.Instance.IsFound(name)))
        {
            Debug.Log("UpdateClueNum.Instance.IsFound: "+ ClueCounter.Instance.IsFound(name));
            panel.SetActive(true);
            ClueCounter.Instance.Increace(name);
            textDisplay.text = "Find CLUE: ";
            textDisplay.text += message;
            textDisplay.text += ClueCounter.Instance.GetProgressStr();
            isFound = true;
            Invoke("Disappear", stayTime);
            AddNewItem(); 
            if(ClueCounter.Instance.IsAllFound())
            {
                PlayerData.Instance.AllCollected = true;
            }
        }
        else
        {
            panel.SetActive(true);
            textDisplay.text = "This clue has been found!";
            isFound = true;
            Invoke("Disappear", 2.0f);
        }
    }

    void Disappear()
    {
        //textDisplay.text = "";
        panel.SetActive(false);
    }

    public void AddNewItem(){
        if (!playerInventory.itemList.Contains(thisItem))
        {
            playerInventory.itemList.Add(thisItem);
            InventoryManager.CreateNewItem(thisItem);
        }
        else
        {
            thisItem.itemHeld += 1;
        }
        InventoryManager.RefreshItem();
    }
}
