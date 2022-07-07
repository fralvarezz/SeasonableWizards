using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{

    private float currentValue = 0f;
    private float rotation;
    private float offset;

    GameObject arm;
    public SeasonManagerScript seasonManager;
    Quaternion currentRotation;
    Vector3 rotationVector = new Vector3(0, 0, 1);

    SeasonManagerScript.Season currentSeason;

    public float CurrentValue
    {
        get
        {
            return currentValue;
        }
        set
        {
            currentValue = value;
            
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        arm = transform.GetChild(1).gameObject;
        currentRotation = arm.GetComponent<RectTransform>().rotation;
        currentSeason = seasonManager.getCurrentSeason();
        rotation = 0.0f;
        offset = 90 / seasonManager.interval;
    }

    // Update is called once per frame
    void Update()
    {
        if(seasonManager.getCurrentSeason() != currentSeason)
        {
            rotation += 90;
            currentSeason = seasonManager.getCurrentSeason();
        }
        CurrentValue = seasonManager.GetCurrentTime();
        arm.GetComponent<RectTransform>().rotation = currentRotation * Quaternion.AngleAxis(-(rotation + CurrentValue * offset), rotationVector);
    }

    public void ResetTimer()
    {
        seasonManager.ResetSeason();
        currentRotation = Quaternion.Euler(0, 0, -45);
        currentSeason = seasonManager.getCurrentSeason();
        rotation = 0.0f;
    }

}
