using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //UpdateClueNum.Instance.Increace();
        }
        if (Input.GetMouseButtonDown(1))
        {
            SceneManager.LoadScene(
                SceneManager.GetActiveScene().buildIndex.Equals(3) ? 1 : 3);
            Debug.Log("Change scence");
            UpdateClueNum.Instance.GetNum();
        }
    }
}
