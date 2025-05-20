using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinBoss : BaseEnemy
{
    protected override void EnemyOnDestroy()
    {
        base.EnemyOnDestroy();
        StatisticManager.Instance.AddEnemyKills(EnemyType.Goblin, 200);
    }
}
