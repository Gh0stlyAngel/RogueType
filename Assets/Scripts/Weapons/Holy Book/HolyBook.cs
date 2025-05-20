using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyBook : BaseWeapon
{
    [SerializeField] private GameObject holyBook;

    [SerializeField] private float lifetime;

    [SerializeField] private int amount;
    [SerializeField] private float speed;
    [SerializeField] private float radius;

    [SerializeField] private List<IDamagable> attacked;

    [SerializeField] private float dealDamageDelay;
    protected override void ItemStart()
    {
        base.ItemStart();
        StartCoroutine(ClearAttackedList());
    }

    IEnumerator ClearAttackedList()
    {
        while (true)
        {
            attacked.Clear();
            yield return new WaitForSeconds(dealDamageDelay);
        }
    }

    protected override void WeaponUpdate()
    {
        base.WeaponUpdate();
    }

    private void StartWeapon()
    {
        StartCoroutine(Attack());
    }

    public override void InitItem()
    {
        maxLevel = 8;
        StartWeapon();
    }

    IEnumerator Attack()
    {
        attacked = new List<IDamagable>();
        while (true)
        {
            for (int i = 0; i < amount; i++)
            {
                
                GameObject book = Instantiate(holyBook, gameObject.transform.position, holyBook.transform.rotation);
                float angle = GetAngle(360 / amount * i, speed);
                book.GetComponent<HolyBookParameters>().Init(damage, pushOutDuration, lifetime, speed, radius, angle, attacked, player);
            }
            yield return new WaitForSeconds(attackDelay);
        }
        
    }

    private float GetAngle(float degrees, float speed)
    {
        return (degrees * Mathf.PI / 180f) / speed;
    }

    public override void SetStats(int level)
    {
        base.SetStats(level);
        attackDelay = 6;
        switch (level)
        {
            case 1:
                CurrentLevel = 1;

                pushOutDuration = 0.17f;
                damage = 10;
                lifetime = 3.2f;
                amount = 1;
                speed = 2;
                radius = 1.7f;
                break;
            case 2:
                CurrentLevel = 2;

                pushOutDuration = 0.17f;
                damage = 10;
                lifetime = 3.2f;
                amount = 2;
                speed = 2;
                radius = 1.7f;
                break;
            case 3:
                CurrentLevel = 3;

                pushOutDuration = 0.17f;
                damage = 10;
                lifetime = 3.2f;
                amount = 2;
                speed = 2.5f;
                radius = 2f;
                break;
            case 4:
                CurrentLevel = 4;

                pushOutDuration = 0.17f;
                damage = 20;
                lifetime = 3.7f;
                amount = 2;
                speed = 2.5f;
                radius = 2f;
                break;
            case 5:
                CurrentLevel = 5;

                pushOutDuration = 0.17f;
                damage = 20;
                lifetime = 3.7f;
                amount = 3;
                speed = 2.5f;
                radius = 2f;
                break;
            case 6:
                CurrentLevel = 6;

                pushOutDuration = 0.17f;
                damage = 20;
                lifetime = 3.7f;
                amount = 3;
                speed = 3f;
                radius = 2.3f;
                break;
            case 7:
                CurrentLevel = 7;

                pushOutDuration = 0.17f;
                damage = 30;
                lifetime = 4.2f;
                amount = 3;
                speed = 3f;
                radius = 2.3f;
                break;

            case 8:
                CurrentLevel = 8;

                pushOutDuration = 0.17f;
                damage = 30;
                lifetime = 4.2f;
                amount = 4;
                speed = 3f;
                radius = 2.3f;
                AtMaxLevel = true;
                break;
        }

        amount += baseCharacter.AmountTotal;
        lifetime *= baseCharacter.DurationTotal;
        damage *= baseCharacter.StrenghtTotal;
        attackDelay *= baseCharacter.CooldownTotal;

        if (attackDelay < 2.4f)
        {
            attackDelay = 2.4f;
        }
    }

    public override string GetLevelDescription(int level)
    {
        switch (level)
        {
            case 0:
            case 1:
                return "Orbits around the character.";
            case 2:
                return "Fires 1 more projectile.";
            case 3:
                return "Base Area up by 20%.\nBase Speed up by 25%.";
            case 4:
                return "Effect lasts 0.5 seconds longer. Base Damage up by 10.";
            case 5:
                return "Fires 1 more projectile.";
            case 6:
                return "Base Area up by 15%. Base Speed up by 20%.";
            case 7:
                return "Effect lasts 0.5 seconds longer. Base Damage up by 10.";
            case 8:
                return "Fires 1 more projectile.";
            default:
                return "";
        }
    }
}
