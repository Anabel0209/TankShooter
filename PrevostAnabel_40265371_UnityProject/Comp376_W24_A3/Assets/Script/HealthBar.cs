using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    //variables to set
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public bool isPlayer = false;

    //flashing parameters
    public float flashThreshold = 0.25f;
    public float flashDuration = 0.5f;
    public Color flashColor = Color.white;

    private Coroutine flashCoroutine;

    //method called to set the value of the health bar
    public void SetHealth(int health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);

        //Apply flashing mechanics on health bar if it is the player health bar
        if(isPlayer)
        {
            //start flashing when health is below the threshold
            if (slider.normalizedValue <= flashThreshold)
            {
                //start flashing if it is not already flashing
                if (flashCoroutine == null)
                {
                    flashCoroutine = StartCoroutine(FlashHealthBar());
                }
            }
            //stop flashing when health is above threshold
            else
            {
                if (flashCoroutine != null)
                {
                    StopCoroutine(FlashHealthBar());
                    flashCoroutine = null;
                    fill.color = gradient.Evaluate(slider.normalizedValue);
                }
            }
        }
    }

    //method called to set the max value of the bar
    public void SetHealth(float health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    //method called to initialize the health bar to maximum
    public void SetMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
        fill.color = gradient.Evaluate(1f);
    }

    //method called to initialize the health bar to maximum
    public void SetMaxHealth(float maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
        fill.color = gradient.Evaluate(1f);
    }

    //Coroutine that handles the flashing effect
    private IEnumerator FlashHealthBar()
    {
        bool isFlashing = true;

        while (isFlashing)
        {
            //change the color to the flashing color
            fill.color = flashColor;
            yield return new WaitForSeconds(flashDuration);

            //change the color to the original color
            fill.color = gradient.Evaluate(slider.normalizedValue);
            yield return new WaitForSeconds(flashDuration);
        }
    }
}
