using UnityEngine;

public class changeColor : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Color color1;
    public Color color2;
    public Color newColor;
    [Range(0f, 1f)]
    public float tc; // Number between 0 and 1 that determines how much does the "Pop" original color approach the second color.
    SpriteRenderer spriteRenderer;
    public HealthBehaviour HealthBehaviour;
    public AbsorbBehaviour1 Absorb;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //tc = Absorb.currentAmmo / Absorb.maxCapacity;
        newColor = Color.Lerp(new Color(color1.r, color1.g, color1.b, HealthBehaviour.alpha), new Color(color2.r, color2.g, color2.b, HealthBehaviour.alpha), tc);
        spriteRenderer.color = newColor;
    }
}
