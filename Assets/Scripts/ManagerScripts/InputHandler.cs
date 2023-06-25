using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{

    //for PC
    bool isdragged;
    bool lastDragCondition;

    Vector3 pressPosition;
    private void Update()
    {
        HandleInputPC();
        HandleInput();
    }
    void HandleInputPC()
    {
        if (Input.GetMouseButton(0))
        {
            pressPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pressPosition.z = 0;
            if (!isdragged)
            {
                isdragged = true;
                InputEventsHandler.instance.StartDrag(pressPosition);
            }
            else
            {
                InputEventsHandler.instance.ChangeDragPosition(pressPosition);
            }
        }
        else
        {
            isdragged = false;
        }
        if (isdragged != lastDragCondition && !isdragged)
        {
            InputEventsHandler.instance.EndDrag();
        }
        lastDragCondition = isdragged;
    }
    void HandleInput()
    {

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            pressPosition = Camera.main.ScreenToWorldPoint(touch.position);
            pressPosition.z = 0;
            if (touch.phase == TouchPhase.Began)
            {
              InputEventsHandler.instance.StartDrag(pressPosition);
            }
            else if (touch.phase != TouchPhase.Ended)
            {
              InputEventsHandler.instance.ChangeDragPosition(pressPosition);
            }
            else
            {
              InputEventsHandler.instance.EndDrag();
            }
        }

    }


}

