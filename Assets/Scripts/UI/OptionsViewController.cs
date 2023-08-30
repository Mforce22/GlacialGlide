using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// This class manages the options view in the game, allowing users to adjust settings and control audio.
/// </summary>
public class OptionsViewController : MonoBehaviour
{
    [SerializeField]
    private GameObject _button; // The GameObject representing the button within the options view.

    /// <summary>
    /// Checks the active scene and hides the button in the main menu.
    /// </summary>
    private void Start()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName == "MainMenu")
        {
            _button.SetActive(false);
        }
    }

    /// <summary>
    /// This method is called to change the game scene and reset game parameters when an option is selected.
    /// </summary>
    /// <param name="scene">The name of the scene to transition to.</param>
    public void ChangeScene(string scene)
    {
        // Reset game parameters.
        GameMaster.Instance.setHearts(3);
        GameMaster.Instance.setPoints(0);
        GameMaster.Instance.setVelocity(2.0f);
        //GameMaster.Instance.GameOver();

        // Load the specified scene and stop background music.
        //TravelSystem.Instance.SceneLoad(scene);
        AudioSystem.Instance.StopMusic();
        FlowSystem.Instance.TriggerFSMEvent(scene);
    }

    /// <summary>
    /// This method is called to close the options view and resume the game.
    /// </summary>
    public void CloseOption()
    {
        // Resume the game and destroy the options view GameObject.
        GameMaster.Instance.setPause(false);
        Destroy(gameObject);
    }

    /// <summary>
    /// This method is called to adjust the audio volume based on the value of a slider.
    /// </summary>
    /// <param name="slider">The slider UI element that controls the audio volume.</param>
    public void SetAudioVolume(Slider slider)
    {
        // Set the audio volume based on the slider value.
        AudioSystem.Instance.SetVolume(slider.value - (slider.value / 5));
        SoundSystem.Instance.SetVolume(slider.value + (slider.value / 10));
    }
}