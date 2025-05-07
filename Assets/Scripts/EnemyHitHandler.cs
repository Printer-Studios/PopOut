using Unity.VisualScripting;
using UnityEngine;

public class EnemyHitHandler : MonoBehaviour
{
    public enum Hit { Nothing, Jump, Box}
    public Hit[] hitTypes;
    public EnemyShotHandler shotHandler;
    public GameObject boxPF;


    public Collider2D weakspot;

    public void OnTriggerEnter2D(Collider2D col)
    {
        for (int i = 0; i < hitTypes.Length; i++)
        {
            if (hitTypes[i] == Hit.Box /*&& col.gameObject.GetPrefabDefinition() == boxPF && col.gameObject.GetComponent<Rigidbody2D>().linearVelocityY > 0 */)
            {
                //Debug.Log("Box Hit");
                if (col.gameObject.name == "Box")
                {
                    Debug.Log("Box Prefab");
                    Debug.Log("Velocity " + col.gameObject.GetComponent<Rigidbody2D>().linearVelocityY);
                    if (col.gameObject.GetComponent<Rigidbody2D>().linearVelocityY < 0)
                    {
                        Debug.Log("Falling");
                        Die();
                    }
                }
            }
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
