using UnityEngine;

public class HeartHandler : MonoBehaviour
{
    public GameObject heart;
    public HealthBehaviour health;

    public enum heartNum
    {
        first = 1, 
        second = 2, 
        third = 3
    }

    public heartNum currentHeart;

    // Update is called once per frame
    private void Update()
    {
        if(health.currentHealth < (int)currentHeart)
        {
            heart.SetActive(false);
        }
    }
}
