using System.Collections.ObjectModel;
using Unity.Mathematics;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(fileName = "PoiMapInfo", menuName = "ScriptableObjects/ PoiMapInfo)")]
public class PoiMapInfoSO : ScriptableObject
{
    [SerializeField] private string mapName;
    [SerializeField] public List<PoiSO> poisList = new();
    [SerializeField] public List<bool> unlockList = new();


    private ReadOnlyDictionary<float3, PoiSO> poiDicitionary;

    public ReadOnlyDictionary<float3, PoiSO> PoiDictionary
    {
        get
        {
            if (poiDicitionary == null)
            {
                poiDicitionary = new(poisList.ToDictionary(x => x.PoiKnotPosition, x => x));
            }
            return poiDicitionary;
        }
    }
}
