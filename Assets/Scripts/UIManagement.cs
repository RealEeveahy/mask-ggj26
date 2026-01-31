using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManagement : MonoBehaviour
{
    public GameObject sanityBar;
    public GameObject dayProgressBar;
    private Slider sanitySlider;
    private Slider dayProgressSlider;

    private bool useGameUI = true;
    void Awake()
    {
        if(useGameUI)
        {
            sanitySlider = sanityBar.GetComponent<Slider>();
            dayProgressSlider = dayProgressBar.GetComponent<Slider>();
        }
    }
    void Update()
    {
        
    }

    public void UpdateSanity(float newValue)
    {
        sanitySlider.value = newValue;
    }

    /// <summary>
    /// Configure the progress increment size for the day-progress bar based on how many
    /// tasks must be completed in that day.
    /// </summary>
    public void DayProgressConfig(int taskCount, bool reset = true)
    {
        dayProgressSlider.maxValue = taskCount;

        if(reset)
        { 
            dayProgressSlider.value = 0;
        }
    }
}
