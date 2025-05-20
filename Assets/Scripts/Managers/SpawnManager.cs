using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameTimerManager gameTimerManager;

    [SerializeField] private GameObject player;
    [SerializeField] private List<GameObject> destroyableWeapons = new List<GameObject>();

    public Queue<GameObject> Radiants = new Queue<GameObject>();
    private int radiantsMaxLength;
    private int radiantsSpawnDelay;
    [SerializeField] private GameObject radiantPrefab;

    [SerializeField] private GameObject flyingEye;
    [SerializeField] private GameObject goblin;
    [SerializeField] private GameObject skeleton;
    [SerializeField] private GameObject mushroom;

    [SerializeField] private GameObject flyingEyeElite;
    [SerializeField] private GameObject goblinElite;
    [SerializeField] private GameObject skeletonElite;
    [SerializeField] private GameObject mushroomElite;

    [SerializeField] private GameObject flyingEyeBoss;
    [SerializeField] private GameObject goblinBoss;
    [SerializeField] private GameObject skeletonBoss;
    [SerializeField] private GameObject mushroomBoss;

    public List<GameObject> Enemies; 
    [SerializeField] private List<GameObject> stageEnemies;

    private bool oppositeSpawn;

    [SerializeField] private float currentSpawnDelay;
    [SerializeField] private int currentMinEnemies;

    [Header("Stage 1")]
    [SerializeField] private float stageOneSpawnDelay;
    [SerializeField] private int stageOneMinEnemies;

    [Header("Stage 2")]
    [SerializeField] private float stageTwoSpawnDelay;
    [SerializeField] private int stageTwoMinEnemies;

    [Header("Stage 3")]
    [SerializeField] private float stageThreeSpawnDelay;
    [SerializeField] private int stageThreeMinEnemies;

    [Header("Stage 4")]
    [SerializeField] private float stageFourSpawnDelay;
    [SerializeField] private int stageFourMinEnemies;

    [Header("Stage 5")]
    [SerializeField] private float stageFiveSpawnDelay;
    [SerializeField] private int stageFiveMinEnemies;

    [Header("Stage 6")]
    [SerializeField] private float stageSixSpawnDelay;
    [SerializeField] private int stageSixMinEnemies;

    [Header("Stage 7")]
    [SerializeField] private float stageSevenSpawnDelay;
    [SerializeField] private int stageSevenMinEnemies;

    [Header("Stage 8")]
    [SerializeField] private float stageEightSpawnDelay;
    [SerializeField] private int stageEightMinEnemies;

    [Header("Stage 9")]
    [SerializeField] private float stageNineSpawnDelay;
    [SerializeField] private int stageNineMinEnemies;

    [Header("Stage 10")]
    [SerializeField] private float stageTenSpawnDelay;
    [SerializeField] private int stageTenMinEnemies;

    [Header("Stage 11")]
    [SerializeField] private float stageElevenSpawnDelay;
    [SerializeField] private int stageElevenMinEnemies;

    [Header("Stage 12")]
    [SerializeField] private float stageTwelveSpawnDelay;
    [SerializeField] private int stageTwelveMinEnemies;

    [Header("Stage 13")]
    [SerializeField] private float stageThirteenSpawnDelay;
    [SerializeField] private int stageThirteenMinEnemies;

    [Header("Stage 14")]
    [SerializeField] private float stageFourteenSpawnDelay;
    [SerializeField] private int stageFourteenMinEnemies;

    [Header("Stage 15")]
    [SerializeField] private float stageFifteenSpawnDelay;
    [SerializeField] private int stageFifteenMinEnemies;

    [Header("Stage 15")]
    [SerializeField] private float stageSixteenSpawnDelay;
    [SerializeField] private int stageSixteenMinEnemies;


    private List<Transform> spawners = new List<Transform>();

    public void Init(GameObject player)
    {
        this.player = player;
        Enemies = new List<GameObject>();
        oppositeSpawn = false;
        foreach (Transform spawner in transform)
        {
            spawners.Add(spawner);
        }

        radiantsMaxLength = 8;
        radiantsSpawnDelay = 20;

        StartCoroutine(SpawnScenario());
        StartCoroutine(SpawnRadiants());
    }

    private void FixedUpdate()
    {
        transform.Translate(player.transform.position - transform.position);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<IDamagable>(out var damagable))
        {
            if (oppositeSpawn)
            {
                Vector3 distance = (collision.gameObject.transform.position - transform.position);
                Vector3 newPosition = collision.gameObject.transform.position - (distance * 2);
                collision.gameObject.transform.position = newPosition;
            }
            else
            {
                collision.gameObject.transform.position = GetRandomPosition();
            }
        }
    }

    private Vector2 GetRandomPosition()
    {
        return spawners[UnityEngine.Random.Range(0, spawners.Count)].position;
    }

    public void RemoveEnemyFromList(GameObject enemy)
    {
        Enemies.Remove(enemy);
    }

    public List<GameObject> GetEnemyList()
    {
        return Enemies;
    }

    IEnumerator SpawnRadiants()
    {
        while (true)
        {
            yield return new WaitForSeconds(radiantsSpawnDelay);
            if (Radiants.Count >= radiantsMaxLength)
            {
                Destroy(Radiants.Dequeue());
            }
            Radiants.Enqueue(Instantiate(radiantPrefab, GetRandomPosition(), radiantPrefab.transform.rotation));
            
        }
        
    }

    IEnumerator SpawnScenario()
    {
        while (gameTimerManager.CurrentMinute < 1)
        {
            yield return new WaitForSeconds(stageOneSpawnDelay);
            SpawnEnemies(stageOneMinEnemies, flyingEye);
        }

        SpawnEnemies(0, flyingEyeElite);

        while (gameTimerManager.CurrentMinute < 2)
        {
            yield return new WaitForSeconds(stageTwoSpawnDelay);
            SpawnEnemies(stageTwoMinEnemies, flyingEye);
        }

        bool strongerEnemy = true;
        while (gameTimerManager.CurrentMinute < 3)
        {
            yield return new WaitForSeconds(stageThreeSpawnDelay);

            GameObject enemyPrefab = strongerEnemy ? goblin : flyingEye;
            SpawnEnemies(stageThreeMinEnemies, enemyPrefab);

            strongerEnemy = !strongerEnemy;
        }

        while (gameTimerManager.CurrentMinute < 4)
        {
            yield return new WaitForSeconds(stageFourSpawnDelay);
            SpawnEnemies(stageFourMinEnemies, goblin);
        }

        SpawnEnemies(0, goblinElite);

        while (gameTimerManager.CurrentMinute < 5)
        {
            yield return new WaitForSeconds(stageFiveSpawnDelay);
            SpawnEnemies(stageFiveMinEnemies, goblin);
        }

        for (int i = 0; i < 3; i++)
        {
            SpawnEnemies(0, goblinElite);
        }
        
        strongerEnemy = true;
        while (gameTimerManager.CurrentMinute < 6)
        {
            yield return new WaitForSeconds(stageSixSpawnDelay);

            GameObject enemyPrefab = strongerEnemy ? goblin : flyingEye;
            SpawnEnemies(stageSixMinEnemies, enemyPrefab);

            strongerEnemy = !strongerEnemy;
        }

        for (int i = 0; i < 5; i++)
        {
            SpawnEnemies(0, goblinElite);
        }

        while (gameTimerManager.CurrentMinute < 7)
        {
            yield return new WaitForSeconds(stageSevenSpawnDelay);
            SpawnEnemies(stageSevenMinEnemies, goblin);
        }

        strongerEnemy = true;
        while (gameTimerManager.CurrentMinute < 8)
        {
            yield return new WaitForSeconds(stageEightSpawnDelay);

            GameObject enemyPrefab = strongerEnemy ? skeleton : goblin;
            SpawnEnemies(stageEightMinEnemies, enemyPrefab);

            strongerEnemy = !strongerEnemy;
        }

        for (int i = 0; i < 3; i++)
        {
            SpawnEnemies(0, skeletonElite);
        }

        while (gameTimerManager.CurrentMinute < 9)
        {
            yield return new WaitForSeconds(stageNineSpawnDelay);
            SpawnEnemies(stageNineMinEnemies, skeleton);
        }

        strongerEnemy = true;
        while (gameTimerManager.CurrentMinute < 10)
        {
            yield return new WaitForSeconds(stageTenSpawnDelay);

            GameObject enemyPrefab = strongerEnemy ? mushroom : skeleton;
            SpawnEnemies(stageTenMinEnemies, enemyPrefab);

            strongerEnemy = !strongerEnemy;
        }

        SpawnEnemies(0, mushroomElite);

        while (gameTimerManager.CurrentMinute < 11)
        {
            yield return new WaitForSeconds(stageElevenSpawnDelay);
            SpawnEnemies(stageElevenMinEnemies, mushroom);
        }

        for (int i = 0; i < 3; i++)
        {
            SpawnEnemies(0, mushroomElite);
        }

        while (gameTimerManager.CurrentMinute < 12)
        {
            yield return new WaitForSeconds(stageTwelveSpawnDelay);
            SpawnEnemies(stageTwelveMinEnemies, mushroom);
        }

        strongerEnemy = true;
        while (gameTimerManager.CurrentMinute < 13)
        {
            yield return new WaitForSeconds(stageThirteenSpawnDelay);

            GameObject enemyPrefab = strongerEnemy ? flyingEyeElite : mushroom;
            SpawnEnemies(stageThirteenMinEnemies, enemyPrefab);

            strongerEnemy = !strongerEnemy;
        }

        while (gameTimerManager.CurrentMinute < 14)
        {
            yield return new WaitForSeconds(stageFourteenSpawnDelay);
            SpawnEnemies(stageFourteenMinEnemies, flyingEyeElite);
        }

        List<GameObject> randomElites = new List<GameObject>
        {
            flyingEyeElite,
            goblinElite,
            skeletonElite,
            mushroomElite
        };

        List<GameObject> randomBosses = new List<GameObject>
        {
            flyingEyeBoss,
            goblinBoss,
            skeletonBoss,
            mushroomBoss
        };

        while (gameTimerManager.CurrentMinute < 15)
        {
            yield return new WaitForSeconds(stageFifteenSpawnDelay);

            GameObject randomEnemy = GetWaveEliteEnemyPrefab(UnityEngine.Random.Range(0, randomElites.Count));

            SpawnEnemies(stageFifteenMinEnemies, randomEnemy);
        }

        int bossIndex = UnityEngine.Random.Range(0, randomBosses.Count);
        GameObject waveEnemy = GetWaveEliteEnemyPrefab(bossIndex);
        BaseEnemy boss = SpawnEnemies(0, randomBosses[bossIndex]).GetComponent<BaseEnemy>();

        

        while (gameTimerManager.CurrentMinute < 999)
        {
            yield return new WaitForSeconds(stageSixteenSpawnDelay);
            SpawnEnemies(stageSixteenMinEnemies, waveEnemy);
            if(boss == null || !boss.alive)
            {
                Debug.Log("boss is dead.");
                break;
            }
        }

        while (true)
        {
            if(Enemies.Count > 0)
            {
                yield return new WaitForSeconds(stageSixteenSpawnDelay);
            }
            else
            {
                break;
            }
        }

        GameManager.Instance.Victory();

    }

    private GameObject SpawnEnemies(int minEnemies, GameObject enemyPrefab)
    {
        GameObject enemyObject;
        if (Enemies.Count < minEnemies)
        {
            int enemiesToSpawn = minEnemies - Enemies.Count;
            
            for (int i = 0; i < enemiesToSpawn; i++)
            {
                Enemies.Add(Instantiate(enemyPrefab, GetRandomPosition(), enemyPrefab.transform.rotation));
            }
            enemyObject = null;
        }

        else
        {
            enemyObject = Instantiate(enemyPrefab, GetRandomPosition(), enemyPrefab.transform.rotation);
            Enemies.Add(enemyObject);
        }

        return enemyObject;
    }

    private GameObject GetWaveEliteEnemyPrefab(int bossIndex)
    {
        switch (bossIndex)
        {
            case 0: return flyingEyeElite;

            case 1: return goblinElite;

            case 2: return skeletonElite;

            case 3: return mushroomElite;

            default: return flyingEyeElite;
        }
    }
}
