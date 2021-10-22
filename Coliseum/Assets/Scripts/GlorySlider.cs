using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlorySlider : MonoBehaviour
{

    public Slider slider;
    
    public void SetMaxGlory(int glory)
    {
        slider.maxValue = glory;
        slider.value = glory;
    }
    
    public void SetGlory(int glory)
    {
        slider.value = glory * -1 ;
    }
}
