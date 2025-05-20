using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : BaseWeapon
{
    [SerializeField] private GameObject dagger;

    [SerializeField] private float speed;
    [SerializeField] private int maxHits;
    [SerializeField] private int amount;
    [SerializeField] private Vector3 direction;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private bool plusOffset;
    protected override void ItemStart()
    {
        
        base.ItemStart();

    }

    public override void InitItem()
    {
        maxLevel = 8;
        player = GameObject.FindWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
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
                direction = playerController.direction;
                Vector3 normal;
                Vector2 offsetPosition;

                normal = new Vector2(-direction.y, direction.x); // Нормаль (перпендикулярный вектор)

                if (plusOffset)
                    offsetPosition = transform.position + normal * 0.15f; // 0.15f - расстояние между даггерами
                else
                    offsetPosition = transform.position - normal * 0.15f;


                GameObject newDagger2 = Instantiate(dagger, offsetPosition, dagger.transform.rotation);
                newDagger2.GetComponent<DaggerParameters>().Init(damage, pushOutDuration, speed, maxHits, direction);
                SFXManager.instance.PlaySoundFXClip(attackSound, transform, 0.1f);


                plusOffset = !plusOffset;
                yield return new WaitForSeconds(0.2f);
            }
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
        speed = 5f;
        attackDelay = 1.5f;
        switch (level)
        {
            case 1:
            case 0:
                CurrentLevel = 1;

                amount = 1;
                maxHits = 1;
                damage = 20;
                pushOutDuration = 0.15f;

                break;
            case 2:
                CurrentLevel = 2;

                amount = 2;
                maxHits = 1;
                damage = 20;
                pushOutDuration = 0.15f;
                break;
            case 3:
                CurrentLevel = 3;

                amount = 3;
                maxHits = 1;
                damage = 25;
                pushOutDuration = 0.15f;
                break;
            case 4:
                CurrentLevel = 4;

                amount = 4;
                maxHits = 1;
                damage = 25;
                pushOutDuration = 0.15f;
                break;
            case 5:
                CurrentLevel = 5;

                amount = 4;
                maxHits = 2;
                damage = 25;
                pushOutDuration = 0.15f;
                break;
            case 6:
                CurrentLevel = 6;

                amount = 5;
                maxHits = 2;
                damage = 25;
                pushOutDuration = 0.15f;
                break;
            case 7:
                CurrentLevel = 7;

                amount = 6;
                maxHits = 2;
                damage = 30;
                pushOutDuration = 0.15f;
                break;
            case 8:
                CurrentLevel = 8;

                amount = 6;
                maxHits = 3;
                damage = 30;
                pushOutDuration = 0.15f;
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
                return "Fires quickly in the faced direction.";
            case 2:
                return "Fires 1 more projectile.";
            case 3:
                return "Fires 1 more projectile. \nBase Damage up by 5.";
            case 4:
                return "Fires 1 more projectile.";
            case 5:
                return "Passes through 1 more enemy.";
            case 6:
                return "Fires 1 more projectile.";
            case 7:
                return "Fires 1 more projectile. \nBase Damage up by 5.";
            case 8:
                return "Passes through 1 more enemy.";
            default:
                return "";
        }
    }
}
