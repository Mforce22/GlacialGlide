using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsViewController : MonoBehaviour
{
    [SerializeField]
    private GameObject _button;
    private void Start() {
        string currentSceneName = SceneManager.GetActiveScene().name;
        if(currentSceneName == "MainMenu") {
            _button.SetActive(false);
        }
    }
    public void ChangeScene(string scene) {
        GameMaster.Instance.setHearts(3);
        GameMaster.Instance.setPause(false);
        GameMaster.Instance.setPoints(0);
        GameMaster.Instance.setVelocity(2);

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
