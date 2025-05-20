using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whip : BaseWeapon
{
    [SerializeField] private int amount;
    [SerializeField] private int direction;

    [SerializeField] private GameObject whipRightHit;
    [SerializeField] private GameObject whipLeftHit;

    [SerializeField] private float startScale;

    [SerializeField] Vector3 startLeftOffset;
    [SerializeField] Vector3 startRightOffset;
    [SerializeField] float topOffset;


    protected override void ItemStart()
    {
        base.ItemStart();
    }

    public override void InitItem()
    {
        maxLevel = 8;
        player = GameObject.FindWithTag("Player");
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
            direction = -1;
            Vector3 leftOffset = startLeftOffset;
            Vector3 rightOffset = startRightOffset;
            for (int i = 0; i < amount; i++)
            {
                GameObject newWhipSlash;
                
                if(ChangeDirection() == 1)
                {
                    newWhipSlash = Instantiate(whipLeftHit, transform.position + leftOffset, whipRightHit.transform.rotation);
                    leftOffset.y += topOffset;
                }
                else
                {
                    newWhipSlash = Instantiate(whipRightHit, transform.position + rightOffset, whipRightHit.transform.rotation);
                    rightOffset.y += topOffset;
                }

                newWhipSlash.GetComponent<WhipSlash>().Init(damage, pushOutDuration, player, startScale);

                SFXManager.instance.PlaySoundFXClip(attackSound, transform, 0.2f);
                yield return new WaitForSeconds(0.2f);
            }
            yield return new WaitForSeconds(attackDelay);
        }

    }

    private void StartWeapon()
    {
        StartCoroutine(Attack());
    }

    private int ChangeDirection()
    {
        direction *= -1;
        return direction;
    }

    public override void SetStats(int level)
    {
        base.SetStats(level);

        topOffset = 0.7f;

        attackDelay = 1.35f;
        switch (level)
        {
            case 1:
            case 0:
                CurrentLevel = 1;

                amount = 1;
                damage = 10;
                pushOutDuration = 0.15f;
                startScale = 1;

                break;
            case 2:
                CurrentLevel = 2;

                amount = 2;
                damage = 10;
                pushOutDuration = 0.15f;
                startScale = 1;

                break;
            case 3:
                CurrentLevel = 3;

                amount = 2;
                damage = 15;
                pushOutDuration = 0.15f;
                startScale = 1f;
                break;
            case 4:
                CurrentLevel = 4;

                amount = 2;
                damage = 15;
                pushOutDuration = 0.15f;
                startScale = 1.1f;
                break;
            case 5:
                CurrentLevel = 5;

                amount = 2;
                damage = 20;
                pushOutDuration = 0.15f;
                startScale = 1.1f;
                break;
            case 6:
                CurrentLevel = 6;

                amount = 2;
                damage = 20;
                pushOutDuration = 0.15f;
                startScale = 1.2f;
                break;
            case 7:
                CurrentLevel = 7;

                amount = 2;
                damage = 25;
                pushOutDuration = 0.15f;
                startScale = 1.2f;
                break;
            case 8:
                CurrentLevel = 8;

                amount = 2;
                damage = 30;
                pushOutDuration = 0.15f;
                startScale = 1.2f;
                AtMaxLevel = true;
                break;
        }


        amount += baseCharacter.AmountTotal;
        damage *= baseCharacter.StrenghtTotal;
        attackDelay *= baseCharacter.CooldownTotal;

        if (attackDelay < 0.5)
        {
            attackDelay = 0.5f;
        }

    }

    public override string GetLevelDescription(int level)
    {
        switch (level)
        {
            case 0:
            case 1:
                return "Attacks horizontally, passes through enemies.";
            case 2:
                return "Fires 1 more projectile.";
            case 3:
                return "Base Damage up by 5.";
            case 4:
                return "Base Area up by 10%.";
            case 5:
                return "Base Damage up by 5.";
            case 6:
                return "Base Area up by 10%.";
            case 7:
                return "Base Damage up by 5.";
            case 8:
                return "Base Damage up by 5.";
            default:
                return "";
        }
    }
}
