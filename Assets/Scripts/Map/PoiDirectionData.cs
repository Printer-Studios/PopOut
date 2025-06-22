using UnityEngine;

[System.Serializable]
public class PoiDirectionData
{
    [field: SerializeField] public int SplineIndex { get; private set; } = -1;
    [field: SerializeField] public bool Reverse { get; private set; }
    public PoiSO nextPoiSO;

    public PoiDirectionData(int splineIndex, bool reverse)
    {
        SplineIndex = splineIndex;
        Reverse = reverse;
    }
}
