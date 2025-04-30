using UnityEngine;

public class changeColor : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Color color1;
    public Color color2;
    [Range(0f, 1f)]
    public float tc; //numero entre 0 i 1 que canvia el daixonsis de color saps
    SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            tc += 0.2f * Time.deltaTime;
        }
        else
        {
            tc -= 0.2f * Time.deltaTime;
        }
        spriteRenderer.color = Color.Lerp(color1, color2, tc);
    }
}
