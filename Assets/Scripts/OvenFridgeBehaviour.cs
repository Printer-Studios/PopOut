using Unity.VisualScripting;
using UnityEngine;

public class OvenFridgeBehaviour : MonoBehaviour
{
    public GameObject icePF;
    public GameObject steamPF;
    public GameObject waterPF;
    private float timeOfPressing, spawnModifier;
    public enum Type { Nothing, Oven, Fridge }
    public enum SpawnPosition { Above, Below }
    public SpawnPosition spawnPosition = SpawnPosition.Above;
    public Type type;

    public void Start()
    {
        switch (spawnPosition)
        {
            case SpawnPosition.Above:
                spawnModifier = 0;
                break;
            case SpawnPosition.Below:
                spawnModifier = -1;
                break;
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (type == Type.Oven)
        {
            Oven(col);
        }
        if (type == Type.Fridge)
        {
            Fridge(col);
        }
        timeOfPressing += Time.deltaTime;
    }

    void SpawnWater(bool SpawnRight)
    {
        if (SpawnRight)
        {
            Instantiate(waterPF, new Vector2(transform.position.x + 1, transform.position.y + spawnModifier), Quaternion.identity);
        }
        else
        {
            Instantiate(waterPF, new Vector2(transform.position.x - 1, transform.position.y + spawnModifier), Quaternion.identity);
        }
    }
    void SpawnSteam()
    {
        Instantiate(steamPF, new Vector2(transform.position.x, transform.position.y + spawnModifier), Quaternion.identity);
    }

    void SpawnIce()
    {
        Instantiate(icePF, new Vector2(transform.position.x, transform.position.y + 1), Quaternion.identity);
    }

    void Oven(Collider2D col)
    {
        if (col.gameObject.tag == "Water" && timeOfPressing >= 0.2f)
        {
            Destroy(col.gameObject);
            SpawnSteam();
            timeOfPressing = 0;
        }
        if (col.gameObject.tag == "Ice" && timeOfPressing >= 0.2f)
        {
            Destroy(col.gameObject);
            SpawnWater(GetDirection(col.gameObject));
            timeOfPressing = 0;
        }
    }

    void Fridge(Collider2D col)
    {
        if (col.gameObject.tag == "Water" && timeOfPressing >= 0.1f)
        {
            Destroy(col.gameObject);
            SpawnIce();
            timeOfPressing = 0;
        }
        if (col.gameObject.tag == "Steam" && timeOfPressing >= 0.2f)
        {
            Destroy(col.gameObject);
            SpawnWater(GetDirection(col.gameObject));
            timeOfPressing = 0;
        }
    }

    bool GetDirection(GameObject obj)
    {
        float xDir = obj.GetComponent<Rigidbody2D>().linearVelocityX;
        if (xDir < 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
