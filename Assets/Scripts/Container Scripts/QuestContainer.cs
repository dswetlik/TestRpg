using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestContainer : MonoBehaviour
{
    Quest quest;
    bool isCompleted = false;

    public Quest GetQuest()
    {
        return quest;
    }
    public void SetQuest(Quest quest)
    {
        this.quest = quest;
        gameObject.transform.GetChild(0).GetComponent<Text>().text = quest.GetName();
    }
    public void SetCompletion(bool value)
    {
        isCompleted = value;
        gameObject.transform.GetChild(1).GetComponent<Toggle>().isOn = value;
    }
    public bool GetCompleted() { return isCompleted; }
}
