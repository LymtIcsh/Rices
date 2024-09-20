using System.Collections;
using System.Collections.Generic;
using GameFramework;
using GameFramework.Event;
using UnityEngine;

/// <summary>
/// 刷出敌人事件参数
/// </summary>
public class SpawnEnemyEventArgs : GameEventArgs
{
    public static readonly int EventId = typeof(SpawnEnemyEventArgs).GetHashCode();

    public int EnemyId { get; private set; }

    public SpawnEnemyEventArgs()
    {
        EnemyId = -1;
    }

    public override int Id => EventId;

    public static SpawnEnemyEventArgs Create(int enemyId, object userData = null)
    {
        SpawnEnemyEventArgs spawnEnemyEventArgs = ReferencePool.Acquire<SpawnEnemyEventArgs>();
        spawnEnemyEventArgs.EnemyId = enemyId;
        return spawnEnemyEventArgs;
    }

    public override void Clear()
    {
        EnemyId = -1;
    }
}