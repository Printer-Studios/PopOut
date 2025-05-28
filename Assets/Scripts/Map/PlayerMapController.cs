using NUnit.Framework.Internal.Commands;
using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Splines;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System.Xml;
using JetBrains.Annotations;
using Unity.VisualScripting;

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

    /*private*/ public bool playerIsMoving = false;
    /*[HideInInspector]*/ public bool inputEnabled = true;

    public static event Action OnPlayerLeavingPOI;
    public static event Action<string> OnPlayerArrivedNewPOI;
    public event Action<int> OnLevelSelected;

    private void Start()
    {
        currentPOI = poiController.poisList[PlayerPrefs.GetInt("CurrentPoi")];

        transform.position = splineContainer.transform.TransformPoint(currentPOI.PoiKnotPosition);

        OnPlayerArrivedNewPOI?.Invoke(currentPOI.PoiName);

        movementUp.action.Enable();
        movementDown.action.Enable();
        movementLeft.action.Enable();
        movementRight.action.Enable();
        jump.action.Enable();
    }
        

        private void Update()
    {
        if (inputEnabled)
        {
            if (movementUp.action.IsInProgress())
            {
                Debug.Log("UPUPUPUPUP");
                MovePlayerOnMap(currentPOI.NorthData.SplineIndex, currentPOI.NorthData.Reverse);
            }
            else if (movementDown.action.IsInProgress())
            {
                MovePlayerOnMap(currentPOI.SouthData.SplineIndex, currentPOI.SouthData.Reverse);
            }
            else if (movementLeft.action.IsInProgress())
            {
                MovePlayerOnMap(currentPOI.WestData.SplineIndex, currentPOI.WestData.Reverse);
            }
            else if (movementRight.action.IsInProgress())
            {
                MovePlayerOnMap(currentPOI.EastData.SplineIndex, currentPOI.EastData.Reverse);
            }
            else if (jump.action.IsInProgress())
            {
                if (playerIsMoving == false && currentPOI.LevelToTransition >= 0)
                {
                    PlayerPrefs.SetInt("CurrentPoi", GetCurrentPoiFromList());
                    PlayerPrefs.Save();
                    OnLevelSelected?.Invoke(currentPOI.LevelToTransition);
                    Debug.Log($"Transition to level {currentPOI.LevelToTransition}");
                    SceneManager.LoadScene(currentPOI.scenePath);
                }
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
            if (currentPOI.GameObject() == poiController.poisList[i].GameObject())
            {
                Debug.Log(i);
                return i;
            }
        }
        return -1;
    }
}

