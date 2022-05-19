using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{

    static InventoryManager instance;

    public Inventory myBag;
    public GameObject bookGrid;
    public Book bookPrefab;
    public Text itemInformation;

    void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
    }
    private void OnEnable(){
        RefreshItem();
        instance.itemInformation.text = "";
    }

    public static void UpdateItemInfo(string des){
        instance.itemInformation.text = des;
    }

    public static void CreateNewItem(Item item)
    {
        Book newItem = Instantiate(instance.bookPrefab, instance.bookGrid.transform.position, Quaternion.identity);
        newItem.gameObject.transform.SetParent(instance.bookGrid.transform);
        newItem.bookItem = item;
        newItem.bookImage.sprite = item.itemImage;
        newItem.bookNum.text = item.itemHeld.ToString();
    }

    public static void RefreshItem(){
        for(int i = 0; i < instance.bookGrid.transform.childCount; i++){
            if ( instance.bookGrid.transform.childCount == 0)
                break;
            Destroy(instance.bookGrid.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < instance.myBag.itemList.Count; i++){
            CreateNewItem( instance.myBag.itemList[i]);
        }
    }
}
