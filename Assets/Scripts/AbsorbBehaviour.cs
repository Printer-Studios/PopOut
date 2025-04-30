using System.Threading;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.UI;

public class AbsorbBehaviour : MonoBehaviour
{
    public int maxCapacity, currentAmmo; //canviar maxCapacity a const priv
    public Collider2D absorbArea;
    public Slider sliderWater;


    private void Start() //Set up slider for water
    {
        sliderWater.minValue = 0;
        sliderWater.maxValue = maxCapacity;
        sliderWater.value = currentAmmo;
    }

    private void Update() //Update the slider value
    {
        sliderWater.value = currentAmmo;
    }

    public void OnTriggerStay2D(Collider2D col)
    {
        if (Input.GetKeyUp(KeyCode.X))
        {
            if (col.gameObject.layer == 4 && currentAmmo < maxCapacity) //Layer 4 is Water
            {
                col.gameObject.transform.localScale /= 2;
                currentAmmo++;
                sliderWater.value++;
                Destroy(col.gameObject);
            }
        }
    }
}
