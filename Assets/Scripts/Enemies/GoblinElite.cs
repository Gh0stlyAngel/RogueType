using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinElite : BaseEnemy
{
    protected override void EnemyOnDestroy()
    {
        base.EnemyOnDestroy();
        StatisticManager.Instance.AddEnemyKills(EnemyType.Goblin, 10);
    }
}
