using System;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    public void Click(string scene)
    {
        SceneManager.LoadScene(scene);
        Debug.Log("play");
    }
}
