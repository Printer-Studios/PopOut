using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public string sceneToChange;
    [SerializeField]
    public string scenePath;

    GoalHandler goalHandler;

    public void Start()
    {
        goalHandler = GetComponent<GoalHandler>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player" && goalHandler.goalCurrent == GoalHandler.Goal.Opened)
        {
            ChangeScene();
        }
    }

    public void ChangeScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(scenePath);
    }

    public void EndGame()
    {
        Application.Quit();
    }
}
