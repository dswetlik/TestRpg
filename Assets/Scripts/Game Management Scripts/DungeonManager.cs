using UnityEngine;
using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using TMPro;

public class DungeonManager : MonoBehaviour
{

    Engine engine;

    //GameObject UIDungeonScreen;

    // Dungeon UI Variables
    //Text dungeonNameTxt;
    //Text floorCountTxt;
    //Text clearedTxt;
    //Text dungeonOutputTxt;

    //Button continueBtn;

    public Dungeon currentDungeon;

    [SerializeField] bool isCleared;
    [SerializeField] List<StartEvent> incompleteEvents;

    void Start()
    {
        engine = GetComponent<Engine>();

        //UIDungeonScreen = GameObject.Find("UI Dungeon");
        //UIDungeonScreen.SetActive(false);
        //dungeonNameTxt = GameObject.Find("DungeonNameTxt").GetComponent<Text>();
        //floorCountTxt = GameObject.Find("FloorCountTxt").GetComponent<Text>();
        //clearedTxt = GameObject.Find("ClearedTxt").GetComponent<Text>();
        //dungeonOutputTxt = GameObject.Find("DungeonOutputTxt").GetComponent<Text>();

        //continueBtn = GameObject.Find("ContinueBtn").GetComponent<Button>();

        isCleared = false;
        incompleteEvents = new List<StartEvent>();

        
    }


    public void EnterDungeon(bool x, Dungeon dungeon = null)
    {
        SetCurrentDungeon(dungeon);

        if (x)
        {

            if (currentDungeon.IsCleared())
                engine.OutputToText(String.Format("{0} has been cleared.", currentDungeon.GetName()));
          
            /*
            dungeonNameTxt.text = currentDungeon.GetName();
            clearedTxt.gameObject.SetActive(currentDungeon.IsCleared());
            continueBtn.interactable = !currentDungeon.IsCleared();
            if (!currentDungeon.IsCleared())
            {
                //dungeonOutputTxt.text += String.Format("You have entered the {0} of the {1}.\n", currentDungeon.GetFloorEvents()[currentDungeon.GetClearedFloorCount()].GetName(), currentDungeon.GetName());
                dungeonOutputTxt.text += "--------------------\n";
            }
            else
            {
                dungeonOutputTxt.text += String.Format("You have already cleared the {0}.\n", currentDungeon.GetName());
                dungeonOutputTxt.text += "--------------------\n";
                floorCountTxt.text = "";
            }
            */
        }
        else
        {
            //engine.SetPlayerPosition(currentDungeon.GetSpawnLocation(), currentDungeon.GetOutputRotation());

            engine.OutputToText(String.Format("You have left {0}.", currentDungeon.GetName()));
            currentDungeon = null;
            //dungeonOutputTxt.text = "";
        }
        //UIDungeonScreen.SetActive(x);
    }

    public void UpdateEvents()
    {
        foreach (StartEvent _incompleteEvent in incompleteEvents.ToList<StartEvent>())
            if (_incompleteEvent.IsComplete())
                incompleteEvents.Remove(_incompleteEvent);

        if (incompleteEvents == null || incompleteEvents.Count == 0)
        {
            currentDungeon.SetCleared(true);
            isCleared = true;
            engine.OutputToText(String.Format("{0} has been cleared.", currentDungeon.GetName()));
        }
    }

    /*
    public void NextDungeonFloor()
    {

        //engine.ActivateEventScreen(true, startEvent);

        floorCountTxt.text = startEvent.GetName();

        if (currentDungeon.GetClearedFloorCount() == currentDungeon.GetFloorCount())
        {
            dungeonOutputTxt.text += String.Format("You have reached the end of {0}.\n", currentDungeon.GetName());
            dungeonOutputTxt.text += "--------------------\n";
            currentDungeon.SetCleared(true);
        }

        clearedTxt.gameObject.SetActive(currentDungeon.IsCleared());
        continueBtn.interactable = !currentDungeon.IsCleared();
    }
    */

    public void SetCurrentDungeon(Dungeon dungeon)
    {
        if (dungeon != null)
        {
            currentDungeon = (Engine.LocationDictionary[dungeon.GetID()] as Dungeon);
            incompleteEvents = currentDungeon.GetEvents();
            isCleared = currentDungeon.IsCleared();
        }
        else
        {
            currentDungeon = null;
            incompleteEvents.Clear();
            isCleared = false;
        }
    }
    public void LeaveDungeon()
    {
        EnterDungeon(false);
    }

}
