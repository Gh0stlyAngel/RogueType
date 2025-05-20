using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : BaseItem
{
    [SerializeField] protected GameObject baseWeaponPrefab;
    [SerializeField] protected BaseCharacter baseCharacter;
    [SerializeField] protected AudioClip attackSound;

    [SerializeField] protected float attackDelay;
    [SerializeField] protected float damage;
    [SerializeField] protected float pushOutDuration;

    private void Update() => WeaponUpdate();

    protected override void ItemStart()
    {
        base.ItemStart();
        player = GameObject.FindWithTag("Player"); // Удалить, добавил для теста
    }

    protected virtual void WeaponUpdate()
    {
        transform.position = player.transform.position;
    }

    public override void LevelUp(int currentLevel, ref List<GameObject> currentItemsList)
    {
        base.LevelUp(currentLevel, ref currentItemsList);
    }

    public override void SetStats(int level)
    {
        base.SetStats(level);
        baseCharacter = player.GetComponent<BaseCharacter>();
    }

    public override void InitItem()
    {

    }
}
