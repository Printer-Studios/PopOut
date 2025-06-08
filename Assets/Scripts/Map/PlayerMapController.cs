using NUnit.Framework.Internal.Commands;
using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Splines;
//using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System.Xml;
using JetBrains.Annotations;
using Unity.VisualScripting;
using System.Linq;
using System.Collections.Generic;

public class PlayerMapController : MonoBehaviour
{
    [SerializeField] private SplineContainer splineContainer;
    [SerializeField] private float animationspeed = 5f;
    [SerializeField] private PoiMapInfoSO poiController;
    [SerializeField] private PoiSO currentPOI;

    [SerializeField] public InputActionReference movementLeft;
    [SerializeField] public InputActionReference movementRight;
    [SerializeField] public InputActionReference movementUp;
    [SerializeField] public InputActionReference movementDown;
    [SerializeField] public InputActionReference jump;
    [SerializeField] public InputActionReference pause;

    /*private*/
    public bool playerIsMoving = false;
    /*[HideInInspector]*/ public bool inputEnabled = true;

    public static event Action OnPlayerLeavingPOI;
    public static event Action<string> OnPlayerArrivedNewPOI;
    public event Action<int> OnLevelSelected;

    private void Start()
    {
        // Initialize from saved state
        PoiSaveManager.InitializePoiUnlockList(poiController);

        currentPOI = poiController.poisList[PoiSaveManager.LoadCurrentPoiIndex()];
        Debug.Log("Current POI: " + currentPOI.name);

        // Unlock neighbors
        PoiSaveManager.UnlockPOIs(currentPOI.poiToUnlock, poiController);

        // Move player to starting POI
        transform.position = splineContainer.transform.TransformPoint(currentPOI.PoiKnotPosition);
        OnPlayerArrivedNewPOI?.Invoke(currentPOI.PoiName);

        // Enable controls
        movementUp.action.Enable();
        movementDown.action.Enable();
        movementLeft.action.Enable();
        movementRight.action.Enable();
        jump.action.Enable();
    }


    private void Update()
    {
        if (inputEnabled) //Comments are for not using Input Action
        {
            if (movementUp.action.IsInProgress() && currentPOI.NorthData.nextPoiSO.isUnlocked && currentPOI.NorthData.nextPoiSO != null)
                //if (Input.GetKey(KeyCode.W) && currentPOI.NorthData.nextPoiSO.isUnlocked && currentPOI.NorthData.nextPoiSO != null)
            {
                MovePlayerOnMap(currentPOI.NorthData.SplineIndex, currentPOI.NorthData.Reverse);
            }
            else if (movementDown.action.IsInProgress() && currentPOI.SouthData.nextPoiSO.isUnlocked && currentPOI.SouthData.nextPoiSO != null)
            //else if (Input.GetKey(KeyCode.S) && currentPOI.SouthData.nextPoiSO.isUnlocked && currentPOI.SouthData.nextPoiSO != null)
            {
                MovePlayerOnMap(currentPOI.SouthData.SplineIndex, currentPOI.SouthData.Reverse);
            }
            else if (movementLeft.action.IsInProgress() && currentPOI.WestData.nextPoiSO.isUnlocked && currentPOI.WestData.nextPoiSO != null)
            //else if (Input.GetKey(KeyCode.A) && currentPOI.WestData.nextPoiSO.isUnlocked && currentPOI.WestData.nextPoiSO != null)
            {
                MovePlayerOnMap(currentPOI.WestData.SplineIndex, currentPOI.WestData.Reverse);
            }
            else if (movementRight.action.IsInProgress() && currentPOI.EastData.nextPoiSO.isUnlocked && currentPOI.EastData.nextPoiSO != null)
            //else if (Input.GetKey(KeyCode.D) && currentPOI.EastData.nextPoiSO.isUnlocked && currentPOI.EastData.nextPoiSO != null)
            {
                MovePlayerOnMap(currentPOI.EastData.SplineIndex, currentPOI.EastData.Reverse);
            }
            else if (jump.action.IsInProgress() && currentPOI.isUnlocked)
            {
                if (!playerIsMoving && currentPOI.LevelToTransition >= 0)
                {
                    int currentPoiIndex = GetCurrentPoiFromList();
                    PoiSaveManager.SaveCurrentPoi(currentPoiIndex);
                    PoiSaveManager.UnlockPOIs(currentPOI.poiToUnlock, poiController);

                    OnLevelSelected?.Invoke(currentPOI.LevelToTransition);
                    Debug.Log($"Transition to level {currentPOI.LevelToTransition}");
                    SceneManager.LoadScene(currentPOI.scenePath);
                }
            }
            if (pause.action.WasPerformedThisFrame())
            {
                GoToMainMenu();
            }
        }
    }

    public void MovePlayerOnMap(int splineIndex, bool reverse = false)
    {
        if (playerIsMoving == false)
        {
            StartCoroutine(FollowSplineCoroutine(splineIndex, reverse));
        }
    }

    private IEnumerator FollowSplineCoroutine(int splineIndex, bool reverse = false)
    {
        if (splineIndex < 0 || splineIndex >= splineContainer.Splines.Count)
        {
            Debug.LogWarning("Invalid spline Index");
            yield break;
        }

        playerIsMoving = true;
        OnPlayerLeavingPOI?.Invoke();

        var chosenSpline = splineContainer.Splines[splineIndex];
        float t = reverse ? 1f : 0f;
        bool completed = false;

        float4x4 transformMatrix = float4x4.identity;

        float totalLength = chosenSpline.CalculateLength(transformMatrix);
        float distanceCovered = 0f;

        UpdatePosition();

        while (!completed)
        {
            float distanceThisFrame = animationspeed * Time.deltaTime;
            distanceCovered += distanceThisFrame;

            if (distanceCovered >= totalLength)
            {
                distanceCovered = totalLength;
                completed = true;
            }

            t = Mathf.Clamp01(distanceCovered / totalLength);
            t = reverse ? 1f - t: t;

            UpdatePosition();
            yield return null;
        }

        //AudioController.Instance.PlaySFX();
        playerIsMoving = false;
        currentPOI = GetDestinyPoiSO(splineIndex, reverse);
        OnPlayerArrivedNewPOI?.Invoke(currentPOI.PoiName);

        void UpdatePosition()
        {
            Vector3 localPosition = (Vector3)chosenSpline.EvaluatePosition(t);
            Vector3 worldPosition = splineContainer.transform.TransformPoint(localPosition);
            transform.position = worldPosition;
        }
    }

    private PoiSO GetDestinyPoiSO(int splineIndex, bool reverse)
    {
        BezierKnot knot;
        if (reverse)
        {
            knot = splineContainer.Splines[splineIndex][0];
        }
        else
        {
            knot = splineContainer.Splines[splineIndex][^1];
        }

        var key = new float3(Mathf.Round(knot.Position.x * 1000f) / 1000f,
            Mathf.Round(knot.Position.y * 1000f) / 1000f,
            Mathf.Round(knot.Position.z * 1000f) / 1000f);

        var outputPoi = poiController.PoiDictionary.TryGetValue(key, out PoiSO poiSO) ? poiSO : null;
        return outputPoi;
    }

    private int GetCurrentPoiFromList()
    {
        for (int i = 0; i < poiController.poisList.Count; i++)
        {
            if (currentPOI.name == poiController.poisList[i].name)
            {
                Debug.Log(i);
                return i;
            }
            Debug.Log(currentPOI.name);
            Debug.Log(poiController.poisList[i].name);
        }
        
        return -1;
    }

    public void GoToMainMenu()
    {
        PlayerPrefs.Save();
        SceneManager.LoadScene("MainMenu");
    }
}

