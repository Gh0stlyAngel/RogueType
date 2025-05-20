using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip upgradeSound;


    private string currentVersion;
    [SerializeField] private List<UpgradeConfig> upgradeConfigs;

    private Dictionary<string, UpgradeConfig> generalUpgradeDictionary;

    [Header("General Stats")]
    [SerializeField] private List<Slider> progressBars;
    [SerializeField] private List<TextMeshProUGUI> progressCostTexts;
    [SerializeField] private List<GameObject> maxLevelTexts;
    [SerializeField] private List<Button> levelUpButtons;

    [Header("PersonalStats")]
    [SerializeField] private List<UpgradeConfig> warriorUpgradeConfigs;
    private Dictionary<string, UpgradeConfig> warriorUpgradeDictionary;

    [SerializeField] private List<UpgradeConfig> reaperUpgradeConfigs;
    private Dictionary<string, UpgradeConfig> reaperUpgradeDictionary;

    [SerializeField] private List<UpgradeConfig> knightUpgradeConfigs;
    private Dictionary<string, UpgradeConfig> knightUpgradeDictionary;


    [SerializeField] private List<GameObject>personalPanels;
    


    private PlayerProgress progress;
    [SerializeField] private TextMeshProUGUI totalCoins;
    private string saveFilePath;
 
    private void Awake()
    {
        saveFilePath = Application.persistentDataPath + "/PlayerProgress.json";
        currentVersion = "1.0";

        generalUpgradeDictionary = FillUpgradeConfigDictionary(upgradeConfigs);
        warriorUpgradeDictionary = FillUpgradeConfigDictionary(warriorUpgradeConfigs);
        reaperUpgradeDictionary = FillUpgradeConfigDictionary(reaperUpgradeConfigs);
        knightUpgradeDictionary = FillUpgradeConfigDictionary(knightUpgradeConfigs);


        try
        {
            progress = ReadJSON();
            if (progress.version != currentVersion)
            {
                Debug.LogWarning("Different versions detected");
                progress = CreateNewJSON();
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning("Ошибка загрузки прогресса:" + e);
            progress = CreateNewJSON();
        }
    }

    private void Start()
    {
        RefreshAll();
        audioSource = FindObjectOfType<Canvas>()?.GetComponent<AudioSource>();
    }

    private void RefreshAll()
    {
        foreach (Upgrade upgrade in progress.upgrades)
        {
            RefreshUpgradeInfo(upgrade);
        }

        foreach (Upgrade upgrade in progress.warriorUpgrades)
        {
            RefreshPersonalUpgradeInfo(upgrade, personalPanels[0], warriorUpgradeConfigs, warriorUpgradeDictionary, ref GameData.Instance.WarriorBonusStats, progress.warriorUpgrades);
        }

        foreach (Upgrade upgrade in progress.reaperUpgrades)
        {
            RefreshPersonalUpgradeInfo(upgrade, personalPanels[1], reaperUpgradeConfigs, reaperUpgradeDictionary, ref GameData.Instance.ReaperBonusStats, progress.reaperUpgrades);
        }

        foreach (Upgrade upgrade in progress.knightUpgrades)
        {
            RefreshPersonalUpgradeInfo(upgrade, personalPanels[2], knightUpgradeConfigs, knightUpgradeDictionary, ref GameData.Instance.KnightBonusStats, progress.knightUpgrades);
        }

        totalCoins.text = progress.coins.ToString();
    }

    private PlayerProgress ReadJSON()
    {
        string json = File.ReadAllText(saveFilePath);
        Debug.Log(json);
        return JsonUtility.FromJson<PlayerProgress>(json);
    }

    private PlayerProgress CreateNewJSON()
    {
        var progress = new PlayerProgress();

        progress.upgrades = CreateUpgradesList(upgradeConfigs);
        progress.warriorUpgrades = CreateUpgradesList(warriorUpgradeConfigs);
        progress.reaperUpgrades = CreateUpgradesList(reaperUpgradeConfigs);
        progress.knightUpgrades = CreateUpgradesList(knightUpgradeConfigs);

        progress.coins = 0;
        progress.version = currentVersion;
        string json = JsonUtility.ToJson(progress);
        File.WriteAllText(saveFilePath, json);

        return progress;
    }

    private Dictionary<string, UpgradeConfig> FillUpgradeConfigDictionary(List<UpgradeConfig> configsList)
    {

        Dictionary<string, UpgradeConfig> upgradeDictionary = new Dictionary<string, UpgradeConfig>();

        foreach (UpgradeConfig upgradeConfig in configsList)
        {
            upgradeDictionary[upgradeConfig.id] = upgradeConfig;
        }

        return upgradeDictionary;
    }

    private List<Upgrade> CreateUpgradesList(List<UpgradeConfig> configs)
    {
        List<Upgrade> upgrades = new List<Upgrade>();

        foreach (UpgradeConfig upgradeConfig in configs)
        {
            Upgrade upgrade = new Upgrade();
            upgrade.currentLevel = 0;
            upgrade.id = upgradeConfig.id;
            upgrades.Add(upgrade);
        }

        return upgrades;
    }

    private void RefreshUpgradeInfo(Upgrade upgrade)
    {
        int currentID = Convert.ToInt32(upgrade.id);
        progressBars[currentID].value = upgrade.currentLevel;
        if (progressBars[currentID].value >= upgradeConfigs[currentID].maxLevel)
        {
            //disable button
            levelUpButtons[currentID].interactable = false;
            ButtonsData.Instance.EnableMask(levelUpButtons[currentID]);

            //disable cost
            progressCostTexts[currentID].gameObject.SetActive(false);

            //enable "Max Level" text
            maxLevelTexts[currentID].SetActive(true);
        }
        else
        {
            progressCostTexts[currentID].text = GetUpgradeCost(upgrade.id, upgrade.currentLevel, generalUpgradeDictionary).ToString();
        }

        float summaryStatBonus = 0;

        for (int i = 0; i < progress.upgrades[currentID].currentLevel; i++)
        {
            summaryStatBonus += GetUpgradeValue(currentID.ToString(), i, generalUpgradeDictionary);
        }

        GameData.Instance.GeneralBonusStats[currentID] = summaryStatBonus;
    }

    public int GetUpgradeCost(string id, int level, Dictionary<string, UpgradeConfig> dictionary)
    {
        return dictionary[id].perLevelCost[level];
    }

    public float GetUpgradeValue(string id, int level, Dictionary<string, UpgradeConfig> dictionary)
    {
        return dictionary[id].perLevelBonus[level];
    }

    public int GetPersonalUpgradeCost()
    {
        return 1;
    }

    public int GetPersonalUpgradeValue()
    {
        return 1;
    }

    public void UpgradeGeneralStat(int id)
    {
        int currentUpgradeLevel = progress.upgrades[id].currentLevel;
        int currentLevelCost = upgradeConfigs[id].perLevelCost[currentUpgradeLevel];
        
        if (currentLevelCost > progress.coins)
        {

        }
        else
        {
            audioSource.PlayOneShot(upgradeSound, .1f);
            progress.upgrades[id].currentLevel += 1;
            progressBars[id].value = progress.upgrades[id].currentLevel;
            RefreshUpgradeInfo(progress.upgrades[id]);

            progress.coins -= currentLevelCost;
            totalCoins.text = progress.coins.ToString();

            string json = JsonUtility.ToJson(progress);
            File.WriteAllText(saveFilePath, json);
            
        }
        
    }

    private void RefreshPersonalUpgradeInfo(Upgrade upgrade, GameObject personalPanel, List<UpgradeConfig> charUpgradeConfigs,
        Dictionary<string, UpgradeConfig> characterUpgradeDictionary, ref List<float> charSummaryStatBonus, List<Upgrade> characterUpgrades)
    {

        int currentID = Convert.ToInt32(upgrade.id);

        GameObject personalStatPanel = personalPanel.transform.GetChild(currentID).gameObject;
        


        Slider progressBar;
        float progressBarValue = 0;
        foreach (Transform item in personalStatPanel.transform)
        {
            if(item.gameObject.TryGetComponent(out Slider slider))
            {
                progressBar = slider;
                progressBar.value = upgrade.currentLevel;
                progressBarValue = progressBar.value;
                break;
            }
        }

        if (progressBarValue >= charUpgradeConfigs[currentID].maxLevel)
        {
            Button levelUpButton;
            GameObject costText;
            GameObject maxLevelText;
            foreach (Transform item in personalStatPanel.transform)
            {
                
                if (item.gameObject.name == "UpgradeBlock")
                {
                    foreach (Transform i in item.transform)
                    {
                        if(i.gameObject.name == "UpgradeButton")
                        {
                            levelUpButton = i.GetComponent<Button>();
                            levelUpButton.interactable = false;
                            ButtonsData.Instance.EnableMask(levelUpButton);
                            continue;
                        }
                        if (i.gameObject.name == "UpgradeCost")
                        {
                            costText = i.gameObject;
                            costText.SetActive(false);
                            continue;
                        }
                        if (i.gameObject.name == "MaxLevel")
                        {
                            maxLevelText = i.gameObject;
                            maxLevelText.SetActive(true);
                            continue;
                        }
                    }
                    break;
                }
            }

        }
        else
        {
            GameObject costText;
            foreach (Transform item in personalStatPanel.transform)
            {
                if (item.gameObject.name == "UpgradeBlock")
                {
                    foreach (Transform i in item.transform)
                    {

                        if (i.gameObject.name == "UpgradeCost")
                        {
                            costText = i.gameObject;
                            costText.GetComponent<TextMeshProUGUI>().text = GetUpgradeCost(currentID.ToString(), upgrade.currentLevel, characterUpgradeDictionary).ToString();
                            break;
                        }

                    }
                    break;
                }
            }
        }

        float summaryStatBonus = 0;

        for (int i = 0; i < characterUpgrades[currentID].currentLevel; i++)
        {
            summaryStatBonus += GetUpgradeValue(currentID.ToString(), i, characterUpgradeDictionary);
        }

        charSummaryStatBonus[currentID] = summaryStatBonus;
    }

    private void UpgradeCharcterPersonalStart(int id, List<Upgrade> characterUpgrades, List<UpgradeConfig> upgradeConfigs, Dictionary<string, UpgradeConfig> characterUpgradeDictionary, int personalPanelID, ref List<float>characterBonusStats)
    {
        int currentUpgradeLevel = characterUpgrades[id].currentLevel;
        int currentLevelCost = upgradeConfigs[id].perLevelCost[currentUpgradeLevel];

        if (currentLevelCost > progress.coins)
        {

        }
        else
        {
            audioSource.PlayOneShot(upgradeSound, .1f);
            characterUpgrades[id].currentLevel += 1;
            progressBars[id].value = characterUpgrades[id].currentLevel;
            RefreshPersonalUpgradeInfo(characterUpgrades[id], personalPanels[personalPanelID], upgradeConfigs, characterUpgradeDictionary, ref characterBonusStats, characterUpgrades);

            progress.coins -= currentLevelCost;
            totalCoins.text = progress.coins.ToString();


            string json = JsonUtility.ToJson(progress);
            File.WriteAllText(saveFilePath, json);
        }
    }

    public void UpgradeWarriorPersonalStat(int id)
    {
        UpgradeCharcterPersonalStart(id, progress.warriorUpgrades, warriorUpgradeConfigs, warriorUpgradeDictionary ,0, ref GameData.Instance.WarriorBonusStats);
    }

    public void UpgradeReaperPersonalStat(int id)
    {
        UpgradeCharcterPersonalStart(id, progress.reaperUpgrades, reaperUpgradeConfigs, reaperUpgradeDictionary, 1, ref GameData.Instance.WarriorBonusStats);
    }

    public void UpgradeKnightPersonalStat(int id)
    {
        UpgradeCharcterPersonalStart(id, progress.knightUpgrades, knightUpgradeConfigs, knightUpgradeDictionary, 2, ref GameData.Instance.WarriorBonusStats);
    }

    


}

