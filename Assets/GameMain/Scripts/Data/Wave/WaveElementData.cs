using System.Collections;
using System.Collections.Generic;
using Suture;
using UnityEngine;

public class WaveElementData
{
    private DRWaveElement _dRWaveElement;

    public WaveElementData(DRWaveElement dRWaveElement)
    {
        _dRWaveElement = dRWaveElement;
    }

    public int Id => _dRWaveElement.Id;

    public int EnemyId => _dRWaveElement.EnemyId;

    public float SpawnTime => _dRWaveElement.SpawnTime;
}