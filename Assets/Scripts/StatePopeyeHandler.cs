using UnityEngine;

public class StatePopeyeHandler : MonoBehaviour
{

    public enum States
    {
        small = 0,
        medium = 1,
        big = 2
    }

    public States currentState;
    public AbsorbBehaviour1 absorb;
    public WaterInteraction waterInteraction;
    public Rigidbody2D rb;
    public CapsuleCollider2D col, trigger;
    public Sprite smallSprite, mediumSprite, bigSprite;
    public float normalMass, bigMass;
    public float normalSpeed, bigSpeed;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentState = States.small;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeState();
    }

    public void ChangeState()
    {
        if(absorb.currentAmmo < 7){
            currentState = States.small;
            rb.mass = normalMass;
            waterInteraction.maxSpeed = normalSpeed;
            GetComponent<SpriteRenderer>().sprite = smallSprite;
            col.direction = CapsuleDirection2D.Horizontal;
            col.size = new Vector2(0.7917263f, 0.5123755f);
            col.offset = new Vector2(0.007073224f, -0.2537207f);
        }
        else if(absorb.currentAmmo < 20) {
            currentState = States.medium;
            rb.mass = normalMass;
            waterInteraction.maxSpeed = normalSpeed;
            GetComponent<SpriteRenderer>().sprite = mediumSprite;
            col.direction = CapsuleDirection2D.Vertical;
            col.size = new Vector2(0.6215117f, 0.8491912f);
            col.offset = new Vector2(-0.02283061f, -0.08331972f);
        }
        else {
            currentState = States.big;
            rb.mass = bigMass;
            waterInteraction.maxSpeed = bigSpeed;
            GetComponent<SpriteRenderer>().sprite = bigSprite;
            col.direction = CapsuleDirection2D.Vertical;
            col.size = new Vector2(0.9235349f, 1.118689f);
            col.offset = new Vector2(-0.02283061f, 0.06263198f);
        }
    }


}
