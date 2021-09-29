 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] bool isMoving = false;
    [SerializeField] Coroutine currentMovement = null;

    [SerializeField] CharacterController characterController;
    [SerializeField] float movementSpeed;
    [SerializeField] float rotationSpeed;

    // Update is called once per frame
    void Update()
    {
        characterController = GetComponent<CharacterController>();
    }

    public void w_MoveForward()
    {
        if(!isMoving)
            currentMovement = StartCoroutine("MoveForward");
    }

    public void w_TurnRight()
    {
        if(!isMoving)
            currentMovement = StartCoroutine("TurnRight");
    }

    public void w_TurnLeft()
    {
        if (!isMoving)
            currentMovement = StartCoroutine("TurnLeft");
    }

    public void w_MoveBackward()
    {
        if (!isMoving)
            currentMovement = StartCoroutine("MoveBackward");
    }

    IEnumerator MoveForward()
    {
        isMoving = true;

        Vector3 target = transform.position + (transform.forward * 10);
        Vector3 offset = target - transform.position;
        CollisionFlags collisionFlags = CollisionFlags.None;

        while (true)
        {
            offset = target - transform.position;

            if (offset.magnitude > 0.1f)
            {
                offset = offset.normalized * movementSpeed;
                collisionFlags = characterController.Move(offset * Time.fixedDeltaTime);
            }
            else
                break;

            if ((collisionFlags & CollisionFlags.Sides) != 0)
            {
                Debug.Log("Collision on Flags");
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

        while (true)
        {
            offset = target - transform.position;

            if (offset.magnitude > 0.1f)
            {
                offset = offset.normalized * movementSpeed;
                collisionFlags = characterController.Move(offset * Time.fixedDeltaTime);
            }
            else
                break;

            if ((collisionFlags & CollisionFlags.Sides) != 0)
            {
                Debug.Log("Collision on Flags");
                break;
            }

            yield return new WaitForFixedUpdate();
        }

        transform.position = new Vector3(Mathf.Round(transform.position.x / 10) * 10, transform.position.y, Mathf.Round(transform.position.z / 10) * 10);

        isMoving = false;

        /*
        isMoving = true;
        float totalMovement = 0;
        while (totalMovement < 10)
        {
            totalMovement += 1f * movementSpeed * Time.fixedDeltaTime;
            GetComponent<CharacterController>().Move(-transform.forward * movementSpeed * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }


        if (transform.position.x < 0)
            transform.position = new Vector3((((int)transform.position.x - 5) / 10) * 10, transform.position.y, transform.position.z);
        else
            transform.position = new Vector3((((int)transform.position.x + 5) / 10) * 10, transform.position.y, transform.position.z);

        if (transform.position.z < 0)
            transform.position = new Vector3(transform.position.x, transform.position.y, (((int)transform.position.z - 5) / 10) * 10);
        else
            transform.position = new Vector3(transform.position.x, transform.position.y, (((int)transform.position.z + 5) / 10) * 10);

        //GameObject.Find("GameManager").GetComponent<Engine>().EnemyMove();

        isMoving = false;
        */
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

        /*
        Vector3 rotation = transform.eulerAngles;
        rotation.y = Mathf.Round(rotation.y / 90) * 90;
        transform.eulerAngles = rotation;
        */
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

        /*
        Vector3 rotation = transform.eulerAngles;
        rotation.y = Mathf.Round(rotation.y / 90) * 90;
        transform.eulerAngles = rotation;
        */
        isMoving = false;
        /*
        isMoving = true;
        float totalRotation = 0;
        while (totalRotation < 90)
        {
            totalRotation += 1 * rotationSpeed * Time.fixedDeltaTime;
            transform.Rotate(Vector3.up, -1 * rotationSpeed * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }

        Vector3 rotation = transform.eulerAngles;
        rotation.y = Mathf.Round(rotation.y / 90) * 90;
        transform.eulerAngles = rotation;

        isMoving = false;
        */
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Impassable")
        {
            if (currentMovement != null) StopCoroutine(currentMovement);
            transform.position = new Vector3(Mathf.Round(transform.position.x / 10) * 10, 2.2f, Mathf.Round(transform.position.z / 10) * 10);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Enemy")
        {
            //GameObject.Find("GameManager").GetComponent<Engine>().StartBattle(other.gameObject);
        }
        else if (other.transform.parent.tag == "Chest")
        {
           // GameObject.Find("GameManager").GetComponent<Engine>().OpenChest(other.GetComponentInParent<ChestInventory>());
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
            else
            {
                Location location = other.GetComponent<LocationContainer>().GetLocation();
                transform.position = location.GetSpawnLocation();
                GameObject.Find("GameManager").GetComponent<Engine>().EnterLocation(location);
            }
        }
        else if (other.transform.parent.tag == "Chest")
        {
          //  GameObject.Find("GameManager").GetComponent<Engine>().OpenChest(other.GetComponentInParent<ChestInventory>(), false);
        }
    }
}
