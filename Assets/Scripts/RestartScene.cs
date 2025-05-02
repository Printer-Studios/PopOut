using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScene : MonoBehaviour
{
    public void RestartCurrent()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
