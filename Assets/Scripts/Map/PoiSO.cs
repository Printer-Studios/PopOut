using System.Net.NetworkInformation;
using Unity.Mathematics;
//using UnityEditor.Timeline.Actions;
using UnityEngine;  
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PoiSO", menuName = "ScriptableObjects/ POI")]
[System.Serializable]
public class PoiSO : ScriptableObject
{
    [field: Header("POI Info")]
    [field: SerializeField] public string PoiName { get; private set; }
    [field: SerializeField] public float3 PoiKnotPosition { get; set; }
    [field: SerializeField] public int LevelToTransition { get; private set; } = -1;

    [field: Header("Directional Data")]
    [field: SerializeField] public PoiDirectionData NorthData { get; private set; }
    [field: SerializeField] public PoiDirectionData EastData { get; private set; }
    [field: SerializeField] public PoiDirectionData WestData { get; private set; }
    [field: SerializeField] public PoiDirectionData SouthData { get; private set; }
    [SerializeField]

    public string scenePath;
    public bool isUnlocked;
    public List<PoiSO> poiToUnlock;
}