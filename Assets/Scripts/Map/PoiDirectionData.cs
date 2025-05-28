using System.Net.NetworkInformation;
using Unity.Mathematics;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PoiDirectionData
{
    [field: SerializeField] public int SplineIndex { get; private set; } = -1;
    [field: SerializeField] public bool Reverse { get; private set; }

    public PoiDirectionData(int splineIndex, bool reverse)
    {
        SplineIndex = splineIndex;
        Reverse = reverse;
    }
}
