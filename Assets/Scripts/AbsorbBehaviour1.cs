using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class AbsorbBehaviour1 : MonoBehaviour
{
    public float maxCapacity, currentAmmo; //canviar maxCapacity a const priv
    public Collider2D absorbArea;
    public Slider waterBar, UISlider;
    private float timeOfPressing;
    [SerializeField] public InputActionReference absorb;

    private void Start()
    {
        waterBar.minValue = 0; waterBar.maxValue = 1;
        UISlider.minValue = waterBar.minValue;
        UISlider.maxValue = waterBar.maxValue;
    }

    public void Update()
    {
        
        waterBar.value = currentAmmo / maxCapacity;
        UISlider.value = waterBar.value;

        if (absorb.action.IsPressed())
        {
            timeOfPressing += Time.deltaTime;
        }
    }

    public void OnTriggerStay2D(Collider2D col)
    {

        if (col.gameObject.layer == LayerMask.NameToLayer("Water") && currentAmmo < maxCapacity && absorb.action.IsInProgress() && timeOfPressing > 0.075f) //Layer 4 is Water
        {
            col.gameObject.transform.localScale /= 2;
            currentAmmo++;
            Destroy(col.gameObject);
            timeOfPressing = 0f;
        }
    }
}
