using Unity.VisualScripting;
using UnityEngine;

public class OvenFridgeBehaviour : MonoBehaviour
{
    public GameObject icePF;
    public GameObject steamPF;
    public GameObject waterPF;
    private float timeOfPressing;
    public enum Type { Nothing, Oven, Fridge }
    public Type type;


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
            Instantiate(waterPF, new Vector2(transform.position.x + 1, transform.position.y), Quaternion.identity);
        }
        else
        {
            Instantiate(waterPF, new Vector2(transform.position.x - 1, transform.position.y), Quaternion.identity);
        }
    }
    void SpawnSteam()
    {
        Instantiate(steamPF, transform.position, Quaternion.identity);
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
