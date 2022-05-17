using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class Dialog : MonoBehaviour
{
    public bool needClick = true;
    public TextMeshProUGUI textDisplay;
    [TextArea(1,3)] public string[] sentences;
    public float typeSpeed = 0.02f;
    public GameObject continueButton;
    public GameObject closeButton;
    public GameObject panel;
    private int id;

    private AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        if(!needClick)
        {
            StartCoroutine(Type());
            Button btn = (Button)continueButton.GetComponent<Button>();
            btn.onClick.AddListener(NextSentence);
        }     
            
    }

    // Update is called once per frame
    void Update()
    {
        if(textDisplay.text == sentences[id]){
            continueButton.SetActive(true);
        }
    }

    IEnumerator Type(){
        foreach( char letter in sentences[id].ToCharArray()) {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typeSpeed);
        }
    }

    public void NextSentence(){
        source.Play();
        continueButton.SetActive(false);
        Debug.Log("Continue!");
        if(id < sentences.Length - 1) {
            id++;
            textDisplay.text = "";
            StartCoroutine(Type());
        } else {
            textDisplay.text = "";
            Button btn = (Button)continueButton.GetComponent<Button>();
            btn.onClick.RemoveAllListeners();
            panel.SetActive(false);
        }
    }

    public void ClosePanel(){
        source.Play();
        Button btn = (Button)continueButton.GetComponent<Button>();
        btn.onClick.RemoveAllListeners();
        panel.SetActive(false);
        id = 0;
    }

    public void restart()
    {
        id = 0;
        textDisplay.text = "";
        //Debug.Log(sentences[1]);
        panel.SetActive(true);
        StartCoroutine(Type());
        Button btn = (Button)continueButton.GetComponent<Button>();
        btn.onClick.AddListener(NextSentence);
    }
}
