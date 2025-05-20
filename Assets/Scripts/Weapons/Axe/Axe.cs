using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Axe : BaseWeapon
{
    [SerializeField] private GameObject axe;

    [SerializeField] private GameObject axeWeaponPrefab;

    [SerializeField] private float maxHits;
    [SerializeField] private int amount;
    [SerializeField] private int direction;
    protected override void ItemStart()
    {
        base.ItemStart();
    }

    public override void InitItem()
    {
        maxLevel = 8;
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
            
            for (int i = 0; i < amount; i++)
            {
                GameObject newAxe = Instantiate(axe, transform.position, axe.transform.rotation);
                if(direction == 0)
                    newAxe.GetComponent<AxeParameters>().Init(damage, maxHits, pushOutDuration, GetRandomDirection());
                else
                    newAxe.GetComponent<AxeParameters>().Init(damage, maxHits, pushOutDuration, ChangeDirection());
                SFXManager.instance.PlaySoundFXClip(attackSound, transform, 0.1f);
                yield return new WaitForSeconds(0.1f);
            }
            direction = 0;
            yield return new WaitForSeconds(attackDelay);
        }
        
    }

    private void StartWeapon()
    {
        StartCoroutine(Attack());
    }

    public override void SetStats(int level)
    {
        base.SetStats(level);
        attackDelay = 3;
        switch (level)
        {
            case 1:
            case 0:
                CurrentLevel = 1;

                pushOutDuration = 0.1f;
                damage = 20;
                maxHits = 3;
                amount = 1;
                break;
            case 2:
                CurrentLevel = 2;

                pushOutDuration = 0.1f;
                damage = 20;
                maxHits = 3;
                amount = 2;
                break;
            case 3:
                CurrentLevel = 3;

                pushOutDuration = 0.1f;
                damage = 30;
                maxHits = 3;
                amount = 2;
                break;
            case 4:
                CurrentLevel = 4;

                pushOutDuration = 0.1f;
                damage = 30;
                maxHits = 5;
                amount = 2;
                break;
            case 5:
                CurrentLevel = 5;

                pushOutDuration = 0.1f;
                damage = 30;
                maxHits = 5;
                amount = 3;
                break;
            case 6:
                CurrentLevel = 6;

                pushOutDuration = 0.1f;
                damage = 40;
                maxHits = 5;
                amount = 3;
                break;
            case 7:
                CurrentLevel = 7;

                pushOutDuration = 0.1f;
                damage = 40;
                maxHits = 7;
                amount = 3;
                break;
            case 8:
                CurrentLevel = 8;

                pushOutDuration = 0.1f;
                damage = 50;
                maxHits = 7;
                amount = 3;
                AtMaxLevel = true;
                break;
        }

        amount += baseCharacter.AmountTotal;
        damage *= baseCharacter.StrenghtTotal;
        attackDelay *= baseCharacter.CooldownTotal;
        if (attackDelay < 1)
        {
            attackDelay = 1;
        }
    }

    public override string GetLevelDescription(int level)
    {
        switch (level)
        {
            case 0:
            case 1:
                return "High damage, high Area scaling.";
            case 2:
                return "Fires 1 more projectile.";
            case 3:
                return "Base Damage up by 10.";
            case 4:
                return "Passes through 2 more enemies.";
            case 5:
                return "Fires 1 more projectile.";
            case 6:
                return "Base Damage up by 10.";
            case 7:
                return "Passes through 2 more enemies.";
            case 8:
                return "Base Damage up by 10.";
            default:
                return "";
        }
    }

    private int GetRandomDirection()
    {
        int dir = Random.Range(0, 2);
        dir = dir - (1 - dir);
        direction = dir;

        return dir;
    }

    private int ChangeDirection()
    {
        direction *= -1;
        return direction;
    }
}
