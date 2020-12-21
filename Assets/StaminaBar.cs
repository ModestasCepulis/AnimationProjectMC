using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{

    public Slider slider;

    // Start is called before the first frame update
    public void setMaxStamina(int stamina)
    {
        slider.maxValue = stamina;
        slider.value = stamina;
    }
    public void setStamina(float stamina)
    {
        slider.value = stamina;
    }

}

