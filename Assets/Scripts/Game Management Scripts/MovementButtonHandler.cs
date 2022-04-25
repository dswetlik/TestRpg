using UnityEngine;
using UnityEngine.EventSystems;

public class MovementButtonHandler : MonoBehaviour, IUpdateSelectedHandler, IPointerDownHandler, IPointerUpHandler
{
    bool isPressed = false;

    enum MovementType
    {
        forward,
        backward,
        strafeRight,
        strafeLeft,
        turnRight,
        turnLeft
    }

    [SerializeField] MovementType movementType;

    public void OnUpdateSelected(BaseEventData data)
    {
        if (isPressed)
        {
            GameObject btn = data.selectedObject;
            switch (movementType)
            {
                case MovementType.forward:
                    GameObject.Find("Player").GetComponent<PlayerMovement>().w_MoveForward();
                    break;
                case MovementType.backward:
                    GameObject.Find("Player").GetComponent<PlayerMovement>().w_MoveBackward();
                    break;
                case MovementType.strafeLeft:
                    GameObject.Find("Player").GetComponent<PlayerMovement>().w_StrafeLeft();
                    break;
                case MovementType.strafeRight:
                    GameObject.Find("Player").GetComponent<PlayerMovement>().w_StrafeRight();
                    break;
                case MovementType.turnLeft:
                    GameObject.Find("Player").GetComponent<PlayerMovement>().w_TurnLeft();
                    break;
                case MovementType.turnRight:
                    GameObject.Find("Player").GetComponent<PlayerMovement>().w_TurnRight();
                    break;
            }
        }
    }

    public void OnPointerDown(PointerEventData data)
    {
        isPressed = true;
    }

    public void OnPointerUp(PointerEventData data)
    {
        isPressed = false;
    }

}
