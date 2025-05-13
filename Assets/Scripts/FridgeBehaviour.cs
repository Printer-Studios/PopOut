using Unity.VisualScripting;
using UnityEngine;

public class FridgeBehaviour : MonoBehaviour
{
    public GameObject icePF;
    public GameObject steamPF;
    private float timeOfPressing;

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Water" && timeOfPressing >= 0.1f)
        {
            Destroy(col.gameObject);
            SpawnIce();
            timeOfPressing = 0;
        }
        timeOfPressing += Time.deltaTime;
    }
    void SpawnSteam()
    {
        Instantiate(steamPF, transform.position, Quaternion.identity);
    }

    void SpawnIce()
    {
        Instantiate(icePF, transform.position, Quaternion.identity);
    }
}
