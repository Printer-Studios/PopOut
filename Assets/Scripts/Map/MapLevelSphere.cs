using UnityEngine;

public class MapLevelSphere : MonoBehaviour
{
    public PoiSO poiSO;
    public Color lockedColor;
    public Color unlockedColor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (poiSO != null)
        {
            if (poiSO.isUnlocked)
            {
                GetComponent<SpriteRenderer>().color = unlockedColor;
            }
            else
            {
                GetComponent<SpriteRenderer>().color = lockedColor;
            }
        }
    }

        // Update is called once per frame
        void Update()
    {
        
    }
}
