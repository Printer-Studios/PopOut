// File: Assets/Editor/PoiBatchEditor.cs
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class PoiBatchEditor : EditorWindow
{
    private List<PoiSO> poiList = new List<PoiSO>();
    private bool newIsUnlockedValue = true;

    [MenuItem("Tools/POI/Batch Set IsUnlocked")]
    public static void ShowWindow()
    {
        GetWindow<PoiBatchEditor>("POI Batch Editor");
    }

    private void OnGUI()
    {
        GUILayout.Label("Batch Set 'isUnlocked' on PoiSO Assets", EditorStyles.boldLabel);

        newIsUnlockedValue = EditorGUILayout.Toggle("New isUnlocked Value", newIsUnlockedValue);

        if (GUILayout.Button("Find All PoiSO Assets"))
        {
            LoadAllPoiAssets();
        }

        if (poiList.Count > 0)
        {
            if (GUILayout.Button($"Apply to {poiList.Count} POIs"))
            {
                ApplyChange();
            }

            GUILayout.Label($"Found {poiList.Count} POI assets.");
        }
    }

    private void LoadAllPoiAssets()
    {
        poiList.Clear();

        string[] guids = AssetDatabase.FindAssets("t:PoiSO");
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            PoiSO poi = AssetDatabase.LoadAssetAtPath<PoiSO>(path);
            if (poi != null)
            {
                poiList.Add(poi);
            }
        }
    }

    private void ApplyChange()
    {
        foreach (PoiSO poi in poiList)
        {
            Undo.RecordObject(poi, "Batch Change isUnlocked");
            typeof(PoiSO).GetField("isUnlocked", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public)
                         .SetValue(poi, newIsUnlockedValue);
            EditorUtility.SetDirty(poi);
        }

        AssetDatabase.SaveAssets();
        Debug.Log($"Applied 'isUnlocked = {newIsUnlockedValue}' to {poiList.Count} assets.");
    }
}
