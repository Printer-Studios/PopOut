using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
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
        Time.timeScale = 1;
        SceneManager.LoadScene(scenePath);
    }

    public void EndGame()
    {
        Application.Quit();
    }
}
