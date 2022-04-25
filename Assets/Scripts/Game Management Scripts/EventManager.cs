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

public class EventManager : MonoBehaviour
{

    Engine engine;

    GameObject UIEventScreen;

    ScrollRect eventOutputScroll;
    ScrollRect eventChainScroll;

    GameObject eventOutputPanel;
    GameObject eventChainPanel;

    public GameObject eventOutputTxtObj;
    public GameObject eventChainBtnObj;

    Button continueEventBtn;

    List<GameObject> eventOutputObjects;
    List<GameObject> eventChainObjects;

    public StartEvent parentEvent;

    public static SortedDictionary<uint, StartEvent> EventDictionary;
    [SerializeField] List<StartEvent> eventList = new List<StartEvent>();


    void Start()
    {
        if (engine == null)
            engine = gameObject.GetComponent<Engine>();

        InitializeUI();
        InitializeEvents();

    }

    void InitializeUI()
    {
        UIEventScreen = GameObject.Find("UI Event");

        eventOutputScroll = GameObject.Find("EventOutputScrollView").GetComponent<ScrollRect>();
        eventChainScroll = GameObject.Find("EventChainScrollView").GetComponent<ScrollRect>();

        eventOutputPanel = GameObject.Find("EventOutputPanel");
        eventChainPanel = GameObject.Find("EventChainPanel");

        continueEventBtn = GameObject.Find("ContinueEventBtn").GetComponent<Button>();

        eventOutputObjects = new List<GameObject>();
        eventChainObjects = new List<GameObject>();

        continueEventBtn.gameObject.SetActive(false);
        UIEventScreen.SetActive(false);
    }

    void InitializeEvents()
    {
        EventDictionary = new SortedDictionary<uint, StartEvent>();

        foreach(StartEvent _event in eventList)
        {
            try { EventDictionary.Add(_event.GetID(), Instantiate(_event)); }
            catch (ArgumentException)
            {
                Debug.LogErrorFormat("ArgumentException in EventDictionary:\n" +
                    "Key Already Exists in EventDictionary!\n" +
                    "Existing Key, Name: {0}, {1};\n" +
                    "Attempted Key, Name: {2}, {3};", _event.GetID(), EventDictionary[_event.GetID()].GetName(),
                    _event.GetID(), _event.GetName());
            }
        }
    }

    public void ActivateEventScreen(bool x, StartEvent startEvent = null)
    {
        if (x)
        {
            parentEvent = EventDictionary[startEvent.GetID()];

            CreateEventOutputObject(startEvent, false);

            CreateEventChainObject(startEvent);

            UIEventScreen.SetActive(true);
        }
        else
        {

            parentEvent = null;

            UIEventScreen.SetActive(false);
            ClearOutputObjects();
            ClearChainObjects();

        }
    }

    void CreateEventOutputObject(Event _event, bool playerAction = false)
    {
        GameObject tGO = Instantiate(eventOutputTxtObj, eventOutputPanel.transform);

        if(playerAction)
        {
            tGO.GetComponent<Text>().text = (_event as IEventPlayer).GetPlayerDescription();
            tGO.GetComponent<Text>().color = new Color(255, 255, 255, 255);
        }
        else
        {
            tGO.GetComponent<Text>().text = (_event as IEventDescription).GetEventDescription();
            tGO.GetComponent<Text>().color = new Color(255, 255, 255, 255);
        }

        eventOutputObjects.Add(tGO);
        StartCoroutine(ForceScrollDown(eventOutputScroll));
    }

    void CreateEventChainObject(Event _event)
    {
        List<Event> _events = (_event as IEventLinkable).GetEventLinks();

        if (_events != null || !(_events.Count == 0))
        {
            for (int i = 0; i < _events.Count; i++)
            {
                GameObject tGO = Instantiate(eventChainBtnObj, eventChainPanel.transform);

                tGO.GetComponent<EventContainer>().SetEvent(_events[i]);
                tGO.GetComponent<Button>().onClick.AddListener(() => { StartCoroutine(PlayEvent(tGO.GetComponent<EventContainer>().GetEvent())); });
                tGO.transform.GetChild(0).GetComponent<Text>().text = (_events[i] as IEventPlayer).GetEventLinkButtonText();

                eventChainObjects.Add(tGO);
            }
        }
    }

    IEnumerator PlayEvent(Event _event)
    {
        CreateEventOutputObject(_event, true);
        CreateEventOutputObject(_event);
        ClearChainObjects();

        continueEventBtn.gameObject.SetActive(true);
        yield return new WaitForUIButtons(continueEventBtn);
        continueEventBtn.gameObject.SetActive(false);

        if (_event.GetEventType() == Event.EventType.loot)
        {
            LootEvent lEvent = (LootEvent)_event;
            engine.GenerateItemPickup(lEvent.GetItemLootTable());
            engine.ActivatePickupScreen(true);
        }                                           
        else if (_event.GetEventType() == Event.EventType.enemy)
        {
            EnemyEvent eEvent = (EnemyEvent)_event;
            engine.EnterBattle(eEvent.GetEnemy());
            //UIEventScreen.SetActive(false);
            //yield return GetComponent<BattleManager>().battleCoroutine;
        }
        else if (_event.GetEventType() == Event.EventType.damage)
        {
            DamageEvent dEvent = (DamageEvent)_event;
            if (dEvent.GetDamageType() == DamageEvent.DamageType.gold)
            {
                int change = (int)(dEvent.IsPercentage() ? Engine.player.GetGold() * dEvent.GetValue() : dEvent.GetValue());
                Engine.player.ChangeGold(-change);
                engine.OutputToText(String.Format("You have lost {0} gold.\n", change));
            }
            else if (dEvent.GetDamageType() == DamageEvent.DamageType.health)
            {
                int change = (int)(dEvent.IsPercentage() ? Engine.player.GetHealth() * dEvent.GetValue() : dEvent.GetValue());
                Engine.player.ChangeHealth(-change);
                engine.OutputToText(String.Format("You have lost {0} health.\n", change));
            }
            else if (dEvent.GetDamageType() == DamageEvent.DamageType.mana)
            {
                int change = (int)(dEvent.IsPercentage() ? Engine.player.GetMana() * dEvent.GetValue() : dEvent.GetValue());
                Engine.player.ChangeMana(-change);
                engine.OutputToText(String.Format("You have lost {0} mana.\n", change));
            }
            else if (dEvent.GetDamageType() == DamageEvent.DamageType.stamina)
            {
                int change = (int)(dEvent.IsPercentage() ? Engine.player.GetStamina() * dEvent.GetValue() : dEvent.GetValue());
                Engine.player.ChangeStamina(-change);
                engine.OutputToText(String.Format("You have lost {0} stamina.\n", change));
            }
        }

        
        
        if ((_event as IEventLinkable).GetEventLinks() == null || (_event as IEventLinkable).GetEventLinks().Count == 0)
        {
            if (!parentEvent.IsRepeatable())
            {
                parentEvent.SetComplete(true);
                if (GetComponent<DungeonManager>().currentDungeon != null)
                    GetComponent<DungeonManager>().UpdateEvents();
            }
            ActivateEventScreen(false);
        }
        else
        {
            CreateEventChainObject(_event);
        }

        
    }

    IEnumerator ForceScrollDown(ScrollRect scrollRect)
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
        Canvas.ForceUpdateCanvases();
    }

    void ClearOutputObjects()
    {
        foreach (GameObject go in eventOutputObjects.ToList<GameObject>())
        {
            Destroy(go);
        }
        eventOutputObjects.Clear();
    }

    void ClearChainObjects()
    {
        foreach (GameObject go in eventChainObjects.ToList<GameObject>())
        {
            Destroy(go);
        }
        eventChainObjects.Clear();
    }

}
