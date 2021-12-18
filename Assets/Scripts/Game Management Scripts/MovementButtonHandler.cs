using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovementButtonHandler : MonoBehaviour, IUpdateSelectedHandler, IPointerDownHandler, IPointerUpHandler
{
    bool isPressed = false;

    public void OnUpdateSelected(BaseEventData data)
    {
        if (isPressed)
        {
            Debug.Log("Updated Object");
            GameObject btn = data.selectedObject;
            if (btn.name == "NorthBtn")
                GameObject.Find("Player").GetComponent<PlayerMovement>().w_MoveForward();
            else if (btn.name == "EastBtn")
                GameObject.Find("Player").GetComponent<PlayerMovement>().w_TurnRight();
            else if (btn.name == "SouthBtn")
                GameObject.Find("Player").GetComponent<PlayerMovement>().w_MoveBackward();
            else if (btn.name == "WestBtn")
                GameObject.Find("Player").GetComponent<PlayerMovement>().w_TurnLeft();
        }
    }

    public void OnPointerDown(PointerEventData data)
    {
        Debug.Log("Pointer Down");
        isPressed = true;
    }

    public void OnPointerUp(PointerEventData data)
    {
        Debug.Log("Pointer Up");
        isPressed = false;
    }

}
