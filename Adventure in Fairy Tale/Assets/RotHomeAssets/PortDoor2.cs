using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using UnityEngine.SceneManagement;

public class PortDoor2 : MonoBehaviour
{
    public GameObject tip;
    public float range = 5.0f;
    //public GameObject dialogue;
    public GameObject player;
    private bool mouseOver;
    // Start is called before the first frame update
    void Start()
    {
        tip.SetActive(false);
        // player = GameObject.FindGameObjectWithTag("Player");
        mouseOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.transform.position - transform.position;
        if(Vector3.SqrMagnitude(direction)< range || mouseOver)
            tip.SetActive(true);
        else
            tip.SetActive(false);
    }

    void OnMouseOver() {
        //Debug.Log("Mouse is over!");
        mouseOver = true;
    }

    void OnMouseExit() {
        mouseOver = false;
    }

    void OnMouseDown() {
        Debug.Log("Change Scene");
        if(tip.activeSelf)
        {
            SceneManager.LoadScene("Forest");
        }
    }
}