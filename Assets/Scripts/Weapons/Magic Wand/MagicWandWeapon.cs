using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class MagicWand : BaseWeapon
{
    [SerializeField] private int amount;
    [SerializeField] private int maxHits;
    [SerializeField] private float speed;
    [SerializeField] private GameObject magicWandProjectile;
    private SpawnManager spawnManager;
    [SerializeField] private List<GameObject> enemies;
    protected override void ItemStart()
    {
        base.ItemStart();
    }

    public override void InitItem()
    {
        maxLevel = 8;
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        StartWeapon();
    }

    protected override void WeaponUpdate()
    {
        base.WeaponUpdate();
    }

    IEnumerator Attack()
    {
        while (true)
        {
            Vector3 target = GetClosestEnemy();
            if (target != null)
            {
                for (int i = 0; i < amount; i++)
                {
                    GameObject newProjectile = Instantiate(magicWandProjectile, transform.position, magicWandProjectile.transform.rotation);
                    target = GetClosestEnemy();
                    newProjectile.GetComponent<WagicWandProjectile>().Init(damage, pushOutDuration, speed, maxHits, spawnManager, target);
                    SFXManager.instance.PlaySoundFXClip(attackSound, transform, 0.03f);
                    yield return new WaitForSeconds(0.12f);
                }
                yield return new WaitForSeconds(attackDelay);
            }
            else
            {
                yield return new WaitForSeconds(0.1f);
            }

            

        }

    }

    private Vector3 GetClosestEnemy()
    {
        Vector3 closestObject;
        enemies = spawnManager.GetEnemyList();


        float minDistance = Mathf.Infinity;
        closestObject = new Vector3(0,1,0);

        foreach (GameObject enemy in enemies)
        {
            Vector3 targetPosition = enemy.transform.position;
            float distance = (targetPosition - transform.position).sqrMagnitude;
            if (distance < minDistance)
            {
                minDistance = distance;
                closestObject = targetPosition;
            }
        }
        return closestObject;
    }

    private void StartWeapon()
    {
        StartCoroutine(Attack());
    }

    public override void SetStats(int level)
    {
        base.SetStats(level);
        switch (level)
        {
            case 1:
            case 0:
                CurrentLevel = 1;

                pushOutDuration = 0.1f;
                damage = 10;
                maxHits = 1;
                amount = 1;
                attackDelay = 1.5f;
                break;
            case 2:
                CurrentLevel = 2;

                pushOutDuration = 0.1f;
                damage = 10;
                maxHits = 1;
                amount = 2;
                attackDelay = 1.5f;
                break;
            case 3:
                CurrentLevel = 3;

                pushOutDuration = 0.1f;
                damage = 10;
                maxHits = 1;
                amount = 2;
                attackDelay = 1.2f;
                break;
            case 4:
                CurrentLevel = 4;

                pushOutDuration = 0.1f;
                damage = 10;
                maxHits = 1;
                amount = 3;
                attackDelay = 1.2f;
                break;
            case 5:
                CurrentLevel = 5;

                pushOutDuration = 0.1f;
                damage = 20;
                maxHits = 1;
                amount = 3;
                attackDelay = 1.2f;
                break;
            case 6:
                CurrentLevel = 6;

                pushOutDuration = 0.1f;
                damage = 20;
                maxHits = 1;
                amount = 4;
                attackDelay = 1.2f;
                break;
            case 7:
                CurrentLevel = 7;

                pushOutDuration = 0.1f;
                damage = 20;
                maxHits = 2;
                amount = 4;
                attackDelay = 1.2f;
                break;
            case 8:
                CurrentLevel = 8;

                pushOutDuration = 0.1f;
                damage = 30;
                maxHits = 2;
                amount = 4;
                attackDelay = 1.2f;
                AtMaxLevel = true;
                break;
        }

        amount += baseCharacter.AmountTotal;
        damage *= baseCharacter.StrenghtTotal;
        attackDelay *= baseCharacter.CooldownTotal;

        if (attackDelay < 0.4)
        {
            attackDelay = 0.4f;
        }
    }

    public override string GetLevelDescription(int level)
    {
        switch (level)
        {
            case 0:
            case 1:
                return "Fires at the nearest enemy.";
            case 2:
                return "Fires 1 more projectile.";
            case 3:
                return "Cooldown reduced by 0.3 seconds.";
            case 4:
                return "Fires 1 more projectile.";
            case 5:
                return "Base Damage up by 10.";
            case 6:
                return "Fires 1 more projectile.";
            case 7:
                return "Passes through 1 more enemy.";
            case 8:
                return "Base Damage up by 10.";
            default:
                return "";
        }
    }
}
