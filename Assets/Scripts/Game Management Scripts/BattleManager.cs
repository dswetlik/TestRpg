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

public class BattleManager : MonoBehaviour
{

    Engine engine;

    GameObject UIBattleScreen;

    // UI Battle Variables
    public GameObject statusEffectSlot;
    GameObject playerStatusEffectGO;
    List<GameObject> playerStatusEffectSlots = new List<GameObject>();
    GameObject enemyStatusEffectGO;
    List<GameObject> enemyStatusEffectSlots = new List<GameObject>();
    
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
    Button battleLeaveBtn;
    Button battleAttackBtn;
    Button battleSkillBtn;
    Button battleDefendBtn;
    Button battleRestBtn;

    public Enemy activeEnemy;
    public bool isInBattle = false, playerHasMoved = false;
    public bool playerDefending = false, enemyDefending = false;
    public int playerDamageOutput, enemyDamageOutput;

    public Coroutine battleCoroutine;

    private void Start()
    {
        if (engine == null)
            engine = gameObject.GetComponent<Engine>();

        UIBattleScreen = GameObject.Find("UI Battle");

        playerStatusEffectGO = GameObject.Find("PlayerStatusEffects");
        enemyStatusEffectGO = GameObject.Find("EnemyStatusEffects");

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
        battleLeaveBtn = GameObject.Find("BattleLeaveBtn").GetComponent<Button>();
        battleAttackBtn = GameObject.Find("BattleAttackBtn").GetComponent<Button>();
        battleSkillBtn = GameObject.Find("BattleSkillBtn").GetComponent<Button>();
        battleDefendBtn = GameObject.Find("BattleDefendBtn").GetComponent<Button>();
        battleRestBtn = GameObject.Find("BattleRestBtn").GetComponent<Button>();

        battleAttackBtn.interactable = false;
        battleSkillBtn.interactable = false;
        battleDefendBtn.interactable = false;
        battleRestBtn.interactable = false;

        UIBattleScreen.SetActive(false);
    }

    public void Battle(Enemy enemy, bool canLeave = false)
    {
        battleCoroutine = StartCoroutine(r_Battle(enemy));

        battleLeaveBtn.interactable = canLeave;
    }

    public void UseSkill(Skill skill)
    {
        PlayerAttack(skill);
    }

    IEnumerator r_Battle(Enemy eGO)
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
        enemyNameTxt.text = activeEnemy.GetName();
        activeEnemy.Initialize();

        UIBattleScreen.SetActive(true);

        UpdateBattleAttributes(enemy);

        float playerSpeed = Engine.player.GetSpeed(), enemySpeed = activeEnemy.GetSpeed();
        playerSpeedSlider.value = playerSpeed;
        enemySpeedSlider.value = enemySpeed;

        while (Engine.player.GetHealth() > 0 && enemy.GetHealth() > 0)
        {

            while (playerSpeed < 100 && enemySpeed < 100)
            {

                int extraPlayerSpeedFlat = 0, extraEnemySpeedFlat = 0;
                float extraPlayerSpeedPer = 1.0f, extraEnemySpeedPer = 1.0f;

                foreach (StatusEffect statusEffect in Engine.player.GetStatusEffects())
                    if (statusEffect.GetStatusEffectType() == StatusEffect.StatusEffectType.speed)
                        if (statusEffect.IsPercentage())
                            extraPlayerSpeedPer += statusEffect.GetStatChange();
                        else
                            extraPlayerSpeedFlat += (int)statusEffect.GetStatChange();

                foreach (StatusEffect statusEffect in activeEnemy.GetStatusEffects())
                    if (statusEffect.GetStatusEffectType() == StatusEffect.StatusEffectType.speed)
                        if (statusEffect.IsPercentage())
                            extraEnemySpeedPer += statusEffect.GetStatChange();
                        else
                            extraEnemySpeedFlat += (int)statusEffect.GetStatChange();

                Debug.Log(String.Format("Player Flat: {0}; Player %: {1};", extraPlayerSpeedFlat, extraPlayerSpeedPer));
                Debug.Log(String.Format("Enemy Flat: {0}; Enemy %: {1};", extraEnemySpeedFlat, extraEnemySpeedPer));

                playerSpeed += (int)extraPlayerSpeedPer * (Engine.player.GetSpeed() + extraPlayerSpeedFlat);
                enemySpeed += (int)extraEnemySpeedPer * (activeEnemy.GetSpeed() + extraEnemySpeedFlat);

                playerSpeedSlider.value = playerSpeed;
                enemySpeedSlider.value = enemySpeed;

                UpdateBattleAttributes(activeEnemy);
                yield return new WaitForSecondsRealtime(0.01f);
            }

            if (playerSpeed >= 100 && playerSpeed >= enemySpeed && Engine.player.GetHealth() > 0)
            {
                if (!CheckIfStunned(true))
                    yield return StartCoroutine("PlayerMove");
                playerSpeed -= 100;

                activeEnemy.ChangeHealth(-playerDamageOutput);
                UpdateBattleAttributes(activeEnemy);

                playerSpeedSlider.value = playerSpeed;

                Engine.player.DecrementStatusEffectTurn();

                EndOfTurnStatusEffect(true);

                UpdateBattleAttributes(activeEnemy);

                if (enemySpeed >= 100 && activeEnemy.GetHealth() > 0)
                {
                    if (!CheckIfStunned(false))
                        EnemyAttack(activeEnemy);
                    enemySpeed -= 100;

                    Engine.player.ChangeHealth(-enemyDamageOutput);
                    UpdateBattleAttributes(activeEnemy);

                    enemySpeedSlider.value = enemySpeed;

                    activeEnemy.DecrementStatusEffectTurn();

                    EndOfTurnStatusEffect(false);

                    UpdateBattleAttributes(activeEnemy);
                }

                playerDamageOutput = 0;
                enemyDamageOutput = 0;

            }
            if (enemySpeed >= 100 && enemySpeed >= playerSpeed && activeEnemy.GetHealth() > 0)
            {
                if (!CheckIfStunned(false))
                    EnemyAttack(activeEnemy);
                enemySpeed -= 100;

                Engine.player.ChangeHealth(-enemyDamageOutput);
                UpdateBattleAttributes(activeEnemy);

                enemySpeedSlider.value = enemySpeed;

                activeEnemy.DecrementStatusEffectTurn();

                EndOfTurnStatusEffect(false);

                UpdateBattleAttributes(activeEnemy);

                if (playerSpeed >= 100 && Engine.player.GetHealth() > 0)
                {
                    if (!CheckIfStunned(true))
                        yield return StartCoroutine("PlayerMove");
                    playerSpeed -= 100;

                    activeEnemy.ChangeHealth(-playerDamageOutput);
                    UpdateBattleAttributes(activeEnemy);

                    playerSpeedSlider.value = playerSpeed;

                    Engine.player.DecrementStatusEffectTurn();

                    EndOfTurnStatusEffect(true);

                    UpdateBattleAttributes(activeEnemy);
                }

                playerDamageOutput = 0;
                enemyDamageOutput = 0;

            }

            UpdateBattleAttributes(activeEnemy);

            yield return null;
        }

        if (activeEnemy.GetHealth() <= 0)
        {
            int goldReward = activeEnemy.GetRandomGoldReward();

            GameObject.Find("BattleWinMusicAudioSource").GetComponent<AudioSource>().Play();
            engine.ActivatePickupScreen(true);
            engine.GenerateItemPickup(enemy.GetItemRewards());
            engine.OutputToText(String.Format("You have killed {0}, gaining {1} exp and {2} gold.", activeEnemy.GetName(), activeEnemy.GetExpReward(), goldReward));
            Engine.player.AddExp(activeEnemy.GetExpReward());
            Engine.player.ChangeGold(goldReward);

            if (activeEnemy.GetEnemyType() == Enemy.EnemyType.boss)
            {
                BossEnemy boss = (BossEnemy)activeEnemy;
                ((BossEnemy)Engine.EnemyDictionary[boss.GetID()]).SetHasBeenDefeated(true);

                Engine.player.SetTitle(boss.GetUnlockedTitle());

                for (int i = 0; i < boss.GetUnlockedItems().Count; i++)
                {
                    Item item = boss.GetUnlockedItems()[i];
                    for (int j = 0; j < boss.GetUnlockedItemsCount()[i]; j++)
                    {
                        if (item.IsWeapon())
                        {
                            Engine.StoreDictionary[Engine.NPCDictionary[0].GetStore().GetID()].AddItem(item);
                        }
                        else if (item.IsArmor())
                        {
                            if (((Armor)item).GetArmorClass() == Armor.ArmorClass.light)
                                Engine.StoreDictionary[Engine.NPCDictionary[2].GetStore().GetID()].AddItem(item);
                            else if (((Armor)item).GetArmorClass() == Armor.ArmorClass.heavy)
                                Engine.StoreDictionary[Engine.NPCDictionary[5].GetStore().GetID()].AddItem(item);
                        }
                        else if (item.IsConsumable())
                        {
                            Engine.StoreDictionary[Engine.NPCDictionary[1].GetStore().GetID()].AddItem(item);
                        }
                    }
                }
            }

            foreach (GameObject sGO in enemyStatusEffectSlots.ToList<GameObject>())
            {
                GameObject.Destroy(sGO);
                enemyStatusEffectSlots.Remove(sGO);
            }

            foreach (KeyValuePair<uint, Quest> kvp in Engine.QuestDictionary)
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
            engine.StartHasDied();
            UIBattleScreen.SetActive(false);
        }

        GameObject.Find("BattleMusicAudioSource").GetComponent<AudioSource>().Stop();
        Engine.player.AddBattleCount(1);
        if (Engine.player.GetBattleCount() % 6 == 0)
            Advertisements.Instance.ShowInterstitial();
  

        engine.UpdateInventoryAttributes();
        battleOutputTxt.text = "";
        isInBattle = false;
        Advertisements.Instance.ShowBanner(BannerPosition.BOTTOM);

        if (Engine.player.GetBattleCount() % 10 == 0 && PlayerPrefs.GetInt("ShowRatings", 0) == 0)
        {
            engine.ActivateRatingsScreen();
        }
        battleCoroutine = null;
    }

    public void RunFromBattle()
    {
        StopCoroutine(battleCoroutine);
        int lostGold = UnityEngine.Random.Range((int)(Engine.player.GetGold() * 0.25f), (int)(Engine.player.GetGold() * 0.5f));
        Engine.player.ChangeGold(-lostGold);

        engine.OutputToText("You have ran away from battle!");
        engine.OutputToText(String.Format("You have lost {0} gold in the attempt.", lostGold));

        GameObject.Find("BattleMusicAudioSource").GetComponent<AudioSource>().Stop();

        foreach (GameObject sGO in enemyStatusEffectSlots.ToList<GameObject>())
        {
            GameObject.Destroy(sGO);
            enemyStatusEffectSlots.Remove(sGO);
        }

        Engine.player.AddBattleCount(1);
        if (Engine.player.GetBattleCount() % 6 == 0)
            Advertisements.Instance.ShowInterstitial();

        engine.UpdateInventoryAttributes();
        battleOutputTxt.text = "";
        isInBattle = false;
        Advertisements.Instance.ShowBanner(BannerPosition.BOTTOM);

        UIBattleScreen.SetActive(false);
        battleCoroutine = null;
    }

    bool CheckIfStunned(bool isPlayer)
    {
        if (isPlayer)
        {
            foreach (StatusEffect statusEffect in Engine.player.GetStatusEffects())
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

    bool CheckIfBleeding(bool isPlayer)
    {
        if (isPlayer)
        {
            foreach (StatusEffect statusEffect in Engine.player.GetStatusEffects())
                if (statusEffect.GetStatusEffectType() == StatusEffect.StatusEffectType.bleed)
                    return true;
            return false;
        }
        else
        {
            foreach (StatusEffect statusEffect in activeEnemy.GetStatusEffects())
                if (statusEffect.GetStatusEffectType() == StatusEffect.StatusEffectType.bleed)
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
                    if (!CheckIfBleeding(true))
                        Engine.player.ChangeHealth((int)(Engine.player.GetMaxHealth() * sGO.GetComponent<StatusContainer>().GetStatusEffect().GetStatChange()));
                }
                else if (sGO.GetComponent<StatusContainer>().GetStatusEffect().GetStatusEffectType() == StatusEffect.StatusEffectType.burn)
                    Engine.player.ChangeHealth(-(int)(Engine.player.GetHealth() * sGO.GetComponent<StatusContainer>().GetStatusEffect().GetStatChange()));
                else if (sGO.GetComponent<StatusContainer>().GetStatusEffect().GetStatusEffectType() == StatusEffect.StatusEffectType.freeze)
                    Engine.player.ChangeStamina(-(int)(Engine.player.GetStamina() * sGO.GetComponent<StatusContainer>().GetStatusEffect().GetStatChange()));
                else if (sGO.GetComponent<StatusContainer>().GetStatusEffect().GetStatusEffectType() == StatusEffect.StatusEffectType.shock)
                    Engine.player.ChangeMana(-(int)(Engine.player.GetMana() * sGO.GetComponent<StatusContainer>().GetStatusEffect().GetStatChange()));

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
                    if (!CheckIfBleeding(false))
                        activeEnemy.ChangeHealth((int)(activeEnemy.GetMaxHealth() * sGO.GetComponent<StatusContainer>().GetStatusEffect().GetStatChange()));
                }
                else if (sGO.GetComponent<StatusContainer>().GetStatusEffect().GetStatusEffectType() == StatusEffect.StatusEffectType.burn)
                    activeEnemy.ChangeHealth(-(int)(activeEnemy.GetHealth() * sGO.GetComponent<StatusContainer>().GetStatusEffect().GetStatChange()));
                else if (sGO.GetComponent<StatusContainer>().GetStatusEffect().GetStatusEffectType() == StatusEffect.StatusEffectType.freeze)
                    activeEnemy.ChangeStamina(-(int)(activeEnemy.GetStamina() * sGO.GetComponent<StatusContainer>().GetStatusEffect().GetStatChange()));
                else if (sGO.GetComponent<StatusContainer>().GetStatusEffect().GetStatusEffectType() == StatusEffect.StatusEffectType.shock)
                    activeEnemy.ChangeMana(-(int)(activeEnemy.GetMana() * sGO.GetComponent<StatusContainer>().GetStatusEffect().GetStatChange()));


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

        playerBattleHealthSlider.maxValue = Engine.player.GetMaxHealth();
        playerBattleStaminaSlider.maxValue = Engine.player.GetMaxStamina();
        playerBattleManaSlider.maxValue = Engine.player.GetMaxMana();

        enemyHealthSlider.value = enemy.GetHealth();
        enemyStaminaSlider.value = enemy.GetStamina();
        enemyManaSlider.value = enemy.GetMana();

        playerBattleHealthSlider.value = Engine.player.GetHealth();
        playerBattleStaminaSlider.value = Engine.player.GetStamina();
        playerBattleManaSlider.value = Engine.player.GetMana();

        playerBattleHealthTxt.text = String.Format("{0}/{1}", Engine.player.GetHealth(), Engine.player.GetMaxHealth());
        playerBattleStaminaTxt.text = String.Format("{0}/{1}", Engine.player.GetStamina(), Engine.player.GetMaxStamina());
        playerBattleManaTxt.text = String.Format("{0}/{1}", Engine.player.GetMana(), Engine.player.GetMaxMana());

        enemyHealthTxt.text = String.Format("{0}/{1}", enemy.GetHealth(), enemy.GetMaxHealth());
        enemyStaminaTxt.text = String.Format("{0}/{1}", enemy.GetStamina(), enemy.GetMaxStamina());
        enemyManaTxt.text = String.Format("{0}/{1}", enemy.GetMana(), enemy.GetMaxMana());
    }

    IEnumerator PlayerMove()
    {
        playerHasMoved = false;
        playerDefending = false;
        playerBattleInventoryBtn.interactable = true;

        battleAttackBtn.interactable = true;
        battleSkillBtn.interactable = true;
        battleDefendBtn.interactable = true;
        battleRestBtn.interactable = true;

        while (!playerHasMoved)
        {
            yield return null;
        }

        playerBattleInventoryBtn.interactable = false;

        battleAttackBtn.interactable = false;
        battleSkillBtn.interactable = false;
        battleDefendBtn.interactable = false;
        battleRestBtn.interactable = false;

        engine.UpdateInventoryAttributes();
    }

    public void BattleWeaponAttack()
    {
        PlayerAttack(Engine.player.GetWeapon().GetRandomSkill());
    }

    public void PlayerAttack(Skill skill)
    {
        playerHasMoved = true;

        int enemyDefValue = activeEnemy.GetDefense();        

        foreach (StatusEffect statusEffect in activeEnemy.GetStatusEffects())
        {
            if (statusEffect.GetStatusEffectType() == StatusEffect.StatusEffectType.defence)
            {
                if (statusEffect.IsPercentage())
                    enemyDefValue = (int)(enemyDefValue * statusEffect.GetStatChange());
                else
                    enemyDefValue = (int)(enemyDefValue + statusEffect.GetStatChange());
            }
        }

        enemyDefValue /= 3;

        switch (skill.GetSkillType())
        {
            case Skill.SkillType.weapon:
                playerDamageOutput = ((int)(Engine.player.GetWeapon().Attack() - enemyDefValue));
                if (playerDamageOutput < 0)
                    playerDamageOutput = 0;
                OutputToBattle(String.Format(skill.GetActionMessage(), Engine.player.GetWeapon().GetName(), playerDamageOutput));
                break;
            case Skill.SkillType.health:
                break;
            case Skill.SkillType.stamina:
                StaminaSkill sSkill = ((StaminaSkill)skill);
                playerDamageOutput = ((int)(sSkill.GetDamageModifier() - enemyDefValue));
                if (sSkill.IsWeaponModifier())
                    playerDamageOutput += Engine.player.GetWeapon().Attack();
                if (playerDamageOutput < 0)
                    playerDamageOutput = 0;
                OutputToBattle(String.Format(skill.GetActionMessage(), playerDamageOutput));
                Engine.player.ChangeStamina(-sSkill.GetAttributeCost());
                break;
            case Skill.SkillType.mana:
                ManaSkill mSkill = ((ManaSkill)skill);
                if (mSkill.GetMagicType() == ManaSkill.MagicType.heal)
                {
                    playerDamageOutput = 0;
                    if (!CheckIfBleeding(true))
                    {
                        int temp = (int)((mSkill.GetDamageModifier()));
                        Engine.player.ChangeHealth(temp);
                        OutputToBattle(String.Format(skill.GetActionMessage(), temp));
                    }
                    else
                        OutputToBattle(String.Format("{0} attempted to heal, but they are bleeding!", Engine.player.GetName()));
                }
                if (mSkill.GetMagicType() == ManaSkill.MagicType.dispel)
                {
                    playerDamageOutput = 0;
                    int x = mSkill.GetDamageModifier();
                    for (int i = 0; i < x; i++)
                    {
                        Engine.player.DecrementStatusEffectTurn();
                        foreach (GameObject sGO in playerStatusEffectSlots.ToList<GameObject>())
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
                if (mSkill.GetMagicType() == ManaSkill.MagicType.damage)
                {
                    playerDamageOutput = (int)((mSkill.GetDamageModifier() - enemyDefValue));
                    if (playerDamageOutput < 0)
                        playerDamageOutput = 0;
                    OutputToBattle(String.Format(skill.GetActionMessage(), playerDamageOutput));
                }
                Engine.player.ChangeMana(-mSkill.GetAttributeCost());
                break;
        }

        if (skill.HasStatusEffects())
        {
            List<StatusEffect> statusEffects = skill.GetStatusEffects();
            foreach (StatusEffect statusEffect in statusEffects)
            {
                float hitChance = UnityEngine.Random.Range(0, 1.0f);

                if (hitChance <= statusEffect.GetHitChance())
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
            Engine.player.AddStatusEffect(statusEffect);

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

    public void Defend(bool isPlayer)
    {
        if(isPlayer)
        {
            OutputToBattle("You enter a defensive stance.");
            playerDefending = true;
            playerHasMoved = true;          
        }
        else
        {
            enemyDefending = true;
        }
    }

    public void Rest(bool isPlayer)
    {
        if (isPlayer)
        {
            OutputToBattle("You rest for a moment.");
            Engine.player.RegenAttributes();
            playerHasMoved = true;
        }
        else
        {
            OutputToBattle("The {0} rests for a moment.");
            activeEnemy.RegenAttributes();
        }
    }

    void EnemyAttack(Enemy enemy)
    {
        enemyDefending = false;

        if (enemy.CheckShouldRest())
        {
            Rest(false);
            enemyDamageOutput = 0;
            OutputToBattle(String.Format("{0} rested.", enemy.GetName()));
        }

        EnemyAttackType attack = enemy.GetAttack();

        int playerDefValue = Engine.player.GetDefense();

        foreach (StatusEffect statusEffect in Engine.player.GetStatusEffects())
        {
            if (statusEffect.GetStatusEffectType() == StatusEffect.StatusEffectType.defence)
            {
                if (statusEffect.IsPercentage())
                    playerDefValue = (int)(playerDefValue * statusEffect.GetStatChange());
                else
                    playerDefValue = (int)(playerDefValue + statusEffect.GetStatChange());
            }
        }

        if (playerDefending)
            playerDefValue = (int)(playerDefValue * 0.5f);
        else
            playerDefValue = (int)(playerDefValue * 0.2f);
    

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

                    if (!CheckIfBleeding(false))
                    {
                        int enemyHealRate = attack.GetDamageModifier();
                        enemy.ChangeHealth(enemyHealRate);
                        OutputToBattle(String.Format(attack.GetDescription(), enemy.GetName(), enemyHealRate));
                    }
                    else
                        OutputToBattle(String.Format("{0} attempted to heal, but is bleeding!", enemy.GetName()));
                    enemy.ChangeMana(-attack.GetAttributeCost());
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
                    enemy.ChangeMana(-attack.GetAttributeCost());
                    OutputToBattle(String.Format(attack.GetDescription(), enemy.GetName(), enemyDamageOutput));
                }
            }
            else if (attack.GetAttackAttribute() == EnemyAttackType.AttackAttribute.stamina)
            {
                if (attack.GetAttackType() == EnemyAttackType.AttackType.heal)
                {
                    enemyDamageOutput = 0;

                    if (!CheckIfBleeding(false))
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

        if (attack.HasStatusEffects())
        {
            List<StatusEffect> statusEffects = attack.GetStatusEffects();
            foreach (StatusEffect statusEffect in statusEffects)
            {
                if (statusEffect != null)
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
                else
                    Debug.LogError(String.Format("Null Status Effect in Enemy Attack {0}", attack.GetName()));
            }
        }

        if (attack.GetAudioClip() != null)
        {
            GameObject.Find("MonsterAudioSource").GetComponent<AudioSource>().clip = attack.GetAudioClip();
            GameObject.Find("MonsterAudioSource").GetComponent<AudioSource>().Play();
        }
    }


    public void ClearPlayerStatusEffectObjects()
    {
        foreach (GameObject sGO in playerStatusEffectSlots.ToList<GameObject>())
        {
            GameObject.Destroy(sGO);
            playerStatusEffectSlots.Remove(sGO);
        }
    }

    public void OutputToBattle(string output)
    {
        battleOutputTxt.text += output + "\n";
        battleOutputTxt.text += "--------------------\n";
    }



}
