using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class EnterPlotFinal : MonoBehaviour
{
    public Image img;
    public Sprite[] faceList;
    public string[] nameList;
    public int[] nameOrder;
    public TextMeshProUGUI nameText;
    public string sceneName;
    public TextMeshProUGUI textDisplay;
    [TextArea(1,3)] public string[] sentences;
    public float typeSpeed = 0.01f;
    public GameObject continueButton;
    public GameObject closeButton;
    public GameObject panel;
    public GameObject namePanel;

    public GameObject door;
    public int openId;
    private int id;
    private AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();

        panel.SetActive(true);
        closeButton.SetActive(false);
        continueButton.SetActive(false);
        FirstEnterChecker.Instance.AddRecord(sceneName);
        img.sprite = faceList[nameOrder[id]];
        if(nameList[nameOrder[id]] == "")
            namePanel.SetActive(false);
        else
            namePanel.SetActive(true);
        nameText.text = nameList[nameOrder[id]];
        StartCoroutine(Type());
        Button btn = (Button)continueButton.GetComponent<Button>();
        btn.onClick.AddListener(NextSentence);          
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
            img.sprite = faceList[nameOrder[id]];
            if(nameList[nameOrder[id]] == "")
                namePanel.SetActive(false);
            else
                namePanel.SetActive(true);
            nameText.text = nameList[nameOrder[id]];

            if(id == openId)
            {
                door.GetComponent<Animation>().Play("opendoor");
                textDisplay.text = sentences[id];
            }
            else    
                StartCoroutine(Type());
        } else {
            closeButton.SetActive(true);
            textDisplay.text = "";
            Button btn = (Button)continueButton.GetComponent<Button>();
            btn.onClick.RemoveAllListeners();
            panel.SetActive(false);       
        }
    }
}
