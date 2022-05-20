using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;


public class Msg : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI textDisplay;
    [TextArea(1,3)] public string message;
    public GameObject panel;
    public float stayTime = 3.0f;
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
        
        
        panel.SetActive(true);
        textDisplay.text = "Tip: ";
        textDisplay.text += message;

        Invoke("Disappear", stayTime);
    }

    void Disappear()
    {
        panel.SetActive(false);
    }
}