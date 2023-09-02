using TMPro;
using UnityEngine;

/// <summary>
/// This class manages the UI elements of the game, including points display, hearts display, and options view.
/// It should be attached to a GameObject as a MonoBehaviour script.
/// </summary>
public class UI_Manager : MonoBehaviour {
    [SerializeField]
    private int _Points; // The current points earned by the player.
    [SerializeField]
    private TextMeshProUGUI _PointsValue; // The UI element that displays the points value.

    [SerializeField]
    private int _Hearts; // The current number of hearts remaining for the player.

    [SerializeField]
    private GameObject _Heart1; // The GameObject representing the first heart icon.
    [SerializeField]
    private GameObject _Heart2; // The GameObject representing the second heart icon.
    [SerializeField]
    private GameObject _Heart3; // The GameObject representing the third heart icon.

    [SerializeField]
    private GameObject _NoHeart1; // The GameObject representing the first empty heart icon.
    [SerializeField]
    private GameObject _NoHeart2; // The GameObject representing the second empty heart icon.
    [SerializeField]
    private GameObject _NoHeart3; // The GameObject representing the third empty heart icon.

    [SerializeField]
    private OptionsViewController _OptionsViewPrefab; // Prefab for the options view.

    private OptionsViewController _optionsViewController; // Reference to the instantiated options view.

    /// <summary>
    /// Updates the game state, including points and hearts displays, based on the current state from GameMaster.
    /// </summary>
    void Update() {
        // Update points and hearts from the GameMaster singleton.
        _Points = GameMaster.Instance.getPoints();
        _Hearts = GameMaster.Instance.getHearts();

        // Update points value display.
        _PointsValue.SetText(_Points.ToString());

        // Update hearts and empty hearts display based on the current number of hearts.
        switch (_Hearts) {
            case 0:
                _NoHeart1.SetActive(true);
                _NoHeart2.SetActive(true);
                _NoHeart3.SetActive(true);

                _Heart1.SetActive(false);
                _Heart2.SetActive(false);
                _Heart3.SetActive(false);
                break;
            case 1:
                _NoHeart1.SetActive(false);
                _NoHeart2.SetActive(true);
                _NoHeart3.SetActive(true);

                _Heart1.SetActive(true);
                _Heart2.SetActive(false);
                _Heart3.SetActive(false);
                break;
            case 2:
                _NoHeart1.SetActive(false);
                _NoHeart2.SetActive(false);
                _NoHeart3.SetActive(true);

                _Heart1.SetActive(true);
                _Heart2.SetActive(true);
                _Heart3.SetActive(false);
                break;
            case 3:
                _NoHeart1.SetActive(false);
                _NoHeart2.SetActive(false);
                _NoHeart3.SetActive(false);

                _Heart1.SetActive(true);
                _Heart2.SetActive(true);
                _Heart3.SetActive(true);
                break;
        }

        // Resume the game if the options view is not open.
        if (_optionsViewController == null) {
            GameMaster.Instance.setPause(false);
        }
    }

    /// <summary>
    /// This method opens the options view and pauses the game.
    /// </summary>
    [ContextMenu("Options")]
    public void OpenOptions() {
        if (_optionsViewController) return;
        _optionsViewController = Instantiate(_OptionsViewPrefab);
        GameMaster.Instance.setPause(true);
    }

    /// <summary>
    /// Initializes points and hearts from the GameMaster singleton.
    /// </summary>
    private void Start() {
        _Points = GameMaster.Instance.getPoints();
        _Hearts = GameMaster.Instance.getHearts();
        GameMaster.Instance.SetStartingSpeed();
    }
}