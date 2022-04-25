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

public class Engine : MonoBehaviour
{

    DialogueManager dialogueManager;
    BattleManager battleManager;
    DungeonManager dungeonManager;
    EventManager eventManager;

    // UI Variables
    public GameObject UIRoot;
    GameObject UIInventoryScreen;
    GameObject UIDirectionScreen;
    GameObject UIQuestScreen;

    GameObject UIPickupScreen;
    GameObject UIArenaScreen;
    GameObject UISkillScreen;
    GameObject UIStatsScreen;

    GameObject UILoadScreen;
    GameObject UIPauseScreen;
    GameObject UICoverScreen;
    GameObject UIRatingScreen;



    // Directional UI Variables

    Slider directionalHealthSlider;
    Slider directionalStaminaSlider;
    Slider directionalManaSlider;

    Queue<string> outputQueue;



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

    GameObject itemWeightObj;
    GameObject itemValueObj;
    GameObject itemDamageObj;
    GameObject itemArmorObj;
    GameObject itemConsumableObj;

    Text nameTxt;
    Text descriptionTxt;
    Text valueTxt;
    Text weightTxt;
    Text totalInvWeightTxt;
    Text goldTxt;
    Text armorValueTxt;
    Text damageValueTxt;

    // Quest UI Variables
    public GameObject uiQuestSlot;
    public GameObject uiFetchQuestObject;
    List<GameObject> uiFetchQuestObjects = new List<GameObject>();
    public SortedDictionary<uint, GameObject> uiQuestSlots = new SortedDictionary<uint, GameObject>();
    ScrollRect activeQuestScroll;

    Quest activeQuest;
    Quest selectedQuest;

    Text questName;
    Text questDescription;

    Button makeActiveBtn;
    Button turnInQuestBtn;

    GameObject noActiveQuestTxt;

    public ScrollRect questScroll;
    public GameObject questPanel;
    public GameObject activeQuestPanel;



    // UI Pickup Variables
    Text otherPickUpNameTxt;
    Text playerPickUpNameTxt;
    Text playerPickupWeightTxt;
    Text pickupGoldTxt;

    Text pickUpDescriptionTxt;
    Text pickUpNameTxt;
    Text pickUpWeightTxt;
    Text pickUpValueTxt;
    Text pickUpDamageTxt;
    Text pickUpArmorTxt;
    Text pickUpConsumableTxt;

    GameObject pickUpWeightObj;
    GameObject pickUpValueObj;
    GameObject pickUpDamageObj;
    GameObject pickUpArmorObj;
    GameObject pickUpConsumableObj;

    Button pickupDropItemBtn;
    Button pickupItemBtn;
    Button pickupExitBtn;

    // UI Arena Variables
    Enemy selectedEnemy;

    GameObject enemyABtn;
    GameObject enemyBBtn;
    GameObject enemyCBtn;

    Text enemyANameTxt;
    Text enemyBNameTxt;
    Text enemyCNameTxt;

    Text enemyARewardTxt;
    Text enemyBRewardTxt;
    Text enemyCRewardTxt;

    Text arenaEnemyHealthTxt;
    Text arenaEnemyStaminaTxt;
    Text arenaEnemyManaTxt;
    Text arenaEnemyDamageTxt;
    Text arenaEnemySpeedTxt;

    GameObject arenaEnemyHealthObj;
    GameObject arenaEnemyStaminaObj;
    GameObject arenaEnemyManaObj;
    GameObject arenaEnemyDamageObj;
    GameObject arenaEnemySpeedObj;

    // UI Skill Variables
    public GameObject uiSkillSlot;
    SortedDictionary<uint, GameObject> uiSkillSlots = new SortedDictionary<uint, GameObject>();
    GameObject skillScrollView;
    GameObject skillPanel;

    Skill selectedSkill;

    Image skillImg;
    Text skillNameTxt;
    Text skillDescriptionTxt;
    Text skillTypeTxt;
    Text skillCostTxt;
    Text skillDamageTxt;
    Text skillShortDescriptionTxt;

    Button useSkillBtn;

    // UI Stats Variales
    Text statsPlayerNameTxt;
    Text statsPlayerTitleTxt;
    Text statsHealthTxt;
    Text statsStaminaTxt;
    Text statsManaTxt;
    Text statsExpTxt;
    Text statsLvlTxt;
    Text statsTotalExpTxt;
    Text statsSkillPointsTxt;

    Text statsWeightTxt;
    Text statsGoldTxt;
    Text statsDmgTxt;
    Text statsDefTxt;
    Text statsSpeedTxt;

    Text playerStrengthTxt;
    Text playerAgilityTxt;
    Text playerIntelligenceTxt;
    Text playerLuckTxt;

    GameObject playerStrengthBtn;
    GameObject playerAgilityBtn;
    GameObject playerIntelligenceBtn;
    GameObject playerLuckBtn;

    Slider statsHealthSlider;
    Slider statsStaminaSlider;
    Slider statsManaSlider;
    Slider statsExpSlider;

    // UI Load Variables
    Slider loadingSlider;

    Text loadingPercentTxt;

    // UI Pause Variables
    Slider musicSlider;
    Slider sfxSlider;



    // UI Cover Variables
    Text messageTxt;
    Button deadRestartBtn;
    Button acceptNameBtn;
    InputField nameInputField;

    // Game Variables
    public static SortedDictionary<uint, Item> ItemDictionary;
    public static SortedDictionary<uint, Quest> QuestDictionary;
    public static SortedDictionary<uint, Location> LocationDictionary;
    public static SortedDictionary<uint, Skill> SkillDictionary;
    public static SortedDictionary<uint, Enemy> EnemyDictionary;
    public static SortedDictionary<uint, NPC> NPCDictionary;
    public static SortedDictionary<uint, Store> StoreDictionary;
    //public static SortedDictionary<uint, Dungeon> DungeonDictionary;

    public static Player player;

    public static Item NULL_ITEM;
    public static Weapon NULL_WEAPON;
    public static Armor NULL_ARMOR;

    Item key;
    Item smallFur;
    Item smallSkull;
    Item ratKingsCrown;
    Item luckyPaw;
    Item blueSlimeGoo;
    Item greenSlimeGoo;
    Item redSlimeGoo;
    Item bone;
    Item eyeball;
    Item greenGem;
    Item hornedStaff;
    Item armorPiece;
    Item brokenSword;
    Item goldAmulet;
    Item goldBar;
    Item lockpickSet;
    Item wolfGem;
    Item wolfPelt;
    Item canine;
    Item grimore;
    Item magicRune;
    Item mortar;
    Item splinteredStaff;
    Item ironBar;
    Item humanJaw;
    Item bearPelt;
    Item bearPaw;
    Item magicStone;

    Weapon crookedDagger;

    Weapon copperDagger;
    Weapon copperSword;
    Weapon copperAxe;
    Weapon copperSpear;
    Weapon copperMace;

    Weapon ironDagger;
    Weapon ironSword;
    Weapon ironAxe;
    Weapon ironSpear;
    Weapon ironMace;

    Weapon steelDagger;
    Weapon steelSword;
    Weapon steelAxe;
    Weapon steelSpear;
    Weapon steelMace;

    Weapon electrumDagger;
    Weapon electrumSword;
    Weapon electrumAxe;
    Weapon electrumSpear;
    Weapon electrumMace;

    Weapon royalDagger;
    Weapon royalSword;
    Weapon royalAxe;
    Weapon royalSpear;
    Weapon royalMace;

    Weapon swordOfLight;

    Armor tatteredShirt;

    Armor leatherHelmet;
    Armor leatherShirt;
    Armor leatherPants;
    Armor leatherBoots;
    Armor leatherGloves;

    Armor copperBelt;
    Armor copperBoots;
    Armor copperGloves;
    Armor copperHelmet;
    Armor copperChest;

    Armor silverBelt;
    Armor silverBoots;
    Armor silverGloves;
    Armor silverHelmet;
    Armor silverChest;

    Armor titaniumBelt;
    Armor titaniumBoots;
    Armor titaniumGloves;
    Armor titaniumHelmet;
    Armor titaniumChest;

    Armor heroicBelt;
    Armor heroicBoots;
    Armor heroicGloves;
    Armor heroicHelmet;
    Armor heroicChest;

    Armor ironBelt;
    Armor ironBoots;
    Armor ironGloves;
    Armor ironHelmet;
    Armor ironChest;

    Armor steelBelt;
    Armor steelBoots;
    Armor steelGloves;
    Armor steelHelmet;
    Armor steelChest;

    Armor electrumBelt;
    Armor electrumBoots;
    Armor electrumGloves;
    Armor electrumHelmet;
    Armor electrumChest;

    Armor magneniumBelt;
    Armor magneniumBoots;
    Armor magneniumGloves;
    Armor magneniumHelmet;
    Armor magneniumChest;

    Armor demonicBelt;
    Armor demonicBoots;
    Armor demonicGloves;
    Armor demonicHelmet;
    Armor demonicChest;

    Potion smallHealthPotion;
    Potion smallStaminaPotion;
    Potion smallManaPotion;
    Potion healthPotion;
    Potion staminaPotion;
    Potion manaPotion;
    Potion largeHealthPotion;
    Potion largeStaminaPotion;
    Potion largeManaPotion;
    Potion giantHealthPotion;
    Potion giantStaminaPotion;
    Potion giantManaPotion;

    Edible rottenMeat;
    Edible bearHeart;
    Edible meatChunk;
    Edible smallMeatChunk;

    SkillBook heavySwingBook;
    SkillBook targetedStrikeBook;
    SkillBook masterfulStabBook;
    SkillBook kickBook;
    SkillBook slamBook;
    SkillBook suplexBook;
    SkillBook backhandBook;
    SkillBook pommelThrowBook;
    SkillBook concussiveStrikeBook;
    SkillBook rushBook;
    SkillBook sprintBook;
    SkillBook litheGraceBook;
    SkillBook temperBook;
    SkillBook rageBook;
    SkillBook frenzyBook;
    SkillBook redoubtBook;
    SkillBook bulwarkBook;
    SkillBook bastionBook;
    SkillBook seepBook;
    SkillBook bleedBook;
    SkillBook hemorrhageBook;
    SkillBook fireballBook;
    SkillBook infernoBook;
    SkillBook immolationBook;
    SkillBook frostbiteBook;
    SkillBook iceSpikeBook;
    SkillBook glaciateBook;
    SkillBook sparksBook;
    SkillBook lightningBoltBook;
    SkillBook smiteBook;
    SkillBook mendBook;
    SkillBook healBook;
    SkillBook rejuvenateBook;
    SkillBook abateBook;
    SkillBook allayBook;
    SkillBook dispelBook;
    SkillBook magicArrowBook;
    SkillBook magicBoltBook;
    SkillBook magicMissileBook;

    Location buruns;
    Location arenthiaBuruns;
    Location burunsArenthia;

    FetchQuest fetchRatSkull;
    FetchQuest fetchSlimeEssense;
    FetchQuest fetchRatFur;
    FetchQuest fetchLockpick;
    FetchQuest fetchRatMeat;
    FetchQuest fetchWolfFur;
    FetchQuest fetchWolfMeat;
    FetchQuest fetchArmorPiece;

    SlayQuest slayGiantRat;

    TalkQuest talkTemrikToBlacksmith;
    TalkQuest talkTemrikToAlchemist;
    TalkQuest talkVarianToMysteriousMan;

    ClearQuest clearIronRatCave;
    ClearQuest clearCreogCave;

    Skill slash;
    Skill stab;
    Skill bash;
    Skill cleave;
    Skill cut;
    Skill jab;
    Skill pierce;
    Skill punch;

    Skill blindingLight;
    Skill holyStrike;

    StaminaSkill heavySwing;
    StaminaSkill targetedStrike;
    StaminaSkill masterfulStab;
    StaminaSkill kick;
    StaminaSkill slam;
    StaminaSkill suplex;
    StaminaSkill backhand;
    StaminaSkill pommelThrow;
    StaminaSkill concussiveStrike;
    StaminaSkill rush;
    StaminaSkill sprint;
    StaminaSkill litheGrace;
    StaminaSkill temper;
    StaminaSkill rage;
    StaminaSkill frenzy;
    StaminaSkill redoubt;
    StaminaSkill bulwark;
    StaminaSkill bastion;
    StaminaSkill seep;
    StaminaSkill bleed;
    StaminaSkill hemorrhage;

    ManaSkill fireball;
    ManaSkill inferno;
    ManaSkill immolation;
    ManaSkill frostbite;
    ManaSkill iceSpike;
    ManaSkill glaciate;
    ManaSkill sparks;
    ManaSkill lightningBolt;
    ManaSkill smite;
    ManaSkill mend;
    ManaSkill heal;
    ManaSkill rejuvenate;
    ManaSkill abate;
    ManaSkill allay;
    ManaSkill dispel;
    ManaSkill magicArrow;
    ManaSkill magicBolt;
    ManaSkill magicMissile;

    Enemy smallRat;

    Enemy rat_01;
    Enemy rat_03;
    Enemy rat_05;

    Enemy ratPack_03;
    Enemy ratPack_07;
    Enemy ratPack_10;

    Enemy giantRat;

    BossEnemy ratKing;

    Enemy blueSlime_03;
    Enemy blueSlime_05;

    Enemy greenSlime_03;
    Enemy greenSlime_05;

    Enemy redSlime_03;
    Enemy redSlime_05;

    Enemy amalgamSlime_06;
    Enemy amalgamSlime_10;
    Enemy amalgamSlime_14;
    Enemy amalgamSlime_18;

    Enemy goblinShaman_06;
    Enemy goblinShaman_09;

    Enemy capturedBandit_06;
    Enemy capturedBandit_10;
    Enemy capturedBandit_14;
    Enemy capturedBandit_18;

    Enemy castleGuard;

    Enemy grayWolf_06;
    Enemy grayWolf_09;

    Enemy direWolf_08;
    Enemy direWolf_12;
    Enemy direWolf_16;

    BossEnemy banditChief;

    Enemy gladiator_11;
    Enemy gladiator_14;
    Enemy gladiator_17;

    Enemy warlock_11;
    Enemy warlock_14;

    Enemy witch_11;
    Enemy witch_14;

    BossEnemy veteran;

    Enemy chiefBattlemage;
    Enemy chiefKnight;
    Enemy warWolf;

    BossEnemy arenaGrandmaster;

    Enemy ironRat;
    Enemy weakenedIronRat;
    Enemy brownBear;
    Enemy forestBandit;
    Enemy errantKnight;
    Enemy stunnedErrantKnight;
    Enemy skeleton;
    Enemy weakenedSkeleton;
    Enemy zombie;

    NPC hirgirdBlacksmith;
    NPC inveraAlchemist;
    NPC kiarnilLeathersmith;
    NPC serikInnkeeper;
    NPC varianArmorsmith;
    NPC malina;
    NPC cityGuard;
    NPC castleGuardNPC;
    NPC temrik;
    NPC mysteriousMan;
    NPC talkingGroup;
    NPC telyrid;

    Store heavyAnvil;
    Store litheWarrior;
    Store magicMortar;
    Store theBulwark;
    Store telyridsTomes;

    Dungeon ironRatCave;
    Dungeon deepForest;
    Dungeon creogCave;

    Sprite healthDrop;
    Sprite staminaDrop;
    Sprite manaDrop;
    Sprite physicalDmgSprite;
    Sprite manaDmgSprite;

    // Local Use Variables
    bool isInPickup = false, isInChest = false, isInDungeon = false;
    List<Enemy> leveledEnemies = new List<Enemy>();


    public GameObject playerObject;
    public GameObject gameManager;
    public GameObject card;


    Coroutine battleCoroutine;
    public AudioMixer mixer;

    public bool isLoadingGame;


    // Game Start Methods

    public void InitializeGame()
    {
        InitializeAssets();
        InitializeSkills();
        InitializeQuests();

        InitializeUI();

        musicSlider.value = PlayerPrefs.GetFloat("MusicAudio", 0.7f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXAudio", 0.7f);
        mixer.SetFloat("MusicAudio", PlayerPrefs.GetFloat("MusicAudio", 0.7f));
        mixer.SetFloat("SFXAudio", PlayerPrefs.GetFloat("SFXAudio", 0.7f));

        if (isLoadingGame)
            LoadGame();
        else
            StartNewGame();

        UpdateInventoryAttributes();
        Advertisements.Instance.ShowBanner(BannerPosition.BOTTOM);
    }

    void StartNewGame()
    {
        //AddToQuestList(leaveTheCave);

        player.GetInventory().AddToInventory(crookedDagger);
        player.GetInventory().AddToInventory(tatteredShirt);
        player.GetInventory().AddToInventory(smallHealthPotion, 1);

        player.GetInventory().OnBeforeSerialize();
        player.GetInventory().OnAfterDeserialize();

        activeItem = crookedDagger;
        EquipItem();
        activeItem = tatteredShirt;
        EquipItem();
        activeItem = null;

        StartCoroutine(LoadScene(buruns, false));
    }

    void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/save.tpg"))
        {
            Debug.Log(Application.persistentDataPath);
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/save.tpg", FileMode.Open);
            SaveLoad load = (SaveLoad)bf.Deserialize(file);
            file.Close();            

            load.LoadNPCs();
            load.LoadStores();
            load.LoadBossEnemies();
            load.LoadLocations();
            load.LoadEvents();

            player = load.LoadPlayer();

            LoadEquipmentSlots();
            LoadQuestSlots();
            SetCurrentWeight();

            statsPlayerNameTxt.text = player.GetName();
            playerPickUpNameTxt.text = player.GetName();
            statsPlayerTitleTxt.text = player.GetTitle();

            damageValueTxt.text = player.GetWeapon().GetMaxDamage().ToString();
            armorValueTxt.text = player.GetDefense().ToString();

            StartCoroutine(LoadScene(player.GetLocation(), true));
        }
        else
        {
            Debug.Log("No Game Saved");
            StartCoroutine(LoadToMain());
        }
    }

    SaveLoad NewSaveLoadObject()
    {
        SaveLoad saveLoad = new SaveLoad();

        saveLoad.SavePlayer(player);
        saveLoad.SaveNPCs();
        saveLoad.SaveStores();
        saveLoad.SaveBossEnemies();
        saveLoad.SaveLocations();
        saveLoad.SaveEvents();

        return saveLoad;
    }

    public void SaveGame()
    {
        SaveLoad save = NewSaveLoadObject();

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/save.tpg");
        bf.Serialize(file, save);
        file.Close();

        UIPauseScreen.SetActive(false);
        OutputToText("Game Successfully Saved.");
    }

    //**

    // Initialization Methods

    void InitializeAssets()
    {
        ItemDictionary = new SortedDictionary<uint, Item>();
        LocationDictionary = new SortedDictionary<uint, Location>();
        EnemyDictionary = new SortedDictionary<uint, Enemy>();
        NPCDictionary = new SortedDictionary<uint, NPC>();
        StoreDictionary = new SortedDictionary<uint, Store>();
        //DungeonDictionary = new SortedDictionary<uint, Dungeon>();

        outputQueue = new Queue<string>();

        InitializeItems();

        buruns = Resources.Load<Location>("Locations/Buruns");
        arenthiaBuruns = Resources.Load<Location>("Locations/ArenthiaBuruns");
        burunsArenthia = Resources.Load<Location>("Locations/BurunsArenthia");

        ironRatCave = Instantiate(Resources.Load<Dungeon>("Locations/IronRatCave"));
        deepForest = Instantiate(Resources.Load<Dungeon>("Dungeons/DeepForest/DeepForest"));
        creogCave = Instantiate(Resources.Load<Dungeon>("Dungeons/CreogCave/CreogCave"));

        LocationDictionary.Add(buruns.GetID(), buruns);
        LocationDictionary.Add(arenthiaBuruns.GetID(), arenthiaBuruns);
        LocationDictionary.Add(burunsArenthia.GetID(), burunsArenthia);

        LocationDictionary.Add(ironRatCave.GetID(), ironRatCave);
        //DungeonDictionary.Add(deepForest.GetID(), deepForest);
        //DungeonDictionary.Add(creogCave.GetID(), creogCave);


        InitializeEnemies();

        hirgirdBlacksmith = Instantiate(Resources.Load<NPC>("NPC/Hirgird"));
        inveraAlchemist = Instantiate(Resources.Load<NPC>("NPC/Invera"));
        kiarnilLeathersmith = Instantiate(Resources.Load<NPC>("NPC/Kiarnil"));
        serikInnkeeper = Instantiate(Resources.Load<NPC>("NPC/Serik"));
        varianArmorsmith = Instantiate(Resources.Load<NPC>("NPC/Varian"));
        malina = Instantiate(Resources.Load<NPC>("NPC/Malina"));
        cityGuard = Instantiate(Resources.Load<NPC>("NPC/GateGuard"));
        castleGuardNPC = Instantiate(Resources.Load<NPC>("NPC/CastleGuard"));
        temrik = Instantiate(Resources.Load<NPC>("NPC/Temrik"));
        mysteriousMan = Instantiate(Resources.Load<NPC>("NPC/MysteriousMan"));
        talkingGroup = Instantiate(Resources.Load<NPC>("NPC/TalkingGroup"));
        telyrid = Instantiate(Resources.Load<NPC>("NPC/Telyrid"));

        NPCDictionary.Add(hirgirdBlacksmith.GetID(), hirgirdBlacksmith);
        NPCDictionary.Add(inveraAlchemist.GetID(), inveraAlchemist);
        NPCDictionary.Add(kiarnilLeathersmith.GetID(), kiarnilLeathersmith);
        NPCDictionary.Add(serikInnkeeper.GetID(), serikInnkeeper);
        NPCDictionary.Add(varianArmorsmith.GetID(), varianArmorsmith);
        NPCDictionary.Add(malina.GetID(), malina);
        NPCDictionary.Add(cityGuard.GetID(), cityGuard);
        NPCDictionary.Add(castleGuardNPC.GetID(), castleGuardNPC);
        NPCDictionary.Add(temrik.GetID(), temrik);
        NPCDictionary.Add(mysteriousMan.GetID(), mysteriousMan);
        NPCDictionary.Add(talkingGroup.GetID(), talkingGroup);
        NPCDictionary.Add(telyrid.GetID(), telyrid);

        heavyAnvil = Instantiate(Resources.Load<Store>("Stores/HeavyAnvil"));
        litheWarrior = Instantiate(Resources.Load<Store>("Stores/LitheWarrior"));
        magicMortar = Instantiate(Resources.Load<Store>("Stores/MagicMortar"));
        theBulwark = Instantiate(Resources.Load<Store>("Stores/TheBulwark"));
        telyridsTomes = Instantiate(Resources.Load<Store>("Stores/TelyridsTomes"));

        StoreDictionary.Add(heavyAnvil.GetID(), heavyAnvil);
        StoreDictionary.Add(litheWarrior.GetID(), litheWarrior);
        StoreDictionary.Add(magicMortar.GetID(), magicMortar);
        StoreDictionary.Add(theBulwark.GetID(), theBulwark);
        StoreDictionary.Add(telyridsTomes.GetID(), telyridsTomes);



        healthDrop = Resources.Load<Sprite>("Textures/Inventory Icons/skill_008");
        staminaDrop = Resources.Load<Sprite>("Textures/Inventory Icons/skill_173");
        manaDrop = Resources.Load<Sprite>("Textures/Inventory Icons/skill_alt_008");
        physicalDmgSprite = Resources.Load<Sprite>("Textures/Inventory Icons/weapon_sword_02");
        manaDmgSprite = Resources.Load<Sprite>("Textures/Inventory Icons/skill_016");

        player = new Player("Player", buruns, new Inventory());
    }

    void InitializeItems()
    {

        NULL_ITEM = Resources.Load<Item>("Items/NULL_ITEM");
        NULL_WEAPON = Resources.Load<Weapon>("Items/NULL_WEAPON");
        NULL_ARMOR = Resources.Load<Armor>("Items/NULL_ARMOR");


        // Miscellaneous

        key = Resources.Load<Item>("Items/Miscellaneous/0_Key");
        smallFur = Resources.Load<Item>("Items/Miscellaneous/1_SmallFur");
        smallSkull = Resources.Load<Item>("Items/Miscellaneous/2_SmallSkull");
        ratKingsCrown = Resources.Load<Item>("Items/Miscellaneous/3_RatKingsCrown");
        luckyPaw = Resources.Load<Item>("Items/Miscellaneous/4_LuckyPaw");
        blueSlimeGoo = Resources.Load<Item>("Items/Miscellaneous/5_BlueSlimeGoo");
        greenSlimeGoo = Resources.Load<Item>("Items/Miscellaneous/6_GreenSlimeGoo");
        redSlimeGoo = Resources.Load<Item>("Items/Miscellaneous/7_RedSlimeGoo");
        bone = Resources.Load<Item>("Items/Miscellaneous/8_Bone");
        eyeball = Resources.Load<Item>("Items/Miscellaneous/9_Eyeball");
        greenGem = Resources.Load<Item>("Items/Miscellaneous/10_GreenGem");
        hornedStaff = Resources.Load<Item>("Items/Miscellaneous/11_HornedStaff");
        armorPiece = Resources.Load<Item>("Items/Miscellaneous/12_ArmorPiece");
        brokenSword = Resources.Load<Item>("Items/Miscellaneous/13_BrokenSword");
        goldAmulet = Resources.Load<Item>("Items/Miscellaneous/14_GoldAmulet");
        goldBar = Resources.Load<Item>("Items/Miscellaneous/15_GoldBar");
        lockpickSet = Resources.Load<Item>("Items/Miscellaneous/16_LockpickSet");
        wolfGem = Resources.Load<Item>("Items/Miscellaneous/17_WolfGem");
        wolfPelt = Resources.Load<Item>("Items/Miscellaneous/18_WolfPelt");
        canine = Resources.Load<Item>("Items/Miscellaneous/19_Canine");
        grimore = Resources.Load<Item>("Items/Miscellaneous/20_Grimore");
        magicRune = Resources.Load<Item>("Items/Miscellaneous/21_MagicRune");
        mortar = Resources.Load<Item>("Items/Miscellaneous/22_Mortar");
        splinteredStaff = Resources.Load<Item>("Items/Miscellaneous/23_SplinteredStaff");
        ironBar = Resources.Load<Item>("Items/Miscellaneous/24_IronBar");
        humanJaw = Resources.Load<Item>("Items/Miscellaneous/25_HumanJaw");
        bearPelt = Resources.Load<Item>("Items/Miscellaneous/26_BearPelt");
        bearPaw = Resources.Load<Item>("Items/Miscellaneous/27_BearPaw");
        magicStone = Resources.Load<Item>("Items/Miscellaneous/28_MagicStone");

        ItemDictionary.Add(key.GetID(), key);
        ItemDictionary.Add(smallFur.GetID(), smallFur);
        ItemDictionary.Add(smallSkull.GetID(), smallSkull);
        ItemDictionary.Add(ratKingsCrown.GetID(), ratKingsCrown);
        ItemDictionary.Add(luckyPaw.GetID(), luckyPaw);
        ItemDictionary.Add(blueSlimeGoo.GetID(), blueSlimeGoo);
        ItemDictionary.Add(greenSlimeGoo.GetID(), greenSlimeGoo);
        ItemDictionary.Add(redSlimeGoo.GetID(), redSlimeGoo);
        ItemDictionary.Add(bone.GetID(), bone);
        ItemDictionary.Add(eyeball.GetID(), eyeball);
        ItemDictionary.Add(greenGem.GetID(), greenGem);
        ItemDictionary.Add(hornedStaff.GetID(), hornedStaff);
        ItemDictionary.Add(armorPiece.GetID(), armorPiece);
        ItemDictionary.Add(brokenSword.GetID(), brokenSword);
        ItemDictionary.Add(goldAmulet.GetID(), goldAmulet);
        ItemDictionary.Add(goldBar.GetID(), goldBar);
        ItemDictionary.Add(lockpickSet.GetID(), lockpickSet);
        ItemDictionary.Add(wolfGem.GetID(), wolfGem);
        ItemDictionary.Add(wolfPelt.GetID(), wolfPelt);
        ItemDictionary.Add(canine.GetID(), canine);
        ItemDictionary.Add(grimore.GetID(), grimore);
        ItemDictionary.Add(magicRune.GetID(), magicRune);
        ItemDictionary.Add(mortar.GetID(), mortar);
        ItemDictionary.Add(splinteredStaff.GetID(), splinteredStaff);
        ItemDictionary.Add(ironBar.GetID(), ironBar);
        ItemDictionary.Add(humanJaw.GetID(), humanJaw);
        ItemDictionary.Add(bearPelt.GetID(), bearPelt);
        ItemDictionary.Add(bearPaw.GetID(), bearPaw);
        ItemDictionary.Add(magicStone.GetID(), magicStone);

        // Weapons

        crookedDagger = Resources.Load<Weapon>("Items/Weapons/100_CrookedDagger");

        copperDagger = Resources.Load<Weapon>("Items/Weapons/101_CopperDagger");
        copperSword = Resources.Load<Weapon>("Items/Weapons/102_CopperSword");
        copperAxe = Resources.Load<Weapon>("Items/Weapons/103_CopperAxe");
        copperSpear = Resources.Load<Weapon>("Items/Weapons/104_CopperSpear");
        copperMace = Resources.Load<Weapon>("Items/Weapons/105_CopperMace");

        ironDagger = Resources.Load<Weapon>("Items/Weapons/106_IronDagger");
        ironSword = Resources.Load<Weapon>("Items/Weapons/107_IronSword");
        ironAxe = Resources.Load<Weapon>("Items/Weapons/108_IronAxe");
        ironSpear = Resources.Load<Weapon>("Items/Weapons/109_IronSpear");
        ironMace = Resources.Load<Weapon>("Items/Weapons/110_IronMace");

        steelDagger = Resources.Load<Weapon>("Items/Weapons/111_SteelDagger");
        steelSword = Resources.Load<Weapon>("Items/Weapons/112_SteelSword");
        steelAxe = Resources.Load<Weapon>("Items/Weapons/113_SteelAxe");
        steelSpear = Resources.Load<Weapon>("Items/Weapons/114_SteelSpear");
        steelMace = Resources.Load<Weapon>("Items/Weapons/115_SteelMace");

        electrumDagger = Resources.Load<Weapon>("Items/Weapons/116_ElectrumDagger");
        electrumSword = Resources.Load<Weapon>("Items/Weapons/117_ElectrumSword");
        electrumAxe = Resources.Load<Weapon>("Items/Weapons/118_ElectrumAxe");
        electrumSpear = Resources.Load<Weapon>("Items/Weapons/119_ElectrumSpear");
        electrumMace = Resources.Load<Weapon>("Items/Weapons/120_ElectrumMace");

        royalDagger = Resources.Load<Weapon>("Items/Weapons/121_RoyalDagger");
        royalSword = Resources.Load<Weapon>("Items/Weapons/122_RoyalSword");
        royalAxe = Resources.Load<Weapon>("Items/Weapons/123_RoyalAxe");
        royalSpear = Resources.Load<Weapon>("Items/Weapons/124_RoyalSpear");
        royalMace = Resources.Load<Weapon>("Items/Weapons/125_RoyalMace");

        swordOfLight = Resources.Load<Weapon>("Items/Weapons/126_SwordOfLight");

        ItemDictionary.Add(crookedDagger.GetID(), crookedDagger);

        ItemDictionary.Add(copperDagger.GetID(), copperDagger);
        ItemDictionary.Add(copperSword.GetID(), copperSword);
        ItemDictionary.Add(copperAxe.GetID(), copperAxe);
        ItemDictionary.Add(copperSpear.GetID(), copperSpear);
        ItemDictionary.Add(copperMace.GetID(), copperMace);

        ItemDictionary.Add(ironDagger.GetID(), ironDagger);
        ItemDictionary.Add(ironSword.GetID(), ironSword);
        ItemDictionary.Add(ironAxe.GetID(), ironAxe);
        ItemDictionary.Add(ironSpear.GetID(), ironSpear);
        ItemDictionary.Add(ironMace.GetID(), ironMace);

        ItemDictionary.Add(steelDagger.GetID(), steelDagger);
        ItemDictionary.Add(steelSword.GetID(), steelSword);
        ItemDictionary.Add(steelAxe.GetID(), steelAxe);
        ItemDictionary.Add(steelSpear.GetID(), steelSpear);
        ItemDictionary.Add(steelMace.GetID(), steelMace);

        ItemDictionary.Add(electrumDagger.GetID(), electrumDagger);
        ItemDictionary.Add(electrumSword.GetID(), electrumSword);
        ItemDictionary.Add(electrumAxe.GetID(), electrumAxe);
        ItemDictionary.Add(electrumSpear.GetID(), electrumSpear);
        ItemDictionary.Add(electrumMace.GetID(), electrumMace);

        ItemDictionary.Add(royalDagger.GetID(), royalDagger);
        ItemDictionary.Add(royalSword.GetID(), royalSword);
        ItemDictionary.Add(royalAxe.GetID(), royalAxe);
        ItemDictionary.Add(royalSpear.GetID(), royalSpear);
        ItemDictionary.Add(royalMace.GetID(), royalMace);

        ItemDictionary.Add(swordOfLight.GetID(), swordOfLight);


        // Armors

        tatteredShirt = Resources.Load<Armor>("Items/Armors/Tattered/Tattered Shirt");

        leatherHelmet = Resources.Load<Armor>("Items/Armors/Leather/Leather Helmet");
        leatherShirt = Resources.Load<Armor>("Items/Armors/Leather/Leather Shirt");
        leatherPants = Resources.Load<Armor>("Items/Armors/Leather/Leather Belt");
        leatherBoots = Resources.Load<Armor>("Items/Armors/Leather/Leather Boots");
        leatherGloves = Resources.Load<Armor>("Items/Armors/Leather/Leather Gloves");

        copperBelt = Resources.Load<Armor>("Items/Armors/Copper/Copper Belt");
        copperBoots = Resources.Load<Armor>("Items/Armors/Copper/Copper Boots");
        copperGloves = Resources.Load<Armor>("Items/Armors/Copper/Copper Gloves");
        copperHelmet = Resources.Load<Armor>("Items/Armors/Copper/Copper Helmet");
        copperChest = Resources.Load<Armor>("Items/Armors/Copper/Copper Shirt");

        silverBelt = Resources.Load<Armor>("Items/Armors/Silver/Silver Belt");
        silverBoots = Resources.Load<Armor>("Items/Armors/Silver/Silver Boots");
        silverGloves = Resources.Load<Armor>("Items/Armors/Silver/Silver Gloves");
        silverHelmet = Resources.Load<Armor>("Items/Armors/Silver/Silver Helmet");
        silverChest = Resources.Load<Armor>("Items/Armors/Silver/Silver Shirt");

        titaniumBelt = Resources.Load<Armor>("Items/Armors/Titanium/Titanium Belt");
        titaniumBoots = Resources.Load<Armor>("Items/Armors/Titanium/Titanium Boots");
        titaniumGloves = Resources.Load<Armor>("Items/Armors/Titanium/Titanium Gloves");
        titaniumHelmet = Resources.Load<Armor>("Items/Armors/Titanium/Titanium Helmet");
        titaniumChest = Resources.Load<Armor>("Items/Armors/Titanium/Titanium Shirt");

        heroicBelt = Resources.Load<Armor>("Items/Armors/Heroic/Heroic Belt");
        heroicBoots = Resources.Load<Armor>("Items/Armors/Heroic/Heroic Boots");
        heroicGloves = Resources.Load<Armor>("Items/Armors/Heroic/Heroic Gloves");
        heroicHelmet = Resources.Load<Armor>("Items/Armors/Heroic/Heroic Helmet");
        heroicChest = Resources.Load<Armor>("Items/Armors/Heroic/Heroic Shirt");

        ironBelt = Resources.Load<Armor>("Items/Armors/Iron/Iron Belt");
        ironBoots = Resources.Load<Armor>("Items/Armors/Iron/Iron Boots");
        ironGloves = Resources.Load<Armor>("Items/Armors/Iron/Iron Gloves");
        ironHelmet = Resources.Load<Armor>("Items/Armors/Iron/Iron Helmet");
        ironChest = Resources.Load<Armor>("Items/Armors/Iron/Iron Chestpiece");

        steelBelt = Resources.Load<Armor>("Items/Armors/Steel/Steel Belt");
        steelBoots = Resources.Load<Armor>("Items/Armors/Steel/Steel Boots");
        steelGloves = Resources.Load<Armor>("Items/Armors/Steel/Steel Gloves");
        steelHelmet = Resources.Load<Armor>("Items/Armors/Steel/Steel Helmet");
        steelChest = Resources.Load<Armor>("Items/Armors/Steel/Steel Chestpiece");

        electrumBelt = Resources.Load<Armor>("Items/Armors/Electrum/Electrum Belt");
        electrumBoots = Resources.Load<Armor>("Items/Armors/Electrum/Electrum Boots");
        electrumGloves = Resources.Load<Armor>("Items/Armors/Electrum/Electrum Gloves");
        electrumHelmet = Resources.Load<Armor>("Items/Armors/Electrum/Electrum Helmet");
        electrumChest = Resources.Load<Armor>("Items/Armors/Electrum/Electrum Chestpiece");

        magneniumBelt = Resources.Load<Armor>("Items/Armors/Magnenium/Magnenium Belt");
        magneniumBoots = Resources.Load<Armor>("Items/Armors/Magnenium/Magnenium Boots");
        magneniumGloves = Resources.Load<Armor>("Items/Armors/Magnenium/Magnenium Gloves");
        magneniumHelmet = Resources.Load<Armor>("Items/Armors/Magnenium/Magnenium Helmet");
        magneniumChest = Resources.Load<Armor>("Items/Armors/Magnenium/Magnenium Chestpiece");

        demonicBelt = Resources.Load<Armor>("Items/Armors/Demonic/Demonic Belt");
        demonicBoots = Resources.Load<Armor>("Items/Armors/Demonic/Demonic Boots");
        demonicGloves = Resources.Load<Armor>("Items/Armors/Demonic/Demonic Gloves");
        demonicHelmet = Resources.Load<Armor>("Items/Armors/Demonic/Demonic Helmet");
        demonicChest = Resources.Load<Armor>("Items/Armors/Demonic/Demonic Chestpiece");

        ItemDictionary.Add(tatteredShirt.GetID(), tatteredShirt);

        ItemDictionary.Add(leatherHelmet.GetID(), leatherHelmet);
        ItemDictionary.Add(leatherShirt.GetID(), leatherShirt);
        ItemDictionary.Add(leatherPants.GetID(), leatherPants);
        ItemDictionary.Add(leatherBoots.GetID(), leatherBoots);
        ItemDictionary.Add(leatherGloves.GetID(), leatherGloves);

        ItemDictionary.Add(copperBelt.GetID(), copperBelt);
        ItemDictionary.Add(copperBoots.GetID(), copperBoots);
        ItemDictionary.Add(copperGloves.GetID(), copperGloves);
        ItemDictionary.Add(copperHelmet.GetID(), copperHelmet);
        ItemDictionary.Add(copperChest.GetID(), copperChest);

        ItemDictionary.Add(silverBelt.GetID(), silverBelt);
        ItemDictionary.Add(silverBoots.GetID(), silverBoots);
        ItemDictionary.Add(silverGloves.GetID(), silverGloves);
        ItemDictionary.Add(silverHelmet.GetID(), silverHelmet);
        ItemDictionary.Add(silverChest.GetID(), silverChest);

        ItemDictionary.Add(titaniumBelt.GetID(), titaniumBelt);
        ItemDictionary.Add(titaniumBoots.GetID(), titaniumBoots);
        ItemDictionary.Add(titaniumGloves.GetID(), titaniumGloves);
        ItemDictionary.Add(titaniumHelmet.GetID(), titaniumHelmet);
        ItemDictionary.Add(titaniumChest.GetID(), titaniumChest);

        ItemDictionary.Add(heroicBelt.GetID(), heroicBelt);
        ItemDictionary.Add(heroicBoots.GetID(), heroicBoots);
        ItemDictionary.Add(heroicGloves.GetID(), heroicGloves);
        ItemDictionary.Add(heroicHelmet.GetID(), heroicHelmet);
        ItemDictionary.Add(heroicChest.GetID(), heroicChest);

        ItemDictionary.Add(ironBelt.GetID(), ironBelt);
        ItemDictionary.Add(ironBoots.GetID(), ironBoots);
        ItemDictionary.Add(ironGloves.GetID(), ironGloves);
        ItemDictionary.Add(ironHelmet.GetID(), ironHelmet);
        ItemDictionary.Add(ironChest.GetID(), ironChest);

        ItemDictionary.Add(steelBelt.GetID(), steelBelt);
        ItemDictionary.Add(steelBoots.GetID(), steelBoots);
        ItemDictionary.Add(steelGloves.GetID(), steelGloves);
        ItemDictionary.Add(steelHelmet.GetID(), steelHelmet);
        ItemDictionary.Add(steelChest.GetID(), steelChest);

        ItemDictionary.Add(electrumBelt.GetID(), electrumBelt);
        ItemDictionary.Add(electrumBoots.GetID(), electrumBoots);
        ItemDictionary.Add(electrumGloves.GetID(), electrumGloves);
        ItemDictionary.Add(electrumHelmet.GetID(), electrumHelmet);
        ItemDictionary.Add(electrumChest.GetID(), electrumChest);

        ItemDictionary.Add(magneniumBelt.GetID(), magneniumBelt);
        ItemDictionary.Add(magneniumBoots.GetID(), magneniumBoots);
        ItemDictionary.Add(magneniumGloves.GetID(), magneniumGloves);
        ItemDictionary.Add(magneniumHelmet.GetID(), magneniumHelmet);
        ItemDictionary.Add(magneniumChest.GetID(), magneniumChest);

        ItemDictionary.Add(demonicBelt.GetID(), demonicBelt);
        ItemDictionary.Add(demonicBoots.GetID(), demonicBoots);
        ItemDictionary.Add(demonicGloves.GetID(), demonicGloves);
        ItemDictionary.Add(demonicHelmet.GetID(), demonicHelmet);
        ItemDictionary.Add(demonicChest.GetID(), demonicChest);

        // Consumables

        smallHealthPotion = Resources.Load<Potion>("Items/Consumables/500_HealthTincture");
        smallManaPotion = Resources.Load<Potion>("Items/Consumables/501_ManaTincture");
        smallStaminaPotion = Resources.Load<Potion>("Items/Consumables/502_StaminaTincture");
        healthPotion = Resources.Load<Potion>("Items/Consumables/503_HealthSolution");
        manaPotion = Resources.Load<Potion>("Items/Consumables/504_ManaSolution");
        staminaPotion = Resources.Load<Potion>("Items/Consumables/505_StaminaSolution");
        largeHealthPotion = Resources.Load<Potion>("Items/Consumables/506_HealthPotion");
        largeManaPotion = Resources.Load<Potion>("Items/Consumables/507_ManaPotion");
        largeStaminaPotion = Resources.Load<Potion>("Items/Consumables/508_StaminaPotion");
        giantHealthPotion = Resources.Load<Potion>("Items/Consumables/509_HealthElixir");
        giantManaPotion = Resources.Load<Potion>("Items/Consumables/510_ManaElixir");
        giantStaminaPotion = Resources.Load<Potion>("Items/Consumables/511_StaminaElixir");

        rottenMeat = Resources.Load<Edible>("Items/Consumables/596_RottenMeat");
        bearHeart = Resources.Load<Edible>("Items/Consumables/597_BearHeart");
        meatChunk = Resources.Load<Edible>("Items/Consumables/598_MeatChunk");
        smallMeatChunk = Resources.Load<Edible>("Items/Consumables/599_SmallMeatChunk");

        heavySwingBook = Resources.Load<SkillBook>("Items/Consumables/600_HeavySwing");
        targetedStrikeBook = Resources.Load<SkillBook>("Items/Consumables/601_TargetedStrike");
        masterfulStabBook = Resources.Load<SkillBook>("Items/Consumables/602_MasterfulStab");
        kickBook = Resources.Load<SkillBook>("Items/Consumables/603_Kick");
        slamBook = Resources.Load<SkillBook>("Items/Consumables/604_Slam");
        suplexBook = Resources.Load<SkillBook>("Items/Consumables/605_Suplex");
        backhandBook = Resources.Load<SkillBook>("Items/Consumables/606_Backhand");
        pommelThrowBook = Resources.Load<SkillBook>("Items/Consumables/607_PommelThrow");
        concussiveStrikeBook = Resources.Load<SkillBook>("Items/Consumables/608_ConcussiveStrike");
        rushBook = Resources.Load<SkillBook>("Items/Consumables/609_Rush");
        sprintBook = Resources.Load<SkillBook>("Items/Consumables/610_Sprint");
        litheGraceBook = Resources.Load<SkillBook>("Items/Consumables/611_LitheGrace");
        temperBook = Resources.Load<SkillBook>("Items/Consumables/612_Temper");
        rageBook = Resources.Load<SkillBook>("Items/Consumables/613_Rage");
        frenzyBook = Resources.Load<SkillBook>("Items/Consumables/614_Frenzy");
        redoubtBook = Resources.Load<SkillBook>("Items/Consumables/615_Redoubt");
        bulwarkBook = Resources.Load<SkillBook>("Items/Consumables/616_Bulwark");
        bastionBook = Resources.Load<SkillBook>("Items/Consumables/617_Bastion");
        seepBook = Resources.Load<SkillBook>("Items/Consumables/618_Seep");
        bleedBook = Resources.Load<SkillBook>("Items/Consumables/619_Bleed");
        hemorrhageBook = Resources.Load<SkillBook>("Items/Consumables/620_Hemorrhage");

        fireballBook = Resources.Load<SkillBook>("Items/Consumables/621_Fireball");
        infernoBook = Resources.Load<SkillBook>("Items/Consumables/622_Inferno");
        immolationBook = Resources.Load<SkillBook>("Items/Consumables/623_Immolation");
        frostbiteBook = Resources.Load<SkillBook>("Items/Consumables/624_Frostbite");
        iceSpikeBook = Resources.Load<SkillBook>("Items/Consumables/625_IceSpike");
        glaciateBook = Resources.Load<SkillBook>("Items/Consumables/626_Glaciate");
        sparksBook = Resources.Load<SkillBook>("Items/Consumables/627_Sparks");
        lightningBoltBook = Resources.Load<SkillBook>("Items/Consumables/628_LightningBolt");
        smiteBook = Resources.Load<SkillBook>("Items/Consumables/629_Smite");
        mendBook = Resources.Load<SkillBook>("Items/Consumables/630_Mend");
        healBook = Resources.Load<SkillBook>("Items/Consumables/631_Heal");
        rejuvenateBook = Resources.Load<SkillBook>("Items/Consumables/632_Rejuvenate");
        abateBook = Resources.Load<SkillBook>("Items/Consumables/633_Abate");
        allayBook = Resources.Load<SkillBook>("Items/Consumables/634_Allay");
        dispelBook = Resources.Load<SkillBook>("Items/Consumables/635_Dispel");
        magicArrowBook = Resources.Load<SkillBook>("Items/Consumables/636_MagicArrow");
        magicBoltBook = Resources.Load<SkillBook>("Items/Consumables/637_MagicBolt");
        magicMissileBook = Resources.Load<SkillBook>("Items/Consumables/638_MagicMissile");

        ItemDictionary.Add(smallHealthPotion.GetID(), smallHealthPotion);
        ItemDictionary.Add(smallStaminaPotion.GetID(), smallStaminaPotion);
        ItemDictionary.Add(smallManaPotion.GetID(), smallManaPotion);
        ItemDictionary.Add(healthPotion.GetID(), healthPotion);
        ItemDictionary.Add(staminaPotion.GetID(), staminaPotion);
        ItemDictionary.Add(manaPotion.GetID(), manaPotion);
        ItemDictionary.Add(largeHealthPotion.GetID(), largeHealthPotion);
        ItemDictionary.Add(largeStaminaPotion.GetID(), largeStaminaPotion);
        ItemDictionary.Add(largeManaPotion.GetID(), largeManaPotion);
        ItemDictionary.Add(giantHealthPotion.GetID(), giantHealthPotion);
        ItemDictionary.Add(giantStaminaPotion.GetID(), giantStaminaPotion);
        ItemDictionary.Add(giantManaPotion.GetID(), giantManaPotion);
        ItemDictionary.Add(rottenMeat.GetID(), rottenMeat);
        ItemDictionary.Add(bearHeart.GetID(), bearHeart);
        ItemDictionary.Add(meatChunk.GetID(), meatChunk);
        ItemDictionary.Add(smallMeatChunk.GetID(), smallMeatChunk);

        ItemDictionary.Add(heavySwingBook.GetID(), heavySwingBook);
        ItemDictionary.Add(targetedStrikeBook.GetID(), targetedStrikeBook);
        ItemDictionary.Add(masterfulStabBook.GetID(), masterfulStabBook);
        ItemDictionary.Add(kickBook.GetID(), kickBook);
        ItemDictionary.Add(slamBook.GetID(), slamBook);
        ItemDictionary.Add(suplexBook.GetID(), suplexBook);
        ItemDictionary.Add(backhandBook.GetID(), backhandBook);
        ItemDictionary.Add(pommelThrowBook.GetID(), pommelThrowBook);
        ItemDictionary.Add(concussiveStrikeBook.GetID(), concussiveStrikeBook);
        ItemDictionary.Add(rushBook.GetID(), rushBook);
        ItemDictionary.Add(sprintBook.GetID(), sprintBook);
        ItemDictionary.Add(litheGraceBook.GetID(), litheGraceBook);
        ItemDictionary.Add(temperBook.GetID(), temperBook);
        ItemDictionary.Add(rageBook.GetID(), rageBook);
        ItemDictionary.Add(frenzyBook.GetID(), frenzyBook);
        ItemDictionary.Add(redoubtBook.GetID(), redoubtBook);
        ItemDictionary.Add(bulwarkBook.GetID(), bulwarkBook);
        ItemDictionary.Add(bastionBook.GetID(), bastionBook);
        ItemDictionary.Add(seepBook.GetID(), seepBook);
        ItemDictionary.Add(bleedBook.GetID(), bleedBook);
        ItemDictionary.Add(hemorrhageBook.GetID(), hemorrhageBook);

        ItemDictionary.Add(fireballBook.GetID(), fireballBook);
        ItemDictionary.Add(infernoBook.GetID(), infernoBook);
        ItemDictionary.Add(immolationBook.GetID(), immolationBook);
        ItemDictionary.Add(frostbiteBook.GetID(), frostbiteBook);
        ItemDictionary.Add(iceSpikeBook.GetID(), iceSpikeBook);
        ItemDictionary.Add(glaciateBook.GetID(), glaciateBook);
        ItemDictionary.Add(sparksBook.GetID(), sparksBook);
        ItemDictionary.Add(lightningBoltBook.GetID(), lightningBoltBook);
        ItemDictionary.Add(smiteBook.GetID(), smiteBook);
        ItemDictionary.Add(mendBook.GetID(), mendBook);
        ItemDictionary.Add(healBook.GetID(), healBook);
        ItemDictionary.Add(rejuvenateBook.GetID(), rejuvenateBook);
        ItemDictionary.Add(abateBook.GetID(), abateBook);
        ItemDictionary.Add(allayBook.GetID(), allayBook);
        ItemDictionary.Add(dispelBook.GetID(), dispelBook);
        ItemDictionary.Add(magicArrowBook.GetID(), magicArrowBook);
        ItemDictionary.Add(magicBoltBook.GetID(), magicBoltBook);
        ItemDictionary.Add(magicMissileBook.GetID(), magicMissileBook);

    }

    void InitializeEnemies()
    {
        smallRat = Instantiate(Resources.Load<Enemy>("Enemies/100_SmallRat"));

        rat_01 = Instantiate(Resources.Load<Enemy>("Enemies/101_Rat_1"));
        rat_03 = Instantiate(Resources.Load<Enemy>("Enemies/101_Rat_3"));
        rat_05 = Instantiate(Resources.Load<Enemy>("Enemies/101_Rat_5"));

        ratPack_03 = Instantiate(Resources.Load<Enemy>("Enemies/102_RatPack_3"));
        ratPack_07 = Instantiate(Resources.Load<Enemy>("Enemies/102_RatPack_7"));
        ratPack_10 = Instantiate(Resources.Load<Enemy>("Enemies/102_RatPack_10"));

        giantRat = Instantiate(Resources.Load<Enemy>("Enemies/103_GiantRat"));

        ratKing = Instantiate(Resources.Load<BossEnemy>("Enemies/104_RatKing"));

        blueSlime_03 = Instantiate(Resources.Load<Enemy>("Enemies/105_BlueSlime_3"));
        blueSlime_05 = Instantiate(Resources.Load<Enemy>("Enemies/105_BlueSlime_5"));

        greenSlime_03 = Instantiate(Resources.Load<Enemy>("Enemies/106_GreenSlime_3"));
        greenSlime_05 = Instantiate(Resources.Load<Enemy>("Enemies/106_GreenSlime_5"));

        redSlime_03 = Instantiate(Resources.Load<Enemy>("Enemies/107_RedSlime_3"));
        redSlime_05 = Instantiate(Resources.Load<Enemy>("Enemies/107_RedSlime_5"));

        amalgamSlime_06 = Instantiate(Resources.Load<Enemy>("Enemies/108_AmalgamSlime_6"));
        amalgamSlime_10 = Instantiate(Resources.Load<Enemy>("Enemies/108_AmalgamSlime_10"));
        amalgamSlime_14 = Instantiate(Resources.Load<Enemy>("Enemies/108_AmalgamSlime_14"));
        amalgamSlime_18 = Instantiate(Resources.Load<Enemy>("Enemies/108_AmalgamSlime_18"));

        goblinShaman_06 = Instantiate(Resources.Load<Enemy>("Enemies/109_GoblinShaman_6"));
        goblinShaman_09 = Instantiate(Resources.Load<Enemy>("Enemies/109_GoblinShaman_9"));

        capturedBandit_06 = Instantiate(Resources.Load<Enemy>("Enemies/110_CapturedBandit_6"));
        capturedBandit_10 = Instantiate(Resources.Load<Enemy>("Enemies/110_CapturedBandit_10"));
        capturedBandit_14 = Instantiate(Resources.Load<Enemy>("Enemies/110_CapturedBandit_14"));
        capturedBandit_18 = Instantiate(Resources.Load<Enemy>("Enemies/110_CapturedBandit_18"));

        castleGuard = Instantiate(Resources.Load<Enemy>("Enemies/112_CastleGuard"));

        grayWolf_06 = Instantiate(Resources.Load<Enemy>("Enemies/113_GrayWolf_6"));
        grayWolf_09 = Instantiate(Resources.Load<Enemy>("Enemies/113_GrayWolf_9"));

        direWolf_08 = Instantiate(Resources.Load<Enemy>("Enemies/114_DireWolf_8"));
        direWolf_12 = Instantiate(Resources.Load<Enemy>("Enemies/114_DireWolf_12"));
        direWolf_16 = Instantiate(Resources.Load<Enemy>("Enemies/114_DireWolf_16"));

        banditChief = Instantiate(Resources.Load<BossEnemy>("Enemies/111_BanditChief"));

        gladiator_11 = Instantiate(Resources.Load<Enemy>("Enemies/115_Gladiator_11"));
        gladiator_14 = Instantiate(Resources.Load<Enemy>("Enemies/115_Gladiator_14"));
        gladiator_17 = Instantiate(Resources.Load<Enemy>("Enemies/115_Gladiator_17"));

        warlock_11 = Instantiate(Resources.Load<Enemy>("Enemies/116_Warlock_11"));
        warlock_14 = Instantiate(Resources.Load<Enemy>("Enemies/116_Warlock_14"));

        witch_11 = Instantiate(Resources.Load<Enemy>("Enemies/117_Witch_11"));
        witch_14 = Instantiate(Resources.Load<Enemy>("Enemies/117_Witch_14"));

        veteran = Instantiate(Resources.Load<BossEnemy>("Enemies/118_Veteran"));

        chiefBattlemage = Instantiate(Resources.Load<Enemy>("Enemies/119_ChiefBattlemage"));
        chiefKnight = Instantiate(Resources.Load<Enemy>("Enemies/120_ChiefKnight"));
        warWolf = Instantiate(Resources.Load<Enemy>("Enemies/121_WarWolf"));

        arenaGrandmaster = Instantiate(Resources.Load<BossEnemy>("Enemies/122_ArenaGrandmaster"));

        ironRat = Instantiate(Resources.Load<Enemy>("Enemies/123_IronRat"));
        weakenedIronRat = Instantiate(Resources.Load<Enemy>("Enemies/123_WeakenedIronRat"));
        brownBear = Instantiate(Resources.Load<Enemy>("Enemies/124_BrownBear"));
        forestBandit = Instantiate(Resources.Load<Enemy>("Enemies/125_ForestBandit"));
        errantKnight = Instantiate(Resources.Load<Enemy>("Enemies/126_ErrantKnight"));
        stunnedErrantKnight = Instantiate(Resources.Load<Enemy>("Enemies/126_StunnedErrantKnight"));
        skeleton = Instantiate(Resources.Load<Enemy>("Enemies/127_Skeleton"));
        weakenedSkeleton = Instantiate(Resources.Load<Enemy>("Enemies/127_WeakenedSkeleton"));
        zombie = Instantiate(Resources.Load<Enemy>("Enemies/128_Zombie"));

        EnemyDictionary.Add(smallRat.GetID(), smallRat);

        EnemyDictionary.Add(rat_01.GetID(), rat_01);
        EnemyDictionary.Add(rat_03.GetID(), rat_03);
        EnemyDictionary.Add(rat_05.GetID(), rat_05);

        EnemyDictionary.Add(ratPack_03.GetID(), ratPack_03);
        EnemyDictionary.Add(ratPack_07.GetID(), ratPack_07);
        EnemyDictionary.Add(ratPack_10.GetID(), ratPack_10);

        EnemyDictionary.Add(giantRat.GetID(), giantRat);

        EnemyDictionary.Add(blueSlime_03.GetID(), blueSlime_03);
        EnemyDictionary.Add(blueSlime_05.GetID(), blueSlime_05);

        EnemyDictionary.Add(greenSlime_03.GetID(), greenSlime_03);
        EnemyDictionary.Add(greenSlime_05.GetID(), greenSlime_05);

        EnemyDictionary.Add(redSlime_03.GetID(), redSlime_03);
        EnemyDictionary.Add(redSlime_05.GetID(), redSlime_05);

        EnemyDictionary.Add(ratKing.GetID(), ratKing);

        EnemyDictionary.Add(amalgamSlime_06.GetID(), amalgamSlime_06);
        EnemyDictionary.Add(amalgamSlime_10.GetID(), amalgamSlime_10);
        EnemyDictionary.Add(amalgamSlime_14.GetID(), amalgamSlime_14);
        EnemyDictionary.Add(amalgamSlime_18.GetID(), amalgamSlime_18);

        EnemyDictionary.Add(goblinShaman_06.GetID(), goblinShaman_06);
        EnemyDictionary.Add(goblinShaman_09.GetID(), goblinShaman_09);

        EnemyDictionary.Add(capturedBandit_06.GetID(), capturedBandit_06);
        EnemyDictionary.Add(capturedBandit_10.GetID(), capturedBandit_10);
        EnemyDictionary.Add(capturedBandit_14.GetID(), capturedBandit_14);
        EnemyDictionary.Add(capturedBandit_18.GetID(), capturedBandit_18);

        EnemyDictionary.Add(castleGuard.GetID(), castleGuard);

        EnemyDictionary.Add(grayWolf_06.GetID(), grayWolf_06);
        EnemyDictionary.Add(grayWolf_09.GetID(), grayWolf_09);

        EnemyDictionary.Add(direWolf_08.GetID(), direWolf_08);
        EnemyDictionary.Add(direWolf_12.GetID(), direWolf_12);
        EnemyDictionary.Add(direWolf_16.GetID(), direWolf_16);

        EnemyDictionary.Add(banditChief.GetID(), banditChief);

        EnemyDictionary.Add(gladiator_11.GetID(), gladiator_11);
        EnemyDictionary.Add(gladiator_14.GetID(), gladiator_14);
        EnemyDictionary.Add(gladiator_17.GetID(), gladiator_17);

        EnemyDictionary.Add(warlock_11.GetID(), warlock_11);
        EnemyDictionary.Add(warlock_14.GetID(), warlock_14);

        EnemyDictionary.Add(witch_11.GetID(), witch_11);
        EnemyDictionary.Add(witch_14.GetID(), witch_14);

        EnemyDictionary.Add(veteran.GetID(), veteran);

        EnemyDictionary.Add(chiefBattlemage.GetID(), chiefBattlemage);
        EnemyDictionary.Add(chiefKnight.GetID(), chiefKnight);
        EnemyDictionary.Add(warWolf.GetID(), warWolf);

        EnemyDictionary.Add(arenaGrandmaster.GetID(), arenaGrandmaster);

        EnemyDictionary.Add(ironRat.GetID(), ironRat);
        EnemyDictionary.Add(weakenedIronRat.GetID(), weakenedIronRat);
        EnemyDictionary.Add(brownBear.GetID(), brownBear);
        EnemyDictionary.Add(forestBandit.GetID(), forestBandit);
        EnemyDictionary.Add(errantKnight.GetID(), errantKnight);
        EnemyDictionary.Add(stunnedErrantKnight.GetID(), stunnedErrantKnight);
        EnemyDictionary.Add(skeleton.GetID(), skeleton);
        EnemyDictionary.Add(weakenedSkeleton.GetID(), weakenedSkeleton);
        EnemyDictionary.Add(zombie.GetID(), zombie);
    }

    void InitializeSkills()
    {
        SkillDictionary = new SortedDictionary<uint, Skill>();

        bash = Instantiate(Resources.Load<Skill>("Skills/Weapon Skills/1_Bash"));
        cleave = Instantiate(Resources.Load<Skill>("Skills/Weapon Skills/2_Cleave"));
        cut = Instantiate(Resources.Load<Skill>("Skills/Weapon Skills/3_Cut"));
        jab = Instantiate(Resources.Load<Skill>("Skills/Weapon Skills/4_Jab"));
        pierce = Instantiate(Resources.Load<Skill>("Skills/Weapon Skills/5_Pierce"));
        punch = Instantiate(Resources.Load<Skill>("Skills/Weapon Skills/6_Punch"));
        slash = Instantiate(Resources.Load<Skill>("Skills/Weapon Skills/7_Slash"));
        stab = Instantiate(Resources.Load<Skill>("Skills/Weapon Skills/8_Stab"));

        blindingLight = Instantiate(Resources.Load<Skill>("Skills/Weapon Skills/Artifact/10_BlindingLight"));
        holyStrike = Instantiate(Resources.Load<Skill>("Skills/Weapon Skills/Artifact/11_HolyStrike"));

        SkillDictionary.Add(bash.GetID(), bash);
        SkillDictionary.Add(cleave.GetID(), cleave);
        SkillDictionary.Add(cut.GetID(), cut);
        SkillDictionary.Add(jab.GetID(), jab);
        SkillDictionary.Add(pierce.GetID(), pierce);
        SkillDictionary.Add(punch.GetID(), punch);
        SkillDictionary.Add(slash.GetID(), slash);
        SkillDictionary.Add(stab.GetID(), stab);

        SkillDictionary.Add(blindingLight.GetID(), blindingLight);
        SkillDictionary.Add(holyStrike.GetID(), holyStrike);

        heavySwing = Instantiate(Resources.Load<StaminaSkill>("Skills/100_HeavySwing"));
        targetedStrike = Instantiate(Resources.Load<StaminaSkill>("Skills/101_TargetedStrike"));
        masterfulStab = Instantiate(Resources.Load<StaminaSkill>("Skills/102_MasterfulStab"));
        kick = Instantiate(Resources.Load<StaminaSkill>("Skills/103_Kick"));
        slam = Instantiate(Resources.Load<StaminaSkill>("Skills/104_Slam"));
        suplex = Instantiate(Resources.Load<StaminaSkill>("Skills/105_Suplex"));
        backhand = Instantiate(Resources.Load<StaminaSkill>("Skills/106_Backhand"));
        pommelThrow = Instantiate(Resources.Load<StaminaSkill>("Skills/107_PommelThrow"));
        concussiveStrike = Instantiate(Resources.Load<StaminaSkill>("Skills/108_ConcussiveStrike"));
        rush = Instantiate(Resources.Load<StaminaSkill>("Skills/109_Rush"));
        sprint = Instantiate(Resources.Load<StaminaSkill>("Skills/110_Sprint"));
        litheGrace = Instantiate(Resources.Load<StaminaSkill>("Skills/111_LitheGrace"));
        temper = Instantiate(Resources.Load<StaminaSkill>("Skills/112_Temper"));
        rage = Instantiate(Resources.Load<StaminaSkill>("Skills/113_Rage"));
        frenzy = Instantiate(Resources.Load<StaminaSkill>("Skills/114_Frenzy"));
        redoubt = Instantiate(Resources.Load<StaminaSkill>("Skills/115_Redoubt"));
        bulwark = Instantiate(Resources.Load<StaminaSkill>("Skills/116_Bulwark"));
        bastion = Instantiate(Resources.Load<StaminaSkill>("Skills/117_Bastion"));
        seep = Instantiate(Resources.Load<StaminaSkill>("Skills/118_Seep"));
        bleed = Instantiate(Resources.Load<StaminaSkill>("Skills/119_Bleed"));
        hemorrhage = Instantiate(Resources.Load<StaminaSkill>("Skills/120_Hemorrhage"));

        SkillDictionary.Add(heavySwing.GetID(), heavySwing);
        SkillDictionary.Add(targetedStrike.GetID(), targetedStrike);
        SkillDictionary.Add(masterfulStab.GetID(), masterfulStab);
        SkillDictionary.Add(kick.GetID(), kick);
        SkillDictionary.Add(slam.GetID(), slam);
        SkillDictionary.Add(suplex.GetID(), suplex);
        SkillDictionary.Add(backhand.GetID(), backhand);
        SkillDictionary.Add(pommelThrow.GetID(), pommelThrow);
        SkillDictionary.Add(concussiveStrike.GetID(), concussiveStrike);
        SkillDictionary.Add(rush.GetID(), rush);
        SkillDictionary.Add(sprint.GetID(), sprint);
        SkillDictionary.Add(litheGrace.GetID(), litheGrace);
        SkillDictionary.Add(temper.GetID(), temper);
        SkillDictionary.Add(rage.GetID(), rage);
        SkillDictionary.Add(frenzy.GetID(), frenzy);
        SkillDictionary.Add(redoubt.GetID(), redoubt);
        SkillDictionary.Add(bulwark.GetID(), bulwark);
        SkillDictionary.Add(bastion.GetID(), bastion);
        SkillDictionary.Add(seep.GetID(), seep);
        SkillDictionary.Add(bleed.GetID(), bleed);
        SkillDictionary.Add(hemorrhage.GetID(), hemorrhage);

        fireball = Instantiate(Resources.Load<ManaSkill>("Skills/121_Fireball"));
        inferno = Instantiate(Resources.Load<ManaSkill>("Skills/122_Inferno"));
        immolation = Instantiate(Resources.Load<ManaSkill>("Skills/123_Immolation"));
        frostbite = Instantiate(Resources.Load<ManaSkill>("Skills/124_Frostbite"));
        iceSpike = Instantiate(Resources.Load<ManaSkill>("Skills/125_IceSpike"));
        glaciate = Instantiate(Resources.Load<ManaSkill>("Skills/126_Glaciate"));
        sparks = Instantiate(Resources.Load<ManaSkill>("Skills/127_Sparks"));
        lightningBolt = Instantiate(Resources.Load<ManaSkill>("Skills/128_LightningBolt"));
        smite = Instantiate(Resources.Load<ManaSkill>("Skills/129_Smite"));
        mend = Instantiate(Resources.Load<ManaSkill>("Skills/130_Mend"));
        heal = Instantiate(Resources.Load<ManaSkill>("Skills/131_Heal"));
        rejuvenate = Instantiate(Resources.Load<ManaSkill>("Skills/132_Rejuvenate"));
        abate = Instantiate(Resources.Load<ManaSkill>("Skills/133_Abate"));
        allay = Instantiate(Resources.Load<ManaSkill>("Skills/134_Allay"));
        dispel = Instantiate(Resources.Load<ManaSkill>("Skills/135_Dispel"));
        magicArrow = Instantiate(Resources.Load<ManaSkill>("Skills/136_MagicArrow"));
        magicBolt = Instantiate(Resources.Load<ManaSkill>("Skills/137_MagicBolt"));
        magicMissile = Instantiate(Resources.Load<ManaSkill>("Skills/138_MagicMissile"));

        SkillDictionary.Add(fireball.GetID(), fireball);
        SkillDictionary.Add(inferno.GetID(), inferno);
        SkillDictionary.Add(immolation.GetID(), immolation);
        SkillDictionary.Add(frostbite.GetID(), frostbite);
        SkillDictionary.Add(iceSpike.GetID(), iceSpike);
        SkillDictionary.Add(glaciate.GetID(), glaciate);
        SkillDictionary.Add(sparks.GetID(), sparks);
        SkillDictionary.Add(lightningBolt.GetID(), lightningBolt);
        SkillDictionary.Add(smite.GetID(), smite);
        SkillDictionary.Add(mend.GetID(), mend);
        SkillDictionary.Add(heal.GetID(), heal);
        SkillDictionary.Add(rejuvenate.GetID(), rejuvenate);
        SkillDictionary.Add(abate.GetID(), abate);
        SkillDictionary.Add(allay.GetID(), allay);
        SkillDictionary.Add(dispel.GetID(), dispel);
        SkillDictionary.Add(magicArrow.GetID(), magicArrow);
        SkillDictionary.Add(magicBolt.GetID(), magicBolt);
        SkillDictionary.Add(magicMissile.GetID(), magicMissile);

    }

    void InitializeQuests()
    {
        QuestDictionary = new SortedDictionary<uint, Quest>();

        fetchRatSkull = Instantiate(Resources.Load<FetchQuest>("Quests/0_FetchRatSkull"));
        fetchSlimeEssense = Instantiate(Resources.Load<FetchQuest>("Quests/1_FetchSlimeEssense"));
        fetchRatFur = Instantiate(Resources.Load<FetchQuest>("Quests/2_FetchRatFur"));
        slayGiantRat = Instantiate(Resources.Load<SlayQuest>("Quests/4_SlayGiantRat"));

        fetchLockpick = Instantiate(Resources.Load<FetchQuest>("Quests/10_FetchLockpick"));
        fetchWolfFur = Instantiate(Resources.Load<FetchQuest>("Quests/11_FetchWolfFur"));
        fetchRatMeat = Instantiate(Resources.Load<FetchQuest>("Quests/12_FetchRatMeat"));
        fetchWolfMeat = Instantiate(Resources.Load<FetchQuest>("Quests/13_FetchWolfMeat"));

        fetchArmorPiece = Instantiate(Resources.Load<FetchQuest>("Quests/15_FetchArmorPiece"));
        talkTemrikToBlacksmith = Instantiate(Resources.Load<TalkQuest>("Quests/16_TalkTemrikToBlacksmith"));
        talkTemrikToAlchemist = Instantiate(Resources.Load<TalkQuest>("Quests/17_TalkTemrikToAlchemist"));
        talkVarianToMysteriousMan = Instantiate(Resources.Load<TalkQuest>("Quests/18_TalkVarianToMysteriousMan"));
        clearIronRatCave = Instantiate(Resources.Load<ClearQuest>("Quests/19_ClearIronRatCave"));
        clearCreogCave = Instantiate(Resources.Load<ClearQuest>("Quests/20_ClearCreogCave"));

        QuestDictionary.Add(fetchRatSkull.GetID(), fetchRatSkull);
        QuestDictionary.Add(fetchSlimeEssense.GetID(), fetchSlimeEssense);
        QuestDictionary.Add(fetchRatFur.GetID(), fetchRatFur);
        QuestDictionary.Add(slayGiantRat.GetID(), slayGiantRat);

        QuestDictionary.Add(fetchLockpick.GetID(), fetchLockpick);
        QuestDictionary.Add(fetchWolfFur.GetID(), fetchWolfFur);
        QuestDictionary.Add(fetchRatMeat.GetID(), fetchRatMeat);
        QuestDictionary.Add(fetchWolfMeat.GetID(), fetchWolfMeat);
        QuestDictionary.Add(fetchArmorPiece.GetID(), fetchArmorPiece);
        QuestDictionary.Add(talkTemrikToBlacksmith.GetID(), talkTemrikToBlacksmith);
        QuestDictionary.Add(talkTemrikToAlchemist.GetID(), talkTemrikToAlchemist);
        QuestDictionary.Add(talkVarianToMysteriousMan.GetID(), talkVarianToMysteriousMan);
        QuestDictionary.Add(clearIronRatCave.GetID(), clearIronRatCave);
        QuestDictionary.Add(clearCreogCave.GetID(), clearCreogCave);

    }

    void InitializeUI()
    {
        dialogueManager = gameObject.GetComponent<DialogueManager>();
        battleManager = gameObject.GetComponent<BattleManager>();
        dungeonManager = gameObject.GetComponent<DungeonManager>();
        eventManager = gameObject.GetComponent<EventManager>();

        UIInventoryScreen = GameObject.Find("UI Inventory");
        UIDirectionScreen = GameObject.Find("UI Directional");
        UIQuestScreen = GameObject.Find("UI Quest");

        UIPickupScreen = GameObject.Find("UI Pickup");
        UIArenaScreen = GameObject.Find("UI Arena");
        UISkillScreen = GameObject.Find("UI Skill");
        UIStatsScreen = GameObject.Find("UI Stats");

        UILoadScreen = GameObject.Find("UI Load");
        UIPauseScreen = GameObject.Find("UI Pause");
        UICoverScreen = GameObject.Find("UI Cover");
        UIRatingScreen = GameObject.Find("UI Rating");

        directionalHealthSlider = GameObject.Find("DirectionalHealthSlider").GetComponent<Slider>();
        directionalStaminaSlider = GameObject.Find("DirectionalStaminaSlider").GetComponent<Slider>();
        directionalManaSlider = GameObject.Find("DirectionalManaSlider").GetComponent<Slider>();

        dropItemBtn = GameObject.Find("DropItemBtn").GetComponent<Button>();
        useItemBtn = GameObject.Find("UseItemBtn").GetComponent<Button>();
        equipItemBtn = GameObject.Find("EquipItemBtn").GetComponent<Button>();
        unequipItemBtn = GameObject.Find("UnequipItemBtn").GetComponent<Button>();

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

        equipItemBtn.gameObject.SetActive(false);
        unequipItemBtn.gameObject.SetActive(false);
        dropItemBtn.gameObject.SetActive(false);
        useItemBtn.gameObject.SetActive(false);

        makeActiveBtn = GameObject.Find("MakeActiveBtn").GetComponent<Button>();
        turnInQuestBtn = GameObject.Find("TurnInQuestBtn").GetComponent<Button>();

        makeActiveBtn.gameObject.SetActive(false);
        turnInQuestBtn.gameObject.SetActive(false);

        itemWeightObj = GameObject.Find("ItemWeight");
        itemValueObj = GameObject.Find("ItemValue");
        itemDamageObj = GameObject.Find("ItemDamage");
        itemArmorObj = GameObject.Find("ItemArmor");
        itemConsumableObj = GameObject.Find("ItemConsumable");

        nameTxt = GameObject.Find("NameTxt").GetComponent<Text>();
        descriptionTxt = GameObject.Find("DescriptionTxt").GetComponent<Text>();
        valueTxt = GameObject.Find("ValueTxt").GetComponent<Text>();
        weightTxt = GameObject.Find("WeightTxt").GetComponent<Text>();
        totalInvWeightTxt = GameObject.Find("TotalInvWeightTxt").GetComponent<Text>();
        goldTxt = GameObject.Find("InventoryGoldTxt").GetComponent<Text>();

        armorValueTxt = GameObject.Find("ArmorValueTxt").GetComponent<Text>();
        damageValueTxt = GameObject.Find("DamageValueTxt").GetComponent<Text>();

        itemWeightObj.SetActive(false);
        itemValueObj.SetActive(false);
        itemDamageObj.SetActive(false);
        itemArmorObj.SetActive(false);
        itemConsumableObj.SetActive(false);

        nameTxt.text = "";
        descriptionTxt.text = "";
        valueTxt.text = "";
        weightTxt.text = "";
        totalInvWeightTxt.text = "";
        goldTxt.text = 0.ToString();
        armorValueTxt.text = "0";
        damageValueTxt.text = "0";

        invScroll.verticalNormalizedPosition = 1;

        activeQuestScroll = GameObject.Find("ActiveQuestScroll").GetComponent<ScrollRect>();

        questName = GameObject.Find("ActiveQuestNameTxt").GetComponent<Text>();
        questDescription = GameObject.Find("ActiveQuestDescriptionTxt").GetComponent<Text>();
        noActiveQuestTxt = GameObject.Find("NoActiveQuestTxt");

        questName.text = "";
        questDescription.text = "";


        otherPickUpNameTxt = GameObject.Find("OtherPickupNameTxt").GetComponent<Text>();
        playerPickUpNameTxt = GameObject.Find("PlayerPickUpNameTxt").GetComponent<Text>();
        playerPickupWeightTxt = GameObject.Find("PlayerPickupWeightTxt").GetComponent<Text>();
        pickupGoldTxt = GameObject.Find("PickupGoldTxt").GetComponent<Text>();

        pickUpDescriptionTxt = GameObject.Find("PickupDescriptionTxt").GetComponent<Text>();
        pickUpNameTxt = GameObject.Find("PickupNameTxt").GetComponent<Text>();
        pickUpWeightTxt = GameObject.Find("PickupWeightTxt").GetComponent<Text>();
        pickUpValueTxt = GameObject.Find("PickupValueTxt").GetComponent<Text>();
        pickUpDamageTxt = GameObject.Find("PickupDamageTxt").GetComponent<Text>();
        pickUpArmorTxt = GameObject.Find("PickupArmorTxt").GetComponent<Text>();
        pickUpConsumableTxt = GameObject.Find("PickupConsumableTxt").GetComponent<Text>();

        pickupDropItemBtn = GameObject.Find("PickupDropItemBtn").GetComponent<Button>();
        pickupItemBtn = GameObject.Find("PickupItemBtn").GetComponent<Button>();
        pickupExitBtn = GameObject.Find("PickupExitBtn").GetComponent<Button>();
        pickupExitBtn.GetComponent<Button>().onClick.AddListener(() => ActivatePickupScreen(false));

        pickUpWeightObj = GameObject.Find("PickupItemWeight");
        pickUpValueObj = GameObject.Find("PickupItemValue");
        pickUpDamageObj = GameObject.Find("PickupItemDamage");
        pickUpArmorObj = GameObject.Find("PickupItemArmor");
        pickUpConsumableObj = GameObject.Find("PickupItemConsumable");

        pickUpDescriptionTxt.text = "";
        pickUpNameTxt.text = "";

        pickUpWeightObj.SetActive(false);
        pickUpValueObj.SetActive(false);
        pickUpDamageObj.SetActive(false);
        pickUpArmorObj.SetActive(false);
        pickUpConsumableObj.SetActive(false);

        enemyABtn = GameObject.Find("EnemyABtn");
        enemyBBtn = GameObject.Find("EnemyBBtn");
        enemyCBtn = GameObject.Find("EnemyCBtn");

        enemyANameTxt = GameObject.Find("EnemyANameTxt").GetComponent<Text>();
        enemyBNameTxt = GameObject.Find("EnemyBNameTxt").GetComponent<Text>();
        enemyCNameTxt = GameObject.Find("EnemyCNameTxt").GetComponent<Text>();

        enemyARewardTxt = GameObject.Find("EnemyARewardTxt").GetComponent<Text>();
        enemyBRewardTxt = GameObject.Find("EnemyBRewardTxt").GetComponent<Text>();
        enemyCRewardTxt = GameObject.Find("EnemyCRewardTxt").GetComponent<Text>();

        arenaEnemyDamageTxt = GameObject.Find("ArenaDamageTxt").GetComponent<Text>();
        arenaEnemySpeedTxt = GameObject.Find("ArenaSpeedTxt").GetComponent<Text>();
        arenaEnemyHealthTxt = GameObject.Find("ArenaHealthTxt").GetComponent<Text>();
        arenaEnemyStaminaTxt = GameObject.Find("ArenaStaminaTxt").GetComponent<Text>();
        arenaEnemyManaTxt = GameObject.Find("ArenaManaTxt").GetComponent<Text>();

        arenaEnemyHealthObj = GameObject.Find("ArenaEnemyHealth");
        arenaEnemyStaminaObj = GameObject.Find("ArenaEnemyStamina");
        arenaEnemyManaObj = GameObject.Find("ArenaEnemyMana");
        arenaEnemySpeedObj = GameObject.Find("ArenaEnemySpeed");
        arenaEnemyDamageObj = GameObject.Find("ArenaEnemyDamage");

        arenaEnemyHealthObj.SetActive(false);
        arenaEnemyStaminaObj.SetActive(false);
        arenaEnemyManaObj.SetActive(false);
        arenaEnemySpeedObj.SetActive(false);
        arenaEnemyDamageObj.SetActive(false);

        skillScrollView = GameObject.Find("SkillScrollView");
        skillPanel = GameObject.Find("SkillPanel");

        skillImg = GameObject.Find("SkillImg").GetComponent<Image>();
        skillNameTxt = GameObject.Find("SkillNameTxt").GetComponent<Text>();
        skillDescriptionTxt = GameObject.Find("SkillDescriptionTxt").GetComponent<Text>();
        skillTypeTxt = GameObject.Find("SkillTypeTxt").GetComponent<Text>();
        skillCostTxt = GameObject.Find("SkillCostTxt").GetComponent<Text>();
        skillDamageTxt = GameObject.Find("SkillDamageTxt").GetComponent<Text>();
        skillShortDescriptionTxt = GameObject.Find("SkillShortDescriptionTxt").GetComponent<Text>();

        useSkillBtn = GameObject.Find("UseSkillBtn").GetComponent<Button>();

        statsPlayerNameTxt = GameObject.Find("StatsNameTxt").GetComponent<Text>();
        statsPlayerTitleTxt = GameObject.Find("StatsTitleTxt").GetComponent<Text>();
        statsHealthTxt = GameObject.Find("StatsHealthTxt").GetComponent<Text>();
        statsStaminaTxt = GameObject.Find("StatsStaminaTxt").GetComponent<Text>();
        statsManaTxt = GameObject.Find("StatsManaTxt").GetComponent<Text>();
        statsExpTxt = GameObject.Find("StatsExpTxt").GetComponent<Text>();
        statsLvlTxt = GameObject.Find("StatsLvlTxt").GetComponent<Text>();
        statsTotalExpTxt = GameObject.Find("StatsTotalExpTxt").GetComponent<Text>();
        statsSkillPointsTxt = GameObject.Find("StatsSkillPointsTxt").GetComponent<Text>();

        statsWeightTxt = GameObject.Find("StatsWeightTxt").GetComponent<Text>();
        statsGoldTxt = GameObject.Find("StatsGoldTxt").GetComponent<Text>();
        statsDmgTxt = GameObject.Find("StatsDmgTxt").GetComponent<Text>();
        statsDefTxt = GameObject.Find("StatsDefTxt").GetComponent<Text>();
        statsSpeedTxt = GameObject.Find("StatsSpeedTxt").GetComponent<Text>();

        playerStrengthTxt = GameObject.Find("PlayerStrengthTxt").GetComponent<Text>();
        playerAgilityTxt = GameObject.Find("PlayerAgilityTxt").GetComponent<Text>();
        playerIntelligenceTxt = GameObject.Find("PlayerIntelligenceTxt").GetComponent<Text>();
        playerLuckTxt = GameObject.Find("PlayerLuckTxt").GetComponent<Text>();

        playerStrengthBtn = GameObject.Find("PlayerStrengthBtn");
        playerAgilityBtn = GameObject.Find("PlayerAgilityBtn");
        playerIntelligenceBtn = GameObject.Find("PlayerIntelligenceBtn");
        playerLuckBtn = GameObject.Find("PlayerLuckBtn");

        statsHealthSlider = GameObject.Find("StatsHealthSlider").GetComponent<Slider>();
        statsStaminaSlider = GameObject.Find("StatsStaminaSlider").GetComponent<Slider>();
        statsManaSlider = GameObject.Find("StatsManaSlider").GetComponent<Slider>();
        statsExpSlider = GameObject.Find("StatsExpSlider").GetComponent<Slider>();



        loadingSlider = GameObject.Find("LoadingSlider").GetComponent<Slider>();
        loadingPercentTxt = GameObject.Find("LoadingPercentTxt").GetComponent<Text>();

        musicSlider = GameObject.Find("MusicVolumeSetting").transform.GetChild(1).GetComponent<Slider>();
        sfxSlider = GameObject.Find("SFXVolumeSetting").transform.GetChild(1).GetComponent<Slider>();

        musicSlider.value = PlayerPrefs.GetFloat("MusicAudio", 0.75f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXAudio", 0.75f);

        messageTxt = GameObject.Find("MessageTxt").GetComponent<Text>();
        deadRestartBtn = GameObject.Find("DeadRestartBtn").GetComponent<Button>();
        acceptNameBtn = GameObject.Find("NameAcceptBtn").GetComponent<Button>();
        nameInputField = GameObject.Find("NameInputField").GetComponent<InputField>();

        UIInventoryScreen.SetActive(false);
        UIQuestScreen.SetActive(false);

        UIPickupScreen.SetActive(false);
        UIArenaScreen.SetActive(false);
        UISkillScreen.SetActive(false);
        UIStatsScreen.SetActive(false);

        UIPauseScreen.SetActive(false);
        UILoadScreen.SetActive(false);
        UIRatingScreen.SetActive(false);



        UpdateInventoryAttributes();
    }

    //**


    // Battle Methods

    //**

    void CheckLocationQuestCompletion()
    {
        foreach (KeyValuePair<uint, GameObject> kvp in uiQuestSlots)
        {
            if (kvp.Value.GetComponent<QuestContainer>().GetQuest().GetQuestType() == Quest.QuestType.Location)
            {
                LocationQuest locationQuest = (LocationQuest)kvp.Value.GetComponent<QuestContainer>().GetQuest();
                if (locationQuest.CheckQuestCompletion(player))
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
        if (!player.CheckForQuest(QuestDictionary[quest.GetID()].GetID()))
        {
            GameObject questSlot = GameObject.Instantiate(uiQuestSlot, questPanel.transform);
            questSlot.GetComponent<QuestContainer>().SetQuest(QuestDictionary[quest.GetID()]);
            questSlot.GetComponent<Button>().onClick.AddListener(() => DisplayQuest(questSlot.GetComponent<QuestContainer>()));
            questSlot.GetComponent<Button>().onClick.AddListener(() => GameObject.Find("ButtonAudioSource").GetComponent<AudioSource>().Play());
            uiQuestSlots.Add(QuestDictionary[quest.GetID()].GetID(), questSlot);
            player.AddQuest(QuestDictionary[quest.GetID()]);
        }
    }

    void LoadQuestSlots()
    {
        for (int i = 0; i < player.GetQuestList().Count; i++)
        {
            GameObject questSlot = Instantiate(uiQuestSlot, questPanel.transform);
            questSlot.GetComponent<QuestContainer>().SetQuest(QuestDictionary[player.GetQuestList()[i].GetID()]);
            questSlot.GetComponent<Button>().onClick.AddListener(() => DisplayQuest(questSlot.GetComponent<QuestContainer>()));
            questSlot.GetComponent<Button>().onClick.AddListener(() => GameObject.Find("ButtonAudioSource").GetComponent<AudioSource>().Play());
            uiQuestSlots.Add(QuestDictionary[player.GetQuestList()[i].GetID()].GetID(), questSlot);
        }
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


    // Inventory Methods

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
                invSlot.GetComponent<Button>().onClick.AddListener(() => GameObject.Find("ButtonAudioSource").GetComponent<AudioSource>().Play());
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

    public void LoadInventorySlot(Item item, uint count = 1)
    {
        while (count > 0)
        {
            // Creates a new slot if Inventory Slots does not contain item
            if (!uiInvSlots.ContainsKey(item.GetID()))
            {
                // Creates a new invSlot clone.
                GameObject invSlot = GameObject.Instantiate(uiInvSlot, invPanel.transform);
                // Sets the ItemContainer component of new invSlot to item
                invSlot.GetComponent<ItemContainer>().SetItem(item);
                // Adds new onClick command to invSlot's button component
                invSlot.GetComponent<Button>().onClick.AddListener(() => DisplayItem(invSlot.GetComponent<ItemContainer>()));
                invSlot.GetComponent<Button>().onClick.AddListener(() => GameObject.Find("ButtonAudioSource").GetComponent<AudioSource>().Play());
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

    public void GenerateItemPickup(ItemLootTable itemLootTable)
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
            pickupSlot.GetComponent<Button>().onClick.AddListener(() => GameObject.Find("ButtonAudioSource").GetComponent<AudioSource>().Play());
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
            if (dialogueManager.isInNPC)
            {
                //currentShop.AddItem(activeItem);
                player.ChangeGold((int)(activeItem.GetValue() * 0.75f));
            }
            RemoveFromInventory(activeItem);
        }
        UpdateInventoryAttributes();
    }

    public void PickupItem()
    {
        RemoveFromPickup(activeItem);
    }

    public void RemoveFromInventory(Item item)
    {
        player.GetInventory().RemoveItem(item.GetID());
        uiInvSlots[item.GetID()].GetComponent<ItemContainer>().RemoveItem();

        if (uiInvSlots[item.GetID()].GetComponent<ItemContainer>().IsEmpty() || !player.GetInventory().CheckForItem(item.GetID()))
        {
            GameObject invSlot = uiInvSlots[item.GetID()];

            if ((UIInventoryScreen.activeInHierarchy && !battleManager.isInBattle) || isInPickup)
                DeactivateInvSelection();
            else
                DeactivateInvSelection(false);

            uiInvSlots.Remove(item.GetID());
            GameObject.Destroy(invSlot);

            OrderDictionary(uiInvSlots);
        }

        SetCurrentWeight();

        //invScroll.verticalNormalizedPosition = 1;
    }

    public void RemoveFromPickup(Item item)
    {
        uiPickupSlots[item.GetID()].GetComponent<ItemContainer>().RemoveItem();
        if (uiPickupSlots[item.GetID()].GetComponent<ItemContainer>().IsEmpty())
        {
            GameObject pickupSlot = uiPickupSlots[item.GetID()];

            uiPickupSlots.Remove(item.GetID());
            GameObject.Destroy(pickupSlot);

            DeactivateInvSelection(false);
            OrderDictionary(uiPickupSlots);
        }

        if (isInPickup)
        {
            AddToInventory(item);
            if (dialogueManager.isInNPC && !battleManager.isInBattle && dialogueManager.currentNPC.HasStore())
            {
                StoreDictionary[dialogueManager.currentNPC.GetStore().GetID()].RemoveItem(item);
                player.ChangeGold(-(int)(item.GetValue()));
                if (player.GetGold() < item.GetValue())
                    pickupItemBtn.interactable = false;
            }
        }
        UpdateInventoryAttributes();
        pickupScroll.verticalNormalizedPosition = 1;
    }

    public void DisplayItem(ItemContainer itemContainer)
    {
        if (!isInPickup)
        {
            if (itemContainer.GetItem() != NULL_ITEM && itemContainer.GetItem() != NULL_WEAPON && itemContainer.GetItem() != NULL_ARMOR)
            {
                activeItem = itemContainer.GetItem();
                dropItemBtn.gameObject.SetActive(true);

                itemWeightObj.SetActive(true);
                itemValueObj.SetActive(true);

                unequipItemBtn.gameObject.SetActive(false);
                equipItemBtn.gameObject.SetActive(false);

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
                    if (c.GetConsumableType() == Consumable.ConsumableType.potion)
                        itemConsumableObj.GetComponentInChildren<Text>().text = ((Potion)c).GetEffectChanges()[0].ToString();
                    else if (c.GetConsumableType() == Consumable.ConsumableType.edible)
                        itemConsumableObj.GetComponentInChildren<Text>().text = ((Edible)c).GetEffectChange().ToString();
                    else
                    {
                        if (battleManager.isInBattle)
                            useItemBtn.gameObject.SetActive(false);
                        itemConsumableObj.SetActive(false);
                    }
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
            if (itemContainer.GetItem() != NULL_ITEM && itemContainer.GetItem() != NULL_WEAPON && itemContainer.GetItem() != NULL_ARMOR)
            {

                pickUpWeightObj.SetActive(true);
                pickUpValueObj.SetActive(true);

                activeItem = itemContainer.GetItem();

                bool npcContainsItem = false;
                foreach (GameObject item in uiPickupSlots.Values.ToList())
                {
                    if (item.GetComponent<ItemContainer>().GetItem() == activeItem)
                    {
                        npcContainsItem = true;
                        break;
                    }
                }

                if ((npcContainsItem && (!dialogueManager.isInNPC || dialogueManager.isInNPC && !dialogueManager.currentNPC.HasStore())) || (dialogueManager.isInNPC && dialogueManager.currentNPC.HasStore() && npcContainsItem && activeItem.GetValue() <= player.GetGold()))
                {
                    if (player.GetCurrentWeight() + itemContainer.GetItem().GetWeight() <= player.GetMaxWeight())
                        pickupItemBtn.interactable = true;
                    else
                        pickupItemBtn.interactable = false;
                }
                else
                    pickupItemBtn.interactable = false;

                if (player.GetInventory().CheckForItem(activeItem.GetID()))
                    pickupDropItemBtn.interactable = true;
                else
                    pickupDropItemBtn.interactable = false;

                pickUpNameTxt.text = activeItem.GetName();
                pickUpDescriptionTxt.text = activeItem.GetDescription();

                if (invScroll.gameObject.activeInHierarchy && (dialogueManager.isInNPC && dialogueManager.currentNPC.HasStore()))
                    pickUpValueTxt.text = ((int)(activeItem.GetValue() * 0.75f)).ToString();
                else
                    pickUpValueTxt.text = activeItem.GetValue().ToString();

                pickUpWeightTxt.text = activeItem.GetWeight().ToString();

                if (itemContainer.GetItem().IsWeapon())
                {
                    pickUpDamageObj.SetActive(true);
                    pickUpArmorObj.SetActive(false);
                    pickUpConsumableObj.SetActive(false);

                    Weapon w = (Weapon)itemContainer.GetItem();
                    pickUpDamageObj.GetComponentInChildren<Text>().text = w.GetMaxDamage().ToString();
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
                    if (c.GetConsumableType() == Consumable.ConsumableType.potion)
                        pickUpConsumableObj.GetComponentInChildren<Text>().text = ((Potion)c).GetEffectChanges()[0].ToString();
                    else if (c.GetConsumableType() == Consumable.ConsumableType.edible)
                        pickUpConsumableObj.GetComponentInChildren<Text>().text = ((Edible)c).GetEffectChange().ToString();
                    else
                        pickUpConsumableObj.SetActive(false);
                }
                else
                {
                    pickUpDamageObj.SetActive(false);
                    pickUpArmorObj.SetActive(false);
                    pickUpConsumableObj.SetActive(false);
                }
            }
        }
    }

    public void UseItem()
    {
        Consumable c = (Consumable)activeItem;
        RemoveFromInventory(c);

        switch (c.GetConsumableType())
        {
            case Consumable.ConsumableType.potion:
                GameObject.Find("FoodDrinkAudioSource").GetComponent<AudioSource>().Play();
                ((Potion)c).UseItem(player);
                UpdateInventoryAttributes();
                break;
            case Consumable.ConsumableType.edible:
                GameObject.Find("FoodDrinkAudioSource").GetComponent<AudioSource>().Play();
                ((Edible)c).UseItem(player);
                UpdateInventoryAttributes();
                break;
            case Consumable.ConsumableType.skillBook:
                GameObject.Find("FoodDrinkAudioSource").GetComponent<AudioSource>().Play();
                ((SkillBook)c).UseItem(player);
                break;
        }
    }

    public void AddSkill(Skill skill)
    {
        if (!uiSkillSlots.ContainsKey(skill.GetID()))
        {
            GameObject skillSlot = GameObject.Instantiate(uiSkillSlot, skillPanel.transform);

            skillSlot.GetComponent<SkillContainer>().SetSkill(skill);

            skillSlot.GetComponent<Button>().onClick.AddListener(() => DisplaySkill(skillSlot.GetComponent<SkillContainer>()));
            skillSlot.GetComponent<Button>().onClick.AddListener(() => GameObject.Find("ButtonAudioSource").GetComponent<AudioSource>().Play());

            if (skill.GetSprite() != null)
                skillSlot.transform.GetChild(0).GetComponent<Image>().sprite = skill.GetSprite();
            else
                skillSlot.transform.GetChild(0).gameObject.SetActive(false);


            uiSkillSlots.Add(skill.GetID(), skillSlot);
            OrderDictionary(uiSkillSlots, true);
        }

        skillScrollView.GetComponent<ScrollRect>().verticalNormalizedPosition = 1;
    }

    public void EquipItem()
    {
        if (activeItem.IsWeapon())
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
            damageValueTxt.text = w.GetMaxDamage().ToString();
        }
        else if (activeItem.IsArmor())
        {
            Armor a = (Armor)activeItem;
            switch (a.GetArmorType())
            {
                case Armor.ArmorType.head:
                    Armor h = (Armor)headBtn.gameObject.GetComponent<ItemContainer>().GetItem();
                    if (h == NULL_ARMOR)
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
                    Armor l = (Armor)legsBtn.gameObject.GetComponent<ItemContainer>().GetItem();
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
                    Armor f = (Armor)feetBtn.gameObject.GetComponent<ItemContainer>().GetItem();
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
                    Armor n = (Armor)handsBtn.gameObject.GetComponent<ItemContainer>().GetItem();
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
        armorValueTxt.text = player.GetDefense().ToString();
        DeactivateInvSelection(false);
    }

    public void UnequipItem()
    {
        if (activeItem.IsWeapon())
        {
            Weapon w = (Weapon)activeItem;
            player.UnequipWeapon();
            weaponBtn.gameObject.GetComponent<ItemContainer>().RemoveItem();
            AddToInventory(w);
        }
        else if (activeItem.IsArmor())
        {
            Armor a = (Armor)activeItem;
            Debug.Log("Armor Name:" + a.GetName());
            player.UnequipArmor(a);
            switch (a.GetArmorType())
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
        DeactivateInvSelection(false);
    }

    void DeactivateInvSelection(bool selectNext = true)
    {
        nameTxt.text = "";
        descriptionTxt.text = "";
        valueTxt.text = "";
        weightTxt.text = "";

        pickUpNameTxt.text = "";
        pickUpDescriptionTxt.text = "";
        pickUpValueTxt.text = "";
        pickUpWeightTxt.text = "";

        itemWeightObj.SetActive(false);
        itemValueObj.SetActive(false);
        itemDamageObj.SetActive(false);
        itemArmorObj.SetActive(false);
        itemConsumableObj.SetActive(false);

        dropItemBtn.gameObject.SetActive(false);
        useItemBtn.gameObject.SetActive(false);
        equipItemBtn.gameObject.SetActive(false);
        unequipItemBtn.gameObject.SetActive(false);

        pickUpWeightObj.SetActive(false);
        pickUpValueObj.SetActive(false);
        pickUpDamageObj.SetActive(false);
        pickUpArmorObj.SetActive(false);
        pickUpConsumableObj.SetActive(false);

        pickupDropItemBtn.interactable = false;
        pickupItemBtn.interactable = false;

        activeItem = null;

        /*
        if(!selectNext)
            activeItem = null;
        else
        {
            List<uint> keyList = uiInvSlots.Keys.ToList();
            int currentIDIndex = keyList.IndexOf(activeItem.GetID());
            if (keyList.Count > currentIDIndex)
                DisplayItem(uiInvSlots[keyList[currentIDIndex + 1]].GetComponent<ItemContainer>());
            else if (keyList.Count <= 1)
                activeItem = null;
            else
                DisplayItem(uiInvSlots[keyList[currentIDIndex - 1]].GetComponent<ItemContainer>());            
        }
        */
    }

    //**

    // Stats Methods

    public void LevelAttribute(int attr)
    {
        switch (attr)
        {
            case 0:
                player.SetStrength(player.GetStrength() + 1);
                break;
            case 1:
                player.SetAgility(player.GetAgility() + 1);
                break;
            case 2:
                player.SetIntelligence(player.GetIntelligence() + 1);
                break;
            case 3:
                player.SetLuck(player.GetLuck() + 1);
                break;
        }
        player.SetSkillPoints(player.GetSkillPoints() - 1);
        UpdateInventoryAttributes();
    }

    public void UpdateInventoryAttributes()
    {
        UpdateHealthSliders();
        UpdateStaminaSliders();
        UpdateManaSliders();

        UpdateExpSliders();

        SetCurrentWeight();
        goldTxt.text = player.GetGold().ToString();
        pickupGoldTxt.text = player.GetGold().ToString();
        statsGoldTxt.text = player.GetGold().ToString();
        statsDmgTxt.text = player.GetWeapon().GetMaxDamage().ToString();
        statsDefTxt.text = player.GetDefense().ToString();
        statsSpeedTxt.text = String.Format("{0:f3}", player.GetSpeed());

    }

    void UpdateExpSliders()
    {
        statsExpSlider.maxValue = player.GetToLevelExp();
        statsExpSlider.value = player.GetExp();
        statsTotalExpTxt.text = player.GetTotalExp().ToString();
        statsSkillPointsTxt.text = player.GetSkillPoints().ToString();
        statsLvlTxt.text = player.GetLevel().ToString();
        statsExpTxt.text = player.GetExp() + "/" + player.GetToLevelExp();

        playerStrengthTxt.text = player.GetStrength().ToString();
        playerAgilityTxt.text = player.GetAgility().ToString();
        playerIntelligenceTxt.text = player.GetIntelligence().ToString();
        playerLuckTxt.text = player.GetLuck().ToString();

        if (player.GetSkillPoints() > 0)
        {
            playerStrengthBtn.SetActive(true);
            playerAgilityBtn.SetActive(true);
            playerIntelligenceBtn.SetActive(true);
            playerLuckBtn.SetActive(true);
        }
        else
        {
            playerStrengthBtn.SetActive(false);
            playerAgilityBtn.SetActive(false);
            playerIntelligenceBtn.SetActive(false);
            playerLuckBtn.SetActive(false);
        }
    }

    void UpdateHealthSliders()
    {
        directionalHealthSlider.maxValue = player.GetMaxHealth();
        directionalHealthSlider.value = player.GetHealth();

        statsHealthSlider.maxValue = player.GetMaxHealth();
        statsHealthSlider.value = player.GetHealth();
        statsHealthTxt.text = player.GetHealth() + "/" + player.GetMaxHealth();
    }

    void UpdateStaminaSliders()
    {
        directionalStaminaSlider.maxValue = player.GetMaxStamina();
        directionalStaminaSlider.value = player.GetStamina();

        statsStaminaSlider.maxValue = player.GetMaxStamina();
        statsStaminaSlider.value = player.GetStamina();
        statsStaminaTxt.text = player.GetStamina() + "/" + player.GetMaxStamina();
    }

    void UpdateManaSliders()
    {
        directionalManaSlider.maxValue = player.GetMaxMana();
        directionalManaSlider.value = player.GetMana();

        statsManaSlider.maxValue = player.GetMaxMana();
        statsManaSlider.value = player.GetMana();
        statsManaTxt.text = player.GetMana() + "/" + player.GetMaxMana();
    }

    void SetCurrentWeight()
    {
        totalInvWeightTxt.text = String.Format("{0:f2}/{1:f2}", player.GetCurrentWeight(), player.GetMaxWeight());
        statsWeightTxt.text = String.Format("{0:f2}", player.GetCurrentWeight());
        playerPickupWeightTxt.text = String.Format("{0:f2}/{1:f2}", player.GetCurrentWeight(), player.GetMaxWeight());
    }

    //**


    void OrderDictionary(SortedDictionary<uint, GameObject> dictionary, bool orderSkills = false)
    {
        List<GameObject> gameObjects = new List<GameObject>();

        foreach (KeyValuePair<uint, GameObject> kvp in dictionary)
        {
            gameObjects.Add(kvp.Value);
        }

        if (orderSkills)
            gameObjects.Sort((x, y) => x.GetComponent<SkillContainer>().GetSkill().GetID().CompareTo(y.GetComponent<SkillContainer>().GetSkill().GetID()));
        else
            gameObjects.Sort((x, y) => String.Compare(x.GetComponent<ItemContainer>().GetItem().GetName(), y.GetComponent<ItemContainer>().GetItem().GetName()));

        for (int i = 0; i < gameObjects.Count; i++)
        {
            gameObjects[i].transform.SetSiblingIndex(i);
        }
    }

    public void DisplayEnemy(EnemyContainer enemyContainer)
    {
        selectedEnemy = enemyContainer.GetEnemy();

        arenaEnemyHealthTxt.text = enemyContainer.GetEnemy().GetMaxHealth().ToString();
        arenaEnemyStaminaTxt.text = enemyContainer.GetEnemy().GetMaxStamina().ToString();
        arenaEnemyManaTxt.text = enemyContainer.GetEnemy().GetMaxMana().ToString();
        arenaEnemyDamageTxt.text = enemyContainer.GetEnemy().GetMaxDamage().ToString();
        arenaEnemySpeedTxt.text = enemyContainer.GetEnemy().GetSpeed().ToString();

        arenaEnemyHealthObj.SetActive(true);
        arenaEnemyStaminaObj.SetActive(true);
        arenaEnemyManaObj.SetActive(true);
        arenaEnemySpeedObj.SetActive(true);
        arenaEnemyDamageObj.SetActive(true);

    }

    public void UseSkill()
    {
        if(battleManager.isInBattle)
        {
            battleManager.UseSkill(selectedSkill);
            ActivateSkillScreen(false);
        }
    }

    public void DisplaySkill(SkillContainer skillContainer)
    {
        selectedSkill = SkillDictionary[skillContainer.GetSkill().GetID()];

        if (skillContainer.GetSkill().GetSprite() != null)
        {
            skillImg.sprite = skillContainer.GetSkill().GetSprite();
            skillImg.color = new Color(255, 255, 255, 1f);
        }
        else
            skillImg.color = new Color(255, 255, 255, 0f);

        skillNameTxt.text = selectedSkill.GetName();
        skillDescriptionTxt.text = selectedSkill.GetDescription();

        skillCostTxt.text = "Cost: " + selectedSkill.GetAttributeCost().ToString();

        if((selectedSkill is ManaSkill) && (selectedSkill as ManaSkill).GetMagicType() == ManaSkill.MagicType.heal)
            skillDamageTxt.text = String.Format("Heal: {0}-{1}", selectedSkill.GetMinDamage(), selectedSkill.GetMaxDamage());
        else
            skillDamageTxt.text = String.Format("Damage: {0}-{1}", selectedSkill.GetMinDamage(), selectedSkill.GetMaxDamage());

        skillShortDescriptionTxt.text = selectedSkill.GetShortDescription();

        if (battleManager.isInBattle || !battleManager.isInBattle && selectedSkill.IsUsableOutsideCombat())
            useSkillBtn.gameObject.SetActive(true);

    }

    public void DisplayQuest(QuestContainer questContainer)
    {
        selectedQuest = questContainer.GetQuest();

        if (activeQuest != null)
        {
            if (selectedQuest != activeQuest)
                makeActiveBtn.gameObject.SetActive(true);
            else
                makeActiveBtn.gameObject.SetActive(false);
        }
        else
            makeActiveBtn.gameObject.SetActive(true);

        questName.text = selectedQuest.GetName();
        questDescription.text = selectedQuest.GetDescription();
        noActiveQuestTxt.SetActive(false);

        /*
        selQuestName.text = selectedQuest.GetName();
        selQuestDescription.text = selectedQuest.GetDescription();
        noSelectedQuestTxt.SetActive(false);
        */
    }

    public void MakeQuestActive()
    {
        activeQuest = selectedQuest;
        questName.text = activeQuest.GetName();
        questDescription.text = activeQuest.GetDescription();
        makeActiveBtn.gameObject.SetActive(false);

        /*
        foreach (GameObject fGO in uiFetchQuestObjects.ToList<GameObject>())
        {
            uiFetchQuestObjects.Remove(fGO);
            Destroy(fGO);
        }

        if (activeQuest.GetQuestType() == Quest.QuestType.Fetch)
        {
            FetchQuest fetchQuest = ((FetchQuest)activeQuest);
            for (int i = 0; i < fetchQuest.GetFetchItems().Count; i++)
            {
                GameObject fGO = Instantiate(uiFetchQuestObject, activeQuestPanel.transform);

                fGO.transform.GetChild(0).GetComponent<Slider>().maxValue = fetchQuest.GetItemCounts()[i];
                fGO.transform.GetChild(1).GetComponent<Text>().text = fetchQuest.GetFetchItems()[i].GetName();
                fGO.transform.GetChild(2).GetComponent<Text>().text = "Fetch";

                if (player.GetInventory().GetItemCount(((FetchQuest)activeQuest).GetFetchItems()[i].GetID()) > 0)
                {
                    fGO.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>().text = player.GetInventory().GetItemCount(((FetchQuest)activeQuest).GetFetchItems()[i].GetID()).ToString();
                    fGO.transform.GetChild(0).GetComponent<Slider>().value = player.GetInventory().GetItemCount(((FetchQuest)activeQuest).GetFetchItems()[i].GetID());
                }
                else
                {
                    fGO.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>().text = "0";
                    fGO.transform.GetChild(0).GetComponent<Slider>().value = 0;
                }

                uiFetchQuestObjects.Add(fGO);
            }
        }
        else if (activeQuest.GetQuestType() == Quest.QuestType.Slay)
        {
            SlayQuest slayQuest = ((SlayQuest)activeQuest);
            for (int i = 0; i < slayQuest.GetSlayEnemies().Count; i++)
            {
                GameObject fGO = Instantiate(uiFetchQuestObject, activeQuestPanel.transform);

                fGO.transform.GetChild(0).GetComponent<Slider>().maxValue = slayQuest.GetNumSlayEnemies()[i];
                fGO.transform.GetChild(1).GetComponent<Text>().text = slayQuest.GetSlayEnemies()[i].GetName();
                fGO.transform.GetChild(2).GetComponent<Text>().text = "Slay";

                fGO.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>().text = slayQuest.GetCountSlayEnemies()[i].ToString();
                fGO.transform.GetChild(0).GetComponent<Slider>().value = slayQuest.GetCountSlayEnemies()[i];

                uiFetchQuestObjects.Add(fGO);
            }
        }
        */

        noActiveQuestTxt.SetActive(false);
    }

    public void SetPlayerPosition(Vector3 position, Vector3 rotation)
    {
        GameObject.Find("Player").transform.position = position;
        GameObject.Find("Player").transform.eulerAngles = rotation;
    }


    // UI Screen Activation Methods

    public void ActivateInventoryScreen(bool x)
    {
        UIInventoryScreen.SetActive(x);
        UIStatsScreen.SetActive(false);

        DeactivateInvSelection(false);
    }

    public void ActivateQuestScreen(bool x)
    {
        UIQuestScreen.SetActive(x);
        makeActiveBtn.gameObject.SetActive(false);

        if (x && activeQuest != null)
        {
            questName.text = activeQuest.GetName();
            questDescription.text = activeQuest.GetDescription();
            activeQuestScroll.verticalNormalizedPosition = 1;
        }
        else if (!x && activeQuest == null)
        {
            questName.text = "";
            questDescription.text = "";
            noActiveQuestTxt.SetActive(true);
        }

        /*
        if (activeQuest != null && activeQuest.GetQuestType() == Quest.QuestType.Fetch)
        {
            FetchQuest fetchQuest = ((FetchQuest)activeQuest);
            for (int i = 0; i < uiFetchQuestObjects.Count; i++)
            {
                GameObject fGO = uiFetchQuestObjects[i];

                fGO.transform.GetChild(0).GetComponent<Slider>().maxValue = fetchQuest.GetItemCounts()[i];
                fGO.transform.GetChild(1).GetComponent<Text>().text = fetchQuest.GetFetchItems()[i].GetName();
                fGO.transform.GetChild(2).GetComponent<Text>().text = "Fetch";

                if (player.GetInventory().GetItemCount(((FetchQuest)activeQuest).GetFetchItems()[i].GetID()) > 0)
                {
                    fGO.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>().text = player.GetInventory().GetItemCount(((FetchQuest)activeQuest).GetFetchItems()[i].GetID()).ToString();
                    fGO.transform.GetChild(0).GetComponent<Slider>().value = player.GetInventory().GetItemCount(((FetchQuest)activeQuest).GetFetchItems()[i].GetID());
                }
                else
                {
                    fGO.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>().text = "0";
                    fGO.transform.GetChild(0).GetComponent<Slider>().value = 0;
                }
            }
        }
        else if (activeQuest != null && activeQuest.GetQuestType() == Quest.QuestType.Slay)
        {
            SlayQuest slayQuest = ((SlayQuest)activeQuest);
            for (int i = 0; i < uiFetchQuestObjects.Count; i++)
            {
                GameObject fGO = uiFetchQuestObjects[i];

                fGO.transform.GetChild(0).GetComponent<Slider>().maxValue = slayQuest.GetNumSlayEnemies()[i];
                fGO.transform.GetChild(1).GetComponent<Text>().text = slayQuest.GetSlayEnemies()[i].GetName();
                fGO.transform.GetChild(2).GetComponent<Text>().text = "Slay";

                fGO.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>().text = slayQuest.GetCountSlayEnemies()[i].ToString();
                fGO.transform.GetChild(0).GetComponent<Slider>().value = slayQuest.GetCountSlayEnemies()[i];           
            }
        }
        */
    }

    public void ActivatePickupScreen(bool x)
    {
        string sourceName = "";
        pickupItemBtn.interactable = false;
        pickupDropItemBtn.interactable = false;

        /*  if (isInChest)
          {
              sourceName = "Chest";
              activeChest.SetHasBeenSearched(true);
          } */

        if (x)
        {
            if (dialogueManager.isInNPC)
            {
                if (dialogueManager.currentNPC.GetNPCType() == NPC.NPCType.merchant)
                {
                    Store currentShop = StoreDictionary[dialogueManager.currentNPC.GetStore().GetID()];
                    sourceName = currentShop.GetName();
                    pickupDropItemBtn.gameObject.transform.GetChild(0).GetComponent<Text>().text = "Sell\n(75% Value)";
                    pickupItemBtn.gameObject.transform.GetChild(0).GetComponent<Text>().text = "Buy";

                    foreach (Item item in currentShop.GetItemList())
                    {
                        AddToPickup(item);
                    }
                }
            }
            else if (battleManager.isInBattle)
            {
                sourceName = battleManager.activeEnemy.GetName();
            }

            invScroll.transform.SetParent(UIPickupScreen.transform);
            invScroll.gameObject.SetActive(false);
            playerPickUpNameTxt.gameObject.SetActive(false);
            pickupDropItemBtn.gameObject.SetActive(false);

            otherPickUpNameTxt.text = sourceName;
            isInPickup = true;
            UIPickupScreen.SetActive(true);
        }
        else
        {
            pickupDropItemBtn.gameObject.transform.GetChild(0).GetComponent<Text>().text = "Drop";
            pickupItemBtn.gameObject.transform.GetChild(0).GetComponent<Text>().text = "Grab";

            invScroll.transform.SetParent(UIInventoryScreen.transform);
            invScroll.gameObject.SetActive(true);
            playerPickUpNameTxt.gameObject.SetActive(true);
            pickupDropItemBtn.gameObject.SetActive(true);

            otherPickUpNameTxt.gameObject.SetActive(true);
            pickupScroll.gameObject.SetActive(true);
            pickupItemBtn.gameObject.SetActive(true);

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

            /*
            if (isInChest)
                OpenChest(activeChest); */
        }
    }

    public void SwapPickup()
    {
        if (pickupScroll.gameObject.activeInHierarchy)
        {
            playerPickUpNameTxt.gameObject.SetActive(true);
            otherPickUpNameTxt.gameObject.SetActive(false);

            pickupScroll.gameObject.SetActive(false);
            invScroll.gameObject.SetActive(true);

            pickupDropItemBtn.gameObject.SetActive(true);
            pickupItemBtn.gameObject.SetActive(false);
        }
        else if (invScroll.gameObject.activeInHierarchy)
        {
            playerPickUpNameTxt.gameObject.SetActive(false);
            otherPickUpNameTxt.gameObject.SetActive(true);

            invScroll.gameObject.SetActive(false);
            pickupScroll.gameObject.SetActive(true);

            pickupDropItemBtn.gameObject.SetActive(false);
            pickupItemBtn.gameObject.SetActive(true);
        }
        DeactivateInvSelection();
    }

    public void ActivateArenaScreen(bool x)
    {
        UIArenaScreen.SetActive(x);

        foreach (KeyValuePair<uint, Enemy> kvp in EnemyDictionary)
        {
            Enemy e = kvp.Value;
            if (leveledEnemies.Contains(e))
            {
                if (player.GetLevel() > e.GetMaxLevel() || player.GetLevel() < e.GetMinLevel() || e.GetEnemyType() == Enemy.EnemyType.boss)
                {
                    if (e.GetEnemyType() == Enemy.EnemyType.normal || ((BossEnemy)EnemyDictionary[e.GetID()]).HasBeenDefeated())
                        leveledEnemies.Remove(e);
                }
            }
            else
            {
                if (player.GetLevel() <= e.GetMaxLevel() && player.GetLevel() >= e.GetMinLevel())
                {
                    if (e.GetEnemyType() == Enemy.EnemyType.normal || !((BossEnemy)EnemyDictionary[e.GetID()]).HasBeenDefeated())
                        leveledEnemies.Add(e);
                }
            }
        }

        enemyABtn.GetComponent<EnemyContainer>().SetEnemy(leveledEnemies[UnityEngine.Random.Range(0, leveledEnemies.Count)]);
        enemyBBtn.GetComponent<EnemyContainer>().SetEnemy(leveledEnemies[UnityEngine.Random.Range(0, leveledEnemies.Count)]);
        enemyCBtn.GetComponent<EnemyContainer>().SetEnemy(leveledEnemies[UnityEngine.Random.Range(0, leveledEnemies.Count)]);

        Enemy enemyA = enemyABtn.GetComponent<EnemyContainer>().GetEnemy();
        Enemy enemyB = enemyBBtn.GetComponent<EnemyContainer>().GetEnemy();
        Enemy enemyC = enemyCBtn.GetComponent<EnemyContainer>().GetEnemy();

        enemyANameTxt.text = enemyA.GetName();
        enemyBNameTxt.text = enemyB.GetName();
        enemyCNameTxt.text = enemyC.GetName();

        enemyARewardTxt.text = String.Format("Reward:\n{0}-{1} Gold", enemyA.GetMinGoldReward(), enemyA.GetMaxGoldReward());
        enemyBRewardTxt.text = String.Format("Reward:\n{0}-{1} Gold", enemyB.GetMinGoldReward(), enemyB.GetMaxGoldReward());
        enemyCRewardTxt.text = String.Format("Reward:\n{0}-{1} Gold", enemyC.GetMinGoldReward(), enemyC.GetMaxGoldReward());

        arenaEnemyHealthObj.SetActive(false);
        arenaEnemyStaminaObj.SetActive(false);
        arenaEnemyManaObj.SetActive(false);
        arenaEnemySpeedObj.SetActive(false);
        arenaEnemyDamageObj.SetActive(false);

        GameObject.Find("Player").transform.position = new Vector3(0, 2.2f, 10);
        GameObject.Find("Player").transform.transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    public void ActivateSkillScreen(bool x)
    {
        DeactivateSkillSelection();

        UIStatsScreen.SetActive(false);
        UISkillScreen.SetActive(x);
    }

    public void ActivateStatsScreen(bool x)
    {
        DeactivateInvSelection(false);

        UpdateInventoryAttributes();

        UIStatsScreen.SetActive(x);
    }

    public void ActivateEventScreen(bool x, StartEvent startEvent = null)
    {
        eventManager.ActivateEventScreen(x, startEvent);
    }

    public void ActivatePauseScreen(bool x)
    {
        UIPauseScreen.SetActive(x);
        UIPauseScreen.GetComponent<Image>().raycastTarget = x;
    }

    //**


    // Dungeon Methods

    //**



    IEnumerator StartGame()
    {
        UICoverScreen.GetComponent<Image>().raycastTarget = true;
        UICoverScreen.GetComponent<Image>().color = new Color(0, 0, 0, 1);
        messageTxt.color = new Color(255, 255, 255, 0);

        yield return StartCoroutine(FadeText("Wake up."));
        yield return StartCoroutine(FadeText("What is your name, gladiator?"));

        while (acceptNameBtn.GetComponent<Image>().color.a < 1)
        {
            acceptNameBtn.GetComponent<Image>().color = new Color(acceptNameBtn.GetComponent<Image>().color.r, acceptNameBtn.GetComponent<Image>().color.g,
                acceptNameBtn.GetComponent<Image>().color.b, acceptNameBtn.GetComponent<Image>().color.a + 0.01f);
            nameInputField.GetComponent<Image>().color = new Color(nameInputField.GetComponent<Image>().color.r, nameInputField.GetComponent<Image>().color.g,
                nameInputField.GetComponent<Image>().color.b, nameInputField.GetComponent<Image>().color.a + 0.01f);
            yield return new WaitForSecondsRealtime(0.05f * Time.deltaTime);
        }
        yield return new WaitForUIButtons(acceptNameBtn);
        acceptNameBtn.interactable = false;

        string name = nameInputField.text != "" ? nameInputField.text : "Player";
        player.SetName(name);

        statsPlayerNameTxt.text = name;
        statsPlayerTitleTxt.text = player.GetTitle();
        playerPickUpNameTxt.text = name;

        while (acceptNameBtn.GetComponent<Image>().color.a > 0)
        {
            acceptNameBtn.GetComponent<Image>().color = new Color(acceptNameBtn.GetComponent<Image>().color.r, acceptNameBtn.GetComponent<Image>().color.g,
                acceptNameBtn.GetComponent<Image>().color.b, acceptNameBtn.GetComponent<Image>().color.a - 0.01f);
            nameInputField.GetComponent<Image>().color = new Color(nameInputField.GetComponent<Image>().color.r, nameInputField.GetComponent<Image>().color.g,
                nameInputField.GetComponent<Image>().color.b, nameInputField.GetComponent<Image>().color.a - 0.01f);
            yield return new WaitForSecondsRealtime(0.05f * Time.deltaTime);
        }
        acceptNameBtn.gameObject.SetActive(false);
        nameInputField.gameObject.SetActive(false);
        yield return new WaitForSecondsRealtime(1f);

        yield return StartCoroutine(FadeText("I've given you a crooked dagger and a tattered shirt."));
        yield return StartCoroutine(FadeText("It should be all you need for now."));
        
        GameObject.Find("Player").transform.position = new Vector3(0, 2.2f, 10f);
        GameObject.Find("Player").transform.eulerAngles = new Vector3(0, 90f, 0);

        yield return StartCoroutine(FadeText("Prove your worth to me, " + name + "."));

        UICoverScreen.GetComponent<Image>().raycastTarget = false;

        battleManager.Battle(smallRat, canLeave: false);

        while (UICoverScreen.GetComponent<Image>().color.a > 0)
        {
            UICoverScreen.GetComponent<Image>().color = new Color(UICoverScreen.GetComponent<Image>().color.r, UICoverScreen.GetComponent<Image>().color.g, UICoverScreen.GetComponent<Image>().color.b, UICoverScreen.GetComponent<Image>().color.a - (0.01f));
            yield return new WaitForSecondsRealtime(0.01f * Time.deltaTime);
        }

        yield return battleManager.battleCoroutine;

        yield return new WaitForUIButtons(GameObject.Find("PickupExitBtn").GetComponent<Button>());
        GameObject.Find("GameManager").GetComponent<DialogueManager>().ActivateDialogueScreen(true);

        // PLAYER DEBUGGING AREA

        //

        StartCoroutine(OverworldMusic());
        StartCoroutine(DirectionalOutput());
        GameObject.Find("BirdsLoopAudioSource").GetComponent<AudioSource>().Play();
        GameObject.Find("WindAmbianceLoopAudioSource").GetComponent<AudioSource>().Play();
        
    }

    public void StartLevelThreeEvent()
    {
        StartCoroutine(LevelThreeEvent());
    }

    IEnumerator LevelThreeEvent()
    {
        GameObject.Find("BirdsLoopAudioSource").GetComponent<AudioSource>().Stop();
        GameObject.Find("WindAmbianceLoopAudioSource").GetComponent<AudioSource>().Stop();
        if (GameObject.Find("OverworldMusicAudioSource").GetComponent<AudioSource>().isPlaying)
            GameObject.Find("OverworldMusicAudioSource").GetComponent<AudioSource>().Stop();

        UICoverScreen.GetComponent<Image>().raycastTarget = true;
        messageTxt.color = new Color(255, 255, 255, 0);

        while (UICoverScreen.GetComponent<Image>().color.a < 1)
        {
            UICoverScreen.GetComponent<Image>().color = new Color(UICoverScreen.GetComponent<Image>().color.r, UICoverScreen.GetComponent<Image>().color.g, UICoverScreen.GetComponent<Image>().color.b, UICoverScreen.GetComponent<Image>().color.a + (0.01f));
            yield return new WaitForSecondsRealtime(0.01f * Time.deltaTime);
        }

        yield return StartCoroutine(FadeText("You've done great work so far."));
        yield return StartCoroutine(FadeText("I wish to open the world to you now."));
        yield return StartCoroutine(FadeText("Go forth, explore the world."));

        while (UICoverScreen.GetComponent<Image>().color.a > 0)
        {
            UICoverScreen.GetComponent<Image>().color = new Color(UICoverScreen.GetComponent<Image>().color.r, UICoverScreen.GetComponent<Image>().color.g, UICoverScreen.GetComponent<Image>().color.b, UICoverScreen.GetComponent<Image>().color.a - (0.01f));
            yield return new WaitForSecondsRealtime(0.01f * Time.deltaTime);
        }

        GameObject.Find("BurunsOutsideBlock").GetComponent<BoxCollider>().isTrigger = true;
        GameObject.Find("GateGuardCollider").SetActive(false);

        GameObject.Find("OverworldMusicAudioSource").GetComponent<AudioSource>().Play();
        GameObject.Find("BirdsLoopAudioSource").GetComponent<AudioSource>().Play();
        GameObject.Find("WindAmbianceLoopAudioSource").GetComponent<AudioSource>().Play();

        UICoverScreen.GetComponent<Image>().raycastTarget = false;
    }

    public void EnterInn(int cost, int attributeRegen)
    {
        StartCoroutine(r_EnterInn(cost, attributeRegen));
    }

    IEnumerator r_EnterInn(int cost, int attributeRegen)
    {
        messageTxt.text = "You rest, healing and rejuvenating you.";
        
        UICoverScreen.GetComponent<Image>().raycastTarget = true;
        messageTxt.raycastTarget = true;
        while (UICoverScreen.GetComponent<Image>().color.a < 1)
        {
            UICoverScreen.GetComponent<Image>().color = new Color(UICoverScreen.GetComponent<Image>().color.r, UICoverScreen.GetComponent<Image>().color.g, UICoverScreen.GetComponent<Image>().color.b, UICoverScreen.GetComponent<Image>().color.a + (0.01f));
            messageTxt.color = new Color(messageTxt.color.r, messageTxt.color.g, messageTxt.color.b, messageTxt.color.a + 0.01f);
            yield return new WaitForSecondsRealtime(0.005f * Time.deltaTime);
        }

        dialogueManager.ActivateDialogueScreen(false);
        player.ChangeGold(-cost);

        player.ChangeHealth(attributeRegen);
        player.ChangeStamina(attributeRegen);
        player.ChangeMana(attributeRegen);
        player.ClearStatusEffects();
        battleManager.ClearPlayerStatusEffectObjects();

        yield return new WaitForSecondsRealtime(0.5f);

        GameObject.Find("Player").transform.position = new Vector3(30f, 2.2f, 70f);

        while(UICoverScreen.GetComponent<Image>().color.a > 0)
        {
            UICoverScreen.GetComponent<Image>().color = new Color(UICoverScreen.GetComponent<Image>().color.r, UICoverScreen.GetComponent<Image>().color.g, UICoverScreen.GetComponent<Image>().color.b, UICoverScreen.GetComponent<Image>().color.a - (0.01f));
            messageTxt.color = new Color(messageTxt.color.r, messageTxt.color.g, messageTxt.color.b, messageTxt.color.a - 0.01f);
            yield return new WaitForSecondsRealtime(0.005f * Time.deltaTime);
        } 
        UICoverScreen.GetComponent<Image>().raycastTarget = false;
        messageTxt.raycastTarget = false;
    }

    public void EnterBattle()
    {
        if (UIArenaScreen.activeInHierarchy)
            ActivateArenaScreen(false);
        StartCoroutine(r_EnterBattle(selectedEnemy));
    }

    public void EnterBattle(Enemy enemy)
    {
        StartCoroutine(r_EnterBattle(enemy));
    }

    IEnumerator r_EnterBattle(Enemy enemy)
    {
        messageTxt.text = "Prepare for Battle";

        UICoverScreen.GetComponent<Image>().raycastTarget = true;
        messageTxt.raycastTarget = true;
        while (UICoverScreen.GetComponent<Image>().color.a < 1)
        {
            UICoverScreen.GetComponent<Image>().color = new Color(UICoverScreen.GetComponent<Image>().color.r, UICoverScreen.GetComponent<Image>().color.g, UICoverScreen.GetComponent<Image>().color.b, UICoverScreen.GetComponent<Image>().color.a + (0.01f));
            messageTxt.color = new Color(messageTxt.color.r, messageTxt.color.g, messageTxt.color.b, messageTxt.color.a + 0.01f);
            yield return new WaitForSecondsRealtime(0.01f * Time.deltaTime);
        }

        dialogueManager.ActivateDialogueScreen(false);
        battleManager.Battle(enemy);

        while (UICoverScreen.GetComponent<Image>().color.a > 0)
        {
            UICoverScreen.GetComponent<Image>().color = new Color(UICoverScreen.GetComponent<Image>().color.r, UICoverScreen.GetComponent<Image>().color.g, UICoverScreen.GetComponent<Image>().color.b, UICoverScreen.GetComponent<Image>().color.a - (0.01f));
            messageTxt.color = new Color(messageTxt.color.r, messageTxt.color.g, messageTxt.color.b, messageTxt.color.a - 0.01f);
            yield return new WaitForSecondsRealtime(0.01f * Time.deltaTime);
        }
        UICoverScreen.GetComponent<Image>().raycastTarget = false;
        messageTxt.raycastTarget = false;
    }

    public void StartHasDied()
    {
        StartCoroutine(r_HasDied());
    }

    IEnumerator r_HasDied()
    {
        GameObject.Find("BattleLoseMusicAudioSource").GetComponent<AudioSource>().Play();
        GameObject.Find("HeartbeatFastLoopAudioSource").GetComponent<AudioSource>().Stop();
        messageTxt.text = "You have Died";

        UICoverScreen.GetComponent<Image>().raycastTarget = true;
        deadRestartBtn.image.raycastTarget = true;
        deadRestartBtn.interactable = true;
        while (UICoverScreen.GetComponent<Image>().color.a < 1)
        {
            UICoverScreen.GetComponent<Image>().color = new Color(UICoverScreen.GetComponent<Image>().color.r, UICoverScreen.GetComponent<Image>().color.g, UICoverScreen.GetComponent<Image>().color.b, UICoverScreen.GetComponent<Image>().color.a + (0.01f));
            messageTxt.color = new Color(messageTxt.color.r, messageTxt.color.g, messageTxt.color.b, messageTxt.color.a + 0.01f);
            deadRestartBtn.image.color = new Color(deadRestartBtn.image.color.r, deadRestartBtn.image.color.g, deadRestartBtn.image.color.b, deadRestartBtn.image.color.a + 0.01f);
            deadRestartBtn.gameObject.GetComponentInChildren<Text>().color = new Color(deadRestartBtn.gameObject.GetComponentInChildren<Text>().color.r,
                deadRestartBtn.gameObject.GetComponentInChildren<Text>().color.g, deadRestartBtn.gameObject.GetComponentInChildren<Text>().color.b,
                deadRestartBtn.gameObject.GetComponentInChildren<Text>().color.a + 0.01f);
            yield return new WaitForSecondsRealtime(0.01f * Time.deltaTime);
        }
    }

    IEnumerator FadeText(string text)
    {
        messageTxt.text = text;

        while (messageTxt.color.a < 1)
        {
            messageTxt.color = new Color(messageTxt.color.r, messageTxt.color.g, messageTxt.color.b, messageTxt.color.a + 0.01f);
            yield return new WaitForSecondsRealtime(0.05f * Time.deltaTime);
        }
        yield return new WaitForSecondsRealtime(1f);
        while (messageTxt.color.a > 0)
        {
            messageTxt.color = new Color(messageTxt.color.r, messageTxt.color.g, messageTxt.color.b, messageTxt.color.a - 0.01f);
            yield return new WaitForSecondsRealtime(0.05f * Time.deltaTime);
        }
        yield return new WaitForSecondsRealtime(1f);
    }

    public void DeactivateActiveQuest()
    {
        activeQuest = null;
        questName.text = "";
        questDescription.text = "";

        foreach (GameObject fGO in uiFetchQuestObjects.ToList<GameObject>())
        {
            uiFetchQuestObjects.Remove(fGO);
            Destroy(fGO);
        }

        noActiveQuestTxt.SetActive(true);
        turnInQuestBtn.interactable = false;
    }

    void DeactivateSkillSelection()
    {
        selectedSkill = null;

        skillImg.color = new Color(255, 255, 255, 0);
        skillNameTxt.text = "";
        skillDescriptionTxt.text = "";
        skillTypeTxt.text = "";
        skillCostTxt.text = "";
        skillDamageTxt.text = "";
        skillShortDescriptionTxt.text = "";

        useSkillBtn.gameObject.SetActive(false);
    }

    public void OutputToText(string output)
    {
        outputQueue.Enqueue(output);
    }


    public void ToMain()
    {
        StartCoroutine(LoadToMain());
    }


    IEnumerator LoadToMain()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        AsyncOperation newScene = SceneManager.LoadSceneAsync(0, LoadSceneMode.Additive);
        newScene.allowSceneActivation = false;

        while (newScene.progress < 0.9f)
        {
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

    public void ChangeScene(Location location)
    {
        player.SetLocation(location);
        if (location.GetLocationType() == Location.LocationType.Dungeon)
            dungeonManager.SetCurrentDungeon((Dungeon)location);
        else
            dungeonManager.SetCurrentDungeon(null);
        StartCoroutine(LoadScene(location));
    }

    IEnumerator LoadScene(Location location, bool notStartingGame = true)
    {
        Debug.Log(location.GetSceneName() + "; " + location.GetName());
        Debug.Log(player.GetLocation().GetSceneName() + "; " + player.GetLocation().GetName());

        Scene currentScene = SceneManager.GetActiveScene();
        
        AsyncOperation newScene = SceneManager.LoadSceneAsync(location.GetSceneName(), LoadSceneMode.Additive);
        newScene.allowSceneActivation = false;
        UILoadScreen.SetActive(true);

        loadingSlider.value = newScene.progress;
        loadingPercentTxt.text = ((int)(newScene.progress * 100)) + "%";

        while (newScene.progress < 0.9f)
        {
            loadingSlider.value = Mathf.MoveTowards(newScene.progress * 100, 90.0f, 0.25f * Time.deltaTime);

            loadingPercentTxt.text = ((int)Mathf.MoveTowards(newScene.progress * 100, 90.0f, 0.25f * Time.deltaTime)) + "%";
            yield return new WaitForEndOfFrame();
        }

        loadingSlider.value = Mathf.MoveTowards(newScene.progress * 100, 90.0f, 0.25f * Time.deltaTime);
        loadingPercentTxt.text = ((int)Mathf.MoveTowards(newScene.progress * 100, 90.0f, 0.25f * Time.deltaTime)) + "%";
        newScene.allowSceneActivation = true;

        while (!newScene.isDone)
        {
            
            loadingSlider.value = Mathf.MoveTowards(newScene.progress * 100, 90.0f, 0.25f * Time.deltaTime);
            loadingPercentTxt.text = ((int)Mathf.MoveTowards(newScene.progress * 100, 90.0f, 0.25f * Time.deltaTime)) + "%";
            yield return new WaitForFixedUpdate();
        }

        Scene thisScene = SceneManager.GetSceneByName(location.GetSceneName());

        if (thisScene.IsValid())
        {

            SceneManager.MoveGameObjectToScene(playerObject, thisScene);
            SceneManager.MoveGameObjectToScene(UIRoot, thisScene);
            SceneManager.MoveGameObjectToScene(gameManager, thisScene);
            SceneManager.SetActiveScene(thisScene);

            if (player.GetLevel() >= 3 && thisScene.buildIndex == 2)
            {
                GameObject.Find("BurunsOutsideBlock").GetComponent<BoxCollider>().isTrigger = true;
                GameObject.Find("GateGuardCollider").SetActive(false);
            }
        }

        AsyncOperation closeScene = SceneManager.UnloadSceneAsync(currentScene);

        while (!closeScene.isDone)
        {
            yield return null;
        }
        
        CheckLocationQuestCompletion();

        playerObject.transform.position = location.GetSpawnLocation();
        playerObject.transform.eulerAngles = location.GetSpawnRotation();

        UILoadScreen.SetActive(false);

        if (notStartingGame)
        {
            OutputToText(String.Format("You have entered {0}.", location.GetName()));
            StartCoroutine(OverworldMusic());
            StartCoroutine(DirectionalOutput());
            UICoverScreen.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            UICoverScreen.GetComponent<Image>().raycastTarget = false;
            acceptNameBtn.gameObject.SetActive(false);
            nameInputField.gameObject.SetActive(false);
            GameObject.Find("BirdsLoopAudioSource").GetComponent<AudioSource>().Play();
            GameObject.Find("WindAmbianceLoopAudioSource").GetComponent<AudioSource>().Play();
        }
        else
            StartCoroutine(StartGame());
    }

    public void Restart()
    {
        StartCoroutine(RestartGame());
    }

    IEnumerator RestartGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        AsyncOperation newScene = SceneManager.LoadSceneAsync(0, LoadSceneMode.Additive);
        newScene.allowSceneActivation = false;
        UILoadScreen.SetActive(true);

        loadingSlider.value = newScene.progress;
        loadingPercentTxt.text = ((int)(newScene.progress * 100)) + "%";

        while (newScene.progress < 0.9f)
        {
            loadingSlider.value = Mathf.MoveTowards(newScene.progress * 100, 90.0f, 0.25f * Time.deltaTime);

            loadingPercentTxt.text = ((int)Mathf.MoveTowards(newScene.progress * 100, 90.0f, 0.25f * Time.deltaTime)) + "%";
            yield return new WaitForEndOfFrame();
        }

        loadingSlider.value = Mathf.MoveTowards(newScene.progress * 100, 90.0f, 0.25f * Time.deltaTime);
        loadingPercentTxt.text = ((int)Mathf.MoveTowards(newScene.progress * 100, 90.0f, 0.25f * Time.deltaTime)) + "%";
        newScene.allowSceneActivation = true;

        while (!newScene.isDone)
        {

            loadingSlider.value = Mathf.MoveTowards(newScene.progress * 100, 90.0f, 0.25f * Time.deltaTime);
            loadingPercentTxt.text = ((int)Mathf.MoveTowards(newScene.progress * 100, 90.0f, 0.25f * Time.deltaTime)) + "%";
            yield return new WaitForFixedUpdate();
        }

        Scene thisScene = SceneManager.GetSceneByName("MainMenu");

        if (thisScene.IsValid())
        {
            SceneManager.SetActiveScene(thisScene);
            GameObject.Find("MainMenuManager").GetComponent<MainMenu>().NewGame();
        }

        AsyncOperation closeScene = SceneManager.UnloadSceneAsync(currentScene);

        while (!closeScene.isDone)
        {
            yield return null;
        }

        StartCoroutine(OverworldMusic());
        StartCoroutine(DirectionalOutput());

    }

    IEnumerator DirectionalOutput()
    {
        while (true)
        {
            if (outputQueue.Count > 0 && !battleManager.isInBattle && !isInPickup && !dialogueManager.isInDialogue)
            {
                GameObject tGO = Instantiate(GameObject.Find("TextSpawn").transform.GetChild(0).gameObject, GameObject.Find("TextSpawn").transform);
                tGO.GetComponent<Text>().text = outputQueue.Dequeue();

                yield return new WaitForSecondsRealtime(2.0f);

                while (tGO.GetComponent<Text>().color.a > 0)
                {
                    tGO.transform.position = new Vector3(tGO.transform.position.x, tGO.transform.position.y + 10f * Time.fixedDeltaTime, 0);
                    tGO.GetComponent<Text>().color = new Color(tGO.GetComponent<Text>().color.r, tGO.GetComponent<Text>().color.g, tGO.GetComponent<Text>().color.b, tGO.GetComponent<Text>().color.a - 1f * Time.fixedDeltaTime);

                    yield return new WaitForSecondsRealtime(0.01f);
                }
                Destroy(tGO);
            }
            yield return null;
        }
    }

    IEnumerator OverworldMusic()
    {
        GameObject overworldMusic = GameObject.Find("OverworldMusicAudioSource");
        List<AudioClip> audioClips = overworldMusic.GetComponent<AudioClipContainer>().GetAudioClips();
        yield return new WaitForSecondsRealtime(10.0f);
        while(true)
        {
            if(!battleManager.isInBattle && !overworldMusic.GetComponent<AudioSource>().isPlaying)
            {
                overworldMusic.GetComponent<AudioSource>().clip = audioClips[UnityEngine.Random.Range(0, audioClips.Count)];
                overworldMusic.GetComponent<AudioSource>().Play();
            }
            yield return new WaitForSecondsRealtime(60.0f);
        }
    }

    public void SetMusicLevel(float sliderValue)
    {
        mixer.SetFloat("MusicAudio", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MusicAudio", sliderValue);
    }

    public void SetSFXLevel(float sliderValue)
    {
        mixer.SetFloat("SFXAudio", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("SFXAudio", sliderValue);
    }

    public void ActivateRatingsScreen()
    {
        UIRatingScreen.SetActive(true);
    }

    public void ToRatingScreen()
    {
        UIRatingScreen.SetActive(false);
        Application.OpenURL("http://play.google.com/store/apps/details?id=com.XaericGameStudioLLC.Gladiatorial");
    }

    public void DisableRatingScreen()
    {
        UIRatingScreen.SetActive(false);
        PlayerPrefs.SetInt("ShowRatings", 1);
    }
}