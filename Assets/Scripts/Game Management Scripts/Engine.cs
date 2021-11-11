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
    // UI Variables
    public GameObject UIRoot;
    GameObject UIInventoryScreen;
    GameObject UIDirectionScreen;
    GameObject UIQuestScreen;
    GameObject UIBattleScreen;
    GameObject UIPickupScreen;
    GameObject UIArenaScreen;
    GameObject UISkillScreen;
    GameObject UIStatsScreen;
    GameObject UIDialogueScreen;
    GameObject UILoadScreen;
    GameObject UIPauseScreen;
    GameObject UICoverScreen;

    // Directional UI Variables
    Button northBtn;
    Button eastBtn;
    Button southBtn;
    Button westBtn;

    Button enterDialogueBtn;

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
    SortedDictionary<uint, GameObject> uiQuestSlots = new SortedDictionary<uint, GameObject>();
    ScrollRect activeQuestScroll;

    Quest activeQuest;
    Quest selectedQuest;

    Text questName;
    Text questDescription;
    Text selQuestName;
    Text selQuestDescription;

    Button makeActiveBtn;
    Button turnInQuestBtn;

    GameObject noActiveQuestTxt;
    GameObject noSelectedQuestTxt;

    public ScrollRect questScroll;
    public GameObject questPanel;
    public GameObject activeQuestPanel;

    // UI Battle Variables
    public GameObject statusEffectSlot;
    GameObject playerStatusEffectGO;
    List<GameObject> playerStatusEffectSlots = new List<GameObject>();
    GameObject enemyStatusEffectGO;
    List<GameObject> enemyStatusEffectSlots = new List<GameObject>();
    Enemy activeEnemy;

    Text enemyNameTxt;
    Text enemyHealthTxt;
    Text enemyStaminaTxt;
    Text enemyManaTxt;
    Text playerBattleNameTxt;
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

    GameObject enemyA;
    GameObject enemyB;
    GameObject enemyC;

    Text enemyANameTxt;
    Text enemyBNameTxt;
    Text enemyCNameTxt;

    Text arenaEnemyNameTxt;
    Text arenaEnemyDescriptionTxt;
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

    GameObject arenaBossImg;

    // UI Skill Variables
    GameObject skillScrollView;
    GameObject magicScrollView;
    GameObject passiveScrollView;

    Skill selectedSkill;

    GameObject skillCostGO;
    GameObject skillDamageGO;

    Text skillNameTxt;
    Text skillDescriptionTxt;
    Text skillCostTxt;
    Text skillDamageTxt;
    Text unlockedTxt;
    Text skillSkillPointsTxt;

    Button unlockSkillBtn;

    // UI Stats Variales
    Text statsPlayerNameTxt;
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

    Slider statsHealthSlider;
    Slider statsStaminaSlider;
    Slider statsManaSlider;
    Slider statsExpSlider;

    Text maxHealthBuffTxt;
    Text maxStaminaBuffTxt;
    Text maxManaBuffTxt;
    Text maxWeaponDmgBuffTxt;
    Text maxSkillDmgBuffTxt;
    Text maxMagicDmgBuffTxt;

    // UI Dialogue Variables
    public GameObject dialogueBtn;
    List<GameObject> dialogueOptionSlots = new List<GameObject>();

    GameObject responsePanel;

    Text diagNPCNameTxt;
    TextMeshProUGUI npcLineTxt;

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

    Player player;

    public static Item NULL_ITEM;
    public static Weapon NULL_WEAPON;
    public static Armor NULL_ARMOR;

    Item key;

    Item ratFur;
    Item ratKingsCrown;
    Item ratLuckyPaw;
    Item ratSkull;
   
    Item blueSlimeGoo;
    Item greenSlimeGoo;
    Item redSlimeGoo;
    Item unknownBone;

    Item brokenSword;
    Item goldBar;
    Item lockpickSet;
    Item goldAmulet;
    Item armorPiece;

    Item wolfTooth;
    Item wolfPelt;
    Item wolfGem;

    Item grimore;
    Item magicRune;
    Item mortar;
    Item splinteredStaff;

    Item goblinEye;
    Item goblinGem;
    Item goblinStaff;

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

    Consumable ratMeat;
    Consumable wolfMeat;
    Consumable smallHealthPotion;
    Consumable smallStaminaPotion;
    Consumable smallManaPotion;
    Consumable healthPotion;
    Consumable staminaPotion;
    Consumable manaPotion;
    Consumable largeHealthPotion;
    Consumable largeStaminaPotion;
    Consumable largeManaPotion;
    Consumable giantHealthPotion;
    Consumable giantStaminaPotion;
    Consumable giantManaPotion;

    Location border;
    Location overworld;
    Location floor1;
    Location floor2;

    FetchQuest fetchRatSkull;
    FetchQuest fetchSlimeGoo;
    FetchQuest fetchRatFur;
    FetchQuest fetchLockpick;
    FetchQuest fetchRatMeat;
    FetchQuest fetchWolfFur;
    FetchQuest fetchWolfMeat;
    FetchQuest fetchArmorPiece;
    FetchQuest fetchMedicalSupply;

    SlayQuest slayGiantRat;
    SlayQuest slayRatPack;
    SlayQuest slayAmalgamSlime;
    SlayQuest slayDireWolf;
    SlayQuest slayGladiator;
    SlayQuest slayChief;

    ActiveSkill slash;
    ActiveSkill stab;
    ActiveSkill bash;
    ActiveSkill cleave;
    ActiveSkill hamstring;
    ActiveSkill jab;
    ActiveSkill pierce;
    ActiveSkill punch;

    ActiveSkill heavySwing;
    ActiveSkill lightSpectralArrow;
    ActiveSkill shortMeditation;
    ActiveSkill slightAmpUp;
    ActiveSkill slightIntimidation;
    ActiveSkill stunningStrike;
    ActiveSkill weakBleedingEdge;
    ActiveSkill weakDefensiveStance;
    ActiveSkill weakVenomStrike;
    ActiveSkill ampUp;
    ActiveSkill defensiveStance;
    ActiveSkill intimidate;
    ActiveSkill jarringStrike;
    ActiveSkill meditation;
    ActiveSkill smite;
    ActiveSkill spectralArrow;
    ActiveSkill toxicStrike;
    ActiveSkill weepingEdge;

    ActiveSkill flames;
    ActiveSkill lightHeal;
    ActiveSkill frostBite;
    ActiveSkill sparks;
    ActiveSkill weakSpecShield;
    ActiveSkill lightDispel;
    ActiveSkill dispel;
    ActiveSkill fireBolt;
    ActiveSkill heal;
    ActiveSkill iceSpike;
    ActiveSkill lightning;
    ActiveSkill spectralShield;

    PassiveSkill weaponDamageA;
    PassiveSkill weaponDamageB;
    PassiveSkill weaponDamageC;
    PassiveSkill weaponDamageD;

    PassiveSkill skillDamageA;
    PassiveSkill skillDamageB;
    PassiveSkill skillDamageC;
    PassiveSkill skillDamageD;

    PassiveSkill manaDamageA;
    PassiveSkill manaDamageB;
    PassiveSkill manaDamageC;
    PassiveSkill manaDamageD;

    PassiveSkill maxHealthA;
    PassiveSkill maxHealthB;
    PassiveSkill maxHealthC;
    PassiveSkill maxHealthD;

    PassiveSkill maxStaminaA;
    PassiveSkill maxStaminaB;
    PassiveSkill maxStaminaC;
    PassiveSkill maxStaminaD;

    PassiveSkill maxManaA;
    PassiveSkill maxManaB;
    PassiveSkill maxManaC;
    PassiveSkill maxManaD;

    Enemy smallRat;
    Enemy giantRat;
    Enemy ratPack;
    Enemy rat;
    Enemy redSlime;
    Enemy greenSlime;
    Enemy blueSlime;

    BossEnemy ratKing;

    Enemy amalgamSlime;
    Enemy goblinShaman;
    Enemy capturedBandit;
    Enemy castleGuard;
    Enemy grayWolf;
    Enemy direWolf;

    BossEnemy banditChief;

    Enemy gladiator;
    Enemy warlock;
    Enemy witch;

    BossEnemy veteran;

    Enemy chiefBattlemage;
    Enemy chiefKnight;
    Enemy warWolf;

    BossEnemy arenaGrandmaster;

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

    Store heavyAnvil;
    Store litheWarrior;
    Store magicMortar;
    Store theBulwark;
    
    Sprite healthDrop;
    Sprite staminaDrop;
    Sprite manaDrop;
    Sprite physicalDmgSprite;
    Sprite manaDmgSprite;

    // Local Use Variables
    bool playerHasMoved, isInPickup = false, isInBattle = false, isInChest = false, isInNPC = false, isInDialogue = false;
    List<Enemy> leveledEnemies = new List<Enemy>();
    int playerDamageOutput, enemyDamageOutput;
    ChestInventory activeChest;
    DoorInteraction activeDoor;
    public GameObject playerObject, enemyObject;
    public GameObject gameManager;
    public GameObject card;
    NPC currentNPC;
    Coroutine textType;
    public AudioMixer mixer;

    public bool isLoadingGame;

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

        StartCoroutine(LoadScene(overworld, false));
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

            load.LoadNPCs();
            load.LoadStores();
            load.LoadBossEnemies();

            LoadEquipmentSlots();
            LoadQuestSlots();
            SetCurrentWeight();

            playerBattleNameTxt.text = player.GetName();
            statsPlayerNameTxt.text = player.GetName();
            playerPickUpNameTxt.text = player.GetName();

            StartCoroutine(LoadScene(overworld, true));
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
        EnemyDictionary = new SortedDictionary<uint, Enemy>();
        NPCDictionary = new SortedDictionary<uint, NPC>();
        StoreDictionary = new SortedDictionary<uint, Store>();

        outputQueue = new Queue<string>();

        NULL_ITEM = Resources.Load<Item>("Items/NULL_ITEM");
        NULL_WEAPON = Resources.Load<Weapon>("Items/NULL_WEAPON");
        NULL_ARMOR = Resources.Load<Armor>("Items/NULL_ARMOR");

        key = Resources.Load<Item>("Items/Miscellaneous/Key");

        ratFur = Resources.Load<Item>("Items/Miscellaneous/RatItems/RatFur");
        ratKingsCrown = Resources.Load<Item>("Items/Miscellaneous/RatItems/RatKingsCrown");
        ratLuckyPaw = Resources.Load<Item>("Items/Miscellaneous/RatItems/RatLuckyPaw");
        ratSkull = Resources.Load<Item>("Items/Miscellaneous/RatItems/RatSkull");

        blueSlimeGoo = Resources.Load<Item>("Items/Miscellaneous/SlimeItems/BlueSlimeGoo");
        greenSlimeGoo = Resources.Load<Item>("Items/Miscellaneous/SlimeItems/GreenSlimeGoo");
        redSlimeGoo = Resources.Load<Item>("Items/Miscellaneous/SlimeItems/RedSlimeGoo");
        unknownBone = Resources.Load<Item>("Items/Miscellaneous/SlimeItems/UnknownBone");

        brokenSword = Resources.Load<Item>("Items/Miscellaneous/BanditItems/BrokenSword");
        goldBar = Resources.Load<Item>("Items/Miscellaneous/BanditItems/GoldBar");
        lockpickSet = Resources.Load<Item>("Items/Miscellaneous/BanditItems/LockpickSet");
        goldAmulet = Resources.Load<Item>("Items/Miscellaneous/BanditItems/GoldAmulet");
        armorPiece = Resources.Load<Item>("Items/Miscellaneous/BanditItems/ArmorPiece");

        wolfTooth = Resources.Load<Item>("Items/Miscellaneous/WolfItems/WolfTooth");
        wolfPelt = Resources.Load<Item>("Items/Miscellaneous/WolfItems/WolfPelt");
        wolfGem = Resources.Load<Item>("Items/Miscellaneous/WolfItems/WolfGem");

        goblinEye = Resources.Load<Item>("Items/Miscellaneous/GoblinItems/GoblinEye");
        goblinGem = Resources.Load<Item>("Items/Miscellaneous/GoblinItems/GoblinGem");
        goblinStaff = Resources.Load<Item>("Items/Miscellaneous/GoblinItems/GoblinStaff");

        grimore = Resources.Load<Item>("Items/Miscellaneous/MagicItems/Grimore");
        magicRune = Resources.Load<Item>("Items/Miscellaneous/MagicItems/MagicRune");
        mortar = Resources.Load<Item>("Items/Miscellaneous/MagicItems/Mortar");
        splinteredStaff = Resources.Load<Item>("Items/Miscellaneous/MagicItems/SplinteredStaff");

        crookedDagger = Resources.Load<Weapon>("Items/Weapons/Crooked Dagger");

        copperDagger = Resources.Load<Weapon>("Items/Weapons/Copper/Copper Dagger");
        copperSword = Resources.Load<Weapon>("Items/Weapons/Copper/Copper Sword");
        copperAxe = Resources.Load<Weapon>("Items/Weapons/Copper/Copper Axe");
        copperSpear = Resources.Load<Weapon>("Items/Weapons/Copper/Copper Spear");
        copperMace = Resources.Load<Weapon>("Items/Weapons/Copper/Copper Mace");

        ironDagger = Resources.Load<Weapon>("Items/Weapons/Iron/Iron Dagger");
        ironSword = Resources.Load<Weapon>("Items/Weapons/Iron/Iron Sword");
        ironAxe = Resources.Load<Weapon>("Items/Weapons/Iron/Iron Axe");
        ironSpear = Resources.Load<Weapon>("Items/Weapons/Iron/Iron Spear");
        ironMace = Resources.Load<Weapon>("Items/Weapons/Iron/Iron Mace");

        steelDagger = Resources.Load<Weapon>("Items/Weapons/Steel/Steel Dagger");
        steelSword = Resources.Load<Weapon>("Items/Weapons/Steel/Steel Sword");
        steelAxe = Resources.Load<Weapon>("Items/Weapons/Steel/Steel Axe");
        steelSpear = Resources.Load<Weapon>("Items/Weapons/Steel/Steel Spear");
        steelMace = Resources.Load<Weapon>("Items/Weapons/Steel/Steel Mace");

        electrumDagger = Resources.Load<Weapon>("Items/Weapons/Electrum/Electrum Dagger");
        electrumSword = Resources.Load<Weapon>("Items/Weapons/Electrum/Electrum Sword");
        electrumAxe = Resources.Load<Weapon>("Items/Weapons/Electrum/Electrum Axe");
        electrumSpear = Resources.Load<Weapon>("Items/Weapons/Electrum/Electrum Spear");
        electrumMace = Resources.Load<Weapon>("Items/Weapons/Electrum/Electrum Mace");

        royalDagger = Resources.Load<Weapon>("Items/Weapons/Royal/Royal Dagger");
        royalSword = Resources.Load<Weapon>("Items/Weapons/Royal/Royal Sword");
        royalAxe = Resources.Load<Weapon>("Items/Weapons/Royal/Royal Axe");
        royalSpear = Resources.Load<Weapon>("Items/Weapons/Royal/Royal Spear");
        royalMace = Resources.Load<Weapon>("Items/Weapons/Royal/Royal Mace");

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

        ratMeat = Resources.Load<Consumable>("Items/Consumables/Rat Meat");
        wolfMeat = Resources.Load<Consumable>("Items/Consumables/Wolf Meat");
        smallHealthPotion = Resources.Load<Consumable>("Items/Consumables/Small Health Potion");
        smallStaminaPotion = Resources.Load<Consumable>("Items/Consumables/Small Stamina Potion");
        smallManaPotion = Resources.Load<Consumable>("Items/Consumables/Small Mana Potion");
        healthPotion = Resources.Load<Consumable>("Items/Consumables/Health Potion");
        staminaPotion = Resources.Load<Consumable>("Items/Consumables/Stamina Potion");
        manaPotion = Resources.Load<Consumable>("Items/Consumables/Mana Potion");
        largeHealthPotion = Resources.Load<Consumable>("Items/Consumables/Large Health Potion");
        largeStaminaPotion = Resources.Load<Consumable>("Items/Consumables/Large Stamina Potion");
        largeManaPotion = Resources.Load<Consumable>("Items/Consumables/Large Mana Potion");
        giantHealthPotion = Resources.Load<Consumable>("Items/Consumables/Giant Health Potion");
        giantStaminaPotion = Resources.Load<Consumable>("Items/Consumables/Giant Stamina Potion");
        giantManaPotion = Resources.Load<Consumable>("Items/Consumables/Giant Mana Potion");

        ItemDictionary.Add(key.GetID(), key);

        ItemDictionary.Add(ratFur.GetID(), ratFur);
        ItemDictionary.Add(ratKingsCrown.GetID(), ratKingsCrown);
        ItemDictionary.Add(ratLuckyPaw.GetID(), ratLuckyPaw);
        ItemDictionary.Add(ratSkull.GetID(), ratSkull);

        ItemDictionary.Add(blueSlimeGoo.GetID(), blueSlimeGoo);
        ItemDictionary.Add(greenSlimeGoo.GetID(), greenSlimeGoo);
        ItemDictionary.Add(redSlimeGoo.GetID(), redSlimeGoo);
        ItemDictionary.Add(unknownBone.GetID(), unknownBone);

        ItemDictionary.Add(brokenSword.GetID(), brokenSword);
        ItemDictionary.Add(goldBar.GetID(), goldBar);
        ItemDictionary.Add(lockpickSet.GetID(), lockpickSet);
        ItemDictionary.Add(goldAmulet.GetID(), goldAmulet);
        ItemDictionary.Add(armorPiece.GetID(), armorPiece);

        ItemDictionary.Add(wolfTooth.GetID(), wolfTooth);
        ItemDictionary.Add(wolfPelt.GetID(), wolfPelt);
        ItemDictionary.Add(wolfGem.GetID(), wolfGem);

        ItemDictionary.Add(goblinEye.GetID(), goblinEye);
        ItemDictionary.Add(goblinGem.GetID(), goblinGem);
        ItemDictionary.Add(goblinStaff.GetID(), goblinStaff);

        ItemDictionary.Add(grimore.GetID(), grimore);
        ItemDictionary.Add(magicRune.GetID(), magicRune);
        ItemDictionary.Add(mortar.GetID(), mortar);
        ItemDictionary.Add(splinteredStaff.GetID(), splinteredStaff);

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

        ItemDictionary.Add(ratMeat.GetID(), ratMeat);
        ItemDictionary.Add(wolfMeat.GetID(), wolfMeat);
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

        border = Resources.Load<Location>("Locations/Border");
        overworld = Resources.Load<Location>("Locations/Overworld");
        floor1 = Resources.Load<Location>("Locations/Floor1");
        floor2 = Resources.Load<Location>("Locations/Floor2");

        LocationDictionary.Add(border.GetID(), border);
        LocationDictionary.Add(overworld.GetID(), overworld);
        LocationDictionary.Add(floor1.GetID(), floor1);
        LocationDictionary.Add(floor2.GetID(), floor2);

        rat = Instantiate(Resources.Load<Enemy>("Enemies/Rats/Rat"));
        giantRat = Instantiate(Resources.Load<Enemy>("Enemies/Rats/GiantRat"));
        smallRat = Instantiate(Resources.Load<Enemy>("Enemies/Rats/Small Rat"));
        ratPack = Instantiate(Resources.Load<Enemy>("Enemies/Rats/RatPack"));
        redSlime = Instantiate(Resources.Load<Enemy>("Enemies/Slimes/RedSlime"));
        greenSlime = Instantiate(Resources.Load<Enemy>("Enemies/Slimes/GreenSlime"));
        blueSlime = Instantiate(Resources.Load<Enemy>("Enemies/Slimes/BlueSlime"));

        ratKing = Instantiate(Resources.Load<BossEnemy>("Enemies/Rats/RatKing"));

        amalgamSlime = Instantiate(Resources.Load<Enemy>("Enemies/Slimes/AmalgamSlime"));
        goblinShaman = Instantiate(Resources.Load<Enemy>("Enemies/Humanoid/GoblinShaman"));
        capturedBandit = Instantiate(Resources.Load<Enemy>("Enemies/Humanoid/CapturedBandit"));
        castleGuard = Instantiate(Resources.Load<Enemy>("Enemies/Humanoid/CastleGuard"));
        grayWolf = Instantiate(Resources.Load<Enemy>("Enemies/Wolves/GrayWolf"));
        direWolf = Instantiate(Resources.Load<Enemy>("Enemies/Wolves/DireWolf"));

        banditChief = Instantiate(Resources.Load<BossEnemy>("Enemies/Humanoid/BanditChief"));

        gladiator = Instantiate(Resources.Load<Enemy>("Enemies/Humanoid/Gladiator"));
        warlock = Instantiate(Resources.Load<Enemy>("Enemies/Humanoid/Warlock"));
        witch = Instantiate(Resources.Load<Enemy>("Enemies/Humanoid/Witch"));

        veteran = Instantiate(Resources.Load<BossEnemy>("Enemies/Humanoid/Veteran"));

        chiefBattlemage = Instantiate(Resources.Load<Enemy>("Enemies/Humanoid/ChiefBattlemage"));
        chiefKnight = Instantiate(Resources.Load<Enemy>("Enemies/Humanoid/ChiefKnight"));
        warWolf = Instantiate(Resources.Load<Enemy>("Enemies/Wolves/WarWolf"));

        arenaGrandmaster = Instantiate(Resources.Load<BossEnemy>("Enemies/Humanoid/ArenaGrandmaster"));

        EnemyDictionary.Add(rat.GetID(), rat);
        EnemyDictionary.Add(smallRat.GetID(), smallRat);
        EnemyDictionary.Add(ratPack.GetID(), ratPack);
        EnemyDictionary.Add(giantRat.GetID(), giantRat);
        EnemyDictionary.Add(redSlime.GetID(), redSlime);
        EnemyDictionary.Add(greenSlime.GetID(), greenSlime);
        EnemyDictionary.Add(blueSlime.GetID(), blueSlime);

        EnemyDictionary.Add(ratKing.GetID(), ratKing);

        EnemyDictionary.Add(amalgamSlime.GetID(), amalgamSlime);
        EnemyDictionary.Add(goblinShaman.GetID(), goblinShaman);
        EnemyDictionary.Add(capturedBandit.GetID(), capturedBandit);
        EnemyDictionary.Add(castleGuard.GetID(), castleGuard);
        EnemyDictionary.Add(grayWolf.GetID(), grayWolf);
        EnemyDictionary.Add(direWolf.GetID(), direWolf);

        EnemyDictionary.Add(banditChief.GetID(), banditChief);

        EnemyDictionary.Add(gladiator.GetID(), gladiator);
        EnemyDictionary.Add(warlock.GetID(), warlock);
        EnemyDictionary.Add(witch.GetID(), witch);

        EnemyDictionary.Add(veteran.GetID(), veteran);

        EnemyDictionary.Add(chiefBattlemage.GetID(), chiefBattlemage);
        EnemyDictionary.Add(chiefKnight.GetID(), chiefKnight);
        EnemyDictionary.Add(warWolf.GetID(), warWolf);

        EnemyDictionary.Add(arenaGrandmaster.GetID(), arenaGrandmaster);

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

        heavyAnvil = Instantiate(Resources.Load<Store>("Stores/HeavyAnvil"));
        litheWarrior = Instantiate(Resources.Load<Store>("Stores/LitheWarrior"));
        magicMortar = Instantiate(Resources.Load<Store>("Stores/MagicMortar"));
        theBulwark = Instantiate(Resources.Load<Store>("Stores/TheBulwark"));

        StoreDictionary.Add(heavyAnvil.GetID(), heavyAnvil);
        StoreDictionary.Add(litheWarrior.GetID(), litheWarrior);
        StoreDictionary.Add(magicMortar.GetID(), magicMortar);
        StoreDictionary.Add(theBulwark.GetID(), theBulwark);

        healthDrop = Resources.Load<Sprite>("Textures/Inventory Icons/skill_008");
        staminaDrop = Resources.Load<Sprite>("Textures/Inventory Icons/skill_173");
        manaDrop = Resources.Load<Sprite>("Textures/Inventory Icons/skill_alt_008");
        physicalDmgSprite = Resources.Load<Sprite>("Textures/Inventory Icons/weapon_sword_02");
        manaDmgSprite = Resources.Load<Sprite>("Textures/Inventory Icons/skill_016");

        player = new Player("Name", overworld, new Inventory());
    }

    void InitializeSkills()
    {
        SkillDictionary = new SortedDictionary<uint, Skill>();

        slash = Instantiate(Resources.Load<ActiveSkill>("Player Moves/Weapon Skills/Slash"));
        stab = Instantiate(Resources.Load<ActiveSkill>("Player Moves/Weapon Skills/Stab"));
        bash = Instantiate(Resources.Load<ActiveSkill>("Player Moves/Weapon Skills/Bash"));
        cleave = Instantiate(Resources.Load<ActiveSkill>("Player Moves/Weapon Skills/Cleave"));
        hamstring = Instantiate(Resources.Load<ActiveSkill>("Player Moves/Weapon Skills/Hamstring"));
        jab = Instantiate(Resources.Load<ActiveSkill>("Player Moves/Weapon Skills/Jab"));
        pierce = Instantiate(Resources.Load<ActiveSkill>("Player Moves/Weapon Skills/Pierce"));
        punch = Instantiate(Resources.Load<ActiveSkill>("Player Moves/Weapon Skills/Punch"));

        heavySwing = Instantiate(Resources.Load<ActiveSkill>("Player Moves/Stamina Skills/Tier 1/Heavy Swing"));
        lightSpectralArrow = Instantiate(Resources.Load<ActiveSkill>("Player Moves/Stamina Skills/Tier 1/Light Spectral Arrow"));
        shortMeditation = Instantiate(Resources.Load<ActiveSkill>("Player Moves/Stamina Skills/Tier 1/Short Meditation"));
        slightAmpUp = Instantiate(Resources.Load<ActiveSkill>("Player Moves/Stamina Skills/Tier 1/Slight Amp Up"));
        slightIntimidation = Instantiate(Resources.Load<ActiveSkill>("Player Moves/Stamina Skills/Tier 1/Slight Intimidation"));
        stunningStrike = Instantiate(Resources.Load<ActiveSkill>("Player Moves/Stamina Skills/Tier 1/Stunning Strike"));
        weakBleedingEdge = Instantiate(Resources.Load<ActiveSkill>("Player Moves/Stamina Skills/Tier 1/Weak Bleeding Edge"));
        weakDefensiveStance = Instantiate(Resources.Load<ActiveSkill>("Player Moves/Stamina Skills/Tier 1/Weak Defensive Stance"));
        weakVenomStrike = Instantiate(Resources.Load<ActiveSkill>("Player Moves/Stamina Skills/Tier 1/Weak Venom Strike"));
        ampUp = Instantiate(Resources.Load<ActiveSkill>("Player Moves/Stamina Skills/Tier 2/Amp Up"));
        defensiveStance = Instantiate(Resources.Load<ActiveSkill>("Player Moves/Stamina Skills/Tier 2/Defensive Stance"));
        intimidate = Instantiate(Resources.Load<ActiveSkill>("Player Moves/Stamina Skills/Tier 2/Intimidate"));
        jarringStrike = Instantiate(Resources.Load<ActiveSkill>("Player Moves/Stamina Skills/Tier 2/Jarring Strike"));
        meditation = Instantiate(Resources.Load<ActiveSkill>("Player Moves/Stamina Skills/Tier 2/Meditation"));
        smite = Instantiate(Resources.Load<ActiveSkill>("Player Moves/Stamina Skills/Tier 2/Smite"));
        spectralArrow = Instantiate(Resources.Load<ActiveSkill>("Player Moves/Stamina Skills/Tier 2/Spectral Arrow"));
        toxicStrike = Instantiate(Resources.Load<ActiveSkill>("Player Moves/Stamina Skills/Tier 2/Toxic Strike"));
        weepingEdge = Instantiate(Resources.Load<ActiveSkill>("Player Moves/Stamina Skills/Tier 2/Weeping Edge"));

        flames = Instantiate(Resources.Load<ActiveSkill>("Player Moves/Mana Skills/Tier 1/Flames"));
        lightHeal = Instantiate(Resources.Load<ActiveSkill>("Player Moves/Mana Skills/Tier 1/Light Heal"));
        frostBite = Instantiate(Resources.Load<ActiveSkill>("Player Moves/Mana Skills/Tier 1/Frostbite"));
        sparks = Instantiate(Resources.Load<ActiveSkill>("Player Moves/Mana Skills/Tier 1/Sparks"));
        weakSpecShield = Instantiate(Resources.Load<ActiveSkill>("Player Moves/Mana Skills/Tier 1/Weak Spectral Shield"));
        lightDispel = Instantiate(Resources.Load<ActiveSkill>("Player Moves/Mana Skills/Tier 1/Light Dispel"));
        dispel = Instantiate(Resources.Load<ActiveSkill>("Player Moves/Mana Skills/Tier 2/Dispel"));
        fireBolt = Instantiate(Resources.Load<ActiveSkill>("Player Moves/Mana Skills/Tier 2/Firebolt"));
        heal = Instantiate(Resources.Load<ActiveSkill>("Player Moves/Mana Skills/Tier 2/Heal"));
        iceSpike = Instantiate(Resources.Load<ActiveSkill>("Player Moves/Mana Skills/Tier 2/Ice Spike"));
        lightning = Instantiate(Resources.Load<ActiveSkill>("Player Moves/Mana Skills/Tier 2/Lightning"));
        spectralShield = Instantiate(Resources.Load<ActiveSkill>("Player Moves/Mana Skills/Tier 2/Spectral Shield"));

        SkillDictionary.Add(slash.GetID(), slash);
        SkillDictionary.Add(stab.GetID(), stab);
        SkillDictionary.Add(bash.GetID(), bash);
        SkillDictionary.Add(cleave.GetID(), cleave);
        SkillDictionary.Add(hamstring.GetID(), hamstring);
        SkillDictionary.Add(jab.GetID(), jab);
        SkillDictionary.Add(pierce.GetID(), pierce);
        SkillDictionary.Add(punch.GetID(), punch);

        SkillDictionary.Add(heavySwing.GetID(), heavySwing);
        SkillDictionary.Add(lightSpectralArrow.GetID(), lightSpectralArrow);
        SkillDictionary.Add(shortMeditation.GetID(), shortMeditation);
        SkillDictionary.Add(slightAmpUp.GetID(), slightAmpUp);
        SkillDictionary.Add(slightIntimidation.GetID(), slightIntimidation);
        SkillDictionary.Add(stunningStrike.GetID(), stunningStrike);
        SkillDictionary.Add(weakBleedingEdge.GetID(), weakBleedingEdge);
        SkillDictionary.Add(weakDefensiveStance.GetID(), weakDefensiveStance);
        SkillDictionary.Add(weakVenomStrike.GetID(), weakVenomStrike);
        SkillDictionary.Add(ampUp.GetID(), ampUp);
        SkillDictionary.Add(defensiveStance.GetID(), defensiveStance);
        SkillDictionary.Add(intimidate.GetID(), intimidate);
        SkillDictionary.Add(jarringStrike.GetID(), jarringStrike);
        SkillDictionary.Add(meditation.GetID(), meditation);
        SkillDictionary.Add(smite.GetID(), smite);
        SkillDictionary.Add(spectralArrow.GetID(), spectralArrow);
        SkillDictionary.Add(toxicStrike.GetID(), toxicStrike);
        SkillDictionary.Add(weepingEdge.GetID(), weepingEdge);

        SkillDictionary.Add(flames.GetID(), flames);
        SkillDictionary.Add(lightHeal.GetID(), lightHeal);
        SkillDictionary.Add(frostBite.GetID(), frostBite);
        SkillDictionary.Add(sparks.GetID(), sparks);
        SkillDictionary.Add(weakSpecShield.GetID(), weakSpecShield);
        SkillDictionary.Add(lightDispel.GetID(), lightDispel);
        SkillDictionary.Add(dispel.GetID(), dispel);
        SkillDictionary.Add(fireBolt.GetID(), fireBolt);
        SkillDictionary.Add(heal.GetID(), heal);
        SkillDictionary.Add(iceSpike.GetID(), iceSpike);
        SkillDictionary.Add(lightning.GetID(), lightning);
        SkillDictionary.Add(spectralShield.GetID(), spectralShield);

        weaponDamageA = Instantiate(Resources.Load<PassiveSkill>("Player Moves/Passive Skills/WeaponDamage/WeaponDamageA"));
        weaponDamageB = Instantiate(Resources.Load<PassiveSkill>("Player Moves/Passive Skills/WeaponDamage/WeaponDamageB"));
        weaponDamageC = Instantiate(Resources.Load<PassiveSkill>("Player Moves/Passive Skills/WeaponDamage/WeaponDamageC"));
        weaponDamageD = Instantiate(Resources.Load<PassiveSkill>("Player Moves/Passive Skills/WeaponDamage/WeaponDamageD"));

        skillDamageA = Instantiate(Resources.Load<PassiveSkill>("Player Moves/Passive Skills/SkillDamage/SkillDamageA"));
        skillDamageB = Instantiate(Resources.Load<PassiveSkill>("Player Moves/Passive Skills/SkillDamage/SkillDamageB"));
        skillDamageC = Instantiate(Resources.Load<PassiveSkill>("Player Moves/Passive Skills/SkillDamage/SkillDamageC"));
        skillDamageD = Instantiate(Resources.Load<PassiveSkill>("Player Moves/Passive Skills/SkillDamage/SkillDamageD"));

        manaDamageA = Instantiate(Resources.Load<PassiveSkill>("Player Moves/Passive Skills/ManaDamage/ManaDamageA"));
        manaDamageB = Instantiate(Resources.Load<PassiveSkill>("Player Moves/Passive Skills/ManaDamage/ManaDamageB"));
        manaDamageC = Instantiate(Resources.Load<PassiveSkill>("Player Moves/Passive Skills/ManaDamage/ManaDamageC"));
        manaDamageD = Instantiate(Resources.Load<PassiveSkill>("Player Moves/Passive Skills/ManaDamage/ManaDamageD"));

        maxHealthA = Instantiate(Resources.Load<PassiveSkill>("Player Moves/Passive Skills/MaxHealth/MaxHealthA"));
        maxHealthB = Instantiate(Resources.Load<PassiveSkill>("Player Moves/Passive Skills/MaxHealth/MaxHealthB"));
        maxHealthC = Instantiate(Resources.Load<PassiveSkill>("Player Moves/Passive Skills/MaxHealth/MaxHealthC"));
        maxHealthD = Instantiate(Resources.Load<PassiveSkill>("Player Moves/Passive Skills/MaxHealth/MaxHealthD"));

        maxStaminaA = Instantiate(Resources.Load<PassiveSkill>("Player Moves/Passive Skills/MaxStamina/MaxStaminaA"));
        maxStaminaB = Instantiate(Resources.Load<PassiveSkill>("Player Moves/Passive Skills/MaxStamina/MaxStaminaB"));
        maxStaminaC = Instantiate(Resources.Load<PassiveSkill>("Player Moves/Passive Skills/MaxStamina/MaxStaminaC"));
        maxStaminaD = Instantiate(Resources.Load<PassiveSkill>("Player Moves/Passive Skills/MaxStamina/MaxStaminaD"));

        maxManaA = Instantiate(Resources.Load<PassiveSkill>("Player Moves/Passive Skills/MaxMana/MaxManaA"));
        maxManaB = Instantiate(Resources.Load<PassiveSkill>("Player Moves/Passive Skills/MaxMana/MaxManaB"));
        maxManaC = Instantiate(Resources.Load<PassiveSkill>("Player Moves/Passive Skills/MaxMana/MaxManaC"));
        maxManaD = Instantiate(Resources.Load<PassiveSkill>("Player Moves/Passive Skills/MaxMana/MaxManaD"));

        SkillDictionary.Add(weaponDamageA.GetID(), weaponDamageA);
        SkillDictionary.Add(weaponDamageB.GetID(), weaponDamageB);
        SkillDictionary.Add(weaponDamageC.GetID(), weaponDamageC);
        SkillDictionary.Add(weaponDamageD.GetID(), weaponDamageD);

        SkillDictionary.Add(skillDamageA.GetID(), skillDamageA);
        SkillDictionary.Add(skillDamageB.GetID(), skillDamageB);
        SkillDictionary.Add(skillDamageC.GetID(), skillDamageC);
        SkillDictionary.Add(skillDamageD.GetID(), skillDamageD);

        SkillDictionary.Add(manaDamageA.GetID(), manaDamageA);
        SkillDictionary.Add(manaDamageB.GetID(), manaDamageB);
        SkillDictionary.Add(manaDamageC.GetID(), manaDamageC);
        SkillDictionary.Add(manaDamageD.GetID(), manaDamageD);

        SkillDictionary.Add(maxHealthA.GetID(), maxHealthA);
        SkillDictionary.Add(maxHealthB.GetID(), maxHealthB);
        SkillDictionary.Add(maxHealthC.GetID(), maxHealthC);
        SkillDictionary.Add(maxHealthD.GetID(), maxHealthD);

        SkillDictionary.Add(maxStaminaA.GetID(), maxStaminaA);
        SkillDictionary.Add(maxStaminaB.GetID(), maxStaminaB);
        SkillDictionary.Add(maxStaminaC.GetID(), maxStaminaC);
        SkillDictionary.Add(maxStaminaD.GetID(), maxStaminaD);

        SkillDictionary.Add(maxManaA.GetID(), maxManaA);
        SkillDictionary.Add(maxManaB.GetID(), maxManaB);
        SkillDictionary.Add(maxManaC.GetID(), maxManaC);
        SkillDictionary.Add(maxManaD.GetID(), maxManaD);
    }

    void InitializeQuests()
    {
        QuestDictionary = new SortedDictionary<uint, Quest>();

        fetchRatSkull = Instantiate(Resources.Load<FetchQuest>("Quests/FetchRatSkull"));
        fetchSlimeGoo = Instantiate(Resources.Load<FetchQuest>("Quests/FetchSlimeGoo"));
        fetchRatFur = Instantiate(Resources.Load<FetchQuest>("Quests/FetchRatFur"));
        fetchLockpick = Instantiate(Resources.Load<FetchQuest>("Quests/FetchLockpick"));
        fetchRatMeat = Instantiate(Resources.Load<FetchQuest>("Quests/FetchRatMeat"));
        fetchWolfFur = Instantiate(Resources.Load<FetchQuest>("Quests/FetchWolfFur"));
        fetchWolfMeat = Instantiate(Resources.Load<FetchQuest>("Quests/FetchWolfMeat"));
        fetchArmorPiece = Instantiate(Resources.Load<FetchQuest>("Quests/FetchArmorPiece"));
        fetchMedicalSupply = Instantiate(Resources.Load<FetchQuest>("Quests/FetchMedicalSupply"));

        slayGiantRat = Instantiate(Resources.Load<SlayQuest>("Quests/SlayGiantRat"));
        slayRatPack = Instantiate(Resources.Load<SlayQuest>("Quests/SlayRatPack"));
        slayAmalgamSlime = Instantiate(Resources.Load<SlayQuest>("Quests/SlayAmalgamSlime"));
        slayDireWolf = Instantiate(Resources.Load<SlayQuest>("Quests/SlayDireWolf"));
        slayGladiator = Instantiate(Resources.Load<SlayQuest>("Quests/SlayGladiator"));
        slayChief = Instantiate(Resources.Load<SlayQuest>("Quests/SlayChief"));

        QuestDictionary.Add(fetchRatSkull.GetID(), fetchRatSkull);
        QuestDictionary.Add(fetchSlimeGoo.GetID(), fetchSlimeGoo);
        QuestDictionary.Add(fetchRatFur.GetID(), fetchRatFur);
        QuestDictionary.Add(fetchLockpick.GetID(), fetchLockpick);
        QuestDictionary.Add(fetchRatMeat.GetID(), fetchRatMeat);
        QuestDictionary.Add(fetchWolfMeat.GetID(), fetchWolfMeat);
        QuestDictionary.Add(fetchWolfFur.GetID(), fetchWolfFur);
        QuestDictionary.Add(fetchArmorPiece.GetID(), fetchArmorPiece);
        QuestDictionary.Add(fetchMedicalSupply.GetID(), fetchMedicalSupply);

        QuestDictionary.Add(slayGiantRat.GetID(), slayGiantRat);
        QuestDictionary.Add(slayRatPack.GetID(), slayRatPack);
        QuestDictionary.Add(slayAmalgamSlime.GetID(), slayAmalgamSlime);
        QuestDictionary.Add(slayDireWolf.GetID(), slayDireWolf);
        QuestDictionary.Add(slayGladiator.GetID(), slayGladiator);
        QuestDictionary.Add(slayChief.GetID(), slayChief);

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
        UIStatsScreen = GameObject.Find("UI Stats");
        UIDialogueScreen = GameObject.Find("UI Dialogue");
        UILoadScreen = GameObject.Find("UI Load");
        UIPauseScreen = GameObject.Find("UI Pause");
        UICoverScreen = GameObject.Find("UI Cover");

        northBtn = GameObject.Find("NorthBtn").GetComponent<Button>();
        eastBtn = GameObject.Find("EastBtn").GetComponent<Button>();
        southBtn = GameObject.Find("SouthBtn").GetComponent<Button>();
        westBtn = GameObject.Find("WestBtn").GetComponent<Button>();
        enterDialogueBtn = GameObject.Find("EnterDialogueBtn").GetComponent<Button>();

        enterDialogueBtn.gameObject.SetActive(false);

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
        selQuestName = GameObject.Find("SelectQuestNameTxt").GetComponent<Text>();
        selQuestDescription = GameObject.Find("SelectQuestDescriptionTxt").GetComponent<Text>();
        noActiveQuestTxt = GameObject.Find("NoActiveQuestTxt");
        noSelectedQuestTxt = GameObject.Find("NoSelectedQuestTxt");

        questName.text = "";
        questDescription.text = "";
        selQuestName.text = "";
        selQuestDescription.text = "";

        playerStatusEffectGO = GameObject.Find("PlayerStatusEffects");
        enemyStatusEffectGO = GameObject.Find("EnemyStatusEffects");

        enemyNameTxt = GameObject.Find("EnemyNameTxt").GetComponent<Text>();
        enemyHealthTxt = GameObject.Find("EnemyHealthTxt").GetComponent<Text>();
        enemyStaminaTxt = GameObject.Find("EnemyStaminaTxt").GetComponent<Text>();
        enemyManaTxt = GameObject.Find("EnemyManaTxt").GetComponent<Text>();
        enemyHealthSlider = GameObject.Find("EnemyHealthSlider").GetComponent<Slider>();
        enemyStaminaSlider = GameObject.Find("EnemyStaminaSlider").GetComponent<Slider>();
        enemyManaSlider = GameObject.Find("EnemyManaSlider").GetComponent<Slider>();

        playerBattleNameTxt = GameObject.Find("PlayerBattleName").GetComponent<Text>();
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

        enemyA = GameObject.Find("EnemyA");
        enemyB = GameObject.Find("EnemyB");
        enemyC = GameObject.Find("EnemyC");

        enemyANameTxt = GameObject.Find("EnemyAName").GetComponent<Text>();
        enemyBNameTxt = GameObject.Find("EnemyBName").GetComponent<Text>();
        enemyCNameTxt = GameObject.Find("EnemyCName").GetComponent<Text>();

        arenaEnemyNameTxt = GameObject.Find("ArenaNameTxt").GetComponent<Text>();
        arenaEnemyDescriptionTxt = GameObject.Find("ArenaDescriptionTxt").GetComponent<Text>();
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

        arenaBossImg = GameObject.Find("ArenaBossImg");

        arenaEnemyHealthObj.SetActive(false);
        arenaEnemyStaminaObj.SetActive(false);
        arenaEnemyManaObj.SetActive(false);
        arenaEnemySpeedObj.SetActive(false);
        arenaEnemyDamageObj.SetActive(false);

        arenaBossImg.SetActive(false);

        skillScrollView = GameObject.Find("SkillScrollView");
        magicScrollView = GameObject.Find("MagicScrollView");
        passiveScrollView = GameObject.Find("PassiveScrollView");
        magicScrollView.SetActive(false);
        passiveScrollView.SetActive(false);

        skillCostGO = GameObject.Find("SkillCost");
        skillDamageGO = GameObject.Find("SkillDamage");

        skillNameTxt = GameObject.Find("SkillNameTxt").GetComponent<Text>();
        skillDescriptionTxt = GameObject.Find("SkillDescriptionTxt").GetComponent<Text>();
        skillCostTxt = GameObject.Find("SkillCostTxt").GetComponent<Text>();
        skillDamageTxt = GameObject.Find("SkillDamageTxt").GetComponent<Text>();
        skillSkillPointsTxt = GameObject.Find("SkillSkillPointsTxt").GetComponent<Text>();
        unlockedTxt = GameObject.Find("UnlockedTxt").GetComponent<Text>();

        skillCostGO.SetActive(false);
        skillDamageGO.SetActive(false);

        statsPlayerNameTxt = GameObject.Find("StatsNameTxt").GetComponent<Text>();
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

        maxHealthBuffTxt = GameObject.Find("MaxHealthBuffTxt").GetComponent<Text>();
        maxStaminaBuffTxt = GameObject.Find("MaxStaminaBuffTxt").GetComponent<Text>();
        maxManaBuffTxt = GameObject.Find("MaxManaBuffTxt").GetComponent<Text>();
        maxWeaponDmgBuffTxt = GameObject.Find("MaxWeaponDmgBuffTxt").GetComponent<Text>();
        maxSkillDmgBuffTxt = GameObject.Find("MaxSkillDmgBuffTxt").GetComponent<Text>();
        maxMagicDmgBuffTxt = GameObject.Find("MaxMagicDmgBuffTxt").GetComponent<Text>();

        statsHealthSlider = GameObject.Find("StatsHealthSlider").GetComponent<Slider>();
        statsStaminaSlider = GameObject.Find("StatsStaminaSlider").GetComponent<Slider>();
        statsManaSlider = GameObject.Find("StatsManaSlider").GetComponent<Slider>();
        statsExpSlider = GameObject.Find("StatsExpSlider").GetComponent<Slider>();

        unlockSkillBtn = GameObject.Find("UnlockSkillBtn").GetComponent<Button>();

        diagNPCNameTxt = GameObject.Find("DiagNPCNameTxt").GetComponent<Text>();
        npcLineTxt = GameObject.Find("NPCLineTxt").GetComponent<TextMeshProUGUI>();
        responsePanel = GameObject.Find("ResponsePanel");

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
        UIBattleScreen.SetActive(false);
        UIPickupScreen.SetActive(false);
        UIArenaScreen.SetActive(false);
        UISkillScreen.SetActive(false);
        UIStatsScreen.SetActive(false);
        UIDialogueScreen.SetActive(false);
        UIPauseScreen.SetActive(false);
        UILoadScreen.SetActive(false);

        UpdateInventoryAttributes();
    }

    public void Battle()
    {
        StartCoroutine(Battle(selectedEnemy));
        ActivateArenaScreen(false);
    }

    IEnumerator Battle(Enemy eGO)
    {
        Advertisements.Instance.HideBanner();
        if (GameObject.Find("OverworldMusicAudioSource").GetComponent<AudioSource>().isPlaying)
            GameObject.Find("OverworldMusicAudioSource").GetComponent<AudioSource>().Stop();
        GameObject.Find("BattleMusicAudioSource").GetComponent<AudioSource>().Play();
        isInBattle = true;

        playerDamageOutput = 0;
        enemyDamageOutput = 0;

        Enemy enemy = Instantiate(eGO);
        activeEnemy = enemy;
        enemyNameTxt.text = enemy.GetName();

        UIBattleScreen.SetActive(true);

        UpdateBattleAttributes(enemy);

        float playerSpeed = player.GetSpeed(), enemySpeed = enemy.GetSpeed();
        playerSpeedSlider.value = playerSpeed;
        enemySpeedSlider.value = enemySpeed;

        while (player.GetHealth() > 0 && enemy.GetHealth() > 0)
        {

            while (playerSpeed < 100 && enemySpeed < 100)
            {
                int extraPlayerSpeedFlat = 0, extraEnemySpeedFlat = 0;
                float extraPlayerSpeedPer = 1.0f, extraEnemySpeedPer = 1.0f;
                foreach (StatusEffect statusEffect in player.GetStatusEffects())
                    if (statusEffect.GetStatusEffectType() == StatusEffect.StatusEffectType.speed)
                        if (statusEffect.IsPercentage())
                            extraPlayerSpeedPer += statusEffect.GetStatChange();
                        else
                            extraPlayerSpeedFlat += (int)statusEffect.GetStatChange();
                foreach (StatusEffect statusEffect in enemy.GetStatusEffects())
                    if (statusEffect.GetStatusEffectType() == StatusEffect.StatusEffectType.speed)
                        if (statusEffect.GetStatusEffectType() == StatusEffect.StatusEffectType.speed)
                            if (statusEffect.IsPercentage())
                                extraEnemySpeedPer += statusEffect.GetStatChange();
                            else
                                extraEnemySpeedFlat += (int)statusEffect.GetStatChange();

                playerSpeed += (int)extraPlayerSpeedPer * (player.GetSpeed() + extraPlayerSpeedFlat);
                enemySpeed += (int)extraEnemySpeedPer * (enemy.GetSpeed() + extraEnemySpeedFlat);

                playerSpeedSlider.value = playerSpeed;
                enemySpeedSlider.value = enemySpeed;

                //player.RegenAttributes();
                //enemy.RegenAttributes();

                UpdateBattleAttributes(enemy);
                yield return new WaitForSecondsRealtime(0.01f);
            }

            if (playerSpeed >= 100 && playerSpeed >= enemySpeed && player.GetHealth() > 0)
            {
                if(!CheckIfStunned(true))
                    yield return StartCoroutine("PlayerMove");
                playerSpeed -= 100;

                enemy.ChangeHealth(-playerDamageOutput);
                UpdateBattleAttributes(enemy);

                playerSpeedSlider.value = playerSpeed;

                player.DecrementStatusEffectTurn();

                EndOfTurnStatusEffect(true);

                UpdateBattleAttributes(enemy);

                if (enemySpeed >= 100 && enemy.GetHealth() > 0)
                {
                    if (!CheckIfStunned(false))
                        EnemyAttack(enemy);
                    enemySpeed -= 100;

                    player.ChangeHealth(-enemyDamageOutput);
                    UpdateBattleAttributes(enemy);

                    enemySpeedSlider.value = enemySpeed;

                    enemy.DecrementStatusEffectTurn();

                    EndOfTurnStatusEffect(false);

                    UpdateBattleAttributes(enemy);
                }

                playerDamageOutput = 0;
                enemyDamageOutput = 0;

            }
            if (enemySpeed >= 100 && enemySpeed >= playerSpeed && enemy.GetHealth() > 0)
            {
                if (!CheckIfStunned(false))
                    EnemyAttack(enemy);
                enemySpeed -= 100;

                player.ChangeHealth(-enemyDamageOutput);
                UpdateBattleAttributes(enemy);

                enemySpeedSlider.value = enemySpeed;

                enemy.DecrementStatusEffectTurn();

                EndOfTurnStatusEffect(false);

                UpdateBattleAttributes(enemy);

                if (playerSpeed >= 100 && player.GetHealth() > 0)
                {
                    if (!CheckIfStunned(true))
                        yield return StartCoroutine("PlayerMove");
                    playerSpeed -= 100;

                    enemy.ChangeHealth(-playerDamageOutput);
                    UpdateBattleAttributes(enemy);

                    playerSpeedSlider.value = playerSpeed;

                    player.DecrementStatusEffectTurn();

                    EndOfTurnStatusEffect(true);

                    UpdateBattleAttributes(enemy);
                }

                playerDamageOutput = 0;
                enemyDamageOutput = 0;

            }

            player.RegenAttributes();
            enemy.RegenAttributes();

            UpdateBattleAttributes(enemy);
            
            yield return null;
        }

        if (enemy.GetHealth() <= 0)
        {
            GameObject.Find("BattleWinMusicAudioSource").GetComponent<AudioSource>().Play();
            ActivatePickupScreen(true);
            GenerateItemPickup(enemy.GetItemRewards());
            OutputToText(String.Format("You have killed {0}, gaining {1} exp and {2} gold.", enemy.GetName(), enemy.GetExpReward(), enemy.GetGoldReward()));
            player.AddExp(enemy.GetExpReward());
            player.ChangeGold(enemy.GetGoldReward());

            if(enemy.GetEnemyType() == Enemy.EnemyType.boss)
            {
                BossEnemy boss = (BossEnemy)enemy;
                ((BossEnemy)EnemyDictionary[boss.GetID()]).SetHasBeenDefeated(true);
                Debug.Log(((BossEnemy)EnemyDictionary[boss.GetID()]).HasBeenDefeated());
                for(int i = 0; i < boss.GetUnlockedItems().Count; i++)
                {
                    Item item = boss.GetUnlockedItems()[i];
                    for(int j = 0; j < boss.GetUnlockedItemsCount()[i]; j++)
                    {
                        if(item.IsWeapon())
                        {
                            StoreDictionary[NPCDictionary[hirgirdBlacksmith.GetID()].GetStore().GetID()].AddItem(item);
                        }
                        else if (item.IsArmor())
                        {
                            if (((Armor)item).GetArmorClass() == Armor.ArmorClass.light)
                                StoreDictionary[NPCDictionary[kiarnilLeathersmith.GetID()].GetStore().GetID()].AddItem(item);
                            else if (((Armor)item).GetArmorClass() == Armor.ArmorClass.heavy)
                                StoreDictionary[NPCDictionary[varianArmorsmith.GetID()].GetStore().GetID()].AddItem(item);
                        }
                        else if(item.IsConsumable())
                        {
                            StoreDictionary[NPCDictionary[inveraAlchemist.GetID()].GetStore().GetID()].AddItem(item);
                        }
                    }
                }
            }

            foreach (GameObject sGO in enemyStatusEffectSlots.ToList<GameObject>())
            {
                GameObject.Destroy(sGO);
                enemyStatusEffectSlots.Remove(sGO);
            }

            foreach(KeyValuePair<uint, Quest> kvp in QuestDictionary)
            {
                if (kvp.Value.GetQuestType() == Quest.QuestType.Slay)
                {
                    SlayQuest quest = (SlayQuest)kvp.Value;
                    quest.IncrementSlainEnemy(enemy);
                }
            }

            UIBattleScreen.SetActive(false);
        }
        else
        {
            StartCoroutine(HasDied());
        }

        GameObject.Find("BattleMusicAudioSource").GetComponent<AudioSource>().Stop();
        player.AddBattleCount(1);
        Debug.Log("Battle Count: " + player.GetBattleCount() + " % 6 = " + player.GetBattleCount() % 6);
        if(player.GetBattleCount() % 6 == 0)
        {
            Debug.Log("Should Be Playing Ad");
            Advertisements.Instance.ShowInterstitial(InterstitialClosed);
        }
        else
        {
            UpdateInventoryAttributes();
            battleOutputTxt.text = "";
            isInBattle = false;
            Advertisements.Instance.ShowBanner(BannerPosition.BOTTOM);
        }
    }

    private void InterstitialClosed()
    {
        if(isInBattle)
        {
            UpdateInventoryAttributes();
            battleOutputTxt.text = "";
            isInBattle = false;
            Advertisements.Instance.ShowBanner(BannerPosition.BOTTOM);
        }
    }

    bool CheckIfStunned(bool isPlayer)
    {
        if (isPlayer)
        {
            foreach (StatusEffect statusEffect in player.GetStatusEffects())
                if (statusEffect.GetStatusEffectType() == StatusEffect.StatusEffectType.stun)
                    return true;
            return false;
        }
        else
        {
            foreach (StatusEffect statusEffect in activeEnemy.GetStatusEffects())
                if (statusEffect.GetStatusEffectType() == StatusEffect.StatusEffectType.stun)
                    return true;
            return false;
        }
    }

    bool CheckIfPoisoned(bool isPlayer)
    {
        if (isPlayer)
        {
            foreach (StatusEffect statusEffect in player.GetStatusEffects())
                if (statusEffect.GetStatusEffectType() == StatusEffect.StatusEffectType.poison)
                    return true;
            return false;
        }
        else
        {
            foreach (StatusEffect statusEffect in activeEnemy.GetStatusEffects())
                if (statusEffect.GetStatusEffectType() == StatusEffect.StatusEffectType.poison)
                    return true;
            return false;
        }
    }

    void EndOfTurnStatusEffect(bool isPlayer)
    {
        if (isPlayer)
        {
            foreach (GameObject sGO in playerStatusEffectSlots.ToList<GameObject>())
            {
                if (sGO.GetComponent<StatusContainer>().GetStatusEffect().GetStatusEffectType() == StatusEffect.StatusEffectType.heal)
                {
                    if (!CheckIfPoisoned(true))
                        player.ChangeHealth((int)(sGO.GetComponent<StatusContainer>().GetStatusEffect().GetStatChange() + player.GetPassiveFlat(PassiveSkill.AttributeType.healthRegen) * player.GetPassivePercent(PassiveSkill.AttributeType.healthRegen)));
                }
                else if (sGO.GetComponent<StatusContainer>().GetStatusEffect().GetStatusEffectType() == StatusEffect.StatusEffectType.bleed)
                    player.ChangeHealth(-(int)(player.GetHealth() * sGO.GetComponent<StatusContainer>().GetStatusEffect().GetStatChange()));
                else if (sGO.GetComponent<StatusContainer>().GetStatusEffect().GetStatusEffectType() == StatusEffect.StatusEffectType.burn)
                    player.ChangeHealth(-(int)sGO.GetComponent<StatusContainer>().GetStatusEffect().GetStatChange());
                else if (sGO.GetComponent<StatusContainer>().GetStatusEffect().GetStatusEffectType() == StatusEffect.StatusEffectType.freeze)
                {
                    player.ChangeHealth(-(int)sGO.GetComponent<StatusContainer>().GetStatusEffect().GetStatChange() / 2);
                    player.ChangeStamina(-(int)sGO.GetComponent<StatusContainer>().GetStatusEffect().GetStatChange());
                }                  
                else if(sGO.GetComponent<StatusContainer>().GetStatusEffect().GetStatusEffectType() == StatusEffect.StatusEffectType.shock)
                {
                    player.ChangeHealth(-(int)sGO.GetComponent<StatusContainer>().GetStatusEffect().GetStatChange() / 2);
                    player.ChangeMana(-(int)sGO.GetComponent<StatusContainer>().GetStatusEffect().GetStatChange());
                }
                else if (sGO.GetComponent<StatusContainer>().GetStatusEffect().GetStatusEffectType() == StatusEffect.StatusEffectType.poison)
                    player.ChangeHealth(-(int)sGO.GetComponent<StatusContainer>().GetStatusEffect().GetStatChange());

                sGO.GetComponent<StatusContainer>().DecrementStatusEffect();
                if (sGO.GetComponent<StatusContainer>().GetTurnAmount() < 1)
                {
                    GameObject.Destroy(sGO);
                    playerStatusEffectSlots.Remove(sGO);
                }
            }
        }
        else
        {
            foreach (GameObject sGO in enemyStatusEffectSlots.ToList<GameObject>())
            {
                if (sGO.GetComponent<StatusContainer>().GetStatusEffect().GetStatusEffectType() == StatusEffect.StatusEffectType.heal)
                {
                    if (!CheckIfPoisoned(false))
                        activeEnemy.ChangeHealth((int)sGO.GetComponent<StatusContainer>().GetStatusEffect().GetStatChange());
                }
                else if (sGO.GetComponent<StatusContainer>().GetStatusEffect().GetStatusEffectType() == StatusEffect.StatusEffectType.bleed)
                    activeEnemy.ChangeHealth(-(int)(activeEnemy.GetHealth() * sGO.GetComponent<StatusContainer>().GetStatusEffect().GetStatChange()));
                else if (sGO.GetComponent<StatusContainer>().GetStatusEffect().GetStatusEffectType() == StatusEffect.StatusEffectType.burn)
                    activeEnemy.ChangeHealth(-(int)sGO.GetComponent<StatusContainer>().GetStatusEffect().GetStatChange());
                else if (sGO.GetComponent<StatusContainer>().GetStatusEffect().GetStatusEffectType() == StatusEffect.StatusEffectType.freeze)
                {
                    activeEnemy.ChangeHealth(-(int)sGO.GetComponent<StatusContainer>().GetStatusEffect().GetStatChange() / 2);
                    activeEnemy.ChangeStamina(-(int)sGO.GetComponent<StatusContainer>().GetStatusEffect().GetStatChange());
                }
                else if (sGO.GetComponent<StatusContainer>().GetStatusEffect().GetStatusEffectType() == StatusEffect.StatusEffectType.shock)
                {
                    activeEnemy.ChangeHealth(-(int)sGO.GetComponent<StatusContainer>().GetStatusEffect().GetStatChange() / 2);
                    activeEnemy.ChangeMana(-(int)sGO.GetComponent<StatusContainer>().GetStatusEffect().GetStatChange());
                }
                else if (sGO.GetComponent<StatusContainer>().GetStatusEffect().GetStatusEffectType() == StatusEffect.StatusEffectType.poison)
                    activeEnemy.ChangeHealth(-(int)sGO.GetComponent<StatusContainer>().GetStatusEffect().GetStatChange());

                sGO.GetComponent<StatusContainer>().DecrementStatusEffect();
                if (sGO.GetComponent<StatusContainer>().GetTurnAmount() < 1)
                {
                    GameObject.Destroy(sGO);
                    enemyStatusEffectSlots.Remove(sGO);
                }
            }
        }
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
        ActiveSkill cardCSkill = player.GetRandomManaSkill();

        Instantiate(card, GameObject.Find("PlayerCardALocation").transform).transform.position = GameObject.Find("PlayerCardALocation").transform.position;
        if(cardBSkill != null)
            Instantiate(card, GameObject.Find("PlayerCardBLocation").transform).transform.position = GameObject.Find("PlayerCardBLocation").transform.position;
        if(cardCSkill != null)
            Instantiate(card, GameObject.Find("PlayerCardCLocation").transform).transform.position = GameObject.Find("PlayerCardCLocation").transform.position;



        GameObject.Find("PlayerCardALocation").transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { PlayerAttack(cardASkill); });
        GameObject.Find("PlayerCardALocation").transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { GameObject.Find("WeaponAudioSource").GetComponent<AudioSource>().Play(); });
        if (cardBSkill != null)
        {
            GameObject.Find("PlayerCardBLocation").transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { PlayerAttack(cardBSkill); });
            GameObject.Find("PlayerCardBLocation").transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { GameObject.Find("SkillAudioSource").GetComponent<AudioSource>().Play(); });
        }
        if (cardCSkill != null)
        {
            GameObject.Find("PlayerCardCLocation").transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { PlayerAttack(cardCSkill); });
            GameObject.Find("PlayerCardCLocation").transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { GameObject.Find("MagicAudioSource").GetComponent<AudioSource>().Play(); });
        }

        SetCard(GameObject.Find("PlayerCardALocation").transform.GetChild(0).gameObject, cardASkill);
        if(cardBSkill != null)
            SetCard(GameObject.Find("PlayerCardBLocation").transform.GetChild(0).gameObject, cardBSkill);
        if(cardCSkill != null)
            SetCard(GameObject.Find("PlayerCardCLocation").transform.GetChild(0).gameObject, cardCSkill);

        if (cardBSkill != null && cardBSkill.GetAttributeChange() > player.GetStamina())
            GameObject.Find("PlayerCardBLocation").transform.GetChild(0).GetComponent<Button>().interactable = false;
        if (cardCSkill != null && cardCSkill.GetAttributeChange() > player.GetMana())
            GameObject.Find("PlayerCardCLocation").transform.GetChild(0).GetComponent<Button>().interactable = false;

        while (!playerHasMoved)
        {
            yield return null;
        }

        playerBattleInventoryBtn.interactable = false;

        UpdateInventoryAttributes();

        Destroy(GameObject.Find("PlayerCardALocation").transform.GetChild(0).gameObject);
        if(cardBSkill != null)
            Destroy(GameObject.Find("PlayerCardBLocation").transform.GetChild(0).gameObject);
        if(cardCSkill != null)
            Destroy(GameObject.Find("PlayerCardCLocation").transform.GetChild(0).gameObject);

    }

    void SetCard(GameObject card, ActiveSkill skill)
    {

        card.GetComponent<AttackContainer>().SetPlayerSkill(skill);
    
        /* GameObject cardSkillName */ card.transform.GetChild(1).GetComponent<Text>().text = skill.GetName();
        /* GameObject cardDescription */ card.transform.GetChild(2).GetComponent<Text>().text = skill.GetDescription();
        /* GameObject cardDamage */ card.transform.GetChild(3).GetComponent<Text>().text = skill.GetMinDamageModifier() + " - " + skill.GetMaxDamageModifier();
        /* GameObject cardCost */ card.transform.GetChild(5).GetComponent<Text>().text = skill.GetAttributeChange().ToString();

       switch (skill.GetAttributeType())
       {
            case ActiveSkill.AttributeType.weapon:
                /* GameObject cardType */ card.transform.GetChild(7).GetComponent<Text>().text = "Weapon";
                /* GameObject cardDamage */ card.transform.GetChild(3).GetComponent<Text>().text = player.GetWeapon().GetMinDamage() + " - " + player.GetWeapon().GetMaxDamage();

                /* GameObject cardTypeImg */ card.transform.GetChild(6).GetComponent<Image>().gameObject.SetActive(false);
                /* GameObject cardCost */ card.transform.GetChild(5).GetComponent<Text>().text = "";

                break;
            case ActiveSkill.AttributeType.health:
                /* GameObject cardType */ card.transform.GetChild(7).GetComponent<Text>().text = "Health";               
                /* GameObject cardTypeImg */ card.transform.GetChild(6).GetComponent<Image>().sprite = healthDrop;

                /* GameObject cardTypeImg */ card.transform.GetChild(6).GetComponent<Image>().gameObject.SetActive(true);
               break;
            case ActiveSkill.AttributeType.stamina:
               /* GameObject cardType */ card.transform.GetChild(7).GetComponent<Text>().text = "Stamina";
               /* GameObject cardTypeImg */ card.transform.GetChild(6).GetComponent<Image>().sprite = staminaDrop;

                /* GameObject cardTypeImg */ card.transform.GetChild(6).GetComponent<Image>().gameObject.SetActive(true);
               break;
            case ActiveSkill.AttributeType.mana:
               /* GameObject cardType */ card.transform.GetChild(7).GetComponent<Text>().text = "Mana";
               /* GameObject cardTypeImg */ card.transform.GetChild(6).GetComponent<Image>().sprite = manaDrop;

                /* GameObject cardTypeImg */ card.transform.GetChild(6).GetComponent<Image>().gameObject.SetActive(true);
               break;
       }
        
        //card.transform.GetChild(13).gameObject.SetActive(true);

    }

    public void PlayerAttack(ActiveSkill skill)
    {
        playerHasMoved = true;

        int enemyDefValue = activeEnemy.GetDefense();

        foreach(StatusEffect statusEffect in activeEnemy.GetStatusEffects())
        {
            if(statusEffect.GetStatusEffectType() == StatusEffect.StatusEffectType.defence)
            {
                if (statusEffect.IsPercentage())
                    enemyDefValue = (int)(enemyDefValue * statusEffect.GetStatChange());
                else
                    enemyDefValue = (int)(enemyDefValue + statusEffect.GetStatChange());
            }
        }

        enemyDefValue /= 2;

        switch (skill.GetAttributeType())
        {
            case ActiveSkill.AttributeType.weapon:
                playerDamageOutput = ((int)(player.GetWeapon().Attack() + player.GetPassiveFlat(PassiveSkill.AttributeType.weaponDamage) * player.GetPassivePercent(PassiveSkill.AttributeType.weaponDamage)) - enemyDefValue);
                if (playerDamageOutput < 0)
                    playerDamageOutput = 0;
                OutputToBattle(String.Format(skill.GetActionMessage(), player.GetWeapon().GetName(), playerDamageOutput));
                break;
            case ActiveSkill.AttributeType.health:
                break;
            case ActiveSkill.AttributeType.stamina:
                playerDamageOutput = ((int)((skill.GetDamageModifier() + player.GetPassiveFlat(PassiveSkill.AttributeType.skillDamage)) * player.GetPassivePercent(PassiveSkill.AttributeType.skillDamage)) - enemyDefValue);
                if (playerDamageOutput < 0)
                    playerDamageOutput = 0;
                OutputToBattle(String.Format(skill.GetActionMessage(), playerDamageOutput));
                player.ChangeStamina(-skill.GetAttributeChange());
                break;
            case ActiveSkill.AttributeType.mana:
                if(skill.GetMagicType() == ActiveSkill.MagicType.heal)
                {
                    playerDamageOutput = 0;
                    if (!CheckIfPoisoned(true))
                    {
                        int temp = (int)((skill.GetDamageModifier() + player.GetPassiveFlat(PassiveSkill.AttributeType.healthRegen)) * player.GetPassivePercent(PassiveSkill.AttributeType.healthRegen));
                        player.ChangeHealth(temp);
                        OutputToBattle(String.Format(skill.GetActionMessage(), temp));
                    }
                    else
                        OutputToBattle(String.Format("{0} attempted to heal, but they are poisoned!", player.GetName()));
                }
                if(skill.GetMagicType() == ActiveSkill.MagicType.dispel)
                {
                    playerDamageOutput = 0;
                    int x = skill.GetDamageModifier();
                    for(int i = 0; i < x; i++)
                    {
                        player.DecrementStatusEffectTurn();
                        foreach(GameObject sGO in playerStatusEffectSlots.ToList<GameObject>())
                        {
                            sGO.GetComponent<StatusContainer>().DecrementStatusEffect();
                            if (sGO.GetComponent<StatusContainer>().GetTurnAmount() < 1)
                            {
                                GameObject.Destroy(sGO);
                                playerStatusEffectSlots.Remove(sGO);
                            }
                        }
                    }
                }
                if(skill.GetMagicType() == ActiveSkill.MagicType.damage)
                {
                    playerDamageOutput = (int)((skill.GetDamageModifier() + player.GetPassiveFlat(PassiveSkill.AttributeType.manaDamage) * player.GetPassivePercent(PassiveSkill.AttributeType.manaDamage)) - enemyDefValue);
                    if (playerDamageOutput < 0)
                        playerDamageOutput = 0;
                    OutputToBattle(String.Format(skill.GetActionMessage(), playerDamageOutput));
                }
                player.ChangeMana(-skill.GetAttributeChange());
                break;
        }
        
        if(skill.HasStatusEffects())
        {
            List<StatusEffect> statusEffects = skill.GetStatusEffects();
            foreach (StatusEffect statusEffect in statusEffects)
            {
                float hitChance = UnityEngine.Random.Range(0, 1.0f);

                if (hitChance > statusEffect.GetHitChance())
                {
                    if (statusEffect.IsNegative())
                        AddStatusEffect(Instantiate(statusEffect), false, activeEnemy);
                    else
                        AddStatusEffect(Instantiate(statusEffect), true, null);
                }
            }
        }
    }

    void AddStatusEffect(StatusEffect statusEffect, bool isPlayer, Enemy enemy)
    {
        if (isPlayer)
        {
            player.AddStatusEffect(statusEffect);

            GameObject GO = Instantiate(statusEffectSlot, playerStatusEffectGO.transform);
            GO.GetComponent<StatusContainer>().SetStatusEffect(statusEffect);
            playerStatusEffectSlots.Add(GO);
        }
        else
        {
            enemy.AddStatusEffect(statusEffect);

            GameObject GO = Instantiate(statusEffectSlot, enemyStatusEffectGO.transform);
            GO.GetComponent<StatusContainer>().SetStatusEffect(statusEffect);
            enemyStatusEffectSlots.Add(GO);
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

        int playerDefValue = player.GetDefense();

        foreach (StatusEffect statusEffect in player.GetStatusEffects())
        {
            if (statusEffect.GetStatusEffectType() == StatusEffect.StatusEffectType.defence)
            {
                if (statusEffect.IsPercentage())
                    playerDefValue = (int)(playerDefValue * statusEffect.GetStatChange());
                else
                    playerDefValue = (int)(playerDefValue + statusEffect.GetStatChange());
            }
        }

        playerDefValue /= 3;

        if (attack == null)
        {
            enemyDamageOutput = (int)enemy.GetBaseDamage() - playerDefValue;
            if (enemyDamageOutput < 0)
                enemyDamageOutput = 0;
            OutputToBattle(String.Format(attack.GetDescription(), enemy.GetName(), enemyDamageOutput));
        }
        else
        {
            if (attack.GetAttackAttribute() == EnemyAttackType.AttackAttribute.mana)
            {
                if (attack.GetAttackType() == EnemyAttackType.AttackType.heal)
                {
                    enemyDamageOutput = 0;

                    if (!CheckIfPoisoned(false))
                    {
                        int enemyHealRate = attack.GetDamageModifier();
                        enemy.ChangeHealth(enemyHealRate);
                        OutputToBattle(String.Format(attack.GetDescription(), enemy.GetName(), enemyHealRate));
                    }
                    else
                        OutputToBattle(String.Format("{0} attempted to heal, but is poisoned!", enemy.GetName()));
                    enemy.ChangeMana(-attack.GetAttributeCost());
                }
                else if(attack.GetAttackType() == EnemyAttackType.AttackType.effect)
                {
                    enemyDamageOutput = 0;
                    OutputToBattle(String.Format(attack.GetDescription(), enemy.GetName()));
                }
                else
                {
                    enemyDamageOutput = (attack.GetDamageModifier() + (int)enemy.GetBaseDamage()) - playerDefValue;
                    if (enemyDamageOutput < 0)
                        enemyDamageOutput = 0;
                    enemy.ChangeMana(-attack.GetAttributeCost());
                    OutputToBattle(String.Format(attack.GetDescription(), enemy.GetName(), enemyDamageOutput));
                }
            }
            else if (attack.GetAttackAttribute() == EnemyAttackType.AttackAttribute.stamina)
            {
                if (attack.GetAttackType() == EnemyAttackType.AttackType.heal)
                {
                    enemyDamageOutput = 0;

                    if(!CheckIfPoisoned(false))
                    {
                        int enemyHealRate = attack.GetDamageModifier();
                        enemy.ChangeHealth(enemyHealRate);
                        OutputToBattle(String.Format(attack.GetDescription(), enemy.GetName(), enemyHealRate));
                    }
                    else
                        OutputToBattle(String.Format("{0} attempted to heal, but is poisoned!", enemy.GetName()));
                    enemy.ChangeStamina(-attack.GetAttributeCost());
                }
                else if (attack.GetAttackType() == EnemyAttackType.AttackType.effect)
                {
                    enemyDamageOutput = 0;
                    OutputToBattle(String.Format(attack.GetDescription(), enemy.GetName()));
                }
                else
                {
                    enemyDamageOutput = (attack.GetDamageModifier() + (int)enemy.GetBaseDamage()) - playerDefValue;
                    if (enemyDamageOutput < 0)
                        enemyDamageOutput = 0;
                    enemy.ChangeStamina(-attack.GetAttributeCost());
                    OutputToBattle(String.Format(attack.GetDescription(), enemy.GetName(), enemyDamageOutput));
                }
            }
        }

        if(attack.HasStatusEffects())
        {
            List<StatusEffect> statusEffects = attack.GetStatusEffects();
            foreach (StatusEffect statusEffect in statusEffects)
            {
                float hitChance = UnityEngine.Random.Range(0, 1.0f);

                if (hitChance > statusEffect.GetHitChance())
                {
                    if (statusEffect.IsNegative())
                        AddStatusEffect(Instantiate(statusEffect), true, null);
                    else
                        AddStatusEffect(Instantiate(statusEffect), false, enemy);                  
                }
            }
        }

        if (attack.GetAudioClip() != null)
        {
            GameObject.Find("MonsterAudioSource").GetComponent<AudioSource>().clip = attack.GetAudioClip();
            GameObject.Find("MonsterAudioSource").GetComponent<AudioSource>().Play();
        }
    }

    void PopulateLocationChests()
    {
        //ChestInventory[] chests = GameObject.Find("ChestList").GetComponentsInChildren<ChestInventory>();
        GameObject[] chests = GameObject.FindGameObjectsWithTag("Chest");
        foreach (GameObject chest in chests)
        {
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

    void SpawnEnemies()
    {
        if (player.GetLocation().GetMaxEnemySpawn() > 0)
        {
            List<GameObject> nodes = GameObject.FindGameObjectsWithTag("NodeType").ToList<GameObject>();
            List<Enemy> spawnEnemies = player.GetLocation().GetEnemyTable().EnemySpawn(player.GetLocation());

            foreach (Enemy enemy in spawnEnemies)
            {
                GameObject eGo = Instantiate(enemyObject, GameObject.Find("EnemyList").transform);

                int i = UnityEngine.Random.Range(0, nodes.Count);
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

            }
        }
    }

    public void EnterLocation(Location location)
    {
        StartCoroutine(LoadScene(location, true));
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
        if(!player.CheckForQuest(QuestDictionary[quest.GetID()].GetID()))
        {
            GameObject questSlot = GameObject.Instantiate(uiQuestSlot, questPanel.transform);
            questSlot.GetComponent<QuestContainer>().SetQuest(QuestDictionary[quest.GetID()]);
            questSlot.GetComponent<Button>().onClick.AddListener(() => DisplayQuest(questSlot.GetComponent<QuestContainer>()));
            questSlot.GetComponent<Button>().onClick.AddListener(() => GameObject.Find("ButtonAudioSource").GetComponent<AudioSource>().Play());
            uiQuestSlots.Add(QuestDictionary[quest.GetID()].GetID(), questSlot);
            player.AddQuest(QuestDictionary[quest.GetID()]);
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

    void LoadQuestSlots()
    {
        for(int i = 0; i < player.GetQuestList().Count; i++)
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
            if (isInChest)
                activeChest.AddItem(activeItem);
            if(isInNPC)
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
            if (isInNPC && !isInBattle && currentNPC.HasStore())
            {
                StoreDictionary[currentNPC.GetStore().GetID()].RemoveItem(item);
                player.ChangeGold(-(int)(item.GetValue()));
                if (player.GetGold() < item.GetValue())
                    pickupItemBtn.interactable = false;
            }                
        }
        UpdateInventoryAttributes();
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

                Debug.Log((npcContainsItem && (!isInNPC || isInNPC && !currentNPC.HasStore())) ? "True" : "False");
                Debug.Log(((isInNPC && currentNPC.HasStore() && npcContainsItem && activeItem.GetValue() < player.GetGold())) ? "True" : "False");

                if ((npcContainsItem && (!isInNPC || isInNPC && !currentNPC.HasStore())) || (isInNPC && currentNPC.HasStore() && npcContainsItem && activeItem.GetValue() < player.GetGold()))
                    pickupItemBtn.interactable = true;
                else
                    pickupItemBtn.interactable = false;

                if (player.GetInventory().CheckForItem(activeItem.GetID()))
                    pickupDropItemBtn.interactable = true;
                else
                    pickupDropItemBtn.interactable = false;

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

    public void DisplayEnemy(EnemyContainer enemyContainer)
    {
        selectedEnemy = enemyContainer.GetEnemy();
        arenaEnemyNameTxt.text = enemyContainer.GetEnemy().GetName();
        arenaEnemyDescriptionTxt.text = enemyContainer.GetEnemy().GetDescription();
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

        if (selectedEnemy.GetEnemyType() == Enemy.EnemyType.boss)
            arenaBossImg.SetActive(true);
        else
            arenaBossImg.SetActive(false);
    }

    public void DisplaySkill(SkillContainer skillContainer)
    {
        skillCostGO.SetActive(true);
        skillDamageGO.SetActive(true);

        selectedSkill = SkillDictionary[skillContainer.GetSkill().GetID()];

        skillNameTxt.text = selectedSkill.GetName();
        skillDescriptionTxt.text = selectedSkill.GetDescription();

        if(selectedSkill.GetSkillType() == Skill.SkillType.active)
        {
            skillCostTxt.text = ((ActiveSkill)selectedSkill).GetAttributeChange().ToString();
            skillDamageTxt.text = ((ActiveSkill)selectedSkill).GetMinDamageModifier().ToString() + " - " + ((ActiveSkill)selectedSkill).GetMaxDamageModifier().ToString();

            if (((ActiveSkill)selectedSkill).GetAttributeType() == ActiveSkill.AttributeType.stamina)
            {
                skillCostGO.transform.GetChild(2).GetComponent<Image>().sprite = staminaDrop;
                skillDamageGO.transform.GetChild(2).GetComponent<Image>().sprite = physicalDmgSprite;
            }
            else if (((ActiveSkill)selectedSkill).GetAttributeType() == ActiveSkill.AttributeType.mana)
            {
                skillCostGO.transform.GetChild(2).GetComponent<Image>().sprite = manaDrop;
                skillDamageGO.transform.GetChild(2).GetComponent<Image>().sprite = manaDmgSprite;
            }
            if (((ActiveSkill)selectedSkill).GetMaxDamageModifier() == 0)
                skillDamageGO.SetActive(false);
        }
        else
        {
            skillCostGO.SetActive(false);
            skillDamageGO.SetActive(false);
        }

        if (player.GetSkillPoints() > 0 && !selectedSkill.IsUnlocked() && selectedSkill.IsUnlockable() && (player.GetLevel() >= selectedSkill.GetLevelRequirement()))
        {
            unlockSkillBtn.gameObject.SetActive(true);
            unlockedTxt.gameObject.SetActive(false);
        }
        else if (selectedSkill.IsUnlocked())
        {
            unlockSkillBtn.gameObject.SetActive(false);
            unlockedTxt.gameObject.SetActive(true);
            unlockedTxt.text = "Skill Unlocked";
        }
        else if (!selectedSkill.IsUnlockable())
        {
            unlockSkillBtn.gameObject.SetActive(false);
            unlockedTxt.gameObject.SetActive(true);
            unlockedTxt.text = "Skill Not Yet Unlockable";
        }
        else if(player.GetLevel() < selectedSkill.GetLevelRequirement())
        {
            unlockSkillBtn.gameObject.SetActive(false);
            unlockedTxt.gameObject.SetActive(true);
            unlockedTxt.text = "Level Requirement Not Reached";
        }
        else if (player.GetSkillPoints() < 1)
        {
            unlockSkillBtn.gameObject.SetActive(false);
            unlockedTxt.gameObject.SetActive(true);
            unlockedTxt.text = "Not Enough Skill Points";
        }
    }

    public void UnlockSkill()
    {
        player.SetSkillPoints(player.GetSkillPoints() - 1);
        if (selectedSkill.GetSkillType() == Skill.SkillType.active)
        {
            if (((ActiveSkill)selectedSkill).GetAttributeType() == ActiveSkill.AttributeType.stamina)
                player.AddActiveStaminaSkill((ActiveSkill)selectedSkill);
            else if (((ActiveSkill)selectedSkill).GetAttributeType() == ActiveSkill.AttributeType.mana)
                player.AddActiveManaSkill((ActiveSkill)selectedSkill);
        }
        else
            player.AddPassiveSkill((PassiveSkill)selectedSkill);

        selectedSkill.SetUnlocked();
        selectedSkill.UnlockNextSkills();

        UpdateInventoryAttributes();
        UpdateExpSliders();
        DeactivateSkillSelection();
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

        selQuestName.text = selectedQuest.GetName();
        selQuestDescription.text = selectedQuest.GetDescription();
        noSelectedQuestTxt.SetActive(false);
    }

    public void MakeQuestActive()
    {
        activeQuest = selectedQuest;
        questName.text = activeQuest.GetName();
        questDescription.text = activeQuest.GetDescription();

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

        noActiveQuestTxt.SetActive(false);
        DeactivateQuestSelection();
    }

    public void TurnInQuest(Quest quest)
    {
        QuestDictionary[quest.GetID()].SetCompletion(true);
        OutputToText(String.Format("You have completed {0}.", QuestDictionary[quest.GetID()].GetName()));
        GameObject questSlot = uiQuestSlots[QuestDictionary[quest.GetID()].GetID()];
        uiQuestSlots.Remove(QuestDictionary[quest.GetID()].GetID());
        GameObject.Destroy(questSlot);

        player.AddExp(QuestDictionary[quest.GetID()].GetExpReward());
        player.ChangeGold((int)QuestDictionary[quest.GetID()].GetGoldReward());
        player.RemoveQuest(QuestDictionary[quest.GetID()]);
        player.AddCompletedQuest(QuestDictionary[quest.GetID()]);
        OutputToText(String.Format("You have earned {0} exp and {1} gold.", QuestDictionary[quest.GetID()].GetExpReward(), QuestDictionary[quest.GetID()].GetGoldReward()));

        if(QuestDictionary[quest.GetID()].GetQuestType() == Quest.QuestType.Fetch)
        {
            FetchQuest fetchQuest = ((FetchQuest)QuestDictionary[quest.GetID()]);
            for (int i = 0; i < fetchQuest.GetFetchItems().Count; i++)
                for (int j = 0; j < fetchQuest.GetItemCounts()[i]; j++)
                    RemoveFromInventory(fetchQuest.GetFetchItems()[i]);
        }

        if(QuestDictionary[quest.GetID()].HasItemReward())
        {
            List<Item> itemRewards = QuestDictionary[quest.GetID()].GetItemRewards();
            foreach (Item item in itemRewards)
                AddToInventory(item);
        }

        UpdateInventoryAttributes();
        DeactivateQuestSelection();
        DeactivateActiveQuest();
    }

    public void ActivateInventoryScreen(bool x)
    {
        UIInventoryScreen.SetActive(x);
        UIStatsScreen.SetActive(false);

        DeactivateInvSelection();
    }

    public void ActivateQuestScreen(bool x)
    {
        UIQuestScreen.SetActive(x);
        activeQuestScroll.verticalNormalizedPosition = 1;

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

        DeactivateQuestSelection();
    }

    public void SetIsInNPC(bool x, GameObject NPC)
    {
        isInNPC = x;
        if (x)
            currentNPC = NPCDictionary[NPC.GetComponent<NPCContainer>().GetNPC().GetID()];
        else
            currentNPC = null;

        enterDialogueBtn.gameObject.SetActive(x);
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

        if(x)
        {
            if (isInNPC)
            {
                if (currentNPC.GetNPCType() == NPC.NPCType.merchant)
                {
                    Store currentShop = StoreDictionary[currentNPC.GetStore().GetID()];
                    sourceName = currentShop.GetName();
                    pickupDropItemBtn.gameObject.transform.GetChild(0).GetComponent<Text>().text = "Sell Item";
                    pickupItemBtn.gameObject.transform.GetChild(0).GetComponent<Text>().text = "Buy Item";

                    foreach (Item item in currentShop.GetItemList())
                    {
                        AddToPickup(item);
                    }
                }
            }
            else if (isInBattle)
            {
                sourceName = activeEnemy.GetName();
            }

            invScroll.transform.SetParent(UIPickupScreen.transform);
            otherPickUpNameTxt.text = sourceName;
            isInPickup = true;
            UIPickupScreen.SetActive(true);
        }
        else
        {
            pickupDropItemBtn.gameObject.transform.GetChild(0).GetComponent<Text>().text = "Drop Item";
            pickupItemBtn.gameObject.transform.GetChild(0).GetComponent<Text>().text = "Grab Item";

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

            /*
            if (isInChest)
                OpenChest(activeChest); */
        }
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
                    if(e.GetEnemyType() == Enemy.EnemyType.normal || ((BossEnemy)EnemyDictionary[e.GetID()]).HasBeenDefeated())
                        leveledEnemies.Remove(e);
                }
            }
            else
            {
                if (player.GetLevel() <= e.GetMaxLevel() && player.GetLevel() >= e.GetMinLevel())
                {
                    if(e.GetEnemyType() == Enemy.EnemyType.normal || !((BossEnemy)EnemyDictionary[e.GetID()]).HasBeenDefeated())
                        leveledEnemies.Add(e);
                }
            }
        }

        //leveledEnemies.ForEach(y => Debug.Log(y.GetName()));
        
        enemyA.GetComponent<EnemyContainer>().SetEnemy(leveledEnemies[UnityEngine.Random.Range(0, leveledEnemies.Count)]);
        enemyB.GetComponent<EnemyContainer>().SetEnemy(leveledEnemies[UnityEngine.Random.Range(0, leveledEnemies.Count)]);
        enemyC.GetComponent<EnemyContainer>().SetEnemy(leveledEnemies[UnityEngine.Random.Range(0, leveledEnemies.Count)]);


        enemyANameTxt.text = enemyA.GetComponent<EnemyContainer>().GetEnemy().GetName();
        enemyBNameTxt.text = enemyB.GetComponent<EnemyContainer>().GetEnemy().GetName();
        enemyCNameTxt.text = enemyC.GetComponent<EnemyContainer>().GetEnemy().GetName();

        arenaEnemyDescriptionTxt.text = "";
        arenaEnemyNameTxt.text = "";

        arenaEnemyHealthObj.SetActive(false);
        arenaEnemyStaminaObj.SetActive(false);
        arenaEnemyManaObj.SetActive(false);
        arenaEnemySpeedObj.SetActive(false);
        arenaEnemyDamageObj.SetActive(false);

        arenaBossImg.SetActive(false);

        GameObject.Find("Player").transform.position = new Vector3(0, 2.2f, 10);
        GameObject.Find("Player").transform.transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    public void CheckSkillArrows(Vector2 position)
    {
        GameObject scrollView;
        if (skillScrollView.activeSelf)
            scrollView = skillScrollView;
        else if (magicScrollView.activeSelf)
            scrollView = magicScrollView;
        else
            scrollView = passiveScrollView;

        if (position.x > 0.9)
            scrollView.transform.GetChild(1).gameObject.SetActive(false);
        else
            scrollView.transform.GetChild(1).gameObject.SetActive(true);

        if (position.y > 0.1)
            scrollView.transform.GetChild(2).gameObject.SetActive(true);
        else
            scrollView.transform.GetChild(2).gameObject.SetActive(false);
    }

    public void ActivateSkillScreen(bool x)
    {
        DeactivateInvSelection();
        DeactivateSkillSelection();

        skillScrollView.SetActive(true);
        magicScrollView.SetActive(false);

        UIInventoryScreen.SetActive(!x);
        UIStatsScreen.SetActive(false);
        UISkillScreen.SetActive(x);
    }

    public void ActivateSkillScrollView()
    {
        skillScrollView.SetActive(true);
        magicScrollView.SetActive(false);
        passiveScrollView.SetActive(false);

        skillScrollView.GetComponent<ScrollRect>().verticalNormalizedPosition = 1;
        skillScrollView.GetComponent<ScrollRect>().horizontalNormalizedPosition = 0;
    }

    public void ActivateMagicScrollView()
    {
        skillScrollView.SetActive(false);
        magicScrollView.SetActive(true);
        passiveScrollView.SetActive(false);

        magicScrollView.GetComponent<ScrollRect>().verticalNormalizedPosition = 1;
        magicScrollView.GetComponent<ScrollRect>().horizontalNormalizedPosition = 0;
    }

    public void ActivatePassiveScrollView()
    {
        skillScrollView.SetActive(false);
        magicScrollView.SetActive(false);
        passiveScrollView.SetActive(true);

        passiveScrollView.GetComponent<ScrollRect>().verticalNormalizedPosition = 1;
        passiveScrollView.GetComponent<ScrollRect>().horizontalNormalizedPosition = 0;
    }

    public void ActivateStatsScreen(bool x)
    {
        DeactivateInvSelection();

        UpdateInventoryAttributes();

        UIStatsScreen.SetActive(x);
    }

    public void ActivateDialogueScreen(bool x)
    {
        if (!x)
        {
            if (textType != null)
                StopCoroutine(textType);
            textType = null;
            ClearDialogue();
        }

        if (isInNPC && x)
        {
            diagNPCNameTxt.text = currentNPC.GetName();
            SetDialogues(currentNPC.GetDialogue());
        }
        isInDialogue = x;
        UIDialogueScreen.SetActive(x);
    }

    public void ActivatePauseScreen(bool x)
    {
        UIPauseScreen.SetActive(x);
        UIPauseScreen.GetComponent<Image>().raycastTarget = x;
    }

    public void SelectDialogue(DialogueContainer dialogueContainer)
    {
        if (textType != null)
            StopCoroutine(textType);
        if(dialogueContainer.GetDialogue().GetDialogueType() == Dialogue.DialogueType.merchant)
        {
            ClearDialogue();
            ActivateDialogueScreen(false);
            ActivatePickupScreen(true);
        }
        else if(dialogueContainer.GetDialogue().GetDialogueType() == Dialogue.DialogueType.conversation)
        {
            ClearDialogue();
            SetDialogues(dialogueContainer.GetDialogue());
        }
        else if(dialogueContainer.GetDialogue().GetDialogueType() == Dialogue.DialogueType.quest)
        {
            ClearDialogue();
            QuestDialogue questDialogue = (QuestDialogue)dialogueContainer.GetDialogue();
            if(questDialogue.GetQuestDialogueType() == QuestDialogue.QuestDialogueType.start)
            {
                if (questDialogue.GetQuest().GetQuestType() == Quest.QuestType.Fetch)
                    if (((FetchQuest)questDialogue.GetQuest()).CheckQuestCompletion(player))
                    {
                        TurnInQuest(questDialogue.GetQuest());
                        currentNPC.SetHasGivenQuest(false);
                    }
                if(questDialogue.GetQuest().GetQuestType() == Quest.QuestType.Slay)
                    if(((SlayQuest)QuestDictionary[questDialogue.GetQuest().GetID()]).CheckQuestCompletion(player))
                    {
                        TurnInQuest(questDialogue.GetQuest());
                        currentNPC.SetHasGivenQuest(false);
                    }
            }
            if(questDialogue.GetQuestDialogueType() == QuestDialogue.QuestDialogueType.accept)
            {
                if(!player.CheckForQuest(questDialogue.GetQuest().GetID()) && !QuestDictionary[questDialogue.GetQuest().GetID()].IsCompleted())
                {
                    AddToQuestList(QuestDictionary[questDialogue.GetQuest().GetID()]);
                    currentNPC.SetHasGivenQuest(true);
                    currentNPC.SetGivenQuest(QuestDictionary[questDialogue.GetQuest().GetID()]);
                }
            }
            SetDialogues(questDialogue);
        }
        else if(dialogueContainer.GetDialogue().GetDialogueType() == Dialogue.DialogueType.inn)
        {
            ClearDialogue();
            SetDialogues(dialogueContainer.GetDialogue());
            StartCoroutine(EnterInn());
        }
        else if(dialogueContainer.GetDialogue().GetDialogueType() == Dialogue.DialogueType.battle)
        {
            ClearDialogue();
            Enemy enemy = ((BattleDialogue)dialogueContainer.GetDialogue()).GetEnemy();
            SetDialogues(dialogueContainer.GetDialogue());
            StartCoroutine(EnterBattle(enemy));
        }

    }

    void SetDialogues(Dialogue dialogue)
    {
        //ClearDialogue();
        if (dialogue.GetDialogueType() == Dialogue.DialogueType.start)
        {
            textType = StartCoroutine(TypeText(dialogue.GetNPCLine()));

            List<Dialogue> dialogues = dialogue.GetDialogueAnswers();
            List<string> dialogueStrings = dialogue.GetDialogueOptions();
            List<Sprite> sprites = dialogue.GetDialogueSprites();
            for (int i = 0; i < dialogues.Count; i++)
            {
                GameObject dGO = Instantiate(dialogueBtn, responsePanel.transform);

                dGO.GetComponent<DialogueContainer>().SetDialogue(dialogues[i]);
                dGO.transform.GetChild(0).GetComponent<Text>().text = dialogueStrings[i];
                if (sprites[i] == null)
                    dGO.transform.GetChild(1).gameObject.SetActive(false);
                else
                    dGO.transform.GetChild(1).GetComponent<Image>().sprite = sprites[i];
                dGO.GetComponent<Button>().onClick.AddListener(() => SelectDialogue(dGO.GetComponent<DialogueContainer>()));

                dialogueOptionSlots.Add(dGO);
            }
            if(currentNPC.HasQuests() && !currentNPC.HasGivenQuest())
            {
                List<Quest> quests = currentNPC.GetQuests();
                List<Quest> possibleQuests = new List<Quest>();
                foreach(Quest quest in quests)
                {
                    bool level = (player.GetLevel() >= QuestDictionary[quest.GetID()].GetLevelRequirement());
                    bool prereq = (!QuestDictionary[quest.GetID()].HasPrerequisite()) || (QuestDictionary[quest.GetID()].HasPrerequisite() && QuestDictionary[quest.GetPrerequisite().GetID()].IsCompleted());
                    if (!QuestDictionary[quest.GetID()].IsCompleted() && level && prereq)
                    {
                        possibleQuests.Add(QuestDictionary[quest.GetID()]);
                    }
                }
                if (possibleQuests.Count > 0)
                {
                    Quest r_Quest = possibleQuests[UnityEngine.Random.Range(0, possibleQuests.Count)];

                    GameObject dGO = Instantiate(dialogueBtn, responsePanel.transform);

                    dGO.GetComponent<DialogueContainer>().SetDialogue(r_Quest.GetDialogue());
                    dGO.transform.GetChild(0).GetComponent<Text>().text = r_Quest.GetDialogue().GetNPCLine();
                    if (((QuestDialogue)r_Quest.GetDialogue()).GetOpeningSprite() == null)
                        dGO.transform.GetChild(1).gameObject.SetActive(false);
                    else
                        dGO.transform.GetChild(1).GetComponent<Image>().sprite = ((QuestDialogue)r_Quest.GetDialogue()).GetOpeningSprite();
                    dGO.GetComponent<Button>().onClick.AddListener(() => SelectDialogue(dGO.GetComponent<DialogueContainer>()));

                    dialogueOptionSlots.Add(dGO);
                }
            }
            else if(currentNPC.HasQuests() && currentNPC.HasGivenQuest())
            {
                Quest r_Quest = QuestDictionary[currentNPC.GetGivenQuest().GetID()];

                GameObject dGO = Instantiate(dialogueBtn, responsePanel.transform);

                dGO.GetComponent<DialogueContainer>().SetDialogue(r_Quest.GetDialogue());
                dGO.transform.GetChild(0).GetComponent<Text>().text = r_Quest.GetDialogue().GetNPCLine();
                if (((QuestDialogue)r_Quest.GetDialogue()).GetOpeningSprite() == null)
                    dGO.transform.GetChild(1).gameObject.SetActive(false);
                else
                    dGO.transform.GetChild(1).GetComponent<Image>().sprite = ((QuestDialogue)r_Quest.GetDialogue()).GetOpeningSprite();
                dGO.GetComponent<Button>().onClick.AddListener(() => SelectDialogue(dGO.GetComponent<DialogueContainer>()));

                dialogueOptionSlots.Add(dGO);
            }

            foreach(Quest quest in player.GetQuestList())
            {
                if(quest.GetQuestType() == Quest.QuestType.Talk)
                {
                    TalkQuest talkQuest = (TalkQuest)QuestDictionary[quest.GetID()];
                    if(currentNPC.GetID() == talkQuest.GetTargetNPC().GetID())
                    {
                        GameObject dGO = Instantiate(dialogueBtn, responsePanel.transform);

                        dGO.GetComponent<DialogueContainer>().SetDialogue(talkQuest.GetTargetNPCDialogue());
                        dGO.transform.GetChild(0).GetComponent<Text>().text = talkQuest.GetTargetNPCDialogue().GetNPCLine();
                        if (((QuestDialogue)talkQuest.GetTargetNPCDialogue()).GetOpeningSprite() == null)
                            dGO.transform.GetChild(1).gameObject.SetActive(false);
                        else
                            dGO.transform.GetChild(1).GetComponent<Image>().sprite = talkQuest.GetTargetNPCDialogue().GetOpeningSprite();
                        dGO.GetComponent<Button>().onClick.AddListener(() => SelectDialogue(dGO.GetComponent<DialogueContainer>()));

                        dialogueOptionSlots.Add(dGO);
                    }
                }
            }
        }
        else if(dialogue.GetDialogueType() == Dialogue.DialogueType.conversation)
        {
            textType = StartCoroutine(TypeText(dialogue.GetNPCLine()));

            List<Dialogue> dialogues = dialogue.GetDialogueAnswers();
            List<string> dialogueStrings = dialogue.GetDialogueOptions();
            List<Sprite> sprites = dialogue.GetDialogueSprites();
            for (int i = 0; i < dialogues.Count; i++)
            {
                GameObject dGO = Instantiate(dialogueBtn, responsePanel.transform);

                dGO.GetComponent<DialogueContainer>().SetDialogue(dialogues[i]);
                dGO.transform.GetChild(0).GetComponent<Text>().text = dialogueStrings[i];
                if (sprites[i] == null)
                    dGO.transform.GetChild(1).gameObject.SetActive(false);
                else
                    dGO.transform.GetChild(1).GetComponent<Image>().sprite = sprites[i];
                dGO.GetComponent<Button>().onClick.AddListener(() => SelectDialogue(dGO.GetComponent<DialogueContainer>()));

                dialogueOptionSlots.Add(dGO);
            }
        }
        else if(dialogue.GetDialogueType() == Dialogue.DialogueType.quest)
        {
            QuestDialogue questDialogue = (QuestDialogue)dialogue;
            if (questDialogue.GetQuestDialogueType() == QuestDialogue.QuestDialogueType.start)
            {
                if (QuestDictionary[questDialogue.GetQuest().GetID()].GetQuestType() == Quest.QuestType.Fetch)
                {
                    FetchQuest quest = (FetchQuest)QuestDictionary[questDialogue.GetQuest().GetID()];
                    if (!player.CheckForQuest(quest.GetID()) && !quest.IsCompleted())
                    {
                        textType = StartCoroutine(TypeText(questDialogue.GetNotStartedResponse()));
                        List<Dialogue> dialogues = dialogue.GetDialogueAnswers();
                        List<string> dialogueStrings = dialogue.GetDialogueOptions();
                        List<Sprite> sprites = dialogue.GetDialogueSprites();
                        for (int i = 0; i < dialogues.Count; i++)
                        {
                            GameObject dGO = Instantiate(dialogueBtn, responsePanel.transform);

                            dGO.GetComponent<DialogueContainer>().SetDialogue(dialogues[i]);
                            dGO.transform.GetChild(0).GetComponent<Text>().text = dialogueStrings[i];
                            if (sprites[i] == null)
                                dGO.transform.GetChild(1).gameObject.SetActive(false);
                            else
                                dGO.transform.GetChild(1).GetComponent<Image>().sprite = sprites[i];
                            dGO.GetComponent<Button>().onClick.AddListener(() => SelectDialogue(dGO.GetComponent<DialogueContainer>()));

                            dialogueOptionSlots.Add(dGO);
                        }
                    }
                    else if (!quest.IsCompleted())
                    {
                        textType = StartCoroutine(TypeText(questDialogue.GetNotCompleteResponse()));
                        List<Dialogue> dialogues = questDialogue.GetNotCompleteDialogueAnswers();
                        List<string> dialogueStrings = questDialogue.GetNotCompleteDialogueOptions();
                        List<Sprite> sprites = questDialogue.GetNotCompleteDialogueSprites();
                        for (int i = 0; i < dialogues.Count; i++)
                        {
                            GameObject dGO = Instantiate(dialogueBtn, responsePanel.transform);

                            dGO.GetComponent<DialogueContainer>().SetDialogue(dialogues[i]);
                            dGO.transform.GetChild(0).GetComponent<Text>().text = dialogueStrings[i];
                            if (sprites[i] == null)
                                dGO.transform.GetChild(1).gameObject.SetActive(false);
                            else
                                dGO.transform.GetChild(1).GetComponent<Image>().sprite = sprites[i];
                            dGO.GetComponent<Button>().onClick.AddListener(() => SelectDialogue(dGO.GetComponent<DialogueContainer>()));

                            dialogueOptionSlots.Add(dGO);
                        }
                    }
                    else
                        textType = StartCoroutine(TypeText(questDialogue.GetCompleteResponse()));
                }
                else if (QuestDictionary[questDialogue.GetQuest().GetID()].GetQuestType() == Quest.QuestType.Slay)
                {
                    SlayQuest quest = (SlayQuest)QuestDictionary[questDialogue.GetQuest().GetID()];
                    if (!player.CheckForQuest(quest.GetID()) && !quest.IsCompleted())
                    {
                        textType = StartCoroutine(TypeText(questDialogue.GetNotStartedResponse()));
                        List<Dialogue> dialogues = dialogue.GetDialogueAnswers();
                        List<string> dialogueStrings = dialogue.GetDialogueOptions();
                        List<Sprite> sprites = dialogue.GetDialogueSprites();
                        for (int i = 0; i < dialogues.Count; i++)
                        {
                            GameObject dGO = Instantiate(dialogueBtn, responsePanel.transform);

                            dGO.GetComponent<DialogueContainer>().SetDialogue(dialogues[i]);
                            dGO.transform.GetChild(0).GetComponent<Text>().text = dialogueStrings[i];
                            if (sprites[i] == null)
                                dGO.transform.GetChild(1).gameObject.SetActive(false);
                            else
                                dGO.transform.GetChild(1).GetComponent<Image>().sprite = sprites[i];
                            dGO.GetComponent<Button>().onClick.AddListener(() => SelectDialogue(dGO.GetComponent<DialogueContainer>()));

                            dialogueOptionSlots.Add(dGO);
                        }
                    }
                    else if (!quest.IsCompleted())
                    {
                        textType = StartCoroutine(TypeText(questDialogue.GetNotCompleteResponse()));
                        List<Dialogue> dialogues = questDialogue.GetNotCompleteDialogueAnswers();
                        List<string> dialogueStrings = questDialogue.GetNotCompleteDialogueOptions();
                        List<Sprite> sprites = questDialogue.GetNotCompleteDialogueSprites();
                        for (int i = 0; i < dialogues.Count; i++)
                        {
                            GameObject dGO = Instantiate(dialogueBtn, responsePanel.transform);

                            dGO.GetComponent<DialogueContainer>().SetDialogue(dialogues[i]);
                            dGO.transform.GetChild(0).GetComponent<Text>().text = dialogueStrings[i];
                            if (sprites[i] == null)
                                dGO.transform.GetChild(1).gameObject.SetActive(false);
                            else
                                dGO.transform.GetChild(1).GetComponent<Image>().sprite = sprites[i];
                            dGO.GetComponent<Button>().onClick.AddListener(() => SelectDialogue(dGO.GetComponent<DialogueContainer>()));

                            dialogueOptionSlots.Add(dGO);

                        }
                    }
                    else
                        textType = StartCoroutine(TypeText(questDialogue.GetCompleteResponse()));
                }
            }
            else
            {
                textType = StartCoroutine(TypeText(dialogue.GetNPCLine()));

                List<Dialogue> dialogues = dialogue.GetDialogueAnswers();
                List<string> dialogueStrings = dialogue.GetDialogueOptions();
                List<Sprite> sprites = dialogue.GetDialogueSprites();
                for (int i = 0; i < dialogues.Count; i++)
                {
                    GameObject dGO = Instantiate(dialogueBtn, responsePanel.transform);

                    dGO.GetComponent<DialogueContainer>().SetDialogue(dialogues[i]);
                    dGO.transform.GetChild(0).GetComponent<Text>().text = dialogueStrings[i];
                    if (sprites[i] == null)
                        dGO.transform.GetChild(1).gameObject.SetActive(false);
                    else
                        dGO.transform.GetChild(1).GetComponent<Image>().sprite = sprites[i];
                    dGO.GetComponent<Button>().onClick.AddListener(() => SelectDialogue(dGO.GetComponent<DialogueContainer>()));

                    dialogueOptionSlots.Add(dGO);
                }
            }
        }
        else
        {
            textType = StartCoroutine(TypeText(dialogue.GetNPCLine()));

            List<Dialogue> dialogues = dialogue.GetDialogueAnswers();
            List<string> dialogueStrings = dialogue.GetDialogueOptions();
            List<Sprite> sprites = dialogue.GetDialogueSprites();
            for (int i = 0; i < dialogues.Count; i++)
            {
                GameObject dGO = Instantiate(dialogueBtn, responsePanel.transform);

                dGO.GetComponent<DialogueContainer>().SetDialogue(dialogues[i]);
                dGO.transform.GetChild(0).GetComponent<Text>().text = dialogueStrings[i];
                if (sprites[i] == null)
                    dGO.transform.GetChild(1).gameObject.SetActive(false);
                else
                    dGO.transform.GetChild(1).GetComponent<Image>().sprite = sprites[i];
                dGO.GetComponent<Button>().onClick.AddListener(() => SelectDialogue(dGO.GetComponent<DialogueContainer>()));

                dialogueOptionSlots.Add(dGO);
            }
        }
    }

    IEnumerator TypeText(string text)
    {
        var waitTimer = new WaitForSeconds(0.025f);
        npcLineTxt.text = text;
        npcLineTxt.maxVisibleCharacters = 0;
        for(int i = 1; i < text.Length + 1; i++)
        {
            npcLineTxt.maxVisibleCharacters = i;
            if(i%2 == 0)
                GameObject.Find("TextAudioSource").GetComponent<AudioSource>().Play();
            yield return waitTimer;
        }
    }

    void ClearDialogue()
    {
        npcLineTxt.text = "";
        foreach(GameObject dGO in dialogueOptionSlots.ToList<GameObject>())
        {
            dialogueOptionSlots.Remove(dGO);
            Destroy(dGO);
        }
    }

    IEnumerator StartGame()
    {
        UICoverScreen.GetComponent<Image>().raycastTarget = true;
        UICoverScreen.GetComponent<Image>().color = new Color(0, 0, 0, 1);
        messageTxt.color = new Color(255, 255, 255, 0);

        messageTxt.text = "Wake up.";

        while(messageTxt.color.a < 1)
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

        messageTxt.text = "What is your name, gladiator?";

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
        playerBattleNameTxt.text = name;
        statsPlayerNameTxt.text = name;
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

        messageTxt.text = "I've given you a crooked dagger and a tattered shirt.";
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

        messageTxt.text = "It should be all you need for now.";

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

        GameObject.Find("Player").transform.position = new Vector3(0, 2.2f, 10f);
        GameObject.Find("Player").transform.eulerAngles = new Vector3(0, 90f, 0);

        messageTxt.text = "Prove your worth to me, " + name + ".";

        while (messageTxt.color.a < 1)
        {
            messageTxt.color = new Color(messageTxt.color.r, messageTxt.color.g, messageTxt.color.b, messageTxt.color.a + 0.01f);
            yield return new WaitForSecondsRealtime(0.05f * Time.deltaTime);
        }
        yield return new WaitForSecondsRealtime(1f);
        UICoverScreen.GetComponent<Image>().raycastTarget = false;
        var coroutine = StartCoroutine(Battle(smallRat));
        while (messageTxt.color.a > 0)
        {
            messageTxt.color = new Color(messageTxt.color.r, messageTxt.color.g, messageTxt.color.b, messageTxt.color.a - 0.01f);
            UICoverScreen.GetComponent<Image>().color = new Color(UICoverScreen.GetComponent<Image>().color.r, UICoverScreen.GetComponent<Image>().color.g, UICoverScreen.GetComponent<Image>().color.b, UICoverScreen.GetComponent<Image>().color.a - (0.01f));
            yield return new WaitForSecondsRealtime(0.01f * Time.deltaTime);
        }
        yield return coroutine;
        yield return new WaitForUIButtons(GameObject.Find("PickupExitBtn").GetComponent<Button>());
        ActivateDialogueScreen(true);

        StartCoroutine(OverworldMusic());
        StartCoroutine(DirectionalOutput());
        GameObject.Find("BirdsLoopAudioSource").GetComponent<AudioSource>().Play();
        GameObject.Find("WindAmbianceLoopAudioSource").GetComponent<AudioSource>().Play();
        
    }

    IEnumerator EnterInn()
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

        ActivateDialogueScreen(false);
        player.ChangeGold(-10);

        player.SetHealth(player.GetMaxHealth());
        player.SetStamina(player.GetMaxStamina());
        player.SetMana(player.GetMaxMana());
        player.ClearStatusEffects();

        yield return new WaitForSecondsRealtime(0.5f);
        foreach (GameObject sGO in playerStatusEffectSlots.ToList<GameObject>())
        {
            GameObject.Destroy(sGO);
            playerStatusEffectSlots.Remove(sGO);
        }

        GameObject.Find("Player").transform.position = new Vector3(30, 1.8f, 70);

        while(UICoverScreen.GetComponent<Image>().color.a > 0)
        {
            UICoverScreen.GetComponent<Image>().color = new Color(UICoverScreen.GetComponent<Image>().color.r, UICoverScreen.GetComponent<Image>().color.g, UICoverScreen.GetComponent<Image>().color.b, UICoverScreen.GetComponent<Image>().color.a - (0.01f));
            messageTxt.color = new Color(messageTxt.color.r, messageTxt.color.g, messageTxt.color.b, messageTxt.color.a - 0.01f);
            yield return new WaitForSecondsRealtime(0.005f * Time.deltaTime);
        }
        UICoverScreen.GetComponent<Image>().raycastTarget = false;
        messageTxt.raycastTarget = false;
    }

    IEnumerator EnterBattle(Enemy enemy)
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

        ActivateDialogueScreen(false);
        StartCoroutine(Battle(enemy));

        while (UICoverScreen.GetComponent<Image>().color.a > 0)
        {
            UICoverScreen.GetComponent<Image>().color = new Color(UICoverScreen.GetComponent<Image>().color.r, UICoverScreen.GetComponent<Image>().color.g, UICoverScreen.GetComponent<Image>().color.b, UICoverScreen.GetComponent<Image>().color.a - (0.01f));
            messageTxt.color = new Color(messageTxt.color.r, messageTxt.color.g, messageTxt.color.b, messageTxt.color.a - 0.01f);
            yield return new WaitForSecondsRealtime(0.01f * Time.deltaTime);
        }
        UICoverScreen.GetComponent<Image>().raycastTarget = false;
        messageTxt.raycastTarget = false;
    }

    IEnumerator HasDied()
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

        UIBattleScreen.SetActive(false);
    }

    public void UseItem()
    {
        Consumable c = (Consumable)activeItem;
        c.UseItem(player);
        RemoveFromInventory(c);

        switch(c.GetConsumableType())
        {
            case Consumable.ConsumableType.healthPotion:
                GameObject.Find("FoodDrinkAudioSource").GetComponent<AudioSource>().Play();
                UpdateHealthSliders();
                break;
            case Consumable.ConsumableType.staminaPotion:
                GameObject.Find("FoodDrinkAudioSource").GetComponent<AudioSource>().Play();
                UpdateStaminaSliders();
                break;
            case Consumable.ConsumableType.manaPotion:
                GameObject.Find("FoodDrinkAudioSource").GetComponent<AudioSource>().Play();
                UpdateManaSliders();
                break;
            case Consumable.ConsumableType.food:
                GameObject.Find("FoodEatAudioSource").GetComponent<AudioSource>().Play();
                UpdateHealthSliders();
                break;
            case Consumable.ConsumableType.drink:
                GameObject.Find("FoodDrinkAudioSource").GetComponent<AudioSource>().Play();
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
            damageValueTxt.text = w.GetMaxDamage().ToString();
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
            Debug.Log("Armor Name:" + a.GetName());
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
    }

    void DeactivateQuestSelection()
    {
        selectedQuest = null;
        selQuestDescription.text = "";
        selQuestName.text = "";

        noSelectedQuestTxt.SetActive(true);

        makeActiveBtn.gameObject.SetActive(false);
    }

    void DeactivateActiveQuest()
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

        skillNameTxt.text = "";
        skillDescriptionTxt.text = "";
        skillCostTxt.text = "";
        skillDamageTxt.text = "";

        skillCostGO.SetActive(false);
        skillDamageGO.SetActive(false);

        unlockSkillBtn.gameObject.SetActive(false);
        unlockedTxt.gameObject.SetActive(false);
    }

    public void OutputToText(string output)
    {
        outputQueue.Enqueue(output);
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
        UpdateExtraStats();

        SetCurrentWeight();
        goldTxt.text = player.GetGold().ToString();
        pickupGoldTxt.text = player.GetGold().ToString();
        statsGoldTxt.text = player.GetGold().ToString();
        statsDmgTxt.text = player.GetWeapon().GetMaxDamage().ToString();
        statsDefTxt.text = player.GetDefense().ToString();
    }

    void UpdateExpSliders()
    {
        statsExpSlider.maxValue = player.GetToLevelExp();
        statsExpSlider.value = player.GetExp();
        statsTotalExpTxt.text = player.GetTotalExp().ToString();
        statsSkillPointsTxt.text = player.GetSkillPoints().ToString();
        skillSkillPointsTxt.text = player.GetSkillPoints().ToString();
        statsLvlTxt.text = player.GetLevel().ToString();
        statsExpTxt.text = player.GetExp() + "/" + player.GetToLevelExp();
    }

    void UpdateHealthSliders()
    {
        statsHealthSlider.maxValue = player.GetMaxHealth();
        statsHealthSlider.value = player.GetHealth();
        statsHealthTxt.text = player.GetHealth() + "/" + player.GetMaxHealth();
    }
    
    void UpdateStaminaSliders()
    {
        statsStaminaSlider.maxValue = player.GetMaxStamina();
        statsStaminaSlider.value = player.GetStamina();
        statsStaminaTxt.text = player.GetStamina() + "/" + player.GetMaxStamina();
    }

    void UpdateManaSliders()
    {
        statsManaSlider.maxValue = player.GetMaxMana();
        statsManaSlider.value = player.GetMana();
        statsManaTxt.text = player.GetMana() + "/" + player.GetMaxMana();
    }

    void UpdateExtraStats()
    {
        maxHealthBuffTxt.text = String.Format("{0}; {1}%", player.GetPassiveFlat(PassiveSkill.AttributeType.maxHealth),
            player.GetPassivePercent(PassiveSkill.AttributeType.maxHealth) * 100);
        maxStaminaBuffTxt.text = String.Format("{0}; {1}%", player.GetPassiveFlat(PassiveSkill.AttributeType.maxStamina),
            player.GetPassivePercent(PassiveSkill.AttributeType.maxStamina) * 100);
        maxManaBuffTxt.text = String.Format("{0}; {1}%", player.GetPassiveFlat(PassiveSkill.AttributeType.maxMana),
            player.GetPassivePercent(PassiveSkill.AttributeType.maxMana) * 100);
        maxWeaponDmgBuffTxt.text = String.Format("{0}; {1}%", player.GetPassiveFlat(PassiveSkill.AttributeType.weaponDamage),
            player.GetPassivePercent(PassiveSkill.AttributeType.weaponDamage) * 100);
        maxSkillDmgBuffTxt.text = String.Format("{0}; {1}%", player.GetPassiveFlat(PassiveSkill.AttributeType.skillDamage),
            player.GetPassivePercent(PassiveSkill.AttributeType.skillDamage) * 100);
        maxMagicDmgBuffTxt.text = String.Format("{0}; {1}%", player.GetPassiveFlat(PassiveSkill.AttributeType.manaDamage),
            player.GetPassivePercent(PassiveSkill.AttributeType.manaDamage) * 100);
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
        statsWeightTxt.text = player.GetCurrentWeight().ToString();
    }

    SaveLoad NewSaveLoadObject()
    {
        SaveLoad saveLoad = new SaveLoad();

        saveLoad.SavePlayer(player);
        saveLoad.SaveNPCs();
        saveLoad.SaveStores();
        saveLoad.SaveBossEnemies();

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

    IEnumerator LoadScene(Location location, bool isLoading)
    {
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

            player.SetLocation(location);
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

        if (isLoading)
        {
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
            if (outputQueue.Count > 0 && !isInBattle && !isInPickup && !isInDialogue)
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
            if(!isInBattle && !overworldMusic.GetComponent<AudioSource>().isPlaying)
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
}