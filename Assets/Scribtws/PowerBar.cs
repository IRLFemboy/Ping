using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerBar : MonoBehaviour
{
    public Slider powerBar;

    public void setPowerMax(int power)
    {
        powerBar.maxValue = power;
        powerBar.value = 0;
    }

    public void setPower(int power)
    {
        powerBar.value = power;
    }
}
