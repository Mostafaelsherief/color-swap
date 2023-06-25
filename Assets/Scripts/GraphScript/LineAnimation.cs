using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Color startColor = GetComponent<LineRenderer>().material.color;   
        Color endColor = GetComponent<LineRenderer>().material.color;
        endColor.a = 0;
        GetComponent<LineRenderer>().material.color = endColor;
        LeanTween.color(gameObject, startColor, 1f).setEaseInOutCirc();
    }
}
