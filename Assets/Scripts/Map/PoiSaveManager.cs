using System;
using System.Collections.Generic;
using UnityEngine;

public static class PoiSaveManager
{
    private const string UnlockedKey = "UnlockedPOIS";
    private const string CurrentPoiKey = "CurrentPoi";

    public static void InitializePoiUnlockList(PoiMapInfoSO poiController)
    {
        var saved = PlayerPrefsX.GetBoolArray(UnlockedKey);
        int poiCount = poiController.poisList.Count;

        // Resize if needed
        if (saved.Length != poiCount)
        {
            Array.Resize(ref saved, poiCount);
            PlayerPrefsX.SetBoolArray(UnlockedKey, saved);
            PlayerPrefs.Save();
        }

        // Sync POI State
        poiController.unlockList = new List<bool>(saved);

        for (int i = 0; i < poiCount; i++)
        {
            poiController.poisList[i].isUnlocked = saved[i];
        }

        Debug.Log("Loaded POI unlocks: " + string.Join(",", saved));
    }

    public static void UnlockPOIs(List<PoiSO> toUnlock, PoiMapInfoSO poiController)
    {
        bool[] saved = PlayerPrefsX.GetBoolArray(UnlockedKey);

        foreach (var poi in toUnlock)
        {
            int index = poiController.poisList.IndexOf(poi);
            if (index != -1)
            {
                saved[index] = true;
                poiController.poisList[index].isUnlocked = true;
                poiController.unlockList[index] = true;
            }
        }

        PlayerPrefsX.SetBoolArray(UnlockedKey, saved);
        PlayerPrefs.Save();

        Debug.Log("Saved POI unlocks: " + string.Join(",", saved));
    }

    public static void SaveCurrentPoi(int poiIndex)
    {
        PlayerPrefs.SetInt(CurrentPoiKey, poiIndex);
        PlayerPrefs.Save();
    }

    public static int LoadCurrentPoiIndex()
    {
        return PlayerPrefs.GetInt(CurrentPoiKey, 0); // default to 0
    }
}
