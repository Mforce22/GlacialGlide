using TMPro;
using UnityEngine;

/// <summary>
/// Updates the displayed multiplier timer value on a TextMeshProUGUI component.
/// </summary>
public class X2Multiplier : MonoBehaviour {
    [SerializeField]
    private TextMeshProUGUI _SecondsValue;

    /// <summary>
    /// Updates the displayed multiplier timer value.
    /// </summary>
    void Update() {
        // Get the current multiplier timer value from the GameMaster instance.
        int seconds = GameMaster.Instance.getMultiplierTimer();

        // Update the TextMeshProUGUI component with the timer value.
        _SecondsValue.SetText(seconds.ToString());
    }
}