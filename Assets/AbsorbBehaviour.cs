using System.Threading;
using UnityEngine;
using Unity.VisualScripting;

public class AbsorbBehaviour : MonoBehaviour
{
    public int maxCapacity, currentAmmo; //canviar maxCapacity a const priv
    public Collider2D absorbArea;

    public void OnTriggerStay2D(Collider2D col)
    {
        if (Input.GetKeyUp(KeyCode.X))
        {
            if (col.gameObject.layer == 4 && currentAmmo < maxCapacity) //Layer 4 is Water
            {
                col.gameObject.transform.localScale /= 2;
                currentAmmo++;
                Destroy(col.gameObject);
            }
        }
    }
}
