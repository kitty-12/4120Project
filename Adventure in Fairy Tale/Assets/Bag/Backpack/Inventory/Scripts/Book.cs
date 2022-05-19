using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Book : MonoBehaviour
{
    public Item bookItem;
    public Image bookImage;
    public Text bookNum;

    public void ItemOnClicked()
    {
        InventoryManager.UpdateItemInfo(bookItem.itemInfo);
    }
}
