using UnityEngine;
using UnityEngine.UI;
using static GoalHandler;

public class GeneratorBehaviour : MonoBehaviour
{ 
    public enum Activate { Nothing, Goal, Fan }
    public Activate whatToActivate;
    public GameObject target;
    public Slider genSlider;
    public int sliderMaxAmount;
    private float timeOfPressing;
    public float top;


    private void Start()
    {
        genSlider.minValue = 0;
        genSlider.maxValue = sliderMaxAmount;
        genSlider.value = genSlider.minValue;
    }

    private void Update()
    {
        if (genSlider.value == sliderMaxAmount)
        {
            if (whatToActivate == Activate.Goal && target.GetComponent<GoalHandler>() != null)
            {
                ActivateGoal();
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        timeOfPressing += Time.deltaTime;
        if (col.gameObject.tag == "Water" && genSlider.value != sliderMaxAmount && timeOfPressing >= top)
        {
            FillGenerator(col.gameObject);
        }

    }

    void FillGenerator(GameObject water)
    {
        //if (timeOfPressing >= 0.1f)
        {
            Destroy(water);
            genSlider.value++;
            timeOfPressing = 0;
        }
    }

    void ActivateGoal()
    {
        target.GetComponent<GoalHandler>().goalCurrent = GoalHandler.Goal.Opened;
    }
}
