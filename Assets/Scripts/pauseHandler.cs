using UnityEngine;
using UnityEngine.InputSystem;

public class pauseHandler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject pausePanel;
    [SerializeField] public InputActionReference pause;

    private void Update()
    {
        if (pause.action.WasPerformedThisFrame())
        {
            if(Time.timeScale == 1)
            {
                PauseGame();
            }
            else
            {
                ContinueCurrent();
            }
            
        }
    }
    public void PauseGame() 
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }
    public void ContinueCurrent()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }
}
