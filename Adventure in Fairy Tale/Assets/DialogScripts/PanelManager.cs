using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class PanelManager : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public GameObject panel;
    public GameObject continueButton;
    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        if(textDisplay.text == "")
            panel.SetActive(false);     
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClosePanel(){
        source.Play();
        Button btn = (Button)continueButton.GetComponent<Button>();
        btn.onClick.RemoveAllListeners();
        panel.SetActive(false);
    }
}
