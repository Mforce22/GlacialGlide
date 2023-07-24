using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsViewController : MonoBehaviour
{
    public void ChangeScene(string scene) {
        TravelSystem.Instance.SceneLoad(scene);
        AudioSystem.Instance.StopMusic();
    }
    public void CloseOption()
    {
        GameMaster.Instance.setPause(false);
        Destroy(gameObject);
    }

    public void SetAudioVolume(Slider slider) {
        //Debug.Log("Slider: "+slider);
        //Debug.Log("Volume: " + slider.value);
        AudioSystem.Instance.SetVolume(slider.value);
    }
}
