using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalHandler : MonoBehaviour
{
    public string sceneToChange;
    [SerializeField]
    public string scenePath;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            ChangeScene();
        }
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(scenePath);
    }

    public void probaDebug()
    {
        Debug.Log("botton clicked");
    }
}
