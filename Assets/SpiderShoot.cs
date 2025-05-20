using Unity.VisualScripting;
using UnityEngine;

public class SpiderShoot : MonoBehaviour
{
    public GameObject bullet;
    public SpiderBehaviour SpiderBehaviour;
    public float cooldown;
    private float maxCooldown = 2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxCooldown = cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (SpiderBehaviour.IsInRange() && cooldown < 0)
        {
            Instantiate(bullet, SpiderBehaviour.gameObject.transform.position, SpiderBehaviour.gameObject.transform.rotation);
            cooldown = maxCooldown;
        }

        if (SpiderBehaviour.IsInRange())
        {
            cooldown -= Time.deltaTime;
        }
        else
        {
            cooldown = maxCooldown;
        }
    }
}
