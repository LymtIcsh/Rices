using System.Collections;
using System.Collections.Generic;
using Suture;
using UnityEngine;

public class WaveData
{
    private DRWave _dRWave;
    private WaveElementData[] _waveElementDatas;

    public int Id => _dRWave.Id;

    public float FinishWaitTime => _dRWave.FinishWaitTIme;

    public WaveElementData[] WaveElementDatas => _waveElementDatas;

    public WaveData(DRWave dRWave, WaveElementData[] waveElementDatas)
    {
        _dRWave = dRWave;
        _waveElementDatas = waveElementDatas;
    }
}