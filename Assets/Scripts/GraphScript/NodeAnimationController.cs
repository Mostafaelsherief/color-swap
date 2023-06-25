using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeAnimationController : MonoBehaviour
{
    bool isEnlarged;
    Vector3 _startScale;
    void Start()
    {
        _startScale = transform.localScale;
        StartFadeInAnimation();
        InputEventsHandler.instance.onDragEnded += EndNodeDragAnimation;
    }
    public void StartNodeDragAnimation()
    {
        isEnlarged = true;
        LeanTween.scale(gameObject, _startScale * 1.4f, 0.2f).setEaseOutCubic();
    }
    public void EndNodeDragAnimation()
    {
        if (isEnlarged)
        {
            LeanTween.scale(gameObject, _startScale, 0.2f).setEaseOutCubic();
            isEnlarged = false;
        }
    }
    void StartFadeInAnimation()
    {
        Color startColor = transform.GetComponent<SpriteRenderer>().color;
        Color currentColor = transform.GetComponent<SpriteRenderer>().color;
        currentColor.a = 0;
        transform.GetComponent<SpriteRenderer>().color = currentColor;
        LeanTween.color(gameObject, startColor, 1f).setEaseInOutCirc();
    }
}
