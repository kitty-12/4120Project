using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour
{
    // Rigidbody2D rb;
    // Collider2D coll;
    // Animator anim;
    public GameObject myBag;
    bool isBagOpen;
  

    private void Awake()
    {
        // rb = GetComponent<Rigidbody2D>();
        // coll = GetComponent<Collider2D>();
        // anim = GetComponent<Animator>();
    }

    private void Update()
    {
        OpenMyBag();
    }

    void OpenMyBag()
    {
        if(Input.GetKeyDown(KeyCode.O)){
            isBagOpen = !isBagOpen;
            myBag.SetActive(isBagOpen);
        }
    }
}