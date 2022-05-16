using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
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
        StartCoroutine(Type());
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
        if(id < sentences.Length - 1) {
            id++;
            textDisplay.text = "";
            StartCoroutine(Type());
        } else {
            textDisplay.text = "";
            panel.SetActive(false);
            continueButton.SetActive(false);
        }
    }

    public void ClosePanel(){
        panel.SetActive(false);
    }
}
