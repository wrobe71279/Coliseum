using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalingButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    void OnMouseEnter()
    {
        Debug.Log("Hi");
        iTween.ScaleTo(gameObject, iTween.Hash(
            "x", 2, "y", 2, 
            "time", 1.2f, 
            "easeType", iTween.EaseType.easeOutElastic));
    }
    void OnMouseExit()
    {
        Debug.Log("Bye");
        iTween.ScaleTo(gameObject, new Vector3(1, 1, 0), 0.8f);
    }
}
