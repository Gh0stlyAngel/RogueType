using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : BaseEnemy
{
    protected override void EnemyOnDestroy()
    {
        base.EnemyOnDestroy();
        StatisticManager.Instance.AddEnemyKills(EnemyType.Mushroom, 1);
    }
}
