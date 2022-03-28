using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{

    Engine engine;

    GameObject UIDialogueScreen;

    // UI Dialogue Variables

    Button enterDialogueBtn;

    public GameObject dialogueBtn;
    public GameObject dialogueTxt;

    List<GameObject> dialogueOptionSlots = new List<GameObject>();
    List<GameObject> dialogueResponseSlots = new List<GameObject>();

    GameObject dialogueScrollView;

    GameObject dialogueOptionPanel;
    GameObject dialogueResponsePanel;

    Text diagNPCNameTxt;
    //TextMeshProUGUI npcLineTxt;

    public NPC currentNPC;

    public bool isInNPC = false, isInDialogue = false;


    private void Start()
    {

        engine = GameObject.Find("GameManager").GetComponent<Engine>();

        UIDialogueScreen = GameObject.Find("UI Dialogue");

        enterDialogueBtn = GameObject.Find("EnterDialogueBtn").GetComponent<Button>();

        enterDialogueBtn.gameObject.SetActive(false);

        diagNPCNameTxt = GameObject.Find("DiagNPCNameTxt").GetComponent<Text>();

        dialogueScrollView = GameObject.Find("DialogueScrollView");

        dialogueOptionPanel = GameObject.Find("ResponsePanel");
        dialogueResponsePanel = GameObject.Find("DialogueLinePanel");

        UIDialogueScreen.SetActive(false);
    }

    public void SetIsInNPC(bool x, NPC npc)
    {
        isInNPC = x;
        if (x)
            currentNPC = Engine.NPCDictionary[npc.GetID()];
        else
            currentNPC = null;

        enterDialogueBtn.gameObject.SetActive(x);
    }

    public void ActivateDialogueScreen(bool x)
    {
        if (!x)
        {
            ClearDialogueOptions();
            ClearDialogueResponses();
        }

        if (isInNPC && x)
        {
            diagNPCNameTxt.text = currentNPC.GetName();
            SelectDialogue(currentNPC.GetDialogue());
        }
        isInDialogue = x;
        UIDialogueScreen.SetActive(x);
    }

    public void SelectDialogue(Dialogue dialogue)
    {
        ClearDialogueOptions();

        if (!(dialogue is StartDialogue))
            CreateDialogueTextObject(true, (dialogue as IPlayerDialogue).GetPlayerLineResponseText());


        if (dialogue.GetDialogueType() == Dialogue.DialogueType.start)
        {
            SelectedStartDialogue(dialogue);
        }
        else if (dialogue.GetDialogueType() == Dialogue.DialogueType.merchant)
        {
            SelectedMerchantDialogue(dialogue);
        }
        else if (dialogue.GetDialogueType() == Dialogue.DialogueType.conversation)
        {
            CreateDialogueTextObject(false, (dialogue as INPCDialogue).GetNPCLine());

            List<Dialogue> dialogues = (dialogue as IRespondable).GetPlayerResponses();

            CreateDialogueOptionObjects(dialogues);
        }
        else if (dialogue.GetDialogueType() == Dialogue.DialogueType.quest)
        {
            SelectedQuestDialogue(dialogue);
        }
        else if (dialogue.GetDialogueType() == Dialogue.DialogueType.purchase)
        {
            SelectedPurchaseDialogue(dialogue);
        }
        else if (dialogue.GetDialogueType() == Dialogue.DialogueType.battle)
        {
            SelectedBattleDialogue(dialogue);
        }

    }

    void SelectedStartDialogue(Dialogue dialogue)
    {

        CreateDialogueTextObject(false, (dialogue as INPCDialogue).GetNPCLine());

        CreateDialogueOptionObjects((dialogue as IRespondable).GetPlayerResponses());

        if (currentNPC.HasQuests() && !currentNPC.HasGivenQuest())
        {
            List<Quest> quests = currentNPC.GetQuests();
            List<Quest> possibleQuests = new List<Quest>();
            foreach (Quest quest in quests)
            {
                bool level = (Engine.player.GetLevel() >= Engine.QuestDictionary[quest.GetID()].GetLevelRequirement());
                bool prereq = (!Engine.QuestDictionary[quest.GetID()].HasPrerequisite()) || (Engine.QuestDictionary[quest.GetID()].HasPrerequisite() && Engine.QuestDictionary[quest.GetPrerequisite().GetID()].IsCompleted());
                if (!Engine.QuestDictionary[quest.GetID()].IsCompleted() && level && prereq)
                {
                    possibleQuests.Add(Engine.QuestDictionary[quest.GetID()]);
                }
            }
            if (possibleQuests.Count > 0)
            {
                CreateDialogueOptionObject(possibleQuests[UnityEngine.Random.Range(0, possibleQuests.Count)].GetDialogue().GetNotStartedDialogue());
            }
        }
        else if (currentNPC.HasQuests() && currentNPC.HasGivenQuest())
        {
            if (CheckQuestCompletion(Engine.QuestDictionary[currentNPC.GetGivenQuest().GetID()]))
                CreateDialogueOptionObject(currentNPC.GetGivenQuest().GetDialogue().GetCompleteDialogue());
            else
                CreateDialogueOptionObject(currentNPC.GetGivenQuest().GetDialogue().GetIncompleteDialogue());
        }

        foreach (Quest quest in Engine.player.GetQuestList())
        {
            if (quest.GetQuestType() == Quest.QuestType.Talk)
            {
                TalkQuest talkQuest = (TalkQuest)Engine.QuestDictionary[quest.GetID()];
                if (currentNPC.GetID() == talkQuest.GetTargetNPC().GetID())
                {
                    if (CheckQuestCompletion(quest))
                        CreateDialogueOptionObject(talkQuest.GetDialogue().GetCompleteDialogue());
                    else
                        CreateDialogueOptionObject(talkQuest.GetDialogue().GetIncompleteDialogue());
                }
            }
        }
    }

    void SelectedMerchantDialogue(Dialogue dialogue)
    {
        ActivateDialogueScreen(false);
        engine.ActivatePickupScreen(true);
    }

    void SelectedQuestDialogue(Dialogue dialogue)
    {
        QuestDialogue questDialogue = dialogue as QuestDialogue;

        if ((dialogue is QuestStatusDialogue) && (dialogue as QuestStatusDialogue).GetQuestStatusType() == QuestStatusDialogue.QuestStatusType.complete)
        {
            Debug.Log("in quest completion");
            Quest quest = Engine.QuestDictionary[questDialogue.GetQuest().GetID()];

            if (CheckQuestCompletion(quest))
            {
                Debug.Log("Quest Completed");
                TurnInQuest(quest);

                if (quest.GetQuestType() == Quest.QuestType.Talk)
                    Engine.NPCDictionary[(quest as TalkQuest).GetSourceNPC().GetID()].SetHasGivenQuest(false);
                else
                    Engine.NPCDictionary[currentNPC.GetID()].SetHasGivenQuest(false);

            }
        }
        if (questDialogue.GetQuestDialogueType() == QuestDialogue.QuestDialogueType.accept)
        {
            Quest quest = questDialogue.GetQuest();
            if (!Engine.player.CheckForQuest(quest.GetID()) && !Engine.QuestDictionary[quest.GetID()].IsCompleted())
            {
                engine.AddToQuestList(Engine.QuestDictionary[questDialogue.GetQuest().GetID()]);
                currentNPC.SetHasGivenQuest(true);
                currentNPC.SetGivenQuest(Engine.QuestDictionary[questDialogue.GetQuest().GetID()]);
            }
        }

        if ((dialogue as QuestDialogue).GetQuestDialogueType() == QuestDialogue.QuestDialogueType.start)
        {
            QuestStartDialogue questStartDialogue = (QuestStartDialogue)dialogue;
            List<Dialogue> dialogues = new List<Dialogue>();

            Quest quest = Engine.QuestDictionary[questDialogue.GetQuest().GetID()];
            if (!Engine.player.CheckForQuest(quest.GetID()) && !quest.IsCompleted())
            {
                CreateDialogueTextObject(false, (questStartDialogue.GetNotStartedDialogue() as INPCDialogue).GetNPCLine());

                dialogues = (questStartDialogue.GetNotStartedDialogue() as IRespondable).GetPlayerResponses();
            }
            else if (!quest.IsCompleted())
            {
                CreateDialogueTextObject(false, (questStartDialogue.GetIncompleteDialogue() as INPCDialogue).GetNPCLine());

                dialogues = (questStartDialogue.GetIncompleteDialogue() as IRespondable).GetPlayerResponses();
            }
            else
            {
                CreateDialogueTextObject(false, (questStartDialogue.GetCompleteDialogue() as INPCDialogue).GetNPCLine());

                dialogues = (questStartDialogue.GetCompleteDialogue() as IRespondable).GetPlayerResponses();
            }

            CreateDialogueOptionObjects(dialogues);
        }
        else
        {
            CreateDialogueTextObject(false, (dialogue as INPCDialogue).GetNPCLine());

            CreateDialogueOptionObjects((dialogue as IRespondable).GetPlayerResponses());
        }
    }

    void SelectedPurchaseDialogue(Dialogue dialogue)
    {
        PurchaseDialogue purchaseDialogue = (PurchaseDialogue)dialogue;

        if (Engine.player.GetGold() >= purchaseDialogue.GetCost())
        {
            CreateDialogueTextObject(false, purchaseDialogue.GetNPCLine(true));

            if (purchaseDialogue is InnDialogue)
                engine.EnterInn(purchaseDialogue.GetCost(), ((InnDialogue)purchaseDialogue).GetAttributeRegen());
        }
        else
        {
            CreateDialogueTextObject(false, purchaseDialogue.GetNPCLine(false));

            List<Dialogue> dialogues = purchaseDialogue.GetPlayerResponses(false);

            CreateDialogueOptionObjects(dialogues);
        }
    }

    void SelectedBattleDialogue(Dialogue dialogue)
    {
        Enemy enemy = ((BattleDialogue)dialogue).GetEnemy();

        CreateDialogueTextObject(false, (dialogue as INPCDialogue).GetNPCLine());

        engine.BattleFromDialogue(enemy);
    }

    bool CheckQuestCompletion(Quest quest)
    {
        quest = Engine.QuestDictionary[quest.GetID()];
        if (quest.GetQuestType() == Quest.QuestType.Clear)
            return ((quest as ClearQuest).CheckQuestCompletion());
        else if (quest.GetQuestType() == Quest.QuestType.Fetch)
            return ((quest as FetchQuest).CheckQuestCompletion(Engine.player));
        else if (quest.GetQuestType() == Quest.QuestType.Location)
            return ((quest as LocationQuest).CheckQuestCompletion(Engine.player));
        else if (quest.GetQuestType() == Quest.QuestType.Slay)
            return ((quest as SlayQuest).CheckQuestCompletion());
        else if (quest.GetQuestType() == Quest.QuestType.Talk)
            return ((quest as TalkQuest).CheckQuestCompletion(currentNPC));

        return false;
    }

    void TurnInQuest(Quest quest)
    {
        Engine.QuestDictionary[quest.GetID()].SetCompletion(true);
        engine.OutputToText(String.Format("You have completed {0}.", Engine.QuestDictionary[quest.GetID()].GetName()));
        GameObject questSlot = engine.uiQuestSlots[Engine.QuestDictionary[quest.GetID()].GetID()];
        engine.uiQuestSlots.Remove(Engine.QuestDictionary[quest.GetID()].GetID());
        GameObject.Destroy(questSlot);

        Engine.player.AddExp(Engine.QuestDictionary[quest.GetID()].GetExpReward());
        Engine.player.ChangeGold((int)Engine.QuestDictionary[quest.GetID()].GetGoldReward());
        Engine.player.RemoveQuest(Engine.QuestDictionary[quest.GetID()]);
        Engine.player.AddCompletedQuest(Engine.QuestDictionary[quest.GetID()]);
        engine.OutputToText(String.Format("You have earned {0} exp and {1} gold.", Engine.QuestDictionary[quest.GetID()].GetExpReward(), Engine.QuestDictionary[quest.GetID()].GetGoldReward()));

        if (Engine.QuestDictionary[quest.GetID()].GetQuestType() == Quest.QuestType.Fetch)
        {
            FetchQuest fetchQuest = ((FetchQuest)Engine.QuestDictionary[quest.GetID()]);
            for (int i = 0; i < fetchQuest.GetFetchItems().Count; i++)
                for (int j = 0; j < fetchQuest.GetItemCounts()[i]; j++)
                    engine.RemoveFromInventory(fetchQuest.GetFetchItems()[i]);
        }

        if (Engine.QuestDictionary[quest.GetID()].HasItemReward())
        {
            List<Item> itemRewards = Engine.QuestDictionary[quest.GetID()].GetItemRewards();
            foreach (Item item in itemRewards)
                engine.AddToInventory(item);
        }

        engine.UpdateInventoryAttributes();
        engine.DeactivateActiveQuest();
    }

    void CreateDialogueOptionObject(Dialogue dialogue)
    {
        if (dialogue != null)
        {
            GameObject dGO = Instantiate(dialogueBtn, dialogueOptionPanel.transform);

            dGO.GetComponent<DialogueContainer>().SetDialogue(dialogue);
            if (dialogue is IPlayerDialogue)
                dGO.transform.GetChild(0).GetComponent<Text>().text = (dialogue as IPlayerDialogue).GetPlayerLineObjectText();

            dGO.GetComponent<Button>().onClick.AddListener(() => SelectDialogue(dGO.GetComponent<DialogueContainer>().GetDialogue()));

            dialogueOptionSlots.Add(dGO);
        }
    }

    void CreateDialogueOptionObjects(List<Dialogue> dialogues)
    {
        if (dialogues != null && dialogues.Count > 0)
        {
            for (int i = 0; i < dialogues.Count; i++)
            {
                GameObject dGO = Instantiate(dialogueBtn, dialogueOptionPanel.transform);

                dGO.GetComponent<DialogueContainer>().SetDialogue(dialogues[i]);
                dGO.transform.GetChild(0).GetComponent<Text>().text = (dialogues[i] as IPlayerDialogue).GetPlayerLineObjectText();

                dGO.GetComponent<Button>().onClick.AddListener(() => SelectDialogue(dGO.GetComponent<DialogueContainer>().GetDialogue()));

                dialogueOptionSlots.Add(dGO);
            }
        }
    }

    void CreateDialogueTextObject(bool isPlayer, string text)
    {
        GameObject dGO = Instantiate(dialogueTxt, dialogueResponsePanel.transform);
        dGO.GetComponent<TextMeshProUGUI>().text = text;

        if (isPlayer)
            dGO.GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 255);
        else
            dGO.GetComponent<TextMeshProUGUI>().color = new Color32(255, 200, 200, 255);

        StartCoroutine(ForceScrollDown());
        dialogueResponseSlots.Add(dGO);
    }

    IEnumerator ForceScrollDown()
    {
        yield return new WaitForEndOfFrame();
        Canvas.ForceUpdateCanvases();
        dialogueScrollView.GetComponent<ScrollRect>().verticalNormalizedPosition = 0f;
        Canvas.ForceUpdateCanvases();
    }

    void ClearDialogueOptions()
    {
        foreach (GameObject dGO in dialogueOptionSlots.ToList<GameObject>())
        {
            dialogueOptionSlots.Remove(dGO);
            Destroy(dGO);
        }
    }

    void ClearDialogueResponses()
    {
        foreach (GameObject dGO in dialogueResponseSlots.ToList<GameObject>())
        {
            dialogueResponseSlots.Remove(dGO);
            Destroy(dGO);
        }
    }

}
