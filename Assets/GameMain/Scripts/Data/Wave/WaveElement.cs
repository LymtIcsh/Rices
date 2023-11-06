using System.Collections;
using System.Collections.Generic;
using GameFramework;
using UnityEngine;

public class WaveElement : IReference
{
    private WaveElementData _waveElementData;

    public int Id => _waveElementData.Id;

    public int EnemyId => _waveElementData.EnemyId;

    public float SpawnTime => _waveElementData.SpawnTime;

    /// <summary>
    /// 累计时间
    /// </summary>
    public float CumulativeTime
    {
        get;
        private set;
    }

    public WaveElement()
    {
        _waveElementData = null;
        CumulativeTime = 0;
    }

    public static WaveElement Create(WaveElementData waveElementData, float cumulativeTime)
    {
        WaveElement waveElement = ReferencePool.Acquire<WaveElement>();
        waveElement._waveElementData = waveElementData;
        waveElement.CumulativeTime = cumulativeTime;
        return waveElement;
    }
    
    
    public void Clear()
    {
        _waveElementData = null;
        CumulativeTime = 0;
    }
}
