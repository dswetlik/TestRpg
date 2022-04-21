﻿ using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] bool isMoving = false;
    [SerializeField] Coroutine currentMovement = null;

    [SerializeField] CharacterController characterController;
    [SerializeField] float movementSpeed;
    [SerializeField] float rotationSpeed;

    bool shouldMove = false;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    public void w_MoveForward()
    {
        if (!isMoving)
            currentMovement = StartCoroutine("MoveForward");
    }

    public void w_MoveBackward()
    {
        if (!isMoving)
            currentMovement = StartCoroutine("MoveBackward");
    }

    public void w_StrafeRight()
    {
        if (!isMoving)
            currentMovement = StartCoroutine("StrafeRight");
    }

    public void w_StrafeLeft()
    {
        if (!isMoving)
            currentMovement = StartCoroutine("StrafeLeft");
    }

    public void w_TurnRight()
    {
        if (!isMoving)
            currentMovement = StartCoroutine("TurnRight");
    }

    public void w_TurnLeft()
    {
        if (!isMoving)
            currentMovement = StartCoroutine("TurnLeft");
    }

    IEnumerator MoveForward()
    {
        isMoving = true;

        Vector3 target = transform.position + (transform.forward * 10);
        Vector3 offset = target - transform.position;
        CollisionFlags collisionFlags = CollisionFlags.None;
        AudioSource walk = GameObject.Find("FootstepAudioSource").GetComponent<AudioSource>();

        while (true)
        {
            offset = target - transform.position;

            if (!walk.isPlaying)
                walk.Play();

            if (offset.magnitude > 0.1f)
            {
                offset = offset.normalized * movementSpeed;
                collisionFlags = characterController.Move(offset * Time.fixedDeltaTime);
            }
            else
                break;

            if ((collisionFlags & CollisionFlags.Sides) != 0)
            {
                break;
            }

            yield return new WaitForFixedUpdate();
        }

        transform.position = new Vector3(Mathf.Round(transform.position.x / 10) * 10, transform.position.y, Mathf.Round(transform.position.z / 10) * 10);

        isMoving = false;
    }

    IEnumerator MoveBackward()
    {
        isMoving = true;

        Vector3 target = transform.position + (-transform.forward * 10);
        Vector3 offset = target - transform.position;
        CollisionFlags collisionFlags = CollisionFlags.None;
        AudioSource walk = GameObject.Find("FootstepAudioSource").GetComponent<AudioSource>();

        while (true)
        {
            offset = target - transform.position;

            if (!walk.isPlaying)
                walk.Play();

            if (offset.magnitude > 0.1f)
            {
                offset = offset.normalized * movementSpeed;
                collisionFlags = characterController.Move(offset * Time.fixedDeltaTime);
            }
            else
                break;

            if ((collisionFlags & CollisionFlags.Sides) != 0)
            {
                break;
            }

            yield return new WaitForFixedUpdate();
        }

        transform.position = new Vector3(Mathf.Round(transform.position.x / 10) * 10, transform.position.y, Mathf.Round(transform.position.z / 10) * 10);

        isMoving = false;
    }

    IEnumerator StrafeRight()
    {
        isMoving = true;

        Vector3 target = transform.position + (transform.right * 10);
        Vector3 offset = target - transform.position;
        CollisionFlags collisionFlags = CollisionFlags.None;
        AudioSource walk = GameObject.Find("FootstepAudioSource").GetComponent<AudioSource>();

        while (true)
        {
            offset = target - transform.position;

            if (!walk.isPlaying)
                walk.Play();

            if (offset.magnitude > 0.1f)
            {
                offset = offset.normalized * movementSpeed;
                collisionFlags = characterController.Move(offset * Time.fixedDeltaTime);
            }
            else
                break;

            if ((collisionFlags & CollisionFlags.Sides) != 0)
            {
                break;
            }

            yield return new WaitForFixedUpdate();
        }

        transform.position = new Vector3(Mathf.Round(transform.position.x / 10) * 10, transform.position.y, Mathf.Round(transform.position.z / 10) * 10);

        isMoving = false;
    }

    IEnumerator StrafeLeft()
    {
        isMoving = true;

        Vector3 target = transform.position + (-transform.right * 10);
        Vector3 offset = target - transform.position;
        CollisionFlags collisionFlags = CollisionFlags.None;
        AudioSource walk = GameObject.Find("FootstepAudioSource").GetComponent<AudioSource>();

        while (true)
        {
            offset = target - transform.position;

            if (!walk.isPlaying)
                walk.Play();

            if (offset.magnitude > 0.1f)
            {
                offset = offset.normalized * movementSpeed;
                collisionFlags = characterController.Move(offset * Time.fixedDeltaTime);
            }
            else
                break;

            if ((collisionFlags & CollisionFlags.Sides) != 0)
            {
                break;
            }

            yield return new WaitForFixedUpdate();
        }

        transform.position = new Vector3(Mathf.Round(transform.position.x / 10) * 10, transform.position.y, Mathf.Round(transform.position.z / 10) * 10);

        isMoving = false;
    }

    IEnumerator TurnRight()
    {
        isMoving = true;

        Vector3 currentAngle = transform.eulerAngles;
        Vector3 targetAngle = new Vector3(0f, transform.eulerAngles.y + 90f, 0);
        float t = 0f;

        while (t < 1)
        {
            t += Time.deltaTime / rotationSpeed;
            currentAngle = new Vector3(0f, Mathf.Lerp(currentAngle.y, targetAngle.y, t), 0f);
            transform.eulerAngles = currentAngle;
            yield return null;
        }

        isMoving = false;
    }

    IEnumerator TurnLeft()
    {
        isMoving = true;

        Vector3 currentAngle = transform.eulerAngles;
        Vector3 targetAngle = new Vector3(0f, transform.eulerAngles.y - 90f, 0);
        float t = 0f;

        while (t < 1)
        {
            t += Time.deltaTime / rotationSpeed;
            currentAngle = new Vector3(0f, Mathf.Lerp(currentAngle.y, targetAngle.y, t), 0f);
            transform.eulerAngles = currentAngle;
            yield return null;
        }

        isMoving = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Impassable")
        {
            if (currentMovement != null) { StopCoroutine(currentMovement); isMoving = false; }
            transform.position = new Vector3(Mathf.Round(transform.position.x / 10) * 10, 2.2f, Mathf.Round(transform.position.z / 10) * 10);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "ScenePortal")
        {
            if (currentMovement != null) { StopCoroutine(currentMovement); currentMovement = null; isMoving = false; }
            transform.position = new Vector3(Mathf.Round(transform.position.x / 10) * 10, 2.2f, Mathf.Round(transform.position.z / 10) * 10);
            GameObject.Find("GameManager").GetComponent<Engine>().ChangeScene(other.gameObject.GetComponent<SceneContainer>().GetLocation());
        }
        else if(other.tag == "EventCollider")
        {
            if (other.GetComponent<EventContainer>().GetEvent().GetEventType() == Event.EventType.start)
            {
                StartEvent startEvent = EventManager.EventDictionary[(other.GetComponent<EventContainer>().GetEvent() as StartEvent).GetID()];
                if (!startEvent.IsComplete())
                    GameObject.Find("GameManager").GetComponent<EventManager>().ActivateEventScreen(true, startEvent);
            }
            else
                Debug.LogErrorFormat("EventCollider is not StartEvent; Event: {0}", (other.GetComponent<EventContainer>().GetEvent() as StartEvent).GetName());
        }
    }
     
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "LocationPortal")
        {
            if (other.name == "ArenaPortal")
            {
                GameObject.Find("GameManager").GetComponent<Engine>().ActivateArenaScreen(true);
            }
            else if (other.name == "DungeonPortal")
            {

            }

        }
        /*
        else if (other.transform.parent.tag == "Chest")
        {
          //  GameObject.Find("GameManager").GetComponent<Engine>().OpenChest(other.GetComponentInParent<ChestInventory>(), false);
        }
        */
    }
}
