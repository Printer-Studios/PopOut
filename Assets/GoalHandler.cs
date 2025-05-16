using UnityEngine;

public class GoalHandler : MonoBehaviour
{
    public enum Goal { None, Opened, Closed }
    public Goal goalCurrent;

    public Sprite openedSprite, closedSprite;

    public SceneHandler sceneHandler;

    // Update is called once per frame
    void Update()
    {
        if (goalCurrent == Goal.Closed)
        {
            sceneHandler.enabled = false;
            gameObject.GetComponent<SpriteRenderer>().sprite = closedSprite;
        }
        if (goalCurrent == Goal.Opened) 
        {
            sceneHandler.enabled = true;
            gameObject.GetComponent<SpriteRenderer>().sprite = openedSprite;
        }
    }
}
