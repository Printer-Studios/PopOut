using Unity.VisualScripting;
using UnityEngine;

public class SpiderBehaviour : MonoBehaviour
{
    public GameObject player;
    public Vector3 toPlayer;
    private float angle;
    public float spiderVision;
    public enum SpiderAngle { Full, Half}
    public SpiderAngle spiderAngle = SpiderAngle.Full;

    // Update is called once per frame
    void Update()
    {
        toPlayer = (player.transform.position - transform.position);

        if(IsInRange())
        {
            switch (spiderAngle)
            {
                case SpiderAngle.Full:
                    angle = Vector2.SignedAngle(Vector2.down, toPlayer.normalized);
                    transform.rotation = Quaternion.Euler(0f, 0f, angle);
                    break;
                case SpiderAngle.Half:
                    if (player.transform.position.y <= transform.position.y + 1)
                    {
                        angle = Vector2.SignedAngle(Vector2.down, toPlayer.normalized);
                        transform.rotation = Quaternion.Euler(0f, 0f, angle);
                    }
                    break;
            }
        }
    }

    public bool IsInRange()
    {
        return toPlayer.magnitude < spiderVision;
    }
}
