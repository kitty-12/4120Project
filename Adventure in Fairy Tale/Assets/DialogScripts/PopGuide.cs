using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopGuide : MonoBehaviour
{
    public GameObject tip;
    public float range = 5.0f;
    public GameObject dialogue;

    //public GameObject panel;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        tip.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.transform.position - transform.position;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction.normalized,out hit, range))
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                tip.SetActive(true);
                //Debug.Log("Player Near");
            }
            else
            {
                tip.SetActive(false);
                //Debug.Log("Too Far");
            }
        }
    }

    void OnMouseDown() {
        if(dialogue != null)
        {
            dialogue.GetComponent<Dialog>().restart();
            Debug.Log("Click on object!");
        }
    }
}
