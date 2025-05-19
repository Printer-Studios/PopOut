using Unity.VisualScripting;
using UnityEngine;

public class SpiderBehaviour : MonoBehaviour
{
    public GameObject player;
    public Vector3 toPlayer;
    private float angle;
    public float spiderVision;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        toPlayer = (player.transform.position - transform.position);

        if(IsInRange())
        {
            angle = Vector2.SignedAngle(Vector2.down, toPlayer.normalized);
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }

    public bool IsInRange()
    {
        return toPlayer.magnitude < spiderVision;
    }
}
