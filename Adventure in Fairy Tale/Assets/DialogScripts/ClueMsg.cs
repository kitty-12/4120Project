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
        if((!isFound) && (!UpdateClueNum.Instance.IsFound(name)))
        {
            Debug.Log("UpdateClueNum.Instance.IsFound: "+ UpdateClueNum.Instance.IsFound(name));
            panel.SetActive(true);
            UpdateClueNum.Instance.Increace(name);
            textDisplay.text += message;
            textDisplay.text += UpdateClueNum.Instance.GetProgress();
            isFound = true;
            Invoke("Disappear", 2.0f);
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
        textDisplay.text = "";
        panel.SetActive(false);
    }
}
