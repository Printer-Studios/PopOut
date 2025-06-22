using UnityEngine;

public class EnemyRespawn : MonoBehaviour
{
    public float respawnTime;
    private float timer;
    LayerMask deadLayer;
    LayerMask regularLayer;
    public Sprite aliveTomato;

    private void Start()
    {
        regularLayer = gameObject.layer;
        deadLayer = LayerMask.NameToLayer("Dead");
    }

    void Update()
    {
        if (deadLayer == gameObject.layer)
        {
            timer += Time.deltaTime;

            if (timer >= respawnTime)
            {
                Activate();
            }
        }
    }

    public void Deactivate()
    {
        gameObject.layer = deadLayer;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void Activate()
    {
        gameObject.layer = regularLayer;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        timer = 0;
        gameObject.GetComponent<SpriteRenderer>().sprite = aliveTomato;
    }
}
