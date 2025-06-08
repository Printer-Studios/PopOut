using UnityEngine;

public class MapLevelSphere : MonoBehaviour
{
    public PoiSO poiSO;
    public Color lockedColor;
    public Color unlockedColor;

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
}
