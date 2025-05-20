using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatisticManager : MonoBehaviour
{
    public class EnemyKillInfo
    {
        public int enemyKills;
        public float enemyCoinReward;

        public EnemyKillInfo(EnemyType enemy)
        {
            enemyKills = 0;
            enemyCoinReward = GetCoinReward(enemy);
        }

        private float GetCoinReward(EnemyType enemy)
        {
            switch (enemy)
            {
                case EnemyType.FlyingEye: return 1f;
                case EnemyType.Goblin: return 1.5f;
                case EnemyType.Skeleton: return 2f;
                case EnemyType.Mushroom: return 2.5f;

                default: return 1f; 
            }
        }
    }

    public static StatisticManager Instance;

    private Dictionary<EnemyType, EnemyKillInfo> enemyKillsCount;
    private Dictionary<EnemyType, (Image icon, TextMeshProUGUI name, TextMeshProUGUI killsAmountText)> enemyUIBindings;

    [Header("Statistic")]
    [Header("Flying Eye Panel")]
    [SerializeField] private Image flyingEyeIcon;
    [SerializeField] private TextMeshProUGUI flyingEyeName;
    [SerializeField] private TextMeshProUGUI flyingEyeKillsAmount;

    [Header("Goblin Panel")]
    [SerializeField] private Image goblinIcon;
    [SerializeField] private TextMeshProUGUI goblinName;
    [SerializeField] private TextMeshProUGUI goblinKillsAmount;

    [Header("Skeleton Panel")]
    [SerializeField] private Image skeletonIcon;
    [SerializeField] private TextMeshProUGUI skeletonName;
    [SerializeField] private TextMeshProUGUI skeletonKillsAmount;

    [Header("Mushroom Panel")]
    [SerializeField] private Image mushroomIcon;
    [SerializeField] private TextMeshProUGUI mushroomName;
    [SerializeField] private TextMeshProUGUI mushroomKillsAmount;

    [Header("General Statistic")]
    [SerializeField] private TextMeshProUGUI totalCoinsEarned;
    [SerializeField] private GameObject statisticPanel;
    private PlayerProgress progress;

    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        enemyUIBindings = new Dictionary<EnemyType, (Image icon, TextMeshProUGUI name, TextMeshProUGUI killsAmountText)>
        {
            {EnemyType.FlyingEye, (flyingEyeIcon,flyingEyeName, flyingEyeKillsAmount)},
            {EnemyType.Goblin, (goblinIcon,goblinName, goblinKillsAmount)},
            {EnemyType.Skeleton, (skeletonIcon,skeletonName, skeletonKillsAmount)},
            {EnemyType.Mushroom, (mushroomIcon,mushroomName, mushroomKillsAmount)}
        };

        enemyKillsCount = new Dictionary<EnemyType, EnemyKillInfo>();

        foreach(EnemyType enemy in System.Enum.GetValues(typeof(EnemyType)))
        {
            EnemyKillInfo enemyKillInfo = new EnemyKillInfo(enemy);
            enemyKillsCount[enemy] = enemyKillInfo;
        }
    }

    private void SetStatistic(int killedEnemies, Image enemyIcon, TextMeshProUGUI enemyName, TextMeshProUGUI enemyKillsAmount)
    {
        if (killedEnemies > 0)
        {
            enemyKillsAmount.text = "x " + killedEnemies;
        }
        else
        {
            enemyIcon.color = Color.black;
            enemyName.text = "???";
            enemyKillsAmount.text = "x 0";
        }
    }

    private int SummaryEarned(int killedAmount, float coinsPerKill)
    {
        return Mathf.FloorToInt(coinsPerKill * killedAmount);
    }

    public void OpenStatisticPanel()
    {
        int summaryCoins = 0;

        foreach(EnemyKillInfo enemyKillInfo in enemyKillsCount.Values)
        {
            summaryCoins += SummaryEarned(enemyKillInfo.enemyKills, enemyKillInfo.enemyCoinReward);
        }

        totalCoinsEarned.text = summaryCoins.ToString();

        string json = System.IO.File.ReadAllText(GameData.Instance.saveFilePath);
        progress = JsonUtility.FromJson<PlayerProgress>(json);
        progress.coins += summaryCoins;
        json = JsonUtility.ToJson(progress);
        System.IO.File.WriteAllText(GameData.Instance.saveFilePath, json);


        foreach(var keyValue in enemyKillsCount)
        {
            var enemyType = keyValue.Key;
            EnemyKillInfo enemyKillInfo = keyValue.Value;
            var ui = enemyUIBindings[enemyType];

            SetStatistic(enemyKillInfo.enemyKills, ui.icon, ui.name, ui.killsAmountText);
        }


        statisticPanel.SetActive(true);
    }

    public void AddEnemyKills(EnemyType enemy, int amount)
    {
        enemyKillsCount[enemy].enemyKills += amount;
    }

}
