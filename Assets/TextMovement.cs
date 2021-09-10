using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextMovement : MonoBehaviour
{
    bool isFinished = false;

    private void Start()
    {
        StartCoroutine(Movement());
    }

    public bool IsFinished() { return isFinished; }

    public void SetText(string text)
    {
        GetComponent<Text>().text = text;
    }

    IEnumerator Movement()
    {
        yield return new WaitForSeconds(2.0f);

        int x = 0;

        while(x < 1000)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1f, 0);
            GetComponent<Text>().color = new Color(GetComponent<Text>().color.r, GetComponent<Text>().color.g, GetComponent<Text>().color.b, GetComponent<Text>().color.a - 0.0051f);
            x++;

            if (GetComponent<Text>().color.a == 0)
                break;

            yield return new WaitForSeconds(0.01f);
        }

        Destroy(this);
    }

}
