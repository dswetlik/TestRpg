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

public class Engine : MonoBehaviour
{
    // UI Variables
    public GameObject UIRoot;
    GameObject UIInventoryScreen;
    GameObject UIDirectionScreen;
    GameObject UIQuestScreen;
    GameObject UIBattleScreen;
    GameObject UIPickupScreen;
    GameObject UIArenaScreen;
    GameObject UISkillScreen;
    GameObject UILoadScreen;

    // Directional UI Variables
    Button northBtn;
    Button eastBtn;
    Button southBtn;
    Button westBtn;
    Button searchBtn;
    Button openBtn;

    Text locationNameTxt;
    static Text outputTxt;
    public ScrollRect mainGameScroll;

    // Inventory / Pickup Variables
    public GameObject uiInvSlot;
    SortedDictionary<uint, GameObject> uiInvSlots = new SortedDictionary<uint, GameObject>();
    public ScrollRect invScroll;
    public GameObject invPanel;
    SortedDictionary<uint, GameObject> uiPickupSlots = new SortedDictionary<uint, GameObject>();
    public ScrollRect pickupScroll;
    public GameObject pickupPanel;
    Item activeItem;

    Button dropItemBtn;
    Button useItemBtn;
    Button equipItemBtn;
    Button unequipItemBtn;

    Button headBtn;
    Button chestBtn;
    Button legsBtn;
    Button feetBtn;
    Button handsBtn;
    Button weaponBtn;

    GameObject itemDamageObj;
    GameObject itemArmorObj;
    GameObject itemConsumableObj;

    Text nameTxt;
    Text descriptionTxt;
    Text valueTxt;
    Text weightTxt;
    Text totalInvWeightTxt;
    Text goldTxt;
    Text expTxt;
    Text invLevelTxt;
    Text invTotalExpTxt;
    Text invSkillPointsTxt;

    Text invHealthTxt;
    Text invStaminaTxt;
    Text invManaTxt;

    Slider invHealthSlider;
    Slider invStaminaSlider;
    Slider invManaSlider;
    Slider invExpSlider;

    // Quest UI Variables
    public GameObject uiQuestSlot;
    Toggle questCompletionTgl;
    SortedDictionary<uint, GameObject> uiQuestSlots = new SortedDictionary<uint, GameObject>();
    Quest activeQuest;
    Quest selectedQuest;

    Text questName;
    Text questDescription;
    Text selQuestName;
    Text selQuestDescription;

    Button makeActiveBtn;
    Button turnInQuestBtn;

    public ScrollRect questScroll;
    public GameObject questPanel;

    // UI Battle Variables
    Text enemyNameTxt;
    Text enemyHealthTxt;
    Text enemyStaminaTxt;
    Text enemyManaTxt;
    Text playerBattleHealthTxt;
    Text playerBattleStaminaTxt;
    Text playerBattleManaTxt;

    Text battleOutputTxt;

    Slider enemyHealthSlider;
    Slider enemyStaminaSlider;
    Slider enemyManaSlider;
    Slider playerBattleHealthSlider;
    Slider playerBattleStaminaSlider;
    Slider playerBattleManaSlider;

    Slider playerSpeedSlider;
    Slider enemySpeedSlider;

    Button playerBattleInventoryBtn;

    GameObject playerCardLocationA;

    // UI Pickup Variables
    Text otherPickUpNameTxt;
    Text playerPickUpNameTxt;

    Text pickUpDescriptionTxt;
    Text pickUpNameTxt;
    Text pickUpWeightTxt;
    Text pickUpValueTxt;
    Text pickUpDamageTxt;
    Text pickUpArmorTxt;
    Text pickUpConsumableTxt;

    GameObject pickUpDamageObj;
    GameObject pickUpArmorObj;
    GameObject pickUpConsumableObj;

    Button pickupExitBtn;

    // UI Arena Variables
    Enemy selectedEnemy;

    GameObject enemyA;
    GameObject enemyB;
    GameObject enemyC;

    Text enemyAName;
    Text enemyBName;
    Text enemyCName;

    Text enemyName;
    Text enemyDescription;
    Text enemyHealth;
    Text enemyDamage;

    // UI Skill Variables
    Skill selectedSkill;

    Text skillNameTxt;
    Text skillDescriptionTxt;
    Text skillCostTxt;
    Text skillDamageTxt;

    Button unlockSkillBtn;

    // UI Load Variables
    Slider loadingSlider;

    Text loadingPercentTxt;

    // Game Variables
    public static SortedDictionary<uint, Item> ItemDictionary;
    public static SortedDictionary<uint, Location> LocationDictionary;
    public static SortedDictionary<string, Skill> SkillDictionary;

    Player player;

    public static Item NULL_ITEM;
    public static Weapon NULL_WEAPON;
    public static Armor NULL_ARMOR;

    Item key;

    Item ratFur;
    Item ratKingsCrown;
    Item ratLuckyPaw;
    Item ratSkull;
    Item ratTooth;

    Item blueSlimeGoo;
    Item greenSlimeGoo;
    Item redSlimeGoo;
    Item unknownBone;

    Weapon ironSword;
    Weapon steelSword;
    Weapon steelDagger;

    Armor leatherHat;
    Armor leatherShirt;
    Armor leatherPants;
    Armor leatherBoots;
    Armor leatherGloves;

    Consumable ratMeat;
    Consumable smallHealthPotion;
    Consumable smallStaminaPotion;

    Location border;
    Location overworld;
    Location floor1;
    Location floor2;

    Quest testBaseQuest;
    Quest leaveTheCave;

    ActiveSkill slash;
    ActiveSkill stab;
    ActiveSkill heavySwing;

    // Local Use Variables
    bool playerHasMoved, isInPickup = false, isInBattle = false, isInChest = false;
    int playerDamageOutput, enemyDamageOutput;
    ChestInventory activeChest;
    DoorInteraction activeDoor;
    public GameObject playerObject, enemyObject;
    public GameObject gameManager;
    public GameObject card;

    public bool isLoadingGame;

    public void InitializeGame()
    {
        InitializeAssets();
        InitializeUI();
        InitializeQuests();

        if (isLoadingGame)
            LoadGame();
        else
            StartNewGame();

        UpdateInventoryAttributes();
    }

    void StartNewGame()
    {
        //AddToQuestList(leaveTheCave);

        player.GetInventory().AddToInventory(ironSword);
        player.GetInventory().AddToInventory(leatherBoots);
        player.GetInventory().AddToInventory(smallHealthPotion, 5);
        player.GetInventory().OnBeforeSerialize();
        player.GetInventory().OnAfterDeserialize();

        StartCoroutine(LoadScene(overworld));
    }

    void LoadGame()
    {
        if(File.Exists(Application.persistentDataPath + "/save.tpg"))
        {
            Debug.Log(Application.persistentDataPath);
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/save.tpg", FileMode.Open);
            SaveLoad load = (SaveLoad)bf.Deserialize(file);
            file.Close();

            player = load.LoadPlayer();

            LoadEquipmentSlots();
            SetCurrentWeight();

            StartCoroutine(LoadScene(player.GetLocation()));
        }
        else
        {
            Debug.Log("No Game Saved");
            StartCoroutine(LoadToMain());
        }
    }

    void InitializeAssets()
    {
        ItemDictionary = new SortedDictionary<uint, Item>();
        LocationDictionary = new SortedDictionary<uint, Location>();
        SkillDictionary = new SortedDictionary<string, Skill>();

        NULL_ITEM = Resources.Load<Item>("Items/NULL_ITEM");
        NULL_WEAPON = Resources.Load<Weapon>("Items/NULL_WEAPON");
        NULL_ARMOR = Resources.Load<Armor>("Items/NULL_ARMOR");

        key = Resources.Load<Item>("Items/Miscellaneous/Key");

        ratFur = Resources.Load<Item>("Items/Miscellaneous/RatItems/RatFur");
        ratKingsCrown = Resources.Load<Item>("Items/Miscellaneous/RatItems/RatKingsCrown");
        ratLuckyPaw = Resources.Load<Item>("Items/Miscellaneous/RatItems/RatLuckyPaw");
        ratSkull = Resources.Load<Item>("Items/Miscellaneous/RatItems/RatSkull");
        ratTooth = Resources.Load<Item>("Items/Miscellaneous/RatItems/RatTooth");

        blueSlimeGoo = Resources.Load<Item>("Items/Miscellaneous/SlimeItems/BlueSlimeGoo");
        greenSlimeGoo = Resources.Load<Item>("Items/Miscellaneous/SlimeItems/GreenSlimeGoo");
        redSlimeGoo = Resources.Load<Item>("Items/Miscellaneous/SlimeItems/RedSlimeGoo");
        unknownBone = Resources.Load<Item>("Items/Miscellaneous/SlimeItems/UnknownBone");

        ironSword = Resources.Load<Weapon>("Items/Weapons/Iron Sword");
        steelSword = Resources.Load<Weapon>("Items/Weapons/Steel Sword");
        steelDagger = Resources.Load<Weapon>("Items/Weapons/Steel Dagger");

        leatherHat = Resources.Load<Armor>("Items/Armors/Leather/Leather Cap");
        leatherShirt = Resources.Load<Armor>("Items/Armors/Leather/Leather Shirt");
        leatherPants = Resources.Load<Armor>("Items/Armors/Leather/Leather Pants");
        leatherBoots = Resources.Load<Armor>("Items/Armors/Leather/Leather Boots");
        leatherGloves = Resources.Load<Armor>("Items/Armors/Leather/Leather Gloves");

        ratMeat = Resources.Load<Consumable>("Items/Consumables/Rat Meat");
        smallHealthPotion = Resources.Load<Consumable>("Items/Consumables/Small Health Potion");
        smallStaminaPotion = Resources.Load<Consumable>("Items/Consumables/Small Stamina Potion");

        ItemDictionary.Add(key.GetID(), key);

        ItemDictionary.Add(ratFur.GetID(), ratFur);
        ItemDictionary.Add(ratKingsCrown.GetID(), ratKingsCrown);
        ItemDictionary.Add(ratLuckyPaw.GetID(), ratLuckyPaw);
        ItemDictionary.Add(ratSkull.GetID(), ratSkull);
        ItemDictionary.Add(ratTooth.GetID(), ratTooth);

        ItemDictionary.Add(blueSlimeGoo.GetID(), blueSlimeGoo);
        ItemDictionary.Add(greenSlimeGoo.GetID(), greenSlimeGoo);
        ItemDictionary.Add(redSlimeGoo.GetID(), redSlimeGoo);
        ItemDictionary.Add(unknownBone.GetID(), unknownBone);

        ItemDictionary.Add(ironSword.GetID(), ironSword);
        ItemDictionary.Add(steelSword.GetID(), steelSword);
        ItemDictionary.Add(steelDagger.GetID(), steelDagger);

        ItemDictionary.Add(leatherHat.GetID(), leatherHat);
        ItemDictionary.Add(leatherShirt.GetID(), leatherShirt);
        ItemDictionary.Add(leatherPants.GetID(), leatherPants);
        ItemDictionary.Add(leatherBoots.GetID(), leatherBoots);
        ItemDictionary.Add(leatherGloves.GetID(), leatherGloves);

        ItemDictionary.Add(ratMeat.GetID(), ratMeat);
        ItemDictionary.Add(smallHealthPotion.GetID(), smallHealthPotion);
        ItemDictionary.Add(smallStaminaPotion.GetID(), smallStaminaPotion);

        border = Resources.Load<Location>("Locations/Border");
        overworld = Resources.Load<Location>("Locations/Overworld");
        floor1 = Resources.Load<Location>("Locations/Floor1");
        floor2 = Resources.Load<Location>("Locations/Floor2");

        LocationDictionary.Add(border.GetID(), border);
        LocationDictionary.Add(overworld.GetID(), overworld);
        LocationDictionary.Add(floor1.GetID(), floor1);
        LocationDictionary.Add(floor2.GetID(), floor2);

        slash = Instantiate(Resources.Load<ActiveSkill>("Player Moves/Weapon Skills/Slash"));
        stab = Instantiate(Resources.Load<ActiveSkill>("Player Moves/Weapon Skills/Stab"));
        heavySwing = Instantiate(Resources.Load<ActiveSkill>("Player Moves/Stamina Skills/Heavy Swing"));

        SkillDictionary.Add(slash.GetName(), slash);
        SkillDictionary.Add(stab.GetName(), stab);
        SkillDictionary.Add(heavySwing.GetName(), heavySwing);

        player = new Player("Name", floor1, new Inventory());
    }

    void InitializeQuests()
    {
        testBaseQuest = Resources.Load<Quest>("Quests/TestBaseQuest");
        leaveTheCave = Resources.Load<Quest>("Quests/Leave The Cave");
    }

    void InitializeUI()
    {
        UIInventoryScreen = GameObject.Find("UI Inventory");
        UIDirectionScreen = GameObject.Find("UI Directional");
        UIQuestScreen = GameObject.Find("UI Quest");
        UIBattleScreen = GameObject.Find("UI Battle");
        UIPickupScreen = GameObject.Find("UI Pickup");
        UIArenaScreen = GameObject.Find("UI Arena");
        UISkillScreen = GameObject.Find("UI Skill");
        UILoadScreen = GameObject.Find("UI Load");

        northBtn = GameObject.Find("NorthBtn").GetComponent<Button>();
        eastBtn = GameObject.Find("EastBtn").GetComponent<Button>();
        southBtn = GameObject.Find("SouthBtn").GetComponent<Button>();
        westBtn = GameObject.Find("WestBtn").GetComponent<Button>();
        searchBtn = GameObject.Find("SearchBtn").GetComponent<Button>();
        searchBtn.onClick.AddListener(() => ActivatePickupScreen(true, ""));
        openBtn = GameObject.Find("OpenBtn").GetComponent<Button>();
        openBtn.gameObject.SetActive(false);

        dropItemBtn = GameObject.Find("DropItemBtn").GetComponent<Button>();
        useItemBtn = GameObject.Find("UseItemBtn").GetComponent<Button>();
        locationNameTxt = GameObject.Find("LocationNameTxt").GetComponent<Text>();

        equipItemBtn = GameObject.Find("EquipItemBtn").GetComponent<Button>();
        unequipItemBtn = GameObject.Find("UnequipItemBtn").GetComponent<Button>();
        makeActiveBtn = GameObject.Find("MakeActiveBtn").GetComponent<Button>();
        turnInQuestBtn = GameObject.Find("TurnInQuestBtn").GetComponent<Button>();

        headBtn = GameObject.Find("HeadBtn").GetComponent<Button>();
        chestBtn = GameObject.Find("ChestBtn").GetComponent<Button>();
        legsBtn = GameObject.Find("LegsBtn").GetComponent<Button>();
        feetBtn = GameObject.Find("FeetBtn").GetComponent<Button>();
        handsBtn = GameObject.Find("HandsBtn").GetComponent<Button>();
        weaponBtn = GameObject.Find("WeaponBtn").GetComponent<Button>();

        headBtn.gameObject.GetComponent<ItemContainer>().SetItem(NULL_ARMOR);
        chestBtn.gameObject.GetComponent<ItemContainer>().SetItem(NULL_ARMOR);
        legsBtn.gameObject.GetComponent<ItemContainer>().SetItem(NULL_ARMOR);
        feetBtn.gameObject.GetComponent<ItemContainer>().SetItem(NULL_ARMOR);
        handsBtn.gameObject.GetComponent<ItemContainer>().SetItem(NULL_ARMOR);
        weaponBtn.gameObject.GetComponent<ItemContainer>().SetItem(NULL_WEAPON);

        searchBtn.gameObject.SetActive(false);
        dropItemBtn.interactable = false;
        makeActiveBtn.interactable = false;
        turnInQuestBtn.interactable = false;
        useItemBtn.gameObject.SetActive(false);
        equipItemBtn.gameObject.SetActive(false);
        unequipItemBtn.gameObject.SetActive(false);

        itemDamageObj = GameObject.Find("ItemDamage");
        itemArmorObj = GameObject.Find("ItemArmor");
        itemConsumableObj = GameObject.Find("ItemConsumable");

        itemDamageObj.SetActive(false);
        itemArmorObj.SetActive(false);
        itemConsumableObj.SetActive(false);

        nameTxt = GameObject.Find("NameTxt").GetComponent<Text>();
        descriptionTxt = GameObject.Find("DescriptionTxt").GetComponent<Text>();
        valueTxt = GameObject.Find("ValueTxt").GetComponent<Text>();
        weightTxt = GameObject.Find("WeightTxt").GetComponent<Text>();
        totalInvWeightTxt = GameObject.Find("TotalInvWeightTxt").GetComponent<Text>();
        selQuestName = GameObject.Find("SelectQuestNameTxt").GetComponent<Text>();
        selQuestDescription = GameObject.Find("SelectQuestDescriptionTxt").GetComponent<Text>();
        goldTxt = GameObject.Find("InventoryGoldTxt").GetComponent<Text>();
        expTxt = GameObject.Find("InventoryExpTxt").GetComponent<Text>();
        invLevelTxt = GameObject.Find("InventoryLvlTxt").GetComponent<Text>();
        invTotalExpTxt = GameObject.Find("InventoryTotalExpTxt").GetComponent<Text>();
        invSkillPointsTxt = GameObject.Find("InventorySkillPointsTxt").GetComponent<Text>();

        invHealthTxt = GameObject.Find("InventoryHealthTxt").GetComponent<Text>();
        invStaminaTxt = GameObject.Find("InventoryStaminaTxt").GetComponent<Text>();
        invManaTxt = GameObject.Find("InventoryManaTxt").GetComponent<Text>();

        nameTxt.text = "";
        descriptionTxt.text = "";
        valueTxt.text = "";
        weightTxt.text = "";
        totalInvWeightTxt.text = "";
        selQuestName.text = "";
        selQuestDescription.text = "";
        questName = GameObject.Find("ActiveQuestNameTxt").GetComponent<Text>();
        questDescription = GameObject.Find("ActiveQuestDescriptionTxt").GetComponent<Text>();
        goldTxt.text = 0.ToString();
        expTxt.text = player.GetExp() + "/" + player.GetToLevelExp();

        questName.text = "";
        questDescription.text = "";

        outputTxt = GameObject.Find("GameTxt").GetComponent<Text>();
        outputTxt.text = "";
        
        invHealthSlider = GameObject.Find("InventoryHealthSlider").GetComponent<Slider>();
        invStaminaSlider = GameObject.Find("InventoryStaminaSlider").GetComponent<Slider>();
        invManaSlider = GameObject.Find("InventoryManaSlider").GetComponent<Slider>();
        invExpSlider = GameObject.Find("InventoryExpSlider").GetComponent<Slider>();

        enemyNameTxt = GameObject.Find("EnemyNameTxt").GetComponent<Text>();
        enemyHealthTxt = GameObject.Find("EnemyHealthTxt").GetComponent<Text>();
        enemyStaminaTxt = GameObject.Find("EnemyStaminaTxt").GetComponent<Text>();
        enemyManaTxt = GameObject.Find("EnemyManaTxt").GetComponent<Text>();
        enemyHealthSlider = GameObject.Find("EnemyHealthSlider").GetComponent<Slider>();
        enemyStaminaSlider = GameObject.Find("EnemyStaminaSlider").GetComponent<Slider>();
        enemyManaSlider = GameObject.Find("EnemyManaSlider").GetComponent<Slider>();

        playerBattleHealthTxt = GameObject.Find("PlayerBattleHealthTxt").GetComponent<Text>();
        playerBattleStaminaTxt = GameObject.Find("PlayerBattleStaminaTxt").GetComponent<Text>();
        playerBattleManaTxt = GameObject.Find("PlayerBattleManaTxt").GetComponent<Text>();
        playerBattleHealthSlider = GameObject.Find("PlayerBattleHealthSlider").GetComponent<Slider>();
        playerBattleStaminaSlider = GameObject.Find("PlayerBattleStaminaSlider").GetComponent<Slider>();
        playerBattleManaSlider = GameObject.Find("PlayerBattleManaSlider").GetComponent<Slider>();

        battleOutputTxt = GameObject.Find("BattleOutputTxt").GetComponent<Text>();
        
        enemySpeedSlider = GameObject.Find("EnemySpeedSlider").GetComponent<Slider>();
        playerSpeedSlider = GameObject.Find("PlayerSpeedSlider").GetComponent<Slider>();

        playerBattleInventoryBtn = GameObject.Find("PlayerBattleInventoryBtn").GetComponent<Button>();
        playerBattleInventoryBtn.interactable = false;

        otherPickUpNameTxt = GameObject.Find("OtherPickupNameTxt").GetComponent<Text>();
        playerPickUpNameTxt = GameObject.Find("PlayerPickUpNameTxt").GetComponent<Text>();

        pickUpDescriptionTxt = GameObject.Find("PickupDescriptionTxt").GetComponent<Text>();
        pickUpNameTxt = GameObject.Find("PickupNameTxt").GetComponent<Text>();
        pickUpWeightTxt = GameObject.Find("PickupWeightTxt").GetComponent<Text>();
        pickUpValueTxt = GameObject.Find("PickupValueTxt").GetComponent<Text>();
        pickUpDamageTxt = GameObject.Find("PickupDamageTxt").GetComponent<Text>();
        pickUpArmorTxt = GameObject.Find("PickupArmorTxt").GetComponent<Text>();
        pickUpConsumableTxt = GameObject.Find("PickupConsumableTxt").GetComponent<Text>();

        pickupExitBtn = GameObject.Find("PickupExitBtn").GetComponent<Button>();
        pickupExitBtn.GetComponent<Button>().onClick.AddListener(() => ActivatePickupScreen(false));
        
        pickUpDamageObj = GameObject.Find("PickupItemDamage");
        pickUpArmorObj = GameObject.Find("PickupItemArmor");
        pickUpConsumableObj = GameObject.Find("PickupItemConsumable");
        pickUpDamageObj.SetActive(false);
        pickUpArmorObj.SetActive(false);
        pickUpConsumableObj.SetActive(false);

        enemyA = GameObject.Find("EnemyA");
        enemyB = GameObject.Find("EnemyB");
        enemyC = GameObject.Find("EnemyC");

        enemyAName = GameObject.Find("EnemyAName").GetComponent<Text>();
        enemyBName = GameObject.Find("EnemyBName").GetComponent<Text>();
        enemyCName = GameObject.Find("EnemyCName").GetComponent<Text>();

        enemyName = GameObject.Find("ArenaNameTxt").GetComponent<Text>();
        enemyDescription = GameObject.Find("ArenaDescriptionTxt").GetComponent<Text>();
        enemyDamage = GameObject.Find("ArenaDamageTxt").GetComponent<Text>();
        enemyHealth = GameObject.Find("ArenaHealthTxt").GetComponent<Text>();

        skillNameTxt = GameObject.Find("SkillNameTxt").GetComponent<Text>();
        skillDescriptionTxt = GameObject.Find("SkillDescriptionTxt").GetComponent<Text>();
        skillCostTxt = GameObject.Find("SkillCostTxt").GetComponent<Text>();
        skillDamageTxt = GameObject.Find("SkillDamageTxt").GetComponent<Text>();

        unlockSkillBtn = GameObject.Find("UnlockSkillBtn").GetComponent<Button>();

        loadingSlider = GameObject.Find("LoadingSlider").GetComponent<Slider>();
        loadingPercentTxt = GameObject.Find("LoadingPercentTxt").GetComponent<Text>();

        invScroll.verticalNormalizedPosition = 1;

        UIInventoryScreen.SetActive(false);
        UIQuestScreen.SetActive(false);
        UIBattleScreen.SetActive(false);
        UIPickupScreen.SetActive(false);
        UIArenaScreen.SetActive(false);
        UISkillScreen.SetActive(false);
        UILoadScreen.SetActive(false);

        UpdateInventoryAttributes();
    }

    public void StartBattle(GameObject enemy)
    {
        if(!isInBattle)
            StartCoroutine(Battle(enemy));
    }

    public void Battle()
    {
        StartCoroutine(Battle(selectedEnemy));
        ActivateArenaScreen(false);
    }

    IEnumerator Battle(GameObject eGO)
    {
        isInBattle = true;

        playerDamageOutput = 0;
        enemyDamageOutput = 0;

        Enemy enemy = Instantiate(eGO.GetComponent<EnemyMovement>().GetEnemy());
        enemyNameTxt.text = enemy.GetName();

        UIBattleScreen.SetActive(true);

        UpdateBattleAttributes(enemy);

        int playerSpeed = player.GetSpeed(), enemySpeed = enemy.GetSpeed();
        playerSpeedSlider.value = playerSpeed;
        enemySpeedSlider.value = enemySpeed;

        while (player.GetHealth() > 0 && enemy.GetHealth() > 0)
        {

            while (playerSpeed < 100 && enemySpeed < 100)
            {
                playerSpeed += player.GetSpeed();
                enemySpeed += enemy.GetSpeed();

                playerSpeedSlider.value = playerSpeed;
                enemySpeedSlider.value = enemySpeed;

                player.RegenAttributes();
                enemy.RegenAttributes();

                UpdateBattleAttributes(enemy);
                yield return new WaitForSeconds(1.0f);
            }

            if (playerSpeed >= 100 && playerSpeed >= enemySpeed && player.GetHealth() > 0)
            {
                yield return StartCoroutine("PlayerMove");
                playerSpeed -= 100;

                enemy.ChangeHealth(-playerDamageOutput);
                UpdateBattleAttributes(enemy);

                playerSpeedSlider.value = playerSpeed;

                if (enemySpeed >= 100 && enemy.GetHealth() > 0)
                {
                    EnemyAttack(enemy);
                    enemySpeed -= 100;

                    player.ChangeHealth(-enemyDamageOutput);
                    UpdateBattleAttributes(enemy);

                    enemySpeedSlider.value = enemySpeed;
                }

                playerDamageOutput = 0;
                enemyDamageOutput = 0;

            }
            if (enemySpeed >= 100 && enemySpeed >= playerSpeed && enemy.GetHealth() > 0)
            {
                EnemyAttack(enemy);
                enemySpeed -= 100;

                player.ChangeHealth(-enemyDamageOutput);
                UpdateBattleAttributes(enemy);

                enemySpeedSlider.value = enemySpeed;

                if (playerSpeed >= 100 && player.GetHealth() > 0)
                {
                    yield return StartCoroutine("PlayerMove");
                    playerSpeed -= 100;

                    enemy.ChangeHealth(-playerDamageOutput);
                    UpdateBattleAttributes(enemy);

                    playerSpeedSlider.value = playerSpeed;
                }

                playerDamageOutput = 0;
                enemyDamageOutput = 0;

            }

            UpdateBattleAttributes(enemy);
            yield return null;
        }

        if(enemy.GetHealth() <= 0)
        {
            ActivatePickupScreen(true, enemy.GetName());
            GenerateItemPickup(enemy.GetItemRewards());
            OutputToText(String.Format("You have killed {0}, gaining {1} exp and {2} gold.", enemy.GetName(), enemy.GetExpReward(), enemy.GetGoldReward()));
            player.AddExp(enemy.GetExpReward());
            player.ChangeGold(enemy.GetGoldReward());
            UIBattleScreen.SetActive(false);
            Destroy(eGO);
        }
        else
        {
            UIBattleScreen.SetActive(false);
            OutputToText("You have Died.");
        }

        UpdateInventoryAttributes();
        battleOutputTxt.text = "";
        isInBattle = false;
    }

    IEnumerator Battle(Enemy eGO)
    {
        isInBattle = true;

        playerDamageOutput = 0;
        enemyDamageOutput = 0;

        Enemy enemy = Instantiate(eGO);
        enemyNameTxt.text = enemy.GetName();

        UIBattleScreen.SetActive(true);

        UpdateBattleAttributes(enemy);

        int playerSpeed = player.GetSpeed(), enemySpeed = enemy.GetSpeed();
        playerSpeedSlider.value = playerSpeed;
        enemySpeedSlider.value = enemySpeed;

        while (player.GetHealth() > 0 && enemy.GetHealth() > 0)
        {

            while (playerSpeed < 100 && enemySpeed < 100)
            {
                playerSpeed += player.GetSpeed();
                enemySpeed += enemy.GetSpeed();

                playerSpeedSlider.value = playerSpeed;
                enemySpeedSlider.value = enemySpeed;

                player.RegenAttributes();
                enemy.RegenAttributes();

                UpdateBattleAttributes(enemy);
                yield return new WaitForSeconds(1.0f);
            }

            if (playerSpeed >= 100 && playerSpeed >= enemySpeed && player.GetHealth() > 0)
            {
                yield return StartCoroutine("PlayerMove");
                playerSpeed -= 100;

                enemy.ChangeHealth(-playerDamageOutput);
                UpdateBattleAttributes(enemy);

                playerSpeedSlider.value = playerSpeed;

                if (enemySpeed >= 100 && enemy.GetHealth() > 0)
                {
                    EnemyAttack(enemy);
                    enemySpeed -= 100;

                    player.ChangeHealth(-enemyDamageOutput);
                    UpdateBattleAttributes(enemy);

                    enemySpeedSlider.value = enemySpeed;
                }

                playerDamageOutput = 0;
                enemyDamageOutput = 0;

            }
            if (enemySpeed >= 100 && enemySpeed >= playerSpeed && enemy.GetHealth() > 0)
            {
                EnemyAttack(enemy);
                enemySpeed -= 100;

                player.ChangeHealth(-enemyDamageOutput);
                UpdateBattleAttributes(enemy);

                enemySpeedSlider.value = enemySpeed;

                if (playerSpeed >= 100 && player.GetHealth() > 0)
                {
                    yield return StartCoroutine("PlayerMove");
                    playerSpeed -= 100;

                    enemy.ChangeHealth(-playerDamageOutput);
                    UpdateBattleAttributes(enemy);

                    playerSpeedSlider.value = playerSpeed;
                }

                playerDamageOutput = 0;
                enemyDamageOutput = 0;

            }

            UpdateBattleAttributes(enemy);
            yield return null;
        }

        if (enemy.GetHealth() <= 0)
        {
            ActivatePickupScreen(true, enemy.GetName());
            GenerateItemPickup(enemy.GetItemRewards());
            OutputToText(String.Format("You have killed {0}, gaining {1} exp and {2} gold.", enemy.GetName(), enemy.GetExpReward(), enemy.GetGoldReward()));
            player.AddExp(enemy.GetExpReward());
            player.ChangeGold(enemy.GetGoldReward());
            UIBattleScreen.SetActive(false);
        }
        else
        {
            UIBattleScreen.SetActive(false);
            OutputToText("You have Died.");
        }

        UpdateInventoryAttributes();
        battleOutputTxt.text = "";
        isInBattle = false;
    }

    void UpdateBattleAttributes(Enemy enemy)
    {
        enemyHealthSlider.maxValue = enemy.GetMaxHealth();
        enemyStaminaSlider.maxValue = enemy.GetMaxStamina();
        enemyManaSlider.maxValue = enemy.GetMaxMana();

        playerBattleHealthSlider.maxValue = player.GetMaxHealth();
        playerBattleStaminaSlider.maxValue = player.GetMaxStamina();
        playerBattleManaSlider.maxValue = player.GetMaxMana();

        enemyHealthSlider.value = enemy.GetHealth();
        enemyStaminaSlider.value = enemy.GetStamina();
        enemyManaSlider.value = enemy.GetMana();

        playerBattleHealthSlider.value = player.GetHealth();
        playerBattleStaminaSlider.value = player.GetStamina();
        playerBattleManaSlider.value = player.GetMana();

        playerBattleHealthTxt.text = String.Format("{0}/{1}", player.GetHealth(), player.GetMaxHealth());
        playerBattleStaminaTxt.text = String.Format("{0}/{1}", player.GetStamina(), player.GetMaxStamina());
        playerBattleManaTxt.text = String.Format("{0}/{1}", player.GetMana(), player.GetMaxMana());

        enemyHealthTxt.text = String.Format("{0}/{1}", enemy.GetHealth(), enemy.GetMaxHealth());
        enemyStaminaTxt.text = String.Format("{0}/{1}", enemy.GetStamina(), enemy.GetMaxStamina());
        enemyManaTxt.text = String.Format("{0}/{1}", enemy.GetMana(), enemy.GetMaxMana());
    }

    IEnumerator PlayerMove()
    {
        playerHasMoved = false;
        playerBattleInventoryBtn.interactable = true;

        ActiveSkill cardASkill = player.GetWeapon().GetRandomSkill();
        ActiveSkill cardBSkill = player.GetRandomStaminaSkill();
        ActiveSkill cardCSkill = player.GetWeapon().GetRandomSkill();

        Instantiate(card, GameObject.Find("PlayerCardALocation").transform).transform.position = GameObject.Find("PlayerCardALocation").transform.position;
        if(cardBSkill != null)
            Instantiate(card, GameObject.Find("PlayerCardBLocation").transform).transform.position = GameObject.Find("PlayerCardBLocation").transform.position;
        //Instantiate(card, GameObject.Find("PlayerCardCLocation").transform).transform.position = GameObject.Find("PlayerCardCLocation").transform.position;



        GameObject.Find("PlayerCardALocation").transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { PlayerAttack(cardASkill); });
        if(cardBSkill != null)
            GameObject.Find("PlayerCardBLocation").transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { PlayerAttack(cardBSkill); });
        //GameObject.Find("PlayerCardCLocation").transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { PlayerAttack(cardCSkill); });

        SetCard(GameObject.Find("PlayerCardALocation").transform.GetChild(0).gameObject, cardASkill);
        if(cardBSkill != null)
            SetCard(GameObject.Find("PlayerCardBLocation").transform.GetChild(0).gameObject, cardBSkill);
       // SetCard(GameObject.Find("PlayerCardCLocation").transform.GetChild(0).gameObject, cardCSkill);

        while (!playerHasMoved)
        {
            yield return null;
        }

        playerBattleInventoryBtn.interactable = false;

        UpdateInventoryAttributes();

        Destroy(GameObject.Find("PlayerCardALocation").transform.GetChild(0).gameObject);
        if(cardBSkill != null)
            Destroy(GameObject.Find("PlayerCardBLocation").transform.GetChild(0).gameObject);
        //Destroy(GameObject.Find("PlayerCardCLocation").transform.GetChild(0).gameObject);

    }

    void SetCard(GameObject card, ActiveSkill skill)
    {

        card.GetComponent<AttackContainer>().SetPlayerSkill(skill);

        card.transform.GetChild(0).GetComponent<Text>().text = skill.GetName();
        card.transform.GetChild(1).GetComponent<Text>().text = skill.GetDescription();
        card.transform.GetChild(2).GetComponent<Text>().text = skill.GetMinDamageModifier() + " - " + skill.GetMaxDamageModifier();
        card.transform.GetChild(4).GetComponent<Text>().text = skill.GetAttributeChange().ToString();

        switch(skill.GetAttributeType())
        {
            case ActiveSkill.AttributeType.weapon:
                card.transform.GetChild(6).GetComponent<Text>().text = "Weapon";
                card.transform.GetChild(2).GetComponent<Text>().text = player.GetWeapon().GetMinDamage() + " - " + player.GetWeapon().GetMaxDamage();
                break;
            case ActiveSkill.AttributeType.health:
                card.transform.GetChild(6).GetComponent<Text>().text = "Health";
                break;
            case ActiveSkill.AttributeType.stamina:
                card.transform.GetChild(6).GetComponent<Text>().text = "Stamina";
                break;
            case ActiveSkill.AttributeType.mana:
                card.transform.GetChild(6).GetComponent<Text>().text = "Mana";
                break;
        }
        
    }

    public void PlayerAttack(ActiveSkill skill)
    {
        playerHasMoved = true;

        switch (skill.GetAttributeType())
        {
            case ActiveSkill.AttributeType.weapon:
                playerDamageOutput = player.GetWeapon().Attack();
                OutputToBattle(String.Format(skill.GetActionMessage(), player.GetWeapon().GetName(), playerDamageOutput));
                break;
            case ActiveSkill.AttributeType.health:
                break;
            case ActiveSkill.AttributeType.stamina:
                playerDamageOutput = player.GetWeapon().Attack() + skill.GetDamageModifier();
                OutputToBattle(String.Format(skill.GetActionMessage(), player.GetWeapon().GetName(), playerDamageOutput));
                player.ChangeStamina(-skill.GetAttributeChange());
                break;
            case ActiveSkill.AttributeType.mana:
                break;
        }
        
    }

    public void EnemyMove()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<EnemyMovement>().Move();
        }
    }

    void EnemyAttack(Enemy enemy)
    {
        EnemyAttackType attack = enemy.GetAttack();

        if (attack == null)
        {
            enemyDamageOutput = (int)enemy.GetBaseDamage();
            OutputToBattle(String.Format("{0} has attacked, dealing {1} damage.", enemy.GetName(), enemyDamageOutput));
        }
        else
        {
            if (attack.GetAttackType() == EnemyAttackType.AttackType.heal)
            {
                enemyDamageOutput = 0;

                int enemyHealRate = attack.GetDamageModifier();

                enemy.ChangeHealth(enemyHealRate);
                enemy.ChangeMana(-attack.GetManaCost());

                OutputToBattle(String.Format("{0} has healed {1} damage.", enemy.GetName(), enemyHealRate));
            }
            else if (attack.GetAttackAttribute() == EnemyAttackType.AttackAttribute.mana)
            {
                enemyDamageOutput = attack.GetDamageModifier() + (int)enemy.GetBaseDamage();

                enemy.ChangeMana(-attack.GetManaCost());

                OutputToBattle(String.Format("{0} has cast {1}, dealing {2} damage.", enemy.GetName(), attack.GetName(), enemyDamageOutput));
            }
            else if (attack.GetAttackAttribute() == EnemyAttackType.AttackAttribute.stamina)
            {
                enemyDamageOutput = attack.GetDamageModifier() + (int)enemy.GetBaseDamage();

                enemy.ChangeStamina(-attack.GetStaminaCost());

                OutputToBattle(String.Format("{0} performed a {1}, dealing {2} damage.", enemy.GetName(), attack.GetName(), enemyDamageOutput));
            }
        }
    }

    void PopulateLocationChests()
    {
        Debug.Log(String.Format("Populating Chests for {0}.", player.GetLocation().GetName()));
        //ChestInventory[] chests = GameObject.Find("ChestList").GetComponentsInChildren<ChestInventory>();
        GameObject[] chests = GameObject.FindGameObjectsWithTag("Chest");
        foreach (GameObject chest in chests)
        {
            Debug.Log(String.Format("Populating {0}", chest.name));
            ChestInventory ch = chest.GetComponent<ChestInventory>();

            if (!ch.HasBeenSearched())
            {
                List<Item> items = new List<Item>();
                List<int> counts = new List<int>();
                player.GetLocation().GetLootTable().ItemDrop(ref items, ref counts);
                
                for (int i = 0; i < counts.Count; i++)
                {
                    int x = counts[i];

                    while (x > 0)
                    {
                        ch.AddItem(items[i]);
                        x--;
                    }
                }

                if (ch.ContainsKey())
                    ch.AddItem(key);
            }
        }

        chests = null;
    }

    public void OpenChest(ChestInventory chest, bool x = true)
    {
        if (x)
        {
            List<Item> items = chest.GetItems();
            foreach (Item item in items)
            {
                AddToPickup(item);
            }
            activeChest = chest;
            searchBtn.gameObject.SetActive(true);
            isInChest = true;
        }
        else
        {
            searchBtn.gameObject.SetActive(false);
            isInChest = false;

            List<Item> items = new List<Item>();

            foreach (KeyValuePair<uint, GameObject> kvp in uiPickupSlots)
            {
                int y = kvp.Value.GetComponent<ItemContainer>().GetCount();

                while (y > 0)
                {
                    items.Add(kvp.Value.GetComponent<ItemContainer>().GetItem());
                    y--;
                }
            }
            foreach (Item item in items)
            {
                RemoveFromPickup(item);
            }

            
        }
    }

    void SpawnEnemies()
    {
        if (player.GetLocation().GetMaxEnemySpawn() > 0)
        {
            List<GameObject> nodes = GameObject.FindGameObjectsWithTag("NodeType").ToList<GameObject>();
            List<Enemy> spawnEnemies = player.GetLocation().GetEnemyTable().EnemySpawn(player.GetLocation());

            foreach (Enemy enemy in spawnEnemies)
            {
                GameObject eGo = Instantiate(enemyObject, GameObject.Find("EnemyList").transform);

                int i = UnityEngine.Random.Range(0, nodes.Count); Debug.Log("Random Node Count: " + i);
                if (i == 0)
                {
                    eGo.GetComponent<EnemyMovement>().SetCurrentNode(nodes[i].GetComponent<NodeType>());
                    eGo.GetComponent<EnemyMovement>().SetPreviousNode(nodes[nodes.Count - 1].GetComponent<NodeType>());
                }
                else
                {
                    eGo.GetComponent<EnemyMovement>().SetCurrentNode(nodes[i].GetComponent<NodeType>());
                    eGo.GetComponent<EnemyMovement>().SetPreviousNode(nodes[i - 1].GetComponent<NodeType>());
                }

                eGo.GetComponent<EnemyMovement>().MoveToNode(nodes[i].GetComponent<NodeType>());
                eGo.GetComponent<EnemyMovement>().SetEnemy(Instantiate(enemy));
                eGo.GetComponent<Renderer>().material = enemy.GetMaterial();
            }
        }
    }

    public void EnterLocation(Location location)
    {
        StartCoroutine(LoadScene(location));
    }

    void SetPlayerLocation(Location location)
    {

    }

    void CheckLocationQuestCompletion()
    {
        foreach(KeyValuePair<uint, GameObject> kvp in uiQuestSlots)
        {
            if(kvp.Value.GetComponent<QuestContainer>().GetQuest().GetQuestType() == Quest.QuestType.Location)
            {
                LocationQuest locationQuest = (LocationQuest)kvp.Value.GetComponent<QuestContainer>().GetQuest();
                if(locationQuest.CheckQuestCompletion(player))
                {
                    kvp.Value.GetComponent<QuestContainer>().SetCompletion(true);
                    if (activeQuest != null && (activeQuest.GetID() == locationQuest.GetID()))
                        turnInQuestBtn.interactable = true;
                }
            }
        }
    }

    public void AddToQuestList(Quest quest)
    {
        if(!player.CheckForQuest(quest.GetID()))
        {
            GameObject questSlot = GameObject.Instantiate(uiQuestSlot, questPanel.transform);
            questSlot.GetComponent<QuestContainer>().SetQuest(quest);
            questSlot.GetComponent<Button>().onClick.AddListener(() => DisplayQuest(questSlot.GetComponent<QuestContainer>()));
            uiQuestSlots.Add(quest.GetID(), questSlot);
            player.AddQuest(quest);
        }
           
    }

    /// <summary>
    /// Adds Item to uiInvSlots.
    /// </summary>
    /// <param name="item"></param>
    public void AddToInventory(Item item)
    {
        // Checks to see if player is overencumbered
        if (player.GetCurrentWeight() < player.GetMaxWeight())
        {
            // If the item does not exist in the inventory...
            if (!player.GetInventory().CheckForItem(item.GetID()))
            {
                // Creates a new invSlot clone.
                GameObject invSlot = GameObject.Instantiate(uiInvSlot, invPanel.transform);
                // Sets the ItemContainer component of new invSlot to item
                invSlot.GetComponent<ItemContainer>().SetItem(item);
                // Adds new onClick command to invSlot's button component
                invSlot.GetComponent<Button>().onClick.AddListener(() => DisplayItem(invSlot.GetComponent<ItemContainer>()));
                // adds invSlot to invSlots
                uiInvSlots.Add(item.GetID(), invSlot);
                // adds item to player's inventory
                player.GetInventory().AddToInventory(item);

                OrderDictionary(uiInvSlots);
            }
            // Else if it does exist in the inventory
            else if (player.GetInventory().CheckForItem(item.GetID()))
            {
                uiInvSlots[item.GetID()].GetComponent<ItemContainer>().AddItem();
                player.GetInventory().AddToInventory(item);
            }

            SetCurrentWeight();
        }
        invScroll.verticalNormalizedPosition = 1;
    }

    void LoadEquipmentSlots()
    {
        if (player.GetWeapon() != NULL_WEAPON)
            weaponBtn.GetComponent<ItemContainer>().SetItem(player.GetWeapon());
        if (player.GetHead() != NULL_ARMOR)
            headBtn.GetComponent<ItemContainer>().SetItem(player.GetHead());
        if (player.GetChest() != NULL_ARMOR)
            chestBtn.GetComponent<ItemContainer>().SetItem(player.GetChest());
        if (player.GetLegs() != NULL_ARMOR)
            legsBtn.GetComponent<ItemContainer>().SetItem(player.GetLegs());
        if (player.GetFeet() != NULL_ARMOR)
            feetBtn.GetComponent<ItemContainer>().SetItem(player.GetFeet());
        if (player.GetHands() != NULL_ARMOR)
            handsBtn.GetComponent<ItemContainer>().SetItem(player.GetHands());
    }

    public void LoadInventorySlot(Item item, uint count)
    {
        while (count > 0)
        {

            if (!uiInvSlots.ContainsKey(item.GetID()))
            {
                // Creates a new invSlot clone.
                GameObject invSlot = GameObject.Instantiate(uiInvSlot, invPanel.transform);
                // Sets the ItemContainer component of new invSlot to item
                invSlot.GetComponent<ItemContainer>().SetItem(item);
                // Adds new onClick command to invSlot's button component
                invSlot.GetComponent<Button>().onClick.AddListener(() => DisplayItem(invSlot.GetComponent<ItemContainer>()));
                // adds invSlot to invSlots
                uiInvSlots.Add(item.GetID(), invSlot);
                // adds item to player's inventory
                OrderDictionary(uiInvSlots);
            }
            else
            {
                uiInvSlots[item.GetID()].GetComponent<ItemContainer>().AddItem();
            }

            count--;
            SetCurrentWeight();
        }
        invScroll.verticalNormalizedPosition = 1;
    }

    public void ClearInventorySlots()
    {
        foreach (KeyValuePair<uint, GameObject> kvp in uiInvSlots)
        {
            GameObject.Destroy(kvp.Value);
        }

        uiInvSlots.Clear();
    }

    void GenerateItemPickup(ItemLootTable itemLootTable)
    {
        List<Item> items = new List<Item>();
        List<int> counts = new List<int>();

        itemLootTable.ItemDrop(ref items, ref counts);

        for (int i = 0; i < items.Count; i++)
        {
            while (counts[i] > 0)
            {
                AddToPickup(items[i]);
                counts[i]--;
            }
        }
    }

    void AddToPickup(Item item)
    {
        if (!uiPickupSlots.ContainsKey(item.GetID()))
        {
            GameObject pickupSlot = GameObject.Instantiate(uiInvSlot, pickupPanel.transform);

            pickupSlot.GetComponent<ItemContainer>().SetItem(item);

            pickupSlot.GetComponent<Button>().onClick.AddListener(() => DisplayItem(pickupSlot.GetComponent<ItemContainer>()));

            uiPickupSlots.Add(item.GetID(), pickupSlot);

            OrderDictionary(uiPickupSlots);
        }
        else
        {
            uiPickupSlots[item.GetID()].GetComponent<ItemContainer>().AddItem();
        }

        pickupScroll.verticalNormalizedPosition = 1;
    }

    public void DropItem()
    {
        if (!isInPickup)
        {
            if (player.GetInventory().CheckForItem(activeItem.GetID()))
                RemoveFromInventory(activeItem);
            else
            {
                Item x = activeItem;
                UnequipItem();
                RemoveFromInventory(x);
            }
        }
        else
        {
            AddToPickup(activeItem);
            if (isInChest)
                activeChest.AddItem(activeItem);
            RemoveFromInventory(activeItem);
        }
    }

    public void PickupItem()
    {
        RemoveFromPickup(activeItem);
    }

    public void RemoveFromInventory(Item item)
    {
        player.GetInventory().RemoveItem(item.GetID());
        uiInvSlots[item.GetID()].GetComponent<ItemContainer>().RemoveItem();

        if(uiInvSlots[item.GetID()].GetComponent<ItemContainer>().IsEmpty() || !player.GetInventory().CheckForItem(item.GetID()))
        {
            GameObject invSlot = uiInvSlots[item.GetID()];

            uiInvSlots.Remove(item.GetID());
            GameObject.Destroy(invSlot);

            DeactivateInvSelection();
            OrderDictionary(uiInvSlots);
        }

        SetCurrentWeight();

        invScroll.verticalNormalizedPosition = 1;
    }

    public void RemoveFromPickup(Item item)
    {
        uiPickupSlots[item.GetID()].GetComponent<ItemContainer>().RemoveItem();
        if(uiPickupSlots[item.GetID()].GetComponent<ItemContainer>().IsEmpty())
        {
            GameObject pickupSlot = uiPickupSlots[item.GetID()];

            uiPickupSlots.Remove(item.GetID());
            GameObject.Destroy(pickupSlot);

            DeactivateInvSelection();
            OrderDictionary(uiPickupSlots);
        }

        if (isInPickup)
        {
            AddToInventory(item);
            if (isInChest)
                activeChest.RemoveItem(item);
        }

        pickupScroll.verticalNormalizedPosition = 1;
    }

    void OrderDictionary(SortedDictionary<uint, GameObject> dictionary)
    {
        List<GameObject> gameObjects = new List<GameObject>();

        foreach(KeyValuePair<uint, GameObject> kvp in dictionary)
        {
            gameObjects.Add(kvp.Value);
        }

        gameObjects.Sort((x, y) => String.Compare(x.GetComponent<ItemContainer>().GetItem().GetName(), y.GetComponent<ItemContainer>().GetItem().GetName()));

        for(int i = 0; i < gameObjects.Count; i++)
        {
            gameObjects[i].transform.SetSiblingIndex(i);
        }
    }

    public void DisplayItem(ItemContainer itemContainer)
    {
        if (!isInPickup)
        {
            if (itemContainer.GetItem() != NULL_ITEM && itemContainer.GetItem() != NULL_WEAPON && itemContainer.GetItem() != NULL_ARMOR)
            {
                activeItem = itemContainer.GetItem();
                dropItemBtn.interactable = true;

                nameTxt.text = activeItem.GetName();
                descriptionTxt.text = activeItem.GetDescription();
                valueTxt.text = activeItem.GetValue().ToString();
                weightTxt.text = activeItem.GetWeight().ToString();

                if (itemContainer.GetItem().IsWeapon())
                {
                    itemDamageObj.SetActive(true);
                    itemArmorObj.SetActive(false);
                    itemConsumableObj.SetActive(false);
                    useItemBtn.gameObject.SetActive(false);

                    if (itemContainer.IsWeaponContainer)
                        unequipItemBtn.gameObject.SetActive(true);
                    else
                        equipItemBtn.gameObject.SetActive(true);

                    Weapon w = (Weapon)itemContainer.GetItem();
                    itemDamageObj.GetComponentInChildren<Text>().text = w.GetMinDamage().ToString() + " - " + w.GetMaxDamage().ToString();
                }
                else if (itemContainer.GetItem().IsArmor())
                {
                    itemDamageObj.SetActive(false);
                    itemArmorObj.SetActive(true);
                    itemConsumableObj.SetActive(false);
                    useItemBtn.gameObject.SetActive(false);

                    if (itemContainer.IsArmorContainer)
                        unequipItemBtn.gameObject.SetActive(true);
                    else
                        equipItemBtn.gameObject.SetActive(true);

                    Armor a = (Armor)itemContainer.GetItem();
                    itemArmorObj.GetComponentInChildren<Text>().text = a.GetRating().ToString();
                }
                else if (itemContainer.GetItem().IsConsumable())
                {
                    itemDamageObj.SetActive(false);
                    itemArmorObj.SetActive(false);
                    itemConsumableObj.SetActive(true);
                    useItemBtn.gameObject.SetActive(true);
                    equipItemBtn.gameObject.SetActive(false);
                    Consumable c = (Consumable)itemContainer.GetItem();
                    itemConsumableObj.GetComponentInChildren<Text>().text = c.GetStatChange().ToString();
                }
                else
                {
                    itemDamageObj.SetActive(false);
                    itemArmorObj.SetActive(false);
                    itemConsumableObj.SetActive(false);
                    useItemBtn.gameObject.SetActive(false);
                    equipItemBtn.gameObject.SetActive(false);
                    unequipItemBtn.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            Debug.Log("Is In Pickup");
            if (itemContainer.GetItem() != NULL_ITEM && itemContainer.GetItem() != NULL_WEAPON && itemContainer.GetItem() != NULL_ARMOR)
            {
                activeItem = itemContainer.GetItem();
                dropItemBtn.interactable = true;

                pickUpNameTxt.text = activeItem.GetName();
                pickUpDescriptionTxt.text = activeItem.GetDescription();
                pickUpValueTxt.text = activeItem.GetValue().ToString();
                pickUpWeightTxt.text = activeItem.GetWeight().ToString();

                if (itemContainer.GetItem().IsWeapon())
                {
                    pickUpDamageObj.SetActive(true);
                    pickUpArmorObj.SetActive(false);
                    pickUpConsumableObj.SetActive(false);

                    Weapon w = (Weapon)itemContainer.GetItem();
                    //pickUpDamageObj.GetComponentInChildren<Text>().text = w.GetDamage().ToString();
                }
                else if (itemContainer.GetItem().IsArmor())
                {
                    pickUpDamageObj.SetActive(false);
                    pickUpArmorObj.SetActive(true);
                    pickUpConsumableObj.SetActive(false);

                    Armor a = (Armor)itemContainer.GetItem();
                    pickUpArmorObj.GetComponentInChildren<Text>().text = a.GetRating().ToString();
                }
                else if (itemContainer.GetItem().IsConsumable())
                {
                    pickUpDamageObj.SetActive(false);
                    pickUpArmorObj.SetActive(false);
                    pickUpConsumableObj.SetActive(true);

                    Consumable c = (Consumable)itemContainer.GetItem();
                    pickUpConsumableObj.GetComponentInChildren<Text>().text = c.GetStatChange().ToString();
                }
                else
                {
                    itemDamageObj.SetActive(false);
                    itemArmorObj.SetActive(false);
                    itemConsumableObj.SetActive(false);
                }
            }
        }
    }

    public void DisplayQuest(QuestContainer questContainer)
    {
        selectedQuest = questContainer.GetQuest();
        makeActiveBtn.interactable = true;
        selQuestName.text = selectedQuest.GetName();
        selQuestDescription.text = selectedQuest.GetDescription();
    }

    public void DisplayEnemy(EnemyContainer enemyContainer)
    {
        selectedEnemy = enemyContainer.GetEnemy();
        enemyName.text = enemyContainer.GetEnemy().GetName();
        enemyDescription.text = enemyContainer.GetEnemy().GetDescription();
        enemyHealth.text = enemyContainer.GetEnemy().GetMaxHealth().ToString();
        enemyDamage.text = enemyContainer.GetEnemy().GetMaxDamage().ToString();
    }

    public void DisplaySkill(SkillContainer skillContainer)
    {
        selectedSkill = SkillDictionary[skillContainer.GetSkill().GetName()];

        skillNameTxt.text = selectedSkill.GetName();
        skillDescriptionTxt.text = selectedSkill.GetDescription();
        skillCostTxt.text = ((ActiveSkill)selectedSkill).GetAttributeChange().ToString();
        skillDamageTxt.text = ((ActiveSkill)selectedSkill).GetMinDamageModifier().ToString() + " - " + ((ActiveSkill)selectedSkill).GetMaxDamageModifier().ToString();
  
        if (player.GetSkillPoints() > 0 && !selectedSkill.IsUnlocked() && selectedSkill.IsUnlockable())
            unlockSkillBtn.interactable = true;
    }

    public void UnlockSkill()
    {
        player.SetSkillPoints(player.GetSkillPoints() - 1);
        player.AddActiveStaminaSkill((ActiveSkill)selectedSkill);
        selectedSkill.SetUnlocked();
        selectedSkill.UnlockNextSkills();

        UpdateExpSliders();
        DeactivateSkillSelection();
    }

    public void MakeQuestActive()
    {
        activeQuest = selectedQuest;
        questName.text = activeQuest.GetName();
        questDescription.text = activeQuest.GetDescription();
        if (uiQuestSlots[activeQuest.GetID()].GetComponent<QuestContainer>().GetCompleted())
            turnInQuestBtn.interactable = true;

        DeactivateQuestSelection();
    }

    public void TurnInQuest()
    {
        Quest quest = uiQuestSlots[activeQuest.GetID()].GetComponent<QuestContainer>().GetQuest();
        OutputToText(String.Format("You have completed {0}.", quest.GetName()));
        GameObject questSlot = uiQuestSlots[quest.GetID()];
        uiQuestSlots.Remove(quest.GetID());
        GameObject.Destroy(questSlot);
        player.AddExp(quest.GetExpReward());
        OutputToText(String.Format("You have earned {0} exp.", quest.GetExpReward()));
        if(quest.HasItemReward())
        {
            List<Item> itemRewards = quest.GetItemRewards();
            foreach (Item item in itemRewards)
                AddToInventory(item);
        }

        DeactivateQuestSelection();
        DeactivateActiveQuest();
    }

    public void ActivateInventoryScreen(bool x)
    {
        UIInventoryScreen.SetActive(x);

        DeactivateInvSelection();
    }

    public void ActivateQuestScreen(bool x)
    {
        UIQuestScreen.SetActive(x);

        DeactivateQuestSelection();
    }

    public void ActivatePickupScreen(bool x, string sourceName = "")
    {
        if (isInChest)
        {
            sourceName = "Chest";
            activeChest.SetHasBeenSearched(true);
        }
        if(x)
        {
            invScroll.transform.SetParent(UIPickupScreen.transform);
            otherPickUpNameTxt.text = sourceName;
            isInPickup = true;
            UIPickupScreen.SetActive(true);
        }
        else
        {
            invScroll.transform.SetParent(UIInventoryScreen.transform);
            isInPickup = false;

            List<Item> items = new List<Item>();

            foreach (KeyValuePair<uint, GameObject> kvp in uiPickupSlots)
            {
                int y = kvp.Value.GetComponent<ItemContainer>().GetCount();

                while (y > 0)
                {
                    items.Add(kvp.Value.GetComponent<ItemContainer>().GetItem());
                    y--;
                }
            }
            foreach (Item item in items)
            {
                RemoveFromPickup(item);
            }

            uiPickupSlots.Clear();
            UIPickupScreen.SetActive(false);
            if (isInChest)
                OpenChest(activeChest);
        }
    }

    public void ActivateArenaScreen(bool x)
    {
        UIArenaScreen.SetActive(x);

        enemyAName.text = enemyA.GetComponent<EnemyContainer>().GetEnemy().GetName();
        enemyBName.text = enemyB.GetComponent<EnemyContainer>().GetEnemy().GetName();
        enemyCName.text = enemyC.GetComponent<EnemyContainer>().GetEnemy().GetName();

        GameObject.Find("Player").transform.position = new Vector3(0, 2.2f, 10);
        GameObject.Find("Player").transform.transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    public void ActivateSkillScreen(bool x)
    {
        DeactivateInvSelection();
        DeactivateSkillSelection();

        UIInventoryScreen.SetActive(!x);
        UISkillScreen.SetActive(x);
    }

    public void UseItem()
    {
        Consumable c = (Consumable)activeItem;
        c.UseItem(player);
        RemoveFromInventory(c);

        switch(c.GetConsumableType())
        {
            case Consumable.ConsumableType.healthPotion:
                UpdateHealthSliders();
                break;
            case Consumable.ConsumableType.staminaPotion:
                UpdateStaminaSliders();
                break;
            case Consumable.ConsumableType.manaPotion:
                UpdateManaSliders();
                break;
            case Consumable.ConsumableType.food:
                UpdateHealthSliders();
                break;
            case Consumable.ConsumableType.drink:
                
                break;
        }

        if(isInBattle)
        {
            DeactivateInvSelection();
            OutputToBattle(String.Format("Player has drank {0}.", c.GetName()));
            UIInventoryScreen.SetActive(false);
            playerHasMoved = true;
        }
    }

    public void EquipItem()
    {
        if(activeItem.IsWeapon())
        {
            Weapon w = (Weapon)activeItem;
            Weapon x = (Weapon)weaponBtn.gameObject.GetComponent<ItemContainer>().GetItem();
            if (x == NULL_WEAPON)
            {
                player.EquipWeapon(w);
                weaponBtn.gameObject.GetComponent<ItemContainer>().SetItem(w);
                RemoveFromInventory(w);
            }
            else
            {
                AddToInventory(x);
                player.EquipWeapon(w);
                weaponBtn.gameObject.GetComponent<ItemContainer>().SetItem(w);
                RemoveFromInventory(w);
            }
        }
        else if(activeItem.IsArmor())
        {
            Armor a = (Armor)activeItem;
            switch(a.GetArmorType())
            {
                case Armor.ArmorType.head:
                    Armor h = (Armor)headBtn.gameObject.GetComponent<ItemContainer>().GetItem();
                    if(h == NULL_ARMOR)
                    {
                        player.EquipArmor(a);
                        headBtn.gameObject.GetComponent<ItemContainer>().SetItem(a);
                        RemoveFromInventory(a);
                    }
                    else
                    {
                        AddToInventory(h);
                        player.EquipArmor(a);
                        headBtn.gameObject.GetComponent<ItemContainer>().SetItem(a);
                        RemoveFromInventory(a);
                    }
                    break;
                case Armor.ArmorType.chest:
                    Armor c = (Armor)chestBtn.gameObject.GetComponent<ItemContainer>().GetItem();
                    if (c == NULL_ARMOR)
                    {
                        player.EquipArmor(a);
                        chestBtn.gameObject.GetComponent<ItemContainer>().SetItem(a);
                        RemoveFromInventory(a);
                    }
                    else
                    {
                        AddToInventory(c);
                        player.EquipArmor(a);
                        chestBtn.gameObject.GetComponent<ItemContainer>().SetItem(a);
                        RemoveFromInventory(a);
                    }
                    break;
                case Armor.ArmorType.legs:
                    Armor l = (Armor)headBtn.gameObject.GetComponent<ItemContainer>().GetItem();
                    if (l == NULL_ARMOR)
                    {
                        player.EquipArmor(a);
                        legsBtn.gameObject.GetComponent<ItemContainer>().SetItem(a);
                        RemoveFromInventory(a);
                    }
                    else
                    {
                        AddToInventory(l);
                        player.EquipArmor(a);
                        legsBtn.gameObject.GetComponent<ItemContainer>().SetItem(a);
                        RemoveFromInventory(a);
                    }
                    break;
                case Armor.ArmorType.feet:
                    Armor f = (Armor)headBtn.gameObject.GetComponent<ItemContainer>().GetItem();
                    if (f == NULL_ARMOR)
                    {
                        player.EquipArmor(a);
                        feetBtn.gameObject.GetComponent<ItemContainer>().SetItem(a);
                        RemoveFromInventory(a);
                    }
                    else
                    {
                        AddToInventory(f);
                        player.EquipArmor(a);
                        feetBtn.gameObject.GetComponent<ItemContainer>().SetItem(a);
                        RemoveFromInventory(a);
                    }
                    break;
                case Armor.ArmorType.hands:
                    Armor n = (Armor)headBtn.gameObject.GetComponent<ItemContainer>().GetItem();
                    if (n == NULL_ARMOR)
                    {
                        player.EquipArmor(a);
                        handsBtn.gameObject.GetComponent<ItemContainer>().SetItem(a);
                        RemoveFromInventory(a);
                    }
                    else
                    {
                        AddToInventory(n);
                        player.EquipArmor(a);
                        handsBtn.gameObject.GetComponent<ItemContainer>().SetItem(a);
                        RemoveFromInventory(a);
                    }
                    break;
            }
        }
        DeactivateInvSelection();
    }

    public void UnequipItem()
    {
        if(activeItem.IsWeapon())
        {
            Weapon w = (Weapon)activeItem;
            player.UnequipWeapon();
            weaponBtn.gameObject.GetComponent<ItemContainer>().RemoveItem();
            AddToInventory(w);
        }
        else if(activeItem.IsArmor())
        {
            Armor a = (Armor)activeItem;
            player.UnequipArmor(a);
            switch(a.GetArmorType())
            {
                case Armor.ArmorType.head:
                    headBtn.gameObject.GetComponent<ItemContainer>().RemoveItem();
                    break;
                case Armor.ArmorType.chest:
                    chestBtn.gameObject.GetComponent<ItemContainer>().RemoveItem();
                    break;
                case Armor.ArmorType.legs:
                    legsBtn.gameObject.GetComponent<ItemContainer>().RemoveItem();
                    break;
                case Armor.ArmorType.feet:
                    feetBtn.gameObject.GetComponent<ItemContainer>().RemoveItem();
                    break;
                case Armor.ArmorType.hands:
                    handsBtn.gameObject.GetComponent<ItemContainer>().RemoveItem();
                    break;
            }
            AddToInventory(a);
        }
        DeactivateInvSelection();
    }

    void DeactivateInvSelection()
    {
        nameTxt.text = "";
        descriptionTxt.text = "";
        valueTxt.text = "";
        weightTxt.text = "";

        pickUpNameTxt.text = "";
        pickUpDescriptionTxt.text = "";
        pickUpValueTxt.text = "";
        pickUpWeightTxt.text = "";

        itemDamageObj.SetActive(false);
        itemArmorObj.SetActive(false);
        itemConsumableObj.SetActive(false);
        useItemBtn.gameObject.SetActive(false);
        equipItemBtn.gameObject.SetActive(false);
        unequipItemBtn.gameObject.SetActive(false);
        pickUpDamageObj.SetActive(false);
        pickUpArmorObj.SetActive(false);
        pickUpConsumableObj.SetActive(false);

        activeItem = null;
        dropItemBtn.interactable = false;
    }

    void DeactivateQuestSelection()
    {
        selectedQuest = null;
        selQuestDescription.text = "";
        selQuestName.text = "";

        makeActiveBtn.interactable = false;
    }

    void DeactivateActiveQuest()
    {
        activeQuest = null;
        questName.text = "";
        questDescription.text = "";

        turnInQuestBtn.interactable = false;
    }

    void DeactivateSkillSelection()
    {
        selectedSkill = null;

        skillNameTxt.text = "";
        skillDescriptionTxt.text = "";
        skillCostTxt.text = "";
        skillDamageTxt.text = "";

        unlockSkillBtn.interactable = false;
    }

    public void SetActiveDoor(DoorInteraction door, bool x = true)
    {
        if (x)
        {
            openBtn.gameObject.SetActive(true);
            activeDoor = door;
        }
        else
        {
            openBtn.gameObject.SetActive(false);
            activeDoor = null;
        }
    }

    public void OpenDoor()
    {
        if (activeDoor.IsLocked())
        {
            if (player.GetInventory().CheckForItem(0))
            {
                RemoveFromInventory(player.GetInventory().GetItem(0));
                activeDoor.OpenDoor();
                SetActiveDoor(activeDoor, false);
            }
            else
                OutputToText("Door is locked!");
        }
        else
        {
            activeDoor.OpenDoor();
            SetActiveDoor(activeDoor, false);
        }
    }
    
    void ChangeHealth(int health)
    {
        player.ChangeHealth(health);
        UpdateHealthSliders();
    }

    void ChangeStamina(int stamina)
    {
        player.ChangeStamina(stamina);
        UpdateStaminaSliders();
    }

    void ChangeMana(int mana)
    {
        player.ChangeMana(mana);
        UpdateManaSliders();
    }

    public void OutputToText(string output)
    {
        outputTxt.text += output + "\n";
        mainGameScroll.verticalNormalizedPosition = 0;
    }

    public void OutputToBattle(string output)
    {
        battleOutputTxt.text += output + "\n";
        
    }

    void UpdateInventoryAttributes()
    {
        UpdateHealthSliders();
        UpdateStaminaSliders();
        UpdateManaSliders();

        UpdateExpSliders();

        SetCurrentWeight();
        goldTxt.text = player.GetGold().ToString();        
    }

    void UpdateExpSliders()
    {
        invExpSlider.maxValue = player.GetToLevelExp();
        invExpSlider.value = player.GetExp();
        invTotalExpTxt.text = player.GetTotalExp().ToString();
        invSkillPointsTxt.text = player.GetSkillPoints().ToString();
        invLevelTxt.text = player.GetLevel().ToString();
        expTxt.text = player.GetExp() + "/" + player.GetToLevelExp();
    }

    void UpdateHealthSliders()
    {
        invHealthSlider.maxValue = player.GetMaxHealth();
        invHealthSlider.value = player.GetHealth();
        invHealthTxt.text = player.GetHealth() + "/" + player.GetMaxHealth();
    }
    
    void UpdateStaminaSliders()
    {
        invStaminaSlider.maxValue = player.GetMaxStamina();
        invStaminaSlider.value = player.GetStamina();
        invStaminaTxt.text = player.GetStamina() + "/" + player.GetMaxStamina();
    }

    void UpdateManaSliders()
    {
        invManaSlider.maxValue = player.GetMaxMana();
        invManaSlider.value = player.GetMana();
        invManaTxt.text = player.GetMana() + "/" + player.GetMaxMana();
    }

    void SetCurrentWeight()
    {
        uint weight = player.GetInventory().GetTotalWeight();

        if(player.GetWeapon() != NULL_WEAPON)
            weight += player.GetWeapon().GetWeight();
        if (player.GetHead() != NULL_ARMOR)
            weight += player.GetHead().GetWeight();
        if (player.GetChest() != NULL_ARMOR)
            weight += player.GetChest().GetWeight();
        if (player.GetLegs() != NULL_ARMOR)
            weight += player.GetLegs().GetWeight();
        if (player.GetFeet() != NULL_ARMOR)
            weight += player.GetFeet().GetWeight();
        if (player.GetHands() != NULL_ARMOR)
            weight += player.GetHands().GetWeight();

        player.SetCurrentWeight(weight);

        totalInvWeightTxt.text = String.Format("{0}/{1}", player.GetCurrentWeight(), player.GetMaxWeight());
    }

    SaveLoad NewSaveLoadObject()
    {
        SaveLoad saveLoad = new SaveLoad();

        saveLoad.SavePlayer(player);

        return saveLoad;
    }

    public void SaveGame()
    {
        SaveLoad save = NewSaveLoadObject();

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/save.tpg");
        bf.Serialize(file, save);
        file.Close();

        StartCoroutine(LoadToMain());
    }

    IEnumerator LoadToMain()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        AsyncOperation newScene = SceneManager.LoadSceneAsync(0, LoadSceneMode.Additive);
        newScene.allowSceneActivation = false;

        while (newScene.progress < 0.9f)
        {
            //Debug.Log("Loading scene: " + newScene.progress);
            yield return null;
        }

        newScene.allowSceneActivation = true;

        while (!newScene.isDone)
        {
            yield return null;
        }

        Scene thisScene = SceneManager.GetSceneByName("MainMenu");

        if (thisScene.IsValid())
        {
            SceneManager.SetActiveScene(thisScene);
        }

        AsyncOperation closeScene = SceneManager.UnloadSceneAsync(currentScene);

        while (!closeScene.isDone)
        {
            yield return null;
        }

    }

    IEnumerator LoadStartScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        //GameObject player = GameObject.Find("Player");
        AsyncOperation newScene = SceneManager.LoadSceneAsync("Floor1", LoadSceneMode.Additive);
        newScene.allowSceneActivation = false;

        while (newScene.progress < 0.9f)
        {
            //Debug.Log("Loading scene: " + newScene.progress);
            yield return null;
        }

        newScene.allowSceneActivation = true;

        while (!newScene.isDone)
        {
            yield return null;
        }

        Scene thisScene = SceneManager.GetSceneByName("Floor1");

        if (thisScene.IsValid())
        {
            SceneManager.MoveGameObjectToScene(playerObject, thisScene);
            SceneManager.MoveGameObjectToScene(UIRoot, thisScene);
            SceneManager.MoveGameObjectToScene(gameManager, thisScene);
            SceneManager.SetActiveScene(thisScene);
            playerObject.transform.position = floor1.GetSpawnLocation();
            playerObject.transform.rotation = floor1.GetSpawnRotation();

            locationNameTxt.text = "Floor 1";
            player.SetLocation(floor1);

        }
        AsyncOperation closeScene = SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("LoadScene"));

        while (!closeScene.isDone)
        {
            yield return null;
        }

        PopulateLocationChests();
        SpawnEnemies();

    }

    IEnumerator LoadScene(Location location)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        //GameObject player = GameObject.Find("Player");
        AsyncOperation newScene = SceneManager.LoadSceneAsync(location.GetSceneName(), LoadSceneMode.Additive);
        newScene.allowSceneActivation = false;
        UILoadScreen.SetActive(true);

        while (newScene.progress < 0.9f)
        {
            loadingSlider.value = newScene.progress;
            loadingPercentTxt.text = ((int)(newScene.progress * 100)) + "%";
            yield return null;
        }

        newScene.allowSceneActivation = true;

        while (!newScene.isDone)
        {
            loadingSlider.value = newScene.progress;
            loadingPercentTxt.text = ((int)(newScene.progress * 100)) + "%";
            yield return null;
        }

        Scene thisScene = SceneManager.GetSceneByName(location.GetSceneName());

        if (thisScene.IsValid())
        {

            SceneManager.MoveGameObjectToScene(playerObject, thisScene);
            SceneManager.MoveGameObjectToScene(UIRoot, thisScene);
            SceneManager.MoveGameObjectToScene(gameManager, thisScene);
            SceneManager.SetActiveScene(thisScene);

            player.SetLocation(location);
            locationNameTxt.text = location.GetName();
        }

        AsyncOperation closeScene = SceneManager.UnloadSceneAsync(currentScene);

        while (!closeScene.isDone)
        {
            yield return null;
        }

        PopulateLocationChests();
        SpawnEnemies();
        CheckLocationQuestCompletion();

        playerObject.transform.position = location.GetSpawnLocation();
        playerObject.transform.rotation = location.GetSpawnRotation();

        UILoadScreen.SetActive(false);

    }
}

