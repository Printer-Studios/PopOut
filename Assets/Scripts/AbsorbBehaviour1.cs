using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using UnityEngine.UI;

public class AbsorbBehaviour1 : MonoBehaviour
{
    public float maxCapacity, currentAmmo; //canviar maxCapacity a const priv
    public Collider2D absorbArea;
    public Slider waterBar;
    [SerializeField] public InputActionReference absorb;

    public void Update()
    {
        waterBar.value = currentAmmo / maxCapacity;
    }

    public void OnTriggerStay2D(Collider2D col)
    {
        if (absorb.action.IsPressed())
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
