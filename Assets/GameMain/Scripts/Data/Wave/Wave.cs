using System.Collections;
using System.Collections.Generic;
using GameFramework;
using Suture;
using UnityEngine;

public class Wave : IReference
{
    private WaveData _waveData;
    private Queue<WaveElement> _waveElements;

    private float timer = 0;

    #region WaveData

    public int Id => _waveData.Id;

    public float FinishWaitTIme => _waveData.FinishWaitTime;

    public WaveElementData[] WaveElementDatas => _waveData.WaveElementDatas;

    #endregion

    /// <summary>
    /// 总刷出时间
    /// </summary>
    public float TotalSpawnTime { get; private set; }

    /// <summary>
    /// 下一波时间
    /// </summary>
    public float NextWaveTime { get; private set; }

    /// <summary>
    /// 当前敌人索引
    /// </summary>
    public int CurrentEnemyIndex { get; private set; }

    // x?.y：null 条件成员访问。 如果左操作数计算结果为 null，则返回 null。
    //    num3 = num1 ?? 222;      // num1 如果为空值则返回 222
    /*   if (waveData == null || waveData.WaveElementDatas == null)
    return 0;
    else
    return waveData.WaveElementDatas.Length;*/
    public int EnemyCount => _waveData?.WaveElementDatas?.Length ?? 0;

    public float Progress
    {
        get
        {
            float result = (timer / NextWaveTime);
            if (result > 1)
                result = 1;
            return result;
        }
    }

    public bool Finish { get; private set; }

    public Wave()
    {
        _waveData = null;
        _waveElements = new Queue<WaveElement>();
        CurrentEnemyIndex = 1;
        TotalSpawnTime = 0f;
        NextWaveTime = 0f;
        Finish = false;
    }

    public void ProcessWave(float elapseSeconds, float realElapseSeconds)
    {
        if (Finish)
            return;

        timer += elapseSeconds;

        if (_waveElements.Count > 0)
        {
            // Peek() 返回位于 Queue 开始处的对象但不将其移除。
            if (timer > _waveElements.Peek().CumulativeTime)
            {
                WaveElement waveElement = _waveElements.Dequeue();
                int enemyId = waveElement.EnemyId;
                ReferencePool.Release(waveElement);
                waveElement = null;
                CurrentEnemyIndex++;
                GameEntry.Event.Fire(this, SpawnEnemyEventArgs.Create(enemyId));
            }
        }
        else if (!Finish && timer >= NextWaveTime)
        {
            Finish = true;
        }
    }

    public static Wave Create(WaveData waveData)
    {
        Wave wave = ReferencePool.Acquire<Wave>();
        wave._waveData = waveData;
        WaveElementData[] waveElementDatas = waveData.WaveElementDatas;
        float timer = 0;

        foreach (var waveElementData in waveElementDatas)
        {
            timer += waveElementData.SpawnTime;
            wave._waveElements.Enqueue(WaveElement.Create(waveElementData, timer));
        }

        wave.TotalSpawnTime = timer;
        wave.NextWaveTime = timer + waveData.FinishWaitTime;
        return wave;
    }

    public void Clear()
    {
        while (_waveElements.Count > 0)
        {
            WaveElement waveElement = _waveElements.Dequeue();
            ReferencePool.Release(waveElement);
        }

        _waveData = null;
        _waveElements.Clear();
        CurrentEnemyIndex = 1;
        TotalSpawnTime = 0f;
        NextWaveTime = 0f;
        timer = 0;
        Finish = false;
    }
}