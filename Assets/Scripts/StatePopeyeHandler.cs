using UnityEngine;

public class StatePopeyeHandler : MonoBehaviour
{

    public enum State
    {
        small = 0,
        medium = 1,
        big = 2
    }

    public State currentState;
    public AbsorbBehaviour1 absorb;
    public WaterInteraction waterInteraction;
    public Rigidbody2D rb;
    public CapsuleCollider2D colSmall, trigger, colMedium, colBig;
    public Sprite smallSprite, mediumSprite, bigSprite;
    public float normalMass, bigMass;
    public float normalSpeed, bigSpeed;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentState = State.small;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeState();
    }

    public void ChangeState()
    {
        if(absorb.currentAmmo < 7){
            currentState = State.small;
            rb.mass = normalMass;
            waterInteraction.maxSpeed = normalSpeed;
            GetComponent<SpriteRenderer>().sprite = smallSprite;
            colSmall.enabled = true;
            colMedium.enabled = false;
            colBig.enabled = false;
            trigger.direction = CapsuleDirection2D.Horizontal;
        }
        else if(absorb.currentAmmo < 20) {
            currentState = State.medium;
            rb.mass = normalMass;
            waterInteraction.maxSpeed = normalSpeed;
            GetComponent<SpriteRenderer>().sprite = mediumSprite;
            //col.direction = CapsuleDirection2D.Vertical;
            //col.size = new Vector2(0.6215117f, 0.8491912f);
            //col.offset = new Vector2(-0.02283061f, -0.08331972f);
            colSmall.enabled = false;
            colMedium.enabled = true;
            colBig.enabled = false;
            trigger.direction = CapsuleDirection2D.Vertical;
        }
        else {
            currentState = State.big;
            rb.mass = bigMass;
            waterInteraction.maxSpeed = bigSpeed;
            GetComponent<SpriteRenderer>().sprite = bigSprite;
            //col.direction = CapsuleDirection2D.Vertical;
            //col.size = new Vector2(0.9235349f, 1.118689f);
            //col.offset = new Vector2(-0.02283061f, 0.06263198f);
            colSmall.enabled = false;
            colMedium.enabled = false;
            colBig.enabled = true;
            trigger.direction = CapsuleDirection2D.Vertical;
        }
        trigger = (colSmall.enabled && !colMedium.enabled && !colBig.enabled) ? colSmall : (colMedium.enabled && !colBig.enabled) ? colMedium : colBig;
    }


}
