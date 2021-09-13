 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] bool isMoving = false;

    [SerializeField] float movementSpeed;
    [SerializeField] float turningSpeed;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void w_MoveForward()
    {
        if(!isMoving)
            StartCoroutine("MoveForward");
    }

    public void w_TurnRight()
    {
        if(!isMoving)
            StartCoroutine("TurnRight");
    }

    public void w_TurnLeft()
    {
        if (!isMoving)
            StartCoroutine("TurnLeft");
    }

    public void w_MoveBackward()
    {
        if (!isMoving)
            StartCoroutine("MoveBackward");
    }

    IEnumerator MoveForward()
    {
        isMoving = true;

        float totalMovement = 0;

        while(totalMovement != 10)
        {
            totalMovement += 1f;
            GetComponent<CharacterController>().Move(transform.forward);
            yield return new WaitForSeconds(movementSpeed);
        }

        if(transform.position.x < 0)
            transform.position = new Vector3((((int)transform.position.x - 5) / 10) * 10, transform.position.y, transform.position.z);
        else
            transform.position = new Vector3((((int)transform.position.x + 5) / 10) * 10, transform.position.y, transform.position.z);

        if(transform.position.z < 0)
            transform.position = new Vector3(transform.position.x, transform.position.y, (((int)transform.position.z - 5) / 10) * 10);
        else
            transform.position = new Vector3(transform.position.x, transform.position.y, (((int)transform.position.z + 5) / 10) * 10);

        GameObject.Find("GameManager").GetComponent<Engine>().EnemyMove();

        isMoving = false;
    }

    IEnumerator MoveBackward()
    {
        isMoving = true;
        float totalMovement = 0;
        while (totalMovement != 10)
        {
            totalMovement += 1f;
            GetComponent<CharacterController>().Move(-transform.forward);
            yield return new WaitForSeconds(movementSpeed);
        }


        if (transform.position.x < 0)
            transform.position = new Vector3((((int)transform.position.x - 5) / 10) * 10, transform.position.y, transform.position.z);
        else
            transform.position = new Vector3((((int)transform.position.x + 5) / 10) * 10, transform.position.y, transform.position.z);

        if (transform.position.z < 0)
            transform.position = new Vector3(transform.position.x, transform.position.y, (((int)transform.position.z - 5) / 10) * 10);
        else
            transform.position = new Vector3(transform.position.x, transform.position.y, (((int)transform.position.z + 5) / 10) * 10);

        GameObject.Find("GameManager").GetComponent<Engine>().EnemyMove();

        isMoving = false;
    }

    IEnumerator TurnRight()
    {
        isMoving = true;
        int totalRotation = 0;
        while (totalRotation != 90)
        {
            totalRotation += 5;
            transform.Rotate(Vector3.up, 5);
            yield return new WaitForSeconds(turningSpeed);
        }
        isMoving = false;
    }

    IEnumerator TurnLeft()
    {
        isMoving = true;
        int totalRotation = 0;
        while (totalRotation != 90)
        {
            totalRotation += 5;
            transform.Rotate(Vector3.up, -5);
            yield return new WaitForSeconds(turningSpeed);
        }
        isMoving = false;
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
