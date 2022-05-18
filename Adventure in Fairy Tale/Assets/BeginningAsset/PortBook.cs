using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using UnityEngine.SceneManagement;

public class PortBook : MonoBehaviour
{
    public GameObject tip;
    public float range = 5.0f;
    //public GameObject dialogue;
    public TextMeshProUGUI textDisplay;
    public GameObject panel;
    public GameObject continueButton;
    public string sentence;
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
        if(Vector3.SqrMagnitude(direction)< range)
            tip.SetActive(true);
        else
            tip.SetActive(false);

        if(textDisplay.text == sentence)
            SceneManager.LoadScene("Town");
    }

    IEnumerator Type(){
        foreach( char letter in sentence.ToCharArray()) {
            textDisplay.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
    }

    void OnMouseDown() {
        Debug.Log("Click on object!");
        continueButton.SetActive(false);
        if(tip.activeSelf && (!panel.activeSelf))
        {
            panel.SetActive(true);
            StartCoroutine(Type());
        }
    }
}
