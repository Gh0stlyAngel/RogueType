using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceStaff : BaseWeapon
{
    [SerializeField] private float speed;
    [SerializeField] private float duration;
    [SerializeField] private GameObject iceStaffProjectile;
    [SerializeField] private float angle;
    protected override void ItemStart()
    {
        base.ItemStart();

    }

    public override void InitItem()
    {
        maxLevel = 7;
        StartWeapon();
    }

    protected override void WeaponUpdate()
    {
        base.WeaponUpdate();
    }

    private void GetNextRotationDirection()
    {
        angle -= 30;
    }

    IEnumerator Attack()
    {
        while (true)
        {
            GameObject newProjectile = Instantiate(iceStaffProjectile, transform.position, iceStaffProjectile.transform.rotation);
            newProjectile.GetComponent<IceStaffProjectile>().Init(0, 0, angle, transform.position, duration, speed);
            GetNextRotationDirection();

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
        speed = 45f;
        angle = 0;

        switch (level)
        {
            case 1:
            case 0:
                CurrentLevel = 1;

                duration = 2;
                attackDelay = 1.5f;
                break;
            case 2:
                CurrentLevel = 2;

                duration = 3;
                attackDelay = 1.5f;
                break;
            case 3:
                CurrentLevel = 3;

                duration = 3;
                attackDelay = 1f;
                break;
            case 4:
                CurrentLevel = 4;

                duration = 4;
                attackDelay = 1f;
                break;
            case 5:
                CurrentLevel = 5;

                duration = 5;
                attackDelay = 1f;
                break;
            case 6:
                CurrentLevel = 6;

                duration = 5;
                attackDelay = 0.5f;
                break;
            case 7:
                CurrentLevel = 7;

                duration = 6;
                attackDelay = 0.5f;
                AtMaxLevel = true;
                break;
        }
        duration *= baseCharacter.DurationTotal;
        attackDelay *= baseCharacter.CooldownTotal;

        if (attackDelay < 0.18)
        {
            attackDelay = 0.18f;
        }
    }

    public override string GetLevelDescription(int level)
    {
        switch (level)
        {
            case 0:
            case 1:
                return "Chance to freeze enemies.";
            case 2:
                return "Effect lasts 1 seconds longer.";
            case 3:
                return "Cooldown reduced by 0.5 seconds.";
            case 4:
                return "Effect lasts 1 seconds longer.";
            case 5:
                return "Effect lasts 1 seconds longer.";
            case 6:
                return "Cooldown reduced by 0.5 seconds.";
            case 7:
                return "Effect lasts 1 seconds longer.";
            default:
                return "";
        }
    }
}
