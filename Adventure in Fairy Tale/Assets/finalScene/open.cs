using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class open : MonoBehaviour
{
    void OnTriggerEnter(Collider coll)
    {
        gameObject.GetComponent<Animation>().Play("opendoor");
    }
}
