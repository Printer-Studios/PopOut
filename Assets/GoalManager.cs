using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public string scene;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene(scene);
    }

}
