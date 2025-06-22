using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private float speed = 5;
    private float lifeTime = 0;
    public GameObject Spider;
    
    void Start()
    {

    }

    
    void Update()
    {
        transform.position += speed * Time.deltaTime * -transform.up /** Spider.GetComponent<SpiderBehaviour>().toPlayer*/;

        lifeTime += Time.deltaTime;
        if (lifeTime > 3) Destroy(gameObject);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
