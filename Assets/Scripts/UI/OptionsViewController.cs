using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsViewController : MonoBehaviour
{
    public void CloseOption()
    {
        Destroy(gameObject);
    }

    public void SetAudioVolume(Slider slider) {
        //Debug.Log("Slider: "+slider);
        //Debug.Log("Volume: " + slider.value);
        AudioSystem.Instance.SetVolume(slider.value);
    }
}
